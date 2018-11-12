using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.SoftDog
{
    public class DTEncrypt
    {

        private string _Spliter = "&&";
        /// <summary>
        /// 加密字串
        /// </summary>
        /// <returns></returns>
        public string Encrypt(string str)
        {
            string key = Guid.NewGuid().ToString();
            string temp = (new DSEncrypt(key)).Encrypt(str);
            temp = (new DSEncrypt()).Encrypt(key + _Spliter + temp);
            return temp;
        }

        /// <summary>
        /// 从字密明字串中解密获取明文
        /// </summary>
        /// <param name="codeWord">加密因子</param>
        public string DSEncrypt(string str)
        {
            string temp = (new DSEncrypt()).Encrypt(str);
            int p = temp.IndexOf(_Spliter);
            if (p > 0)
            {
                string key = temp.Substring(0, p);
                temp = (new DSEncrypt(key)).Encrypt(temp.Substring(p + _Spliter.Length));
                return temp;
            }
            return str;
        }
    }
}
