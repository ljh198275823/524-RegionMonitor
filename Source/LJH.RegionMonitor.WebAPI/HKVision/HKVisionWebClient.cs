using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LJH.OneCard.HKVisionClient
{
    public class HKVisionWebClient : WebClient
    {
        #region 构造函数
        public HKVisionWebClient()
            : base()
        {
        }
        #endregion

        #region 重写基类方法
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AllowAutoRedirect = false;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }
        #endregion
    }
}
