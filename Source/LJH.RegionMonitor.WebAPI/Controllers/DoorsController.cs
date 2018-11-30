using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using LJH.GeneralLibrary.Core;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using LJH.RegionMonitor.Model;
using LJH.OneCard.HKVisionClient;

namespace LJH.RegionMonitor.WebAPI.Controllers
{
    public sealed class DoorsController : HttpGetOnlyBaseController<string, Door>
    {
        #region 构造函数
        public DoorsController(ILoggerFactory loggerFactory, HKVisionClient client) :
            base(loggerFactory)
        {
            _Client = client;
        }
        #endregion

        private HKVisionClient _Client;

        #region 重写基类方法
        protected override QueryResult<Door> GetingItemByID(string id)
        {
            Door item = null;
            var ret = _Client.GetDoors();
            if (ret.QueryObjects != null && ret.QueryObjects.Count > 0) item = ret.QueryObjects.SingleOrDefault(it => it.ID == id);
            return new QueryResult<Door>(ret.Result, ret.Message, item);
        }

        //protected override QueryResultList<Door> GetingItems(SearchCondition search)
        //{
        //    return _Client.GetDoors();
        //}

        protected override QueryResultList<Door> GetingItems(SearchCondition search)
        {
            return MockAcsProvider.GetDoors();
        }
        #endregion
    }
}
