using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datapoint.ACS.DivisionMonitor.Model
{
    /// <summary>
    /// 表示在场人员
    /// </summary>
    public class User
    {
        #region 构造函数
        public User() { }
        #endregion

        #region 公共属性
        public string CardID { get; set; }

        public string UserName { get; set; }

        public string Department { get; set; }
        #endregion
    }
}
