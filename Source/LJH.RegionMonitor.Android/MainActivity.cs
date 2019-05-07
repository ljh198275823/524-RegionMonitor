using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;
using Android.Content;
using Android.Graphics;
using System.Threading;
using LJH.RegionMonitor.Model;
using LJH.RegionMonitor.WebAPIClient;
using LJH.GeneralLibrary.Core;
using LJH.GeneralLibrary;
using Newtonsoft.Json;

namespace LJH.RegionMonitor.AndroidAPP
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/view")]
    public class MainActivity : Activity
    {
        #region 私有变量
        private readonly string _MyDoors = "500KV";
        //private readonly string _MyDoors = "地下厂房";
        //private readonly int _GetReionTicks = 10;
        private readonly string _Url = @"http://192.168.2.116:13002/rm/api";
        //private readonly string _Url = @"http://47.92.81.39:13002/rm/api";
        private MonitorRegion _CurrentRegion = null;
        private Thread _ReadCardEventThread = null;
        //private DateTime _LastDateTime = DateTime.MinValue;
        private ListView _RegionView = null;
        private CardEvent _LastCardEvent = null;
        private bool _FirstTime = true;
        private AlertDialog _CurDialog = null;
        private DateTime _CurDialogCreateTime = DateTime.MinValue;
        private System.Timers.Timer _Timer = null;
        private MediaPlayer _MediaPlayer = null;
        private Dictionary<string, PersonDetail> _DicPerson = new Dictionary<string, PersonDetail>();
        private List<CardEvent> _LastEvents = new List<CardEvent>();
        #endregion

        #region 私有方法
        private void InitCurrentRegion()
        {
            var ret = new RegionClient(_Url).GetByID(1, true);
            if (ret.Result == ResultCode.Successful && ret.QueryObject != null) _CurrentRegion = new MonitorRegion(ret.QueryObject);
            if (this.ActionBar != null)
            {
                this.ActionBar.Title = _CurrentRegion != null ? _CurrentRegion.Name : "没有设置区域";
            }
            if (_CurrentRegion != null)
            {
                _FirstTime = true;
            }
        }

        private void FreshRegion_Thread()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    if (_CurrentRegion == null) continue;
                    var con = new CardEventSearchCondition()
                    {
                        EventTime = new DateTimeRange()
                        {
                            Begin = _FirstTime ? DateTime.Now.AddDays(-2) : DateTime.Now.AddMinutes(-30), //第一次获取两天之前的记录
                            End = DateTime.Now.AddMinutes(30)   //这里获取事件的时间为当前时间再往前半个小时
                        }
                    };
                    _LastCardEvent = null;
                    List<CardEvent> events = new CardEventClient(_Url).GetItems(con, true).QueryObjects;
                    if (events != null && events.Count > 0)
                    {
                        events = (from it in events orderby it.EventTime ascending select it).ToList();
                        foreach (var item in events)
                        {
                            if (!_CurrentRegion.IsMyDoor(item.DoorID)) continue;
                            if (!_LastEvents.Exists(it => it.EventTime == item.EventTime && it.UserID == item.UserID))
                            {
                                _LastEvents.Add(item);
                                if (!_FirstTime && IsMyDoor(item.DoorName)) _LastCardEvent = item;
                                _CurrentRegion.HandleCardEvent(item);
                            }
                        }
                        if (_LastCardEvent != null) GetPersonDetail(_LastCardEvent.UserID);
                    }
                    if (_FirstTime) _FirstTime = false;
                    _LastEvents.RemoveAll(it => it.EventTime < con.EventTime.Begin);
                    if (_CurrentRegion.PersonChanged)
                    {
                        _CurrentRegion.PersonChanged = false;
                        this.RunOnUiThread(() => FreshRegion());
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception)
                {

                }
            }
        }

        private bool IsMyDoor(string doorName)
        {
            if (_MyDoors == "地下厂房")
            {
                return doorName.Contains(_MyDoors);
            }
            else if(_MyDoors =="500KV")
            {
                return doorName.Contains(_MyDoors) || doorName.Contains("排风竖井");
            }
            return true;
        }

        private void GetPersonDetail(string userID)
        {
            if (_DicPerson.ContainsKey(userID)) return;
            var person = new WebAPIClient.PersonDetailClient(_Url).GetByID(userID, true).QueryObject;
            if (person != null)
            {
                if (!string.IsNullOrEmpty(person.PhotoUrl))
                {
                    var bytes = DownloadPhoto(person.PhotoUrl);
                    person.Photo = bytes;
                }
                _DicPerson.Add(userID, person);
            }
        }

        private void FreshRegion()
        {
            try
            {
                if (this.ActionBar != null)
                {
                    this.ActionBar.Title = _CurrentRegion != null ? $"{ _CurrentRegion.Name}    在场总人数 {_CurrentRegion.InregionUsers.Count }" : "没有设置区域";
                }
                if (_RegionView.Adapter == null)
                {
                    _RegionView.Adapter = new RegionMonitorAdapter(this, _CurrentRegion, 200, 48);
                }
                else
                {
                    (_RegionView.Adapter as RegionMonitorAdapter).NotifyDataSetChanged();
                }
                if (_LastCardEvent != null)
                {
                    ShowInRegionPersonDetail(_LastCardEvent, _CurrentRegion.EnterDoors.Contains(_LastCardEvent.DoorID));
                }
            }
            catch (Exception)
            {
            }
        }

        private void _Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (_CurDialog != null && _CurDialogCreateTime.AddSeconds(15) <= DateTime.Now)
                {
                    _CurDialog.Dismiss();
                    _CurDialog.Dispose();
                    _CurDialog = null;
                    _Timer.Stop();
                }
                else if (_CurDialog == null)
                {
                    _Timer.Stop();
                }
            }
            catch (Exception)
            {
            }
        }

        private byte[] DownloadPhoto(string url)
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    return client.DownloadData(url);
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private void StartAudio()
        {
            try
            {
                if (_MediaPlayer != null && !_MediaPlayer.IsPlaying) _MediaPlayer.Start();
            }
            catch (Exception)
            {

            }
        }

        private void ReleaseAudio()
        {
            try
            {
                if (_MediaPlayer != null)
                {
                    _MediaPlayer.Stop();
                    _MediaPlayer.Release();
                    _MediaPlayer = null;
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 重写基类方法
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            _RegionView = FindViewById<ListView>(Resource.Id.lvRegion);
            _Timer = new System.Timers.Timer(1000);
            _Timer.Elapsed += _Timer_Elapsed;
            InitCurrentRegion();
        }

        protected override void OnResume()
        {
            if (_ReadCardEventThread == null)
            {
                _ReadCardEventThread = new Thread(new ThreadStart(FreshRegion_Thread));
                _ReadCardEventThread.IsBackground = true;
                _ReadCardEventThread.Start();
            }
            if (_MediaPlayer == null) _MediaPlayer = MediaPlayer.Create(this, Resource.Raw.Alerting);
            base.OnResume();
        }

        protected override void OnPause()
        {
            try
            {
                if (_ReadCardEventThread != null)
                {
                    _ReadCardEventThread.Abort();
                    _ReadCardEventThread = null;
                }
                if (_Timer != null && _Timer.Enabled) _Timer.Stop();
                ReleaseAudio();
            }
            catch (Exception)
            {

            }
            base.OnPause();
        }

        protected override void OnDestroy()
        {
            try
            {
                if (_ReadCardEventThread != null)
                {
                    _ReadCardEventThread.Abort();
                    _ReadCardEventThread = null;
                }
                if (_Timer != null)
                {
                    _Timer.Stop();
                    _Timer.Dispose();
                }
            }
            catch (Exception)
            {

            }
            base.OnDestroy();
        }
        #endregion

        #region 公共方法
        public void ShowInRegionPersonDetail(InRegionPerson item)
        {
            if (_CurDialog == null || !_CurDialog.IsShowing) _CurDialog = CreateDialog();
            _CurDialogCreateTime = DateTime.Now;
            var txtName = _CurDialog.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = item.UserName;
            var txtCardID = _CurDialog.FindViewById<TextView>(Resource.Id.txtCardID);
            txtCardID.Text = item.CardID;
            var txtDept = _CurDialog.FindViewById<TextView>(Resource.Id.txtDept);
            txtDept.Text = item.Department;
            var txtDoor = _CurDialog.FindViewById<TextView>(Resource.Id.txtDoor);
            txtDoor.Text = item.DoorName;
            var txtTime = _CurDialog.FindViewById<TextView>(Resource.Id.txtTime);
            txtTime.Text = item.EnterDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            var picAlarm = _CurDialog.FindViewById<ImageView>(Resource.Id.picAlarm);
            picAlarm.Visibility = Android.Views.ViewStates.Gone;
            if (!_DicPerson.ContainsKey(item.UserID)) GetPersonDetail(item.UserID); //如果不存在此用户信息，则先获取一次
            if (_DicPerson.ContainsKey(item.UserID))
            {
                var person = _DicPerson[item.UserID];
                var txtPhone = _CurDialog.FindViewById<TextView>(Resource.Id.txtPhone);
                txtPhone.Text = person.Phone;
                var picPhoto = _CurDialog.FindViewById<ImageView>(Resource.Id.picPhoto);
                picPhoto.SetImageBitmap(null);
                if (person.Photo != null && person.Photo.Length > 0)
                {
                    var bmp = BitmapFactory.DecodeByteArray(person.Photo, 0, person.Photo.Length);
                    picPhoto.SetImageBitmap(bmp);
                }
            }
        }

        public void ShowInRegionPersonDetail(CardEvent ce, bool isIn)
        {
            if (_CurDialog == null || !_CurDialog.IsShowing) _CurDialog = CreateDialog();
            _CurDialogCreateTime = DateTime.Now;
            var txtName = _CurDialog.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = ce.UserName;
            var txtCardID = _CurDialog.FindViewById<TextView>(Resource.Id.txtCardID);
            txtCardID.Text = ce.CardID;
            var txtDept = _CurDialog.FindViewById<TextView>(Resource.Id.txtDept);
            txtDept.Text = ce.Department;
            var txtDoor = _CurDialog.FindViewById<TextView>(Resource.Id.txtDoor);
            txtDoor.Text = ce.DoorName;
            var txtTime = _CurDialog.FindViewById<TextView>(Resource.Id.txtTime);
            txtTime.Text = ce.EventTime.ToString("yyyy-MM-dd HH:mm:ss");
            var picAlarm = _CurDialog.FindViewById<ImageView>(Resource.Id.picAlarm);
            if (!isIn) picAlarm.Visibility = Android.Views.ViewStates.Gone;
            else picAlarm.Visibility = Android.Views.ViewStates.Visible;
            if (_DicPerson.ContainsKey(ce.UserID))
            {
                var person = _DicPerson[ce.UserID];
                var txtPhone = _CurDialog.FindViewById<TextView>(Resource.Id.txtPhone);
                txtPhone.Text = person.Phone;
                var picPhoto = _CurDialog.FindViewById<ImageView>(Resource.Id.picPhoto);
                picPhoto.SetImageBitmap(null);
                if (person.Photo != null && person.Photo.Length > 0)
                {
                    var bmp = BitmapFactory.DecodeByteArray(person.Photo, 0, person.Photo.Length);
                    picPhoto.SetImageBitmap(bmp);
                }
            }
            if (isIn) StartAudio();
        }

        private AlertDialog CreateDialog()
        {
            if (_CurDialog != null)
            {
                _CurDialog.Dismiss();
                _CurDialog.Dispose();
                _CurDialog = null;
            }
            var builder = new AlertDialog.Builder(this);
            var _AlertView = LayoutInflater.Inflate(Resource.Layout.FrmUserDetail, null);
            builder.SetView(_AlertView);
            _Timer.Start();
            return builder.Show();
        }

        private void AlertDialog_Click(object sender, DialogClickEventArgs e)
        {
            _CurDialog = null;
            _Timer.Stop();
        }
        #endregion
    }
}

