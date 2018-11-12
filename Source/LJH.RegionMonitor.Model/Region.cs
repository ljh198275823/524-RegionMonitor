using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LJH.RegionMonitor.Model
{
    public class Region : LJH.GeneralLibrary.Core.IEntity<int>
    {
        #region 构造函数
        public Region()
        {
        }
        #endregion

        #region 公共属性
        public int ID { get; set; }
        /// <summary>
        /// 获取或设置区域名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置所有入场门禁点
        /// </summary>
        public List<string> EnterDoors { get; set; }
        /// <summary>
        /// 获取或设置所有出场门禁点
        /// </summary>
        public List<string> ExitDoors { get; set; }
        #endregion
    }
}
