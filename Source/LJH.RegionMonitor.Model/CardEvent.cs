using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LJH.RegionMonitor.Model
{
    /// <summary>
    /// 表示刷卡记录
    /// </summary>
    public class CardEvent : LJH.GeneralLibrary.Core.IEntity<string>
    {
        #region 构造函数
        public CardEvent() { }
        #endregion

        #region 公共属性
        public string ID { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名，系统不能识别的卡刷卡时持卡人姓名为空
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 获取或设置刷卡人部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置门禁点ID
        /// </summary>
        public string DoorID { get; set; }
        /// <summary>
        /// 获取或设置门禁点名称
        /// </summary>
        public string DoorName { get; set; }
        /// <summary>
        /// 获取或设置事件类型
        /// </summary>
        public int EventType { get; set; }
        /// <summary>
        /// 获取或设置刷卡时间
        /// </summary>
        public DateTime EventTime { get; set; }
        /// <summary>
        /// 获取或设置是否是有效事件,否则为无效刷卡事件
        /// </summary>
        public bool Permitted { get; set; }
        /// <summary>
        /// 获取或设置照片文件名称
        /// </summary>
        public string Photo { get; set; }
        #endregion
    }
}
