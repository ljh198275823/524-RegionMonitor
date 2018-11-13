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
            this.EnterDoors = region.EnterDoors;
            this.ExitDoors = region.ExitDoors;
        }
        #endregion

        #region 私有变量
        private object _PersonLocker = new object();
        private Dictionary<string, InRegionPerson> _Person = new Dictionary<string, InRegionPerson>();
        private bool _PersonChanged = false;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置区域名称
        /// </summary>
        public string Name { get; set; }

        public List<string> EnterDoors { get; set; }

        public List<string> ExitDoors { get; set; }
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
                    _PersonChanged = false; //获取到当前的所有人员后,变化标志设置为假
                    return (from u in _Person.Values
                            orderby u.EnterDateTime ascending
                            select u).ToList();
                }
            }
        }
        /// <summary>
        /// 场内人没是否有变化
        /// </summary>
        public bool InregionUsersChanged
        {
            get
            {
                lock (_PersonLocker)
                {
                    return _PersonChanged;
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
                    _PersonChanged = true;
                }
                else
                {
                    _Person[item.UserID].EnterDateTime = item.EventTime;
                    _PersonChanged = true;
                }
            }
        }

        private void ExitRegion(CardEvent item)
        {
            lock (_PersonLocker)
            {
                if (_Person.ContainsKey(item.UserID))
                {
                    _Person.Remove(item.UserID);
                    _PersonChanged = true;
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
        #endregion
    }
}
