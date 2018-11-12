using System;
using System.Collections.Generic;
using System.Text;
using LJH.GeneralLibrary;

namespace LJH.RegionMonitor.Model
{
    public class CardEventSearchCondition:LJH.GeneralLibrary.Core.SearchCondition
    {
        public DateTimeRange EventTime { get; set; }
    }
}
