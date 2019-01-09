using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LJH.OneCard.HKVisionClient
{
    internal class HKVisionListResponse<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public HKVisionListData<T> Data { get; set; }
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

    internal class HKVisionDept
    {
        [JsonProperty("orgIndexCode")]
        public string ID { get; set; }
        [JsonProperty("orgName")]
        public string Name { get; set; }
    }

    internal class HKVisionPerson
    {
        [JsonProperty("personId")]
        public string ID { get; set; }
        [JsonProperty("personName")]
        public string Name { get; set; }
        [JsonProperty("orgIndexCode")]
        public  string DepartmentID { get; set; }
        [JsonProperty("orgName")]
        public string Department { get; set; }
        [JsonProperty("phoneNo")]
        public string Phone { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("certificateNo")]
        public string Certificate { get; set; }
        [JsonProperty("personPhoto")]
        public PhotoInfo PhotoInfo { get; set; }
    }

    internal class PhotoInfo
    {
        [JsonProperty("picUri")]
        public string PicUri { get; set; }
        [JsonProperty("serverIndexCode")]
        public string ServerIndexCode { get; set; }
    }

    internal class HKVisionDoor
    {
        [JsonProperty("doorIndexCode")]
        public string ID { get; set; }
        [JsonProperty("doorName")]
        public string Name { get; set; }
        [JsonProperty("acsDevIndexCode")]
        public string DeviceCode { get; set; }
    }

    internal class HKVisionCardEvent
    {
        [JsonProperty("eventId")]
        public string EventUuid { get; set; }
        [JsonProperty("doorIndexCode")]
        public string DoorID { get; set; }
        [JsonProperty("doorName")]
        public string DoorName { get; set; }
        [JsonProperty("eventType")]
        public int EventType { get; set; }
        [JsonProperty("eventTime")]
        public DateTime EventTime { get; set; }
        [JsonProperty("eventName")]
        public string EventName { get; set; }
        [JsonProperty("personId")]
        public string PersonId { get; set; }
        [JsonProperty("personName")]
        public string PersonName { get; set; }
        [JsonProperty("orgIndexCode")]
        public string DeptName { get; set; }
        [JsonProperty("cardNo")]
        public string CardNo { get; set; }
        [JsonProperty("picUrl")]
        public string PicUrl { get; set; }
    }
}
