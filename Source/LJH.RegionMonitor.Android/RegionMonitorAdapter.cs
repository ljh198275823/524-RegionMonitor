using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Java.Lang;
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.Android
{
    public class RegionMonitorAdapter : BaseAdapter
    {
        #region 构造函数
        public RegionMonitorAdapter(Context context, MonitorRegion mr) : base()
        {
            _Context = context;
            _CurrentRegion = mr;
            GetInregionPerson();
            _GridviewItemHeight = DP2PX(context, 50);
        }
        #endregion

        #region 私有变量
        private Context _Context = null;
        private MonitorRegion _CurrentRegion = null;
        private Dictionary<string, List<InRegionPerson>> _Depts = new Dictionary<string, List<InRegionPerson>>();
        private int _GridviewItemHeight = 0;  //刷卡人员信息显示高度
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
                lblCount.Text = item.Value.Count.ToString();
                gvUser.Adapter = new InregionPersonGridViewAdapter(_Context, item.Value, _GridviewItemHeight);
                var columns = gvUser.NumColumns;
                var rows = (int)(System.Math.Ceiling((decimal)item.Value.Count / columns));
                gvUser.LayoutParameters.Height = gvUser.PaddingTop + (_GridviewItemHeight + gvUser.VerticalSpacing) * rows;
            }
            return convertView;
        }

        private void LblUser_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var v = e.View;
            var tag = e.View.Tag as JavaTag<InRegionPerson>;
            var builder = new AlertDialog.Builder(_Context);
            builder.SetTitle($"{tag.Value.UserName} 的明细");
            builder.SetMessage($"姓名={tag.Value.UserName} 部门={tag.Value.Department } 入场地点={tag.Value.DoorName } 入场时间={tag.Value.EnterDateTime.ToString("yyyy-MM-dd HH:mm:ss")}");
            builder.SetPositiveButton("确定", (EventHandler<DialogClickEventArgs>)null);
            builder.Show();
        }
        #endregion
    }
}