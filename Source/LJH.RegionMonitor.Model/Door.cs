using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Datapoint.ACS.DivisionMonitor.Model
{
    public class Door : LJH.GeneralLibrary.Core.IEntity<string>
    {
        public Door() { }

        #region 公共属性
        /// <summary>
        /// 获取或设置门ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 获取或设置门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置所属控制器名称
        /// </summary>
        public string ControlName { get; set; }
        #endregion
    }
}
