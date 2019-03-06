using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LJH.RegionMonitor.Model;
using LJH.GeneralLibrary.Core;

namespace LJH.RegionMonitor.WebAPI.Controllers
{
    public class MockAcsProvider
    {
        private static List<Door> _Doors = new List<Door>()
        {
            new Door (){ID ="1",Name ="入口1",ControlName ="入口闸机"},
            new Door (){ID ="2",Name ="入口2",ControlName ="入口闸机"},
            new Door (){ID ="3",Name ="入口3",ControlName ="入口闸机"},
            new Door (){ID ="4",Name ="入口4",ControlName ="入口闸机"},
            new Door (){ID ="5",Name ="入口5",ControlName ="入口闸机"},
            new Door (){ID ="6",Name ="出口1",ControlName ="出口闸机"},
            new Door (){ID ="7",Name ="出口2",ControlName ="出口闸机"},
            new Door (){ID ="8",Name ="出口3",ControlName ="出口闸机"},
            new Door (){ID ="9",Name ="出口4",ControlName ="出口闸机"},
            new Door (){ID ="10",Name ="出口5",ControlName ="出口闸机"},
        };

        private static Random _Random = new Random();

        public static QueryResultList<Door> GetDoors()
        {
            return new QueryResultList<Door>(ResultCode.Successful, string.Empty, _Doors.ToList());
        }

        public static QueryResultList<CardEvent> GetCardEvents(CardEventSearchCondition con)
        {
            System.Threading.Thread.Sleep(_Random.Next(1, 120) * 1000);
            List<CardEvent> ret = new List<CardEvent>();
            DateTime dt = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                var userid = _Random.Next(1, 300);
                var doorID = _Random.Next(1, 10);
                var door = _Doors.Single(it => it.ID == doorID.ToString());
                var ce = new CardEvent()
                {
                    ID = Guid.NewGuid().ToString(),
                    UserID = userid.ToString(),
                    UserName = $"用户{userid}",
                    Department = $"电力部门{Math.Floor((double)userid / 30)}",
                    DoorID = door.ID,
                    DoorName = door.Name,
                    EventTime = dt,
                    Permitted = true,
                    Photo = null,
                };
                ret.Add(ce);
            }
            return new QueryResultList<CardEvent>(ResultCode.Successful, String.Empty, ret);
        }
    }
}
