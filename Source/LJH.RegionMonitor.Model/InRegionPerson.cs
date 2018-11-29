using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.RegionMonitor.Model
{
    /// <summary>
    /// 表示在场人员
    /// </summary>
    public class InRegionPerson
    {
        #region 构造函数
        public InRegionPerson() { }

        public InRegionPerson(CardEvent ce)
        {
            UserID = ce.UserID;
            UserName = ce.UserName;
            Department = ce.Department;
            PhotoPath = ce.Photo;
            DoorID = ce.DoorID;
            DoorName = ce.DoorName;
            EnterDateTime = ce.EventTime;
        }
        #endregion

        #region 公共属性
        public string UserID { get; set; }

        public string UserName { get; set; }

        public string Department { get; set; }

        public string PhotoPath { get; set; }

        public string DoorID { get; set; }

        public string DoorName { get; set; }

        public DateTime EnterDateTime { get; set; }
        /// <summary>
        /// 获取人员是否超时未出
        /// </summary>
        public bool IsTimeout
        {
            get
            {
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - EnterDateTime.Ticks);
                return ts.TotalDays >= 1 && ts.TotalDays < 2;
            }
        }
        /// <summary>
        /// 获取人员是否在场
        /// </summary>
        public bool IsInRegion
        {
            get
            {
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - EnterDateTime.Ticks);
                return ts.TotalDays < 1;
            }
        }
        /// <summary>
        /// 是否超过48小时未出场
        /// </summary>
        public bool IsOver48
        {
            get
            {
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - EnterDateTime.Ticks);
                return ts.TotalDays >= 2;
            }
        }
        #endregion
    }
}
