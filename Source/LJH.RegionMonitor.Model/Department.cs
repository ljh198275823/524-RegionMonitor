using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.RegionMonitor.Model
{
    public class Department
    {
        #region 构造函数
        public Department() { }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置门ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 获取或设置门名称
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
}
