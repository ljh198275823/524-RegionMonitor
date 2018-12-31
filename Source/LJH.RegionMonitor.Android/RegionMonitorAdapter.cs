﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Graphics;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Java.Lang;
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.AndroidAPP
{
    public class RegionMonitorAdapter : BaseAdapter
    {
        #region 构造函数
        public RegionMonitorAdapter(Context context, MonitorRegion mr, int deptWidth, int userHeight) : base()
        {
            _Context = context;
            _CurrentRegion = mr;
            GetInregionPerson();
            _GridviewItemHeight = userHeight;
            _DeptWidth = deptWidth;
        }
        #endregion

        #region 私有变量
        private Context _Context = null;
        private MonitorRegion _CurrentRegion = null;
        private Dictionary<string, List<InRegionPerson>> _Depts = new Dictionary<string, List<InRegionPerson>>();
        private int _GridviewItemHeight = 0;  //刷卡人员信息显示高度
        private int _DeptWidth = 0; //部门列的宽度
        #endregion

        #region 私有方法
        private void GetInregionPerson()
        {
            var items = _CurrentRegion.InregionUsers;
            _Depts.Clear();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items.OrderBy(it => it.Department))
                {
                    if (!_Depts.ContainsKey(item.Department)) _Depts.Add(item.Department, new List<InRegionPerson>());
                    _Depts[item.Department].Add(item);
                }
            }
        }

        private int DP2PX(Context context, int dp)
        {
            var scale = context.Resources.DisplayMetrics.Density;
            var dpi = context.Resources.DisplayMetrics.DensityDpi;
            return (int)(System.Math.Ceiling(dp * scale));
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
            catch(System.Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 重写基类方法
        public override void NotifyDataSetChanged()
        {
            GetInregionPerson();
            base.NotifyDataSetChanged();
        }

        public override void NotifyDataSetInvalidated()
        {
            GetInregionPerson();
            base.NotifyDataSetInvalidated();
        }

        public override int Count
        {
            get
            {
                return _Depts.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(_Context).Inflate(Resource.Layout.InregionDeptView, null);
            }
            var lblDept = convertView.FindViewById<TextView>(Resource.Id.lblDept);
            var lblCount = convertView.FindViewById<TextView>(Resource.Id.lblCount);
            var gvUser = convertView.FindViewById<GridView>(Resource.Id.userGridview);
            gvUser.ItemClick -= LblUser_ItemClick;
            gvUser.ItemClick += LblUser_ItemClick;
            if (_Depts.Count > position)
            {
                var item = _Depts.ElementAt(position);
                lblDept.Text = $"{item.Key}";
                lblDept.LayoutParameters.Width = _DeptWidth;
                //根据部门view里的实际宽度和当前文字计算出部门列的高度，作为整个convertview的最小高度
                int intw = View.MeasureSpec.MakeMeasureSpec(_DeptWidth, MeasureSpecMode.Exactly);
                lblDept.Measure(intw, 0);
                var minH = lblDept.MeasuredHeight;

                lblCount.Text = item.Value.Count.ToString();
                gvUser.Adapter = new InregionPersonGridViewAdapter(_Context, item.Value, _GridviewItemHeight);
                var columns = gvUser.NumColumns;
                var rows = columns > 0 ? (int)(System.Math.Ceiling((decimal)item.Value.Count / columns)) : 1;
                var height = gvUser.PaddingTop + (_GridviewItemHeight + gvUser.VerticalSpacing) * rows;
                if (minH > height) height = minH;
                gvUser.LayoutParameters.Height = height;
            }
            return convertView;
        }

        private void LblUser_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var v = e.View;
            var tag = e.View.Tag as JavaTag<InRegionPerson>;
            var builder = new AlertDialog.Builder(_Context);
            var view = (_Context as Activity).LayoutInflater.Inflate(Resource.Layout.FrmUserDetail, null);
            var txtName = view.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = tag.Value.UserName;
            var txtCardID = view.FindViewById<TextView>(Resource.Id.txtCardID);
            txtCardID.Text = tag.Value.CardID;
            var txtDept = view.FindViewById<TextView>(Resource.Id.txtDept);
            txtDept.Text = tag.Value.Department;
            var txtDoor = view.FindViewById<TextView>(Resource.Id.txtDoor);
            txtDoor.Text = tag.Value.DoorName;
            var txtTime = view.FindViewById<TextView>(Resource.Id.txtTime);
            txtTime.Text = tag.Value.EnterDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            var person = new WebAPIClient.PersonDetailClient((_Context as MainActivity)._Url).GetByID(tag.Value.UserID, true).QueryObject;
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
            builder.Show();
        }
        #endregion
    }
}