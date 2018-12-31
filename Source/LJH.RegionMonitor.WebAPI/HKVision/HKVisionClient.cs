using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using LJH.GeneralLibrary.Core;
using LJH.RegionMonitor.Model;
using Microsoft.Extensions.Logging;

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
        private Dictionary<string, HKVisionDept> _Depts = null;
        #endregion

        #region 私有方法
        private static long GetMilliSeconds(DateTime dt)
        {
            return (long)Math.Floor(new TimeSpan(dt.Ticks - new DateTime(1970, 1, 1).Ticks).TotalMilliseconds);
        }

        private static DateTime GetDateTimeFromMilliSeconds(long milSeconds)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(milSeconds);
        }

        private string GetHASH256(string content)
        {
            try
            {
                using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Secret)))
                {
                    return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(content)));
                }
            }
            catch
            {
                return null;
            }
        }

        private string Sign(string method, Dictionary<string, string> headers, string url)
        {
            var sb = new StringBuilder();
            sb.Append(method.ToUpper() + "\n");
            if (headers.ContainsKey("Accept")) sb.Append(headers["Accept"] + "\n");
            if (headers.ContainsKey("Content-MD5")) sb.Append(headers["Content-MD5"] + "\n");
            if (headers.ContainsKey("Content-Type")) sb.Append(headers["Content-Type"] + "\n");
            sb.Append(string.Format("{0}:{1}", "X-Ca-Key", headers["X-Ca-Key"]).ToLower() + "\n");
            sb.Append(string.Format("{0}:{1}", "X-Ca-Timestamp", headers["X-Ca-Timestamp"]).ToLower() + "\n");
            sb.Append(url);
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return GetHASH256(sb.ToString());
        }

        private HKVisionListResponse<T> PostRequest<T>(string url, string content, ILoggerFactory loggerFactory)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var dics = new Dictionary<string, string>();
                    dics.Add("Accept", "application/json;charset=utf-8;");
                    dics.Add("Content-Type", "application/json;charset=utf-8;");
                    if (!string.IsNullOrEmpty(content)) dics.Add("Content-MD5", Convert.ToBase64String(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(content))));
                    dics.Add("X-Ca-Key", AppKey);
                    dics.Add("X-Ca-Timestamp", GetMilliSeconds(DateTime.Now).ToString());
                    foreach (var item in dics)
                    {
                        client.Headers.Add(item.Key, item.Value);
                    }
                    client.Headers.Add("X-Ca-Signature-Headers", "x-ca-key,x-ca-timestamp");
                    client.Headers.Add("X-Ca-Signature", Sign("POST", dics, url));
                    url = BaseUrl.TrimEnd('/') + url;
                    var retBytes = client.UploadData(url, "POST", System.Text.ASCIIEncoding.UTF8.GetBytes(content));
                    //if (loggerFactory != null) loggerFactory.CreateLogger("HKVS").Log(LogLevel.Information, string.Join(string.Empty, retBytes.Select(it => it.ToString("X2")).ToList()));
                    var ret = JsonConvert.DeserializeObject<HKVisionListResponse<T>>(System.Text.ASCIIEncoding.UTF8.GetString(retBytes));
                    return ret;

                    //var str = "7B22636F6465223A2230222C226D7367223A2273756363657373222C2264617461223A7B227061676553697A65223A313030302C226C697374223A5B7B226576656E744964223A2265633266653838312D653431642D343261622D623936392D333265633965303161376238222C226576656E744E616D65223A226163732E6163732E6576656E74547970652E73756363657373506572736F6E41757468656E7469636174696F6E222C226576656E7454696D65223A22323031382D31322D32355431333A30343A34372E3030302B30383A3030222C22706572736F6E4964223A2238633534343837352D316132362D343738312D616161322D646638626338366234376666222C22636172644E6F223A223030303030303133222C22706572736F6E4E616D65223A22E69D8EE7829C222C226F7267496E646578436F6465223A2234646237633839642D306365362D343832362D393134362D366237316630333764383165222C22646F6F724E616D65223A22353630335FE997A831222C22646F6F72496E646578436F6465223A226430393239323732323239653433326638306162313536383433353065333634222C22646F6F72526567696F6E496E646578436F6465223A22726F6F74303030303030222C22706963557269223A222F7069633F3D64353D693265317A30636239733235332D3131356D3565703D74326933692A64313D2A697064383D2A697364303D2A3162613933663161332D653366323638352D3462343336322D3738693230322A65353331386437222C22737672496E646578436F6465223A2233643935386332662D316639612D343262662D623462322D633434326136383431376661222C226576656E7454797065223A3139373136322C22696E416E644F757454797065223A317D2C7B226576656E744964223A2237326237373231302D326638632D343635342D613263302D383638356361623136363235222C226576656E744E616D65223A226163732E6163732E6576656E74547970652E73756363657373506572736F6E41757468656E7469636174696F6E222C226576656E7454696D65223A22323031382D31322D32355431333A30343A34302E3030302B30383A3030222C22706572736F6E4964223A2238633534343837352D316132362D343738312D616161322D646638626338366234376666222C22636172644E6F223A223030303030303133222C22706572736F6E4E616D65223A22E69D8EE7829C222C226F7267496E646578436F6465223A2234646237633839642D306365362D343832362D393134362D366237316630333764383165222C22646F6F724E616D65223A22353630335FE997A831222C22646F6F72496E646578436F6465223A226430393239323732323239653433326638306162313536383433353065333634222C22646F6F72526567696F6E496E646578436F6465223A22726F6F74303030303030222C22706963557269223A222F7069633F3D64303D693265337A33636239733235332D3131356D3565703D74326932692A64313D2A697064383D2A697364303D2A3162613933663161332D653366323638352D3462343336322D3737693230322A65373338386437222C22737672496E646578436F6465223A2233643935386332662D316639612D343262662D623462322D633434326136383431376661222C226576656E7454797065223A3139373136322C22696E416E644F757454797065223A317D5D2C22746F74616C223A322C22746F74616C50616765223A312C22706167654E6F223A317D7D";
                    //var bs = LJH.GeneralLibrary.HexStringConverter.StringToHex(str);
                    //str = Encoding.UTF8.GetString(bs);
                    //var ret = JsonConvert.DeserializeObject<HKVisionListResponse<T>>(str);
                    //return ret;
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new HKVisionListResponse<T> { Code = -1, Msg = ex.Message, Data = null };
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
        public QueryResultList<Door> GetDoors(ILoggerFactory loggerFactory)
        {
            var url = "/artemis/api/resource/v1/acsDoor/acsDoorList";
            var content = new
            {
                pageNo = 1,
                pageSize = 1000
            };
            var ret = PostRequest<HKVisionDoor>(url, JsonConvert.SerializeObject(content), loggerFactory);
            if (ret.Code == 0 && ret.Data != null && ret.Data.List != null && ret.Data.List.Count > 0)
            {
                List<Door> doors = null;
                doors = ret.Data.List.Select(it => new Door() { ID = it.ID, Name = it.Name, ControlName = it.DeviceCode }).ToList();
                return new QueryResultList<Door>(ResultCode.Successful, ret.Msg, doors);
            }
            return new QueryResultList<Door>(ResultCode.Fail, ret.Msg, null);
        }

        /// <summary>
        /// 获取所有的门禁点
        /// </summary>
        /// <returns></returns>
        private QueryResultList<HKVisionDept> GetDepts(ILoggerFactory loggerFactory)
        {
            var url = "/artemis/api/resource/v1/org/orgList";
            var content = new
            {
                pageNo = 1,
                pageSize = 999
            };
            var ret = PostRequest<HKVisionDept>(url, JsonConvert.SerializeObject(content), loggerFactory);
            if (ret.Code == 0 && ret.Data != null && ret.Data.List != null && ret.Data.List.Count > 0)
            {
                return new QueryResultList<HKVisionDept>(ResultCode.Successful, ret.Msg, ret.Data.List);
            }
            return new QueryResultList<HKVisionDept>(ResultCode.Fail, ret.Msg, null);
        }

        /// <summary>
        /// 获取所有的门禁点
        /// </summary>
        /// <returns></returns>
        public QueryResult<PersonDetail> GetPersonDetail(string personID, ILoggerFactory loggerFactory)
        {
            var url = "/artemis/api/resource/v1/person/advance/personList";
            var content = new
            {
                personIds = personID,
                pageNo = 1,
                pageSize = 1000,
            };
            var ret = PostRequest<HKVisionPerson>(url, JsonConvert.SerializeObject(content), loggerFactory);
            if (ret.Code == 0 && ret.Data != null && ret.Data.List != null && ret.Data.List.Count > 0)
            {
                var person = new PersonDetail()
                {
                    ID = ret.Data.List[0].ID,
                    Name = ret.Data.List[0].Name,
                    Department = ret.Data.List[0].Department,
                    Phone = ret.Data.List[0].Phone,
                    Certificate = ret.Data.List[0].Certificate,
                    Gender = ret.Data.List[0].Gender == "1" ? "男" : "女"
                };
                if (ret.Data.List[0].PhotoInfo != null)
                {
                    person.PhotoUrl = GetPhoto(ret.Data.List[0].PhotoInfo, loggerFactory);
                }
                return new QueryResult<PersonDetail>(ResultCode.Successful, ret.Msg, person);
            }
            return new QueryResult<PersonDetail>(ResultCode.Fail, ret.Msg, null);
        }

        /// <summary>
        /// 获取所有的门禁点
        /// </summary>
        /// <returns></returns>
        private string GetPhoto(PhotoInfo ph, ILoggerFactory loggerFactory)
        {
            try
            {
                var url = "/artemis/api/resource/v1/person/picture";
                var content = JsonConvert.SerializeObject(new
                {
                    picUri = ph.PicUri,
                    serverIndexCode = ph.ServerIndexCode
                });
                using (var client = new HKVisionWebClient())
                {
                    var dics = new Dictionary<string, string>();
                    dics.Add("Content-Type", "application/json;charset=utf-8;");
                    if (!string.IsNullOrEmpty(content)) dics.Add("Content-MD5", Convert.ToBase64String(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(content))));
                    dics.Add("X-Ca-Key", AppKey);
                    dics.Add("X-Ca-Timestamp", GetMilliSeconds(DateTime.Now).ToString());
                    foreach (var item in dics)
                    {
                        client.Headers.Add(item.Key, item.Value);
                    }
                    client.Headers.Add("X-Ca-Signature-Headers", "x-ca-key,x-ca-timestamp");
                    client.Headers.Add("X-Ca-Signature", Sign("POST", dics, url));
                    url = BaseUrl.TrimEnd('/') + url;
                    var retBytes = client.UploadData(url, "POST", System.Text.ASCIIEncoding.UTF8.GetBytes(content));
                    if (client.ResponseHeaders != null && client.ResponseHeaders.AllKeys != null)
                    {
                        var location = client.ResponseHeaders["Location"];
                        return location;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return null;
            }
        }

        public QueryResultList<CardEvent> GetCardEvents(DateTime begin, DateTime end, ILoggerFactory loggerFactory)
        {
            if (_Depts == null)
            {
                var ds = GetDepts(loggerFactory).QueryObjects;
                if (ds != null && ds.Count > 0)
                {
                    _Depts = new Dictionary<string, HKVisionDept>();
                    foreach (var d in ds)
                    {
                        if (!_Depts.ContainsKey(d.ID)) _Depts.Add(d.ID, d);
                    }
                }
            }
            var url = "/artemis/api/acs/v1/door/events";
            var content = new
            {
                pageNo = 1,
                pageSize = 1000,
                startTime = begin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzzz"),
                endTime = end.ToString("yyyy-MM-ddTHH:mm:ss.fffzzzz"),
            };
            var ret = PostRequest<HKVisionCardEvent>(url, JsonConvert.SerializeObject(content), loggerFactory);
            if (ret.Code == 0 && ret.Data != null && ret.Data.List != null && ret.Data.List.Count > 0)
            {
                List<CardEvent> events = null;
                events = ret.Data.List.Where(it => IsPermitted(it.EventType)).Select(it => new CardEvent()
                {
                    ID = it.EventUuid,
                    UserID = it.PersonId,
                    UserName = it.PersonName,
                    Department = (_Depts != null & _Depts.ContainsKey(it.DeptName)) ? _Depts[it.DeptName].Name : it.DeptName,
                    CardID = it.CardNo,
                    DoorID = it.DoorID,
                    DoorName = it.DoorName,
                    EventType = it.EventType,
                    EventTime = it.EventTime,
                    Permitted = IsPermitted(it.EventType),
                    Photo = it.PicUrl,
                }).ToList();
                return new QueryResultList<CardEvent>(ResultCode.Successful, ret.Msg, events);
            }
            return new QueryResultList<CardEvent>(ResultCode.Fail, ret.Msg, null);
        }
        #endregion
    }
}
