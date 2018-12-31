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
    public sealed class PersonDetailsController : HttpGetOnlyBaseController<string, PersonDetail>
    {
        #region 构造函数
        public PersonDetailsController(ILoggerFactory loggerFactory, HKVisionClient client) :
            base(loggerFactory)
        {
            _Client = client;
        }
        #endregion

        private HKVisionClient _Client;

        #region 重写基类方法
        protected override QueryResult<PersonDetail> GetingItemByID(string id)
        {
            PersonDetail item = null;
            var ret = _Client.GetPersonDetail(id, _ILoggerFactory);
            if (ret.QueryObject != null) item = ret.QueryObject;
            return new QueryResult<PersonDetail>(ret.Result, ret.Message, item);
        }

        //protected override QueryResult<PersonDetail> GetingItemByID(string id)
        //{
        //    PersonDetail item = new PersonDetail();
        //    item.ID = id;
        //    item.Name = $"用户{id}";
        //    item.Phone = "13485932340";
        //    item.PhotoUrl = $"http://47.92.81.39:13002/rm/img/{id}.jpg";
        //    return new QueryResult<PersonDetail>(ResultCode.Successful, string.Empty, item);
        //}

        protected override QueryResultList<PersonDetail> GetingItems(SearchCondition search)
        {
            return new QueryResultList<PersonDetail>(ResultCode.Successful, "没有实现此方法", null);
        }
        #endregion
    }
}
