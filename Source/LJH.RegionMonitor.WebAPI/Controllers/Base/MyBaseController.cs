using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LJH.GeneralLibrary.Core;

namespace LJH.RegionMonitor.WebAPI
{
    [Route("api/[controller]")]
    public abstract class MyBaseController<TID, TEntity> : Controller where TEntity : class, IEntity<TID>
    {
        #region 构造函数
        public MyBaseController(ILoggerFactory loggerFactory)
        {
            _ILoggerFactory = loggerFactory;
        }
        #endregion

        #region 私有变量
        protected ILoggerFactory _ILoggerFactory;
        #endregion

        #region 子类要重写的方法
        protected virtual SearchCondition ExtraQuery()
        {
            SearchCondition con = null;
            var query = HttpContext.Request.Query["q"];
            if (!string.IsNullOrEmpty(query))
            {
                var logger = _ILoggerFactory.CreateLogger(this.GetType().FullName);
                logger.LogInformation("准备解析查询字符串内容 q={query}", query);
                var temp = GZipHelper.DecompressString(query);
                var s = JsonConvert.DeserializeObject<SearchConditionWrap>(temp);
                if (!string.IsNullOrEmpty(s.SearchType))
                {
                    var t = TypeHelper.GetTypeOfName("LJH.RegionMonitor.Model", s.SearchType);
                    if (t != null)
                    {
                        con = JsonConvert.DeserializeObject(s.Search, t) as SearchCondition;
                        logger.LogInformation("解析查询字符串内容 type={type} con={con}", s.SearchType, con == null ? "null" : Newtonsoft.Json.JsonConvert.SerializeObject(con));
                    }
                    else
                    {
                        logger.LogWarning("解析查询字符串失败");
                    }
                }
                else
                {
                    logger.LogWarning("解析查询字符串失败");
                }
            }
            return con;
        }

        protected abstract QueryResult<TEntity> GetingItemByID(TID id);

        protected abstract QueryResultList<TEntity> GetingItems(SearchCondition search);

        protected virtual CommandResult<TEntity> AddEntity(TEntity info)
        {
            throw new Exception("子类没有实现此方法");
        }

        protected virtual CommandResult<TEntity> UpdateEntity(TEntity info, TEntity original)
        {
            throw new Exception("子类没有实现此方法");
        }

        protected virtual CommandResult<TEntity> PatchEntity(TEntity info, Dictionary<string, string> patches)
        {
            throw new Exception("子类没有实现此方法");
        }

        protected virtual CommandResult DeleteEntity(TEntity info)
        {
            throw new Exception("子类没有实现此方法");
        }
        #endregion

        #region 公共方法
        [HttpGet("{id}")]
        //[Authorize]
        public virtual IActionResult GetByID(TID id)
        {
            var ret = GetingItemByID(id);
            return Ok(ret);
        }

        [HttpGet]
        //[Authorize]
        public virtual IActionResult GetList()
        {
            var con = ExtraQuery();
            DateTime dt = DateTime.Now;
            var ret = GetingItems(con);
            var ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
            var logger = _ILoggerFactory.CreateLogger(this.GetType().FullName);
            logger.LogInformation("查询耗时 {0}", ts.TotalSeconds);
            return Ok(ret);
        }

        [HttpPost]
        //[Authorize]
        public virtual IActionResult Add([FromBody]TEntity info)
        {
            if (ModelState.IsValid)
            {
                var original = GetingItemByID(info.ID).QueryObject;
                if (original != null)
                {
                    var ret = UpdateEntity(info, original);
                    return Ok(ret);
                }
                else
                {
                    var ret = AddEntity(info);
                    return Ok(ret);
                }
            }
            return Ok(new CommandResult(ResultCode.Fail, "参数错误"));
        }

        [HttpPut("{id}")]
        //[Authorize]
        public virtual IActionResult Update(TID id, [FromBody]TEntity info)
        {
            if (ModelState.IsValid)
            {
                var original = GetingItemByID(info.ID).QueryObject;
                if (original != null)
                {
                    var ret = UpdateEntity(info, original);
                    return Ok(ret);
                }
                else
                {
                    NotFound();
                }
            }
            return Ok(new CommandResult(ResultCode.Fail, "参数错误"));
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public virtual IActionResult Delete(TID id)
        {
            var original = GetingItemByID(id).QueryObject;
            if (original == null) return Ok(new CommandResult(ResultCode.Successful, string.Empty));
            var ret = DeleteEntity(original);
            return Ok(ret);
        }

        [HttpPatch("{id}")]
        //[Authorize]
        public virtual IActionResult Patch(TID id, [FromBody] Dictionary<string, string> items)
        {
            if (ModelState.IsValid)
            {
                var info = GetingItemByID(id).QueryObject;
                if (info == null) return NotFound();
                var ret = PatchEntity(info, items);
                return Ok(ret);
            }
            return Ok(new CommandResult(ResultCode.Fail, "参数错误"));
        }
        #endregion
    }
}
