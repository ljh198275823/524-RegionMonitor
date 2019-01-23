using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LJH.RegionMonitor.Model
{
    public class MonitorRegion
    {
        public MonitorRegion Current { get; set; }

        #region 构造函数
        public MonitorRegion(Region region)
        {
            this.Name = region.Name;
            this.HideTimeOutPerson = region.HideTimeOutPerson;
            this.EnterDoors = region.EnterDoors;
            this.ExitDoors = region.ExitDoors;
        }
        #endregion

        #region 私有变量
        private object _PersonLocker = new object();
        private Dictionary<string, InRegionPerson> _Person = new Dictionary<string, InRegionPerson>();
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置区域名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置入口门禁点
        /// </summary>
        public List<string> EnterDoors { get; set; }
        /// <summary>
        /// 获取或设置出场门禁点
        /// </summary>
        public List<string> ExitDoors { get; set; }
        /// <summary>
        /// 获取或设置隐藏超时未出人员
        /// </summary>
        public bool HideTimeOutPerson { get; set; }
        /// <summary>
        /// 获取或设置区域人员是否有变化
        /// </summary>
        public bool PersonChanged { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取目前区域的所有在场人数
        /// </summary>
        public List<InRegionPerson> InregionUsers
        {
            get
            {
                lock (_PersonLocker)
                {
                    return (from u in _Person.Values
                            where u.IsInRegion || (!HideTimeOutPerson && u.IsTimeout) //只有在场和超时未出的人员是有效的在场人员
                            orderby u.EnterDateTime ascending
                            select u).ToList();
                }
            }
        }
        #endregion

        #region 私有方法
        private void EnterRegion(CardEvent item)
        {
            lock (_PersonLocker)
            {
                if (!_Person.ContainsKey(item.UserID))
                {
                    _Person.Add(item.UserID, new InRegionPerson(item));
                    PersonChanged = true;
                }
                else
                {
                    if (_Person[item.UserID].EnterDateTime < item.EventTime) //要判断一下本次刷卡时间是否大于人员入场时间
                    {
                        _Person[item.UserID].EnterDateTime = item.EventTime;
                        PersonChanged = true;
                    }
                }
            }
        }

        private void ExitRegion(CardEvent item)
        {
            lock (_PersonLocker)
            {
                if (_Person.ContainsKey(item.UserID))
                {
                    if (_Person[item.UserID].EnterDateTime < item.EventTime) //要判断一下本次刷卡时间是否大于人员入场时间
                    {
                        _Person.Remove(item.UserID);
                        PersonChanged = true;
                    }
                }
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 是否是区域内的门
        /// </summary>
        /// <param name="doorID"></param>
        /// <returns></returns>
        public bool IsMyDoor(string doorID)
        {
            return EnterDoors.Contains(doorID) || ExitDoors.Contains(doorID);
        }
        /// <summary>
        /// 处理刷卡事件
        /// </summary>
        /// <param name="events"></param>
        public void HandleCardEvent(CardEvent item)
        {
            if (EnterDoors == null || EnterDoors.Count == 0 || ExitDoors == null || ExitDoors.Count == 0) return;
            if (item.Permitted && item.UserID != item.UserName && !string.IsNullOrEmpty(item.Department) && !string.IsNullOrEmpty(item.UserName)) //有效刷卡，并且是已经登记的人员
            {
                if (EnterDoors.Contains(item.DoorID)) //说明门是入场门,刷卡即入场
                {
                    EnterRegion(item); //换成相对于区域的出入状态
                    return;
                }
                else if (ExitDoors.Contains(item.DoorID)) //门是出场门,刷卡即出场
                {
                    ExitRegion(item);
                    return;
                }
            }
        }

        public void SetRegionParams(Region region)
        {
            if(this.HideTimeOutPerson !=region.HideTimeOutPerson )
            {
                this.HideTimeOutPerson = region.HideTimeOutPerson;
                PersonChanged = true;
            }
            this.EnterDoors = region.EnterDoors;
            this.ExitDoors = this.ExitDoors;
        }
        #endregion
    }
}
