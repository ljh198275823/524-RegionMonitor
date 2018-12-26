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
    public sealed class CardEventsController : HttpGetOnlyBaseController<string, CardEvent>
    {
        #region 构造函数
        public CardEventsController(ILoggerFactory loggerFactory, HKVisionClient client) :
            base(loggerFactory)
        {
            _Client = client;
        }
        #endregion

        private HKVisionClient _Client;

        #region 重写基类方法
        protected override QueryResult<CardEvent> GetingItemByID(string id)
        {
            return new QueryResult<CardEvent>(ResultCode.Fail, "没有实现按事件ID查询刷卡记录的功能", null);
        }

        protected override QueryResultList<CardEvent> GetingItems(SearchCondition search)
        {
            if (search is CardEventSearchCondition)
            {
                var con = search as CardEventSearchCondition;
                return _Client.GetCardEvents(con.EventTime.Begin, con.EventTime.End, _ILoggerFactory);
            }
            return new QueryResultList<CardEvent>(ResultCode.Fail, "没有指定刷卡记录查询条件", null);
        }

        //protected override QueryResultList<CardEvent> GetingItems(SearchCondition search)
        //{
        //    if (search is CardEventSearchCondition)
        //    {
        //        var con = search as CardEventSearchCondition;
        //        return MockAcsProvider.GetCardEvents(con);
        //    }
        //    return new QueryResultList<CardEvent>(ResultCode.Fail, "没有指定刷卡记录查询条件", null);
        //}
        #endregion
    }
}
