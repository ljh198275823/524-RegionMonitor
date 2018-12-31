using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.RegionMonitor.Model
{
    public class PersonDetail : LJH.GeneralLibrary.Core.IEntity<string>
    {
        #region 公共属性
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 人员部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 人员手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string Certificate { get; set; }

        public string Photo { get; set; }
        /// <summary>
        /// 获取或设置照片URL
        /// </summary>
        public string PhotoUrl { get; set; }
        #endregion
    }
}
