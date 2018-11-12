using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LJH.GeneralLibrary.Core
{
    /// <summary>
    /// 表示数据库查询条件
    /// </summary>
    public abstract class SearchCondition
    {
        #region 公共方法
        public string GetQueryString()
        {
            var str = JsonConvert.SerializeObject(new
            {
                SearchType = this.GetType().Name,
                Search = JsonConvert.SerializeObject(this)
            });
            return GZipHelper.CompressString(str);
        }
        #endregion
    }
}
