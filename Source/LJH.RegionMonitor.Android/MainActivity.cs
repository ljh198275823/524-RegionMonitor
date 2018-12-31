﻿using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.App;
using Android.Util;
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
        //private readonly string _LogName = "MainActivity";
        //public readonly string _Url = @"http://192.168.2.116:13002/rm/api";  
        public readonly string _Url = @"http://47.92.81.39:13002/rm/api";
        private MonitorRegion _CurrentRegion = null;
        private Thread _ReadCardEventThread = null;
        private DateTime _LastDateTime = DateTime.MinValue;  
        private ListView _RegionView = null;
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
                    }
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
                _RegionView.Adapter = new RegionMonitorAdapter(this, _CurrentRegion, 120, 48);
            }
            else
            {
                (_RegionView.Adapter as RegionMonitorAdapter).NotifyDataSetChanged();
            }
        }
        #endregion

        #region 重写基类方法
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            _RegionView = FindViewById<ListView>(Resource.Id.lvRegion);
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
            base.OnPause();
        }

        protected override void OnDestroy()
        {
            if (_ReadCardEventThread != null)
            {
                _ReadCardEventThread.Abort();
                _ReadCardEventThread = null;
            }
            base.OnDestroy();
        }
        #endregion
    }
}

