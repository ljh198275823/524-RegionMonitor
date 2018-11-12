using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace LJH.GeneralLibrary.SoftDog
{
    public class KeyCreator
    {
        private static byte[] mainKey = new byte[] { 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, 0xF0 }; //默认通讯密钥
        /// <summary>
        /// 通过项目编号生成项目密钥
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public static byte[] CreateKey(int projectCode)
        {
            byte[] inputByteArray = SEBinaryConverter.LongToBytes(projectCode);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = mainKey;
                des.IV = mainKey;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.None;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                byte[] ret = ms.ToArray();
                ms.Close();
                return ret;
            }
        }
    }
}
