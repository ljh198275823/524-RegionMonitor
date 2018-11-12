using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace LJH.GeneralLibrary
{
    /// <summary>
    /// 表示一个时间段
    /// </summary>
    [Serializable]
    [DataContract]
    public class DateTimeRange
    {
        /// <summary>
        /// 开始时间(如果不指定为日期的最小值)
        /// </summary>
        [DataMember]
        public DateTime Begin { get; set; }

        /// <summary>
        /// 结束时间(如果不指定为日期的最大值)
        /// </summary>
        [DataMember]
        public DateTime End { get; set; }

        public DateTimeRange()
        {
            Begin = DateTime.MinValue ;
            End = DateTime.MaxValue;
        }

        public DateTimeRange(DateTime bengin, DateTime dtend)
        {
            Begin  = bengin;
            End = dtend;
        }
    }
}
