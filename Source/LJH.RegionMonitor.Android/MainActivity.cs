using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using LJH.RegionMonitor.Model;
using LJH.RegionMonitor.WebAPIClient;
using LJH.GeneralLibrary.Core;
using Newtonsoft.Json;

namespace LJH.RegionMonitor.Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly string _LogName = "MainActivity";
        private readonly string _Url = @"http://47.92.81.39:13002/rm/api";
        private MonitorRegion _CurrentRegion = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var ret = new RegionClient(_Url).GetByID(1, true);
            if (ret.Result == ResultCode.Successful && ret.QueryObject != null) _CurrentRegion = new MonitorRegion(ret.QueryObject);
            if (this.ActionBar != null)
            {
                this.ActionBar.Title = _CurrentRegion != null ? _CurrentRegion.Name : GetString(Resource.String.app_title);
            }
        }
    }
}

