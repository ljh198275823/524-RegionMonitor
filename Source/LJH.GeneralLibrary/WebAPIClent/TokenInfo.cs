using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LJH.GeneralLibrary.WebAPIClient
{
    internal class TokenInfo
    {
        public static Dictionary<string, TokenInfo> Tokens = new Dictionary<string, TokenInfo>();

        #region 构造函数
        public TokenInfo()
        {
        }
        #endregion

        #region 公共属性
        public string Token { get; set; }

        public DateTime ExpireTime { get; set; }
        #endregion

        public bool NeedNewToken()
        {
            return string.IsNullOrEmpty(Token) || ExpireTime.AddMinutes (-1) < DateTime.Now; //比过期日期提前一分钟就要开始重新获取TOKEN了
        }
    }
}
