using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LJH.OneCard.HKVisionClient
{
    public class HKVisionResponseBase
    {
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }

    public class HKVisionListData<T>
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("pageNo")]
        public int PageNo { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        [JsonProperty("list")]
        public List<T> List { get; set; }
    }

    public class HKVisionDoor
    {
        [JsonProperty("doorUuid")]
        public string DoorID { get; set; }
        [JsonProperty("doorName")]
        public string DoorName { get; set; }
        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }
    }

    public class HKVisionCardEvent
    {
        [JsonProperty("doorUuid")]
        public string DoorID { get; set; }
        [JsonProperty("doorName")]
        public string DoorName { get; set; }
        public string EventUuid { get; set; }
        public int EventType { get; set; }

        public long EventTime { get; set; }

        public string EventName { get; set; }

        public string CardNo { get; set; }

        public string PersonName { get; set; }

        public string DeptName { get; set; }

        public string PicUrl { get; set; }
    }
}
