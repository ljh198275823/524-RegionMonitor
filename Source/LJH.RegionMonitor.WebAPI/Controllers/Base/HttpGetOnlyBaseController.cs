using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LJH.GeneralLibrary.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LJH.RegionMonitor.WebAPI
{
    public abstract class HttpGetOnlyBaseController<TID, TEntity> : MyBaseController<TID, TEntity> where TEntity : class, IEntity<TID>
    {
        #region 构造函数
        public HttpGetOnlyBaseController(ILoggerFactory loggerFactory) :
            base(loggerFactory)
        {
        }
        #endregion

        #region 公共方法
        [NonAction]
        public override IActionResult Add(TEntity info)
        {
            return BadRequest("不支持的方法");
        }

        [NonAction]
        public override IActionResult Update(TID id, TEntity info)
        {
            return BadRequest("不支持的方法");
        }

        [NonAction]
        public override IActionResult Patch(TID id, Dictionary<string, string> items)
        {
            return BadRequest("不支持的方法");
        }

        [NonAction]
        public override IActionResult Delete(TID id)
        {
            return BadRequest("不支持的方法");
        }
        #endregion
    }
}
