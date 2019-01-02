using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.OS;
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
        //private readonly string _Url = @"http://192.168.2.116:13002/rm/api";
        private readonly string _Url = @"http://47.92.81.39:13002/rm/api";
        private MonitorRegion _CurrentRegion = null;
        private Thread _ReadCardEventThread = null;
        private DateTime _LastDateTime = DateTime.MinValue;
        private ListView _RegionView = null;
        private CardEvent _LastCardEvent = null;
        private bool _FirstTime = true;
        private AlertDialog _CurDialog = null;
        private DateTime _CurDialogCreateTime = DateTime.MinValue;
        private System.Timers.Timer _Timer = null;
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
                _LastDateTime = DateTime.Now.AddDays(-3);  //从某个时间点的刷卡记录开始算起,一般来说人员不会在区域里面呆超过三天
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
                    if (_CurrentRegion == null) this.RunOnUiThread(() => InitCurrentRegion());
                    if (_CurrentRegion == null) continue;
                    var con = new CardEventSearchCondition() { EventTime = new DateTimeRange() { Begin = _LastDateTime, End = DateTime.Now } };
                    List<CardEvent> events = new CardEventClient(_Url).GetItems(con, true).QueryObjects;
                    if (events != null && events.Count > 0)
                    {
                        events = (from it in events orderby it.EventTime ascending select it).ToList();
                        foreach (var item in events)
                        {
                            _CurrentRegion.HandleCardEvent(item);
                        }
                        _LastDateTime = events.Max(it => it.EventTime).AddSeconds(-10);
                        if (!_FirstTime) _LastCardEvent = events.LastOrDefault(it => _CurrentRegion.IsMyDoor(it.DoorID));
                    }
                    if (_FirstTime) _FirstTime = false;
                    if (_CurrentRegion.InregionUsersChanged)
                    {
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

        private void FreshRegion()
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

        private byte[] DownloadPhoto(string url)
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    return client.DownloadData(url);
                }
            }
            catch (System.Exception ex)
            {
                return null;
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
        }

        private void _Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_CurDialog != null && _CurDialogCreateTime.AddSeconds(3) <= DateTime.Now)
            {
                _CurDialog.Dismiss();
                _CurDialog = null;
                _Timer.Stop();
            }
        }

        protected override void OnResume()
        {
            if (_ReadCardEventThread == null)
            {
                _ReadCardEventThread = new Thread(new ThreadStart(FreshRegion_Thread));
                _ReadCardEventThread.IsBackground = true;
                _ReadCardEventThread.Start();
            }
            base.OnResume();
        }

        protected override void OnPause()
        {
            if (_ReadCardEventThread != null)
            {
                _ReadCardEventThread.Abort();
                _ReadCardEventThread = null;
            }
            if (_Timer != null && _Timer.Enabled) _Timer.Stop();
            base.OnPause();
        }

        protected override void OnDestroy()
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
            base.OnDestroy();
        }
        #endregion

        #region 公共方法
        public void ShowInRegionPersonDetail(InRegionPerson regionPerson)
        {
            if (_CurDialog != null) _CurDialog.Dismiss();
            var builder = new AlertDialog.Builder(this);
            var view = LayoutInflater.Inflate(Resource.Layout.FrmUserDetail, null);
            var txtName = view.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = regionPerson.UserName;
            var txtCardID = view.FindViewById<TextView>(Resource.Id.txtCardID);
            txtCardID.Text = regionPerson.CardID;
            var txtDept = view.FindViewById<TextView>(Resource.Id.txtDept);
            txtDept.Text = regionPerson.Department;
            var txtDoor = view.FindViewById<TextView>(Resource.Id.txtDoor);
            txtDoor.Text = regionPerson.DoorName;
            var txtTime = view.FindViewById<TextView>(Resource.Id.txtTime);
            txtTime.Text = regionPerson.EnterDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            var person = new WebAPIClient.PersonDetailClient(_Url).GetByID(regionPerson.UserID, true).QueryObject;
            if (person != null)
            {
                if (!string.IsNullOrEmpty(person.Phone))
                {
                    var txtPhone = view.FindViewById<TextView>(Resource.Id.txtPhone);
                    txtPhone.Text = person.Phone;
                }
                if (!string.IsNullOrEmpty(person.PhotoUrl))
                {
                    var bytes = DownloadPhoto(person.PhotoUrl);
                    if (bytes != null && bytes.Length > 0)
                    {
                        var picPhoto = view.FindViewById<ImageView>(Resource.Id.picPhoto);
                        var bmp = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                        picPhoto.SetImageBitmap(bmp);
                    }
                }
            }
            builder.SetView(view);
            builder.SetPositiveButton("确定", (EventHandler<DialogClickEventArgs>)null);
            _CurDialog = builder.Show();
            _CurDialogCreateTime = DateTime.Now;
            _Timer.Start();
        }

        public void ShowInRegionPersonDetail(CardEvent ce, bool isIn)
        {
            if (_CurDialog != null) _CurDialog.Dismiss();
            var builder = new AlertDialog.Builder(this);
            var view = LayoutInflater.Inflate(Resource.Layout.FrmUserDetail, null);
            var txtName = view.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = ce.UserName;
            var txtCardID = view.FindViewById<TextView>(Resource.Id.txtCardID);
            txtCardID.Text = ce.CardID;
            var txtDept = view.FindViewById<TextView>(Resource.Id.txtDept);
            txtDept.Text = ce.Department;
            var txtDoor = view.FindViewById<TextView>(Resource.Id.txtDoor);
            txtDoor.Text = ce.DoorName;
            var txtTime = view.FindViewById<TextView>(Resource.Id.txtTime);
            txtTime.Text = ce.EventTime.ToString("yyyy-MM-dd HH:mm:ss");
            var picAlarm = view.FindViewById<ImageView>(Resource.Id.picAlarm);
            if (!isIn) picAlarm.Visibility = Android.Views.ViewStates.Gone;
            var person = new WebAPIClient.PersonDetailClient(_Url).GetByID(ce.UserID, true).QueryObject;
            if (person != null)
            {
                if (!string.IsNullOrEmpty(person.Phone))
                {
                    var txtPhone = view.FindViewById<TextView>(Resource.Id.txtPhone);
                    txtPhone.Text = person.Phone;
                }
                if (!string.IsNullOrEmpty(person.PhotoUrl))
                {
                    var bytes = DownloadPhoto(person.PhotoUrl);
                    if (bytes != null && bytes.Length > 0)
                    {
                        var picPhoto = view.FindViewById<ImageView>(Resource.Id.picPhoto);
                        var bmp = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                        picPhoto.SetImageBitmap(bmp);
                    }
                }
            }
            builder.SetView(view);
            builder.SetPositiveButton("确定", (EventHandler<DialogClickEventArgs>)null);
            _CurDialog = builder.Show();
            _CurDialogCreateTime = DateTime.Now;
            _Timer.Start();
        }
        #endregion
    }
}

