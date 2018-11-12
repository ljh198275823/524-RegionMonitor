using System;
using System.Collections.Generic;
using System.Text;
using LJH.RegionMonitor.Model;
using LJH.GeneralLibrary.WebAPIClient;

namespace LJH.RegionMonitor.WebAPIClient
{
    public class DoorClient : APIClientBase<string, Door>
    {
        #region 构造函数
        public DoorClient(string repUri)
            : base(repUri)
        {

        }
        #endregion

        #region 实例方法
        #endregion
    }
}
