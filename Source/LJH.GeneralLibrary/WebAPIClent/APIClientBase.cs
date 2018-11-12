using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using LJH.GeneralLibrary.Core;
using Newtonsoft.Json;

namespace LJH.GeneralLibrary.WebAPIClient
{
    public class APIClientBase<TID, TEntity> where TEntity : class, IEntity<TID>
    {
        #region 构造函数
        public APIClientBase(string repoUri)
        {
            RepoUri = repoUri;
        }
        #endregion

        #region  公共属性
        /// <summary>
        /// 获取或设置服务器URL
        /// </summary>
        public string RepoUri { get; set; }
        /// <summary>
        /// 获取或设置服务器用户名
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 获取或设置用户名密码
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region 私有方法
        private string GetQueryString(SearchCondition con)
        {
            var str = JsonConvert.SerializeObject(new
            {
                SearchType = con.GetType().Name,
                Search = JsonConvert.SerializeObject(con)
            });
            return GZipHelper.CompressString(str);
        }

        protected virtual string ToQueryString(SearchCondition con)
        {
            if (con != null) return string.Format("q={0}", HttpUtility.UrlEncode(GetQueryString(con), ASCIIEncoding.UTF8));
            return null;
        }

        protected virtual string GetControllerUrl()
        {
            return RepoUri.TrimEnd('/') + "/" + typeof(TEntity).Name + "s/";
        }

