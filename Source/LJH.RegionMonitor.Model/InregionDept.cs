using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.RegionMonitor.Model
{
    public class InregionDept
    {
        #region 构造函数
        public InregionDept()
        {
        }

        public InregionDept(string name,List<InRegionPerson > person)
        {
            Name = name;
            InregionPerson = person;
        }
        #endregion

        #region 公共属性
        public string Name { get; set; }

        public List<InRegionPerson> InregionPerson { get; set; }
        #endregion
    }
}
