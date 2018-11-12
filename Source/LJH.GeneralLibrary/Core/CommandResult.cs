using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Core
{
    /// <summary>
    /// 表示数据操作返回值
    /// </summary>
    public class CommandResult
    {
        #region 构造函数
        public CommandResult()
        {
        }

        public CommandResult(ResultCode code, string msg)
        {
            this.Result = code;
            this.Message = msg;
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
        #endregion
    }

    /// <summary>
    /// 表示数据操作返回值
    /// </summary>
    public class CommandResult<T> : CommandResult where T : class
    {
        #region 构造函数
        public CommandResult(ResultCode code, string msg, T t) : base(code, msg)
        {
            Value = t;
        }
        #endregion

        #region 公共属性
        public T Value { get; set; }
        #endregion
    }
}