        private int GetTimestamp(DateTime dt)
        {
            DateTime temp = new DateTime(1970, 1, 1);
            long a = (dt.Ticks - temp.Ticks) / 10000000 - 8 * 60 * 60;
            return (int)a;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取TOKEN
        /// </summary>
        /// <returns></returns>
        public virtual bool GetToken()
        {
            if (string.IsNullOrEmpty(UserID) && string.IsNullOrEmpty(Password)) throw new Exception("没有提供用户名和密码，不能获取Token ");
            using (var client = new GZipWebClient())
            {
                string url = RepoUri.TrimEnd('/') + "/" + "tokens/";
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("accept", "application/json;charset=utf-8");
                client.Headers.Add("content-type", "application/json;charset=utf-8;");
                var ts = GetTimestamp(DateTime.Now);
                var content = JsonConvert.SerializeObject(new
                {
                    userid = UserID,
                    password = Password,
                    timestamp = ts,
                });
                var retBytes = client.UploadData(url, "POST", System.Text.ASCIIEncoding.UTF8.GetBytes(content));
                var ret = JsonConvert.DeserializeObject<CommandResult<TokenInfo>>(System.Text.ASCIIEncoding.UTF8.GetString(retBytes));
                if (ret.Result == ResultCode.Successful && ret.Value != null)
                {
                    TokenInfo.Tokens[RepoUri] = ret.Value;
                    return true;
                }
                throw new Exception("获取token失败 ");
            }
        }
        /// <summary>
        /// 通过ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual QueryResult<TEntity> GetByID(TID id, bool anonymous = false)
        {
            try
            {
                using (var client = new GZipWebClient())
                {
                    if (!anonymous)
                    {
                        if (TokenInfo.Tokens.ContainsKey(RepoUri) == false || TokenInfo.Tokens[RepoUri].NeedNewToken()) GetToken();
                        client.Headers.Add("Authorization", string.Format("{0} {1}", "Bearer", TokenInfo.Tokens[RepoUri].Token));
                    }
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    string url = GetControllerUrl() + HttpUtility.UrlEncode(string.Format("{0}", id), ASCIIEncoding.UTF8) + "/";
                    var retBytes = client.DownloadData(url);
                    var ret = JsonConvert.DeserializeObject<QueryResult<TEntity>>(ASCIIEncoding.UTF8.GetString(retBytes));
                    return ret;
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    var response = wex.Response as System.Net.HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized) TokenInfo.Tokens[RepoUri] = null;  //如果是未授权，则清掉当前Token
                }
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new QueryResult<TEntity>(ResultCode.Fail, ex.Message, null);
            }
        }
        /// <summary>
        /// 通过查询条件获取指定的实体信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual QueryResultList<TEntity> GetItems(SearchCondition con, bool anonymous = false)
        {
            try
            {
                using (var client = new GZipWebClient())
                {
                    if (!anonymous)
                    {
                        if (TokenInfo.Tokens.ContainsKey(RepoUri) == false || TokenInfo.Tokens[RepoUri].NeedNewToken()) GetToken();
                        client.Headers.Add("Authorization", string.Format("{0} {1}", "Bearer", TokenInfo.Tokens[RepoUri].Token));
                    }
                    string url = GetControllerUrl();
                    if (con != null) url += "?" + ToQueryString(con);
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    client.Headers.Add(System.Net.HttpRequestHeader.AcceptEncoding, "gzip");
                    var retBytes = client.DownloadData(url);
                    var ret = JsonConvert.DeserializeObject<QueryResultList<TEntity>>(ASCIIEncoding.UTF8.GetString(retBytes));
                    return ret;
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    var response = wex.Response as System.Net.HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized) TokenInfo.Tokens[RepoUri] = null;  //如果是未授权，则清掉当前Token
                }
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new QueryResultList<TEntity>(ResultCode.Fail, ex.Message, new List<TEntity>());
            }
        }
        /// <summary>
        /// 根据指定的路径获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public QueryResultList<T> GetListBySubPath<T>(string path, SearchCondition con, bool anonymous = false) where T : class
        {
            try
            {
                using (var client = new GZipWebClient())
                {
                    if (!anonymous)
                    {
                        if (TokenInfo.Tokens.ContainsKey(RepoUri) == false || TokenInfo.Tokens[RepoUri].NeedNewToken()) GetToken();
                        client.Headers.Add("Authorization", string.Format("{0} {1}", "Bearer", TokenInfo.Tokens[RepoUri].Token));
                    }
                    string url = GetControllerUrl() + path.TrimStart('/');
                    if (con != null) url += "?" + ToQueryString(con);
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    client.Headers.Add(System.Net.HttpRequestHeader.AcceptEncoding, "gzip");
                    var retBytes = client.DownloadData(url);
                    var ret = JsonConvert.DeserializeObject<QueryResultList<T>>(ASCIIEncoding.UTF8.GetString(retBytes));
                    return ret;
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    var response = wex.Response as System.Net.HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized) TokenInfo.Tokens[RepoUri] = null;  //如果是未授权，则清掉当前Token
                }
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new QueryResultList<T>(ResultCode.Fail, ex.Message, new List<T>());
            }
        }
        /// <summary>
        /// 根据指定的URL获取数据,这里的URL是完整的URL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T GetListByUrl<T>(string url, bool anonymous = false) where T : class
        {
            try
            {
                using (var client = new GZipWebClient())
                {
                    if (!anonymous)
                    {
                        if (TokenInfo.Tokens.ContainsKey(RepoUri) == false || TokenInfo.Tokens[RepoUri].NeedNewToken()) GetToken();
                        client.Headers.Add("Authorization", string.Format("{0} {1}", "Bearer", TokenInfo.Tokens[RepoUri].Token));
                    }
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    client.Headers.Add(System.Net.HttpRequestHeader.AcceptEncoding, "gzip");
                    var retBytes = client.DownloadData(url);
                    var ret = JsonConvert.DeserializeObject<T>(ASCIIEncoding.UTF8.GetString(retBytes));
                    return ret;
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    var response = wex.Response as System.Net.HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized) TokenInfo.Tokens[RepoUri] = null;  //如果是未授权，则清掉当前Token
                }
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return null;
            }
        }
        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual CommandResult<TEntity> Add(TEntity info, bool anonymous = false)
        {
            try
            {
                using (var client = new GZipWebClient())
                {
                    if (!anonymous)
                    {
                        if (TokenInfo.Tokens.ContainsKey(RepoUri) == false || TokenInfo.Tokens[RepoUri].NeedNewToken()) GetToken();
                        client.Headers.Add("Authorization", string.Format("{0} {1}", "Bearer", TokenInfo.Tokens[RepoUri].Token));
                    }
                    string url = GetControllerUrl();
                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    client.Headers.Add("content-type", "application/json;charset=utf-8;");
                    var content = JsonConvert.SerializeObject(info);
                    var retBytes = client.UploadData(url, "POST", System.Text.ASCIIEncoding.UTF8.GetBytes(content));
                    var ret = JsonConvert.DeserializeObject<CommandResult<TEntity>>(System.Text.ASCIIEncoding.UTF8.GetString(retBytes));
                    if (ret.Result == ResultCode.Successful && ret.Value != null) info.ID = ret.Value.ID;
                    return ret;
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    var response = wex.Response as System.Net.HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized) TokenInfo.Tokens[RepoUri] = null;  //如果是未授权，则清掉当前Token
                }
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult<TEntity>(ResultCode.Fail, ex.Message, null);
            }
        }
        /// <summary>
        /// 更新更新实体
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual CommandResult<TEntity> Update(TEntity info, bool anonymous = false)
        {
            try
            {
                using (var client = new GZipWebClient())
                {
                    if (!anonymous)
                    {
                        if (TokenInfo.Tokens.ContainsKey(RepoUri) == false || TokenInfo.Tokens[RepoUri].NeedNewToken()) GetToken();
                        client.Headers.Add("Authorization", string.Format("{0} {1}", "Bearer", TokenInfo.Tokens[RepoUri].Token));
                    }
                    string url = GetControllerUrl() + HttpUtility.UrlEncode(string.Format("{0}", info.ID), ASCIIEncoding.UTF8) + "/";
                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    client.Headers.Add("content-type", "application/json;charset=utf-8");
                    var content = JsonConvert.SerializeObject(info);
                    var retBytes = client.UploadData(url, "PUT", System.Text.ASCIIEncoding.UTF8.GetBytes(content));
                    return JsonConvert.DeserializeObject<CommandResult<TEntity>>(System.Text.ASCIIEncoding.UTF8.GetString(retBytes));
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    var response = wex.Response as System.Net.HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized) TokenInfo.Tokens[RepoUri] = null;  //如果是未授权，则清掉当前Token
                }
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult<TEntity>(ResultCode.Fail, ex.Message, null);
            }
        }
        /// <summary>
        /// Patch数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public virtual CommandResult<TEntity> Patch(TID id, Dictionary<string, string> items, bool anonymous = false)
        {
            try
            {
                using (var client = new GZipWebClient())
                {
                    if (!anonymous)
                    {
                        if (TokenInfo.Tokens.ContainsKey(RepoUri) == false || TokenInfo.Tokens[RepoUri].NeedNewToken()) GetToken();
                        client.Headers.Add("Authorization", string.Format("{0} {1}", "Bearer", TokenInfo.Tokens[RepoUri].Token));
                    }
                    string url = GetControllerUrl() + HttpUtility.UrlEncode(string.Format("{0}", id), ASCIIEncoding.UTF8) + "/";
                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    client.Headers.Add("content-type", "application/json;charset=utf-8;");
                    var content = JsonConvert.SerializeObject(items);
                    var retBytes = client.UploadData(url, "PATCH", System.Text.ASCIIEncoding.UTF8.GetBytes(content));
                    return JsonConvert.DeserializeObject<CommandResult<TEntity>>(System.Text.ASCIIEncoding.UTF8.GetString(retBytes));
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    var response = wex.Response as System.Net.HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized) TokenInfo.Tokens[RepoUri] = null;  //如果是未授权，则清掉当前Token
                }
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult<TEntity>(ResultCode.Fail, ex.Message, null);
            }
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual CommandResult Delete(TEntity info, bool anonymous = false)
        {
            try
            {
                using (var client = new GZipWebClient())
                {
                    if (!anonymous)
                    {
                        if (TokenInfo.Tokens.ContainsKey(RepoUri) == false || TokenInfo.Tokens[RepoUri].NeedNewToken()) GetToken();
                        client.Headers.Add("Authorization", string.Format("{0} {1}", "Bearer", TokenInfo.Tokens[RepoUri].Token));
                    }
                    string url = GetControllerUrl() + HttpUtility.UrlEncode(string.Format("{0}", info.ID), ASCIIEncoding.UTF8) + "/";
                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers.Add("accept", "application/json;charset=utf-8");
                    client.Headers.Add("content-type", "application/json;charset=utf-8;");
                    var retBytes = client.UploadString(url, "DELETE", string.Empty);
                    if (!string.IsNullOrEmpty(retBytes)) return JsonConvert.DeserializeObject<CommandResult>(retBytes);
                    return new CommandResult(ResultCode.Successful, string.Empty);
                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var wex = ex as WebException;
                    var response = wex.Response as System.Net.HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.Unauthorized) TokenInfo.Tokens[RepoUri] = null;  //如果是未授权，则清掉当前Token
                }
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 获取表单多字段提交的内容
        /// </summary>
        /// <param name="postParameters"></param>
        /// <param name="boundary"></param>
        /// <returns></returns>
        public byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            var encoding = Encoding.UTF8;
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                if (needsCLRF) formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));
                needsCLRF = true;
                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;
                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }
            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }
        #endregion
    }
}
