using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Core
{
    /// <summary>
    /// 查询数据返回单个对象时的查询结果
    /// </summary>
    public class QueryResult<T>
    {
#region 构造函数
        public QueryResult()
        {
        }

        public QueryResult(ResultCode code, string msg, T obj)
        {
            this.Result  = code;
            this.Message  = msg;
            this.QueryObject = obj;
        }
#endregion

        #region 公共属性
        /// <summary>
        ///获取或设置执行结果
        /// </summary>
        public ResultCode Result { get; set; }
        /// <summary>
        /// 获取或设置执行结果的文字描述
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 获取或设置返回的查询对象
        /// </summary>
        public T QueryObject { get; set; }
        #endregion
    }
}
