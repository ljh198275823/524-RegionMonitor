using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LJH.OneCard.HKVisionClient
{
    internal class HKVisionResponseBase
    {
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }

    internal class HKVisionListData<T>
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

    internal class HKVisionDoor
    {
        [JsonProperty("doorUuid")]
        public string DoorID { get; set; }
        [JsonProperty("doorName")]
        public string DoorName { get; set; }
        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }
    }

    internal class HKVisionCardEvent
    {
        [JsonProperty("doorUuid")]
        public string DoorID { get; set; }
        [JsonProperty("doorName")]
        public string DoorName { get; set; }
        [JsonProperty("eventUuid")]
        public string EventUuid { get; set; }
        [JsonProperty("eventType")]
        public int EventType { get; set; }
        [JsonProperty("eventTime")]
        public long EventTime { get; set; }
        [JsonProperty("eventName")]
        public string EventName { get; set; }
        [JsonProperty("personId")]
        public int PersonId { get; set; }
        [JsonProperty("personName")]
        public string PersonName { get; set; }
        [JsonProperty("personName")]
        public string DeptName { get; set; }
        [JsonProperty("picUrl")]
        public string PicUrl { get; set; }
    }
}
