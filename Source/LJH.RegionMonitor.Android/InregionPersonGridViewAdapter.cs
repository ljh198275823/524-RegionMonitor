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
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.Android
{
    public class InregionPersonGridViewAdapter : BaseAdapter
    {
        #region 构造函数
        public InregionPersonGridViewAdapter(Context context, List<InRegionPerson> items) : base()
        {
            _Context = context;
            _Users = items;
        }
        #endregion

        #region 私有变量
        private Context _Context = null;
        private List<InRegionPerson> _Users = null;
        #endregion

        #region 私有方法
        #endregion

        #region 重写基类方法
        public override int Count
        {
            get
            {
                return _Users.Count;
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
                convertView = LayoutInflater.From(_Context).Inflate(Resource.Layout.InregionPersonView, null);
            }
            var lblUser = convertView.FindViewById<TextView>(Resource.Id.lblUser);
            if (_Users.Count > position)
            {
                var item = _Users[position];
                lblUser.Text = item.UserName;
            }
            return convertView;
        }
        #endregion
    }
}