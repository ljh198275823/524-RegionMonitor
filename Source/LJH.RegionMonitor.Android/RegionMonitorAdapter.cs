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
        }
        #endregion

        #region 私有变量
        private Context _Context = null;
        private MonitorRegion _CurrentRegion = null;
        private Dictionary<string, List<InRegionPerson>> _Depts = new Dictionary<string, List<InRegionPerson>>();
        #endregion

        #region 私有方法
        private void GetInregionPerson()
        {
            var items = _CurrentRegion.InregionUsers;
            if (items != null && items.Count > 0)
            {
                _Depts.Clear();
                foreach (var item in items.OrderBy(it => it.Department))
                {
                    if (!_Depts.ContainsKey(item.Department)) _Depts.Add(item.Department, new List<InRegionPerson>());
                    _Depts[item.Department].Add(item);
                }
            }
        }

        private void SetGridHeight(GridView gv)
        {
            int totalHeight = 0;
            int columns = gv.NumColumns;
            for (int i = 0; i < gv.Adapter.Count; i += columns)
            {
                var view = gv.Adapter.GetView(i, null, gv);

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
            var lblUser = convertView.FindViewById<GridView>(Resource.Id.userGridview);
            if (_Depts.Count > position)
            {
                var item = _Depts.ElementAt(position);
                lblDept.Text = $"{item.Key}({item.Value.Count }人)";
                
                //lblUser.LayoutParameters.Height = 300;
                lblUser.Adapter = new InregionPersonGridViewAdapter(_Context, item.Value);
                var columns = lblUser.NumColumns;
            }
            return convertView;
        }
        #endregion
    }
}