using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using LJH.GeneralLibrary.Core;
using LJH.RegionMonitor.Model;

namespace LJH.OneCard.HKVisionClient
{
    public class HKVisionClient
    {
        #region 构造函数
        public HKVisionClient(string baseUrl, string appkey, string secret)
        {
            BaseUrl = baseUrl;
            AppKey = appkey;
            Secret = secret;
        }
        #endregion

        #region 私有变量
        public string AppKey { get; set; } // = "f8db0c75";
        public string Secret { get; set; } //= "41455e9d614643569a53bd338d095062";
        private string BaseUrl { get; set; }
        private static string OP_USER_UUID { get; set; } // "0be83d40695011e7981e0f190ed6d2e7";
        #endregion

        #region 私有方法
        private bool GetDefaultUserUUID()
        {
            String url = "/openapi/service/base/user/getDefaultUserUuid";
            var body = new
            {
                appkey = AppKey,
                time = GetMilliSeconds(DateTime.Now),
            };
            var content = JsonConvert.SerializeObject(body);
            var ret = PostRequest(url, content);
            if (ret.ErrorCode == 0) OP_USER_UUID = ret.Data;
            return (ret.ErrorCode == 0);
        }

        private static long GetMilliSeconds(DateTime dt)
        {
            return (long)Math.Floor(new TimeSpan(dt.Ticks - new DateTime(1970, 1, 1).Ticks).TotalMilliseconds);
        }

        private static DateTime GetDateTimeFromMilliSeconds(long milSeconds)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(milSeconds);
        }

        private static string MD5Encrypt(string source)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));
            return LJH.GeneralLibrary.HexStringConverter.HexToString(data, string.Empty);
        }

        private HKVisionResponseBase PostRequest(string url, string content)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var token = MD5Encrypt($"{url}{content}{Secret}");
                    url += $"?token={token }";
                    url = BaseUrl.TrimEnd('/') + url;
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    client.Headers.Add("content-type", "application/json;charset=utf-8;");
                    var retBytes = client.UploadData(url, "POST", System.Text.ASCIIEncoding.UTF8.GetBytes(content));
                    var ret = JsonConvert.DeserializeObject<HKVisionResponseBase>(System.Text.ASCIIEncoding.UTF8.GetString(retBytes));
                    return ret;
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new HKVisionResponseBase() { ErrorCode = -1, ErrorMessage = ex.Message, Data = null };
            }
        }

        private bool IsPermitted(int eventType)
        {
            switch (eventType)
            {
                case 196881:
                case 196883:
                case 196884:
                case 196885:
                case 196886:
                case 196887:
                case 196888:
                case 196889:
                case 196890:
                case 196891:
                case 196892:
                case 196893:
                case 197128:
                case 197162:
                case 198914:
                case 198915:
                    return true;
                default:
                    return false;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取所有的门禁点
        /// </summary>
        /// <returns></returns>
        public QueryResultList<Door> GetDoors()
        {
            if (string.IsNullOrEmpty(OP_USER_UUID)) GetDefaultUserUUID();
            var url = "/openapi/service/acs/res/getDoors";
            var content = new
            {
                appkey = AppKey,
                opUserUuid = OP_USER_UUID,
                time = GetMilliSeconds(DateTime.Now),
                pageNo = 1,
                pageSize = 999
            };
            var ret = PostRequest(url, JsonConvert.SerializeObject(content));
            if (ret.ErrorCode == 0 && !string.IsNullOrEmpty(ret.Data))
            {
                var items = JsonConvert.DeserializeObject<HKVisionListData<HKVisionDoor>>(ret.Data);
                List<Door> doors = null;
                if (items.List != null && items.List.Count > 0) doors = items.List.Select(it => new Door() { ID = it.DoorID, Name = it.DoorName, ControlName = it.DeviceName }).ToList();
                return new QueryResultList<Door>(ResultCode.Successful, ret.ErrorMessage, doors);
            }
            return new QueryResultList<Door>(ResultCode.Fail, ret.ErrorMessage, null);
        }

        public QueryResultList<CardEvent> GetCardEvents(DateTime begin, DateTime end)
        {
            if (string.IsNullOrEmpty(OP_USER_UUID)) GetDefaultUserUUID();
            var url = "/openapi/service/acs/event/getDoorEventsHistory";
            var content = new
            {
                appkey = AppKey,
                opUserUuid = OP_USER_UUID,
                time = GetMilliSeconds(DateTime.Now),
                pageNo = 1,
                pageSize = 200,
                startTime = GetMilliSeconds(begin),
                endTime = GetMilliSeconds(end)
            };
            var ret = PostRequest(url, JsonConvert.SerializeObject(content));
            if (ret.ErrorCode == 0 && !string.IsNullOrEmpty(ret.Data))
            {
                var items = JsonConvert.DeserializeObject<HKVisionListData<HKVisionCardEvent>>(ret.Data);
                List<CardEvent> events = null;
                if (items.List != null && items.List.Count > 0)
                {
                    events = items.List.Where(it => IsPermitted(it.EventType)).Select(it => new CardEvent()
                    {
                        ID = it.EventUuid,
                        UserID = it.PersonId.ToString(),
                        UserName = it.PersonName,
                        Department = it.DeptName,
                        DoorID = it.DoorID,
                        DoorName = it.DoorName,
                        EventTime = GetDateTimeFromMilliSeconds(it.EventTime),
                        Permitted = IsPermitted(it.EventType),
                        Photo = it.PicUrl,
                    }).ToList();
                }
                return new QueryResultList<CardEvent>(ResultCode.Successful, ret.ErrorMessage, events);
            }
            return new QueryResultList<CardEvent>(ResultCode.Fail, ret.ErrorMessage, null);
        }
        #endregion
    }
}
