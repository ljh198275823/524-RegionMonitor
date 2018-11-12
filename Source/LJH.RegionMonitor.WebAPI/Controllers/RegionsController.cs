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

namespace LJH.RegionMonitor.WebAPI.Controllers
{
    public class RegionsController : MyBaseController<int, Region>
    {
        #region 构造函数
        public RegionsController(ILoggerFactory loggerFactory) :
            base(loggerFactory)
        {
        }
        #endregion

        #region 重写基类方法
        protected override QueryResult<Region> GetingItemByID(int id)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Regions", $"{id}.region");
            if (System.IO.File.Exists(path))
            {
                var item = JsonConvert.DeserializeObject<Region>(System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8));
                return new QueryResult<Region>(ResultCode.Successful, string.Empty, item);
            }
            return new QueryResult<Region>(ResultCode.Fail, "没有找到相关数据", null);
        }

        protected override CommandResult<Region> AddEntity(Region info)
        {
            if (info.ID == 0) info.ID = 1;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Regions", $"{info.ID }.region");
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(info), System.Text.Encoding.UTF8);
            return new CommandResult<Region>(ResultCode.Successful, string.Empty, info);
        }

        protected override CommandResult<Region> UpdateEntity(Region info, Region original)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Regions", $"{info.ID }.region");
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(info), System.Text.Encoding.UTF8);
            return new CommandResult<Region>(ResultCode.Successful, string.Empty, info);
        }

        protected override CommandResult DeleteEntity(Region info)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Regions", $"{info.ID }.region");
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            return new CommandResult(ResultCode.Successful, string.Empty);
        }

        protected override CommandResult<Region> PatchEntity(Region info, Dictionary<string, string> patches)
        {
            throw new NotImplementedException();
        }

        protected override QueryResultList<Region> GetingItems(SearchCondition search)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
