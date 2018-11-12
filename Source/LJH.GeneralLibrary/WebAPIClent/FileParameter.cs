using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.WebAPIClient
{
    public class FileParameter
    {
        #region 构造函数
        public FileParameter(byte[] file, string name, string filename, string contenttype)
        {
            Name = name;
            File = file;
            FileName = filename;
            ContentType = contenttype;
        }
        #endregion

        #region 公共属性
        public string Name { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        #endregion
    }
}
