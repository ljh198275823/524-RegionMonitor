﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJH.RegionMonitor.Model;
using LJH.GeneralLibrary.WebAPIClient;

namespace LJH.RegionMonitor.WebAPIClient
{
    public class RegionClient : APIClientBase<int, Region>
    {
        #region 构造函数
        public RegionClient(string repUri)
            : base(repUri)
        {

        }
        #endregion

        #region 实例方法
        #endregion
    }
}
