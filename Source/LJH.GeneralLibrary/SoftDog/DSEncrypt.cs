using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.SoftDog
{
    /// <summary>
    /// 字符串加解密类,加密一次得到密文,再加密一次得到明文 只能用于ASCII字串的加解密
    /// </summary>
    public class DSEncrypt
    {
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeWord">加密因子</param>
        public DSEncrypt(string codeWord)
        {
            this._CodeWord = codeWord;
            _Matrix = @"vbnm,./QWE`12345TYUIOP{}ASDF67890-=\~!@#$%+|qwert^&*()_yuiop[]asdfghjkl;zxcRGHJKL:ZXCVBNM<>? ";

            int length = _Matrix.Length;
            strCryptMatrix = new string[length];

            strCryptMatrix[0] = _Matrix;

            for (int i = 1; i < length; i++)
            {
                strCryptMatrix[i] = strCryptMatrix[i - 1].Substring(1, length - 1) + strCryptMatrix[i - 1].Substring(0, 1);
            }
        }

        public DSEncrypt()
            : this("ljh198275823")
        {
        }
        #endregion

        #region 私有变量
        private string _Matrix;               //Starting Matrix
        private string _CodeWord;             //CodeWord
        private string[] strCryptMatrix;  //Matrix Array
        #endregion

        #region 公共方法
        /// <summary>
        /// 加密字串
        /// </summary>
        /// <param name="mstext">要加密的明文字串</param>
        /// <returns></returns>
        public string Encrypt(string mstext)
        {
            int i;
            int mp;  //要加密字串中的字符在_MATRIX中的位置
            string EncryptedString = string.Empty;

            for (i = 0; i < mstext.Length; i++)
            {
                mp = _Matrix.IndexOf(mstext.Substring(i, 1), 0);
                mp = (mp == -1 ? _Matrix.Length - 1 : mp);

                foreach (string str in strCryptMatrix)
                {
                    if (str.Substring(mp, 1) == _CodeWord.Substring(i % _CodeWord.Length, 1))
                    {
                        EncryptedString += str.Substring(0, 1);
                        break;
                    }
                }
            }
            return EncryptedString;
        }
        #endregion
    }
}
