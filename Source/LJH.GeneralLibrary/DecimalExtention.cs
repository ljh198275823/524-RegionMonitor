using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary
{
    public static class DecimalExtention
    {
        /// <summary>
        /// 去掉实数小数点后不必要的0,比如12.500 会变成12.5,12.00会变成12等
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static decimal Trim(this decimal d)
        {
            string temp = d.ToString();
            //去掉数字两头的0，如果是整数，把小数点也去掉
            if (temp.IndexOf('.') > 0)
            {
                temp = temp.TrimEnd('0');
                temp = temp.Trim('.');
            }
            decimal ret = 0;
            decimal.TryParse(temp, out ret);
            return ret;
        }
    }
}
