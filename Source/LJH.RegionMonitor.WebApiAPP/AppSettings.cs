using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using LJH.GeneralLibrary.SoftDog;

namespace LJH.RegionMonitor.WebApiAPP
{

    public class AppSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public static AppSettings Current
        {
            get
            {
                if (_instance == null)
                    _instance = new AppSettings(Path.Combine(Application.StartupPath, "config.xml"));
                return _instance;
            }
        }

        #region 私有变量
        private static AppSettings _instance = null;
        private XmlDocument _doc = null;
        private XmlNode _parent = null;
        private string _path;

        private string _ConnectString;
        private bool _DatabaseNeedUpgrade;
        private bool _RememberLogID;
        #endregion

        #region 构造函数
        public AppSettings(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    _path = path;
                    this._doc = new XmlDocument();
                    this._doc.Load(_path);
                    _parent = this._doc.SelectSingleNode("configuration/appSettings");

                    _ConnectString = GetConfigContent("ConnectString");

                    string temp;
                    temp = GetConfigContent("DatabaseNeedUpgrade");
                    bool.TryParse(temp, out _DatabaseNeedUpgrade);

                    temp = GetConfigContent("RememberLogID");
                    bool.TryParse(temp, out _RememberLogID);
                }
                catch
                {
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 停车场连接字串
        /// </summary>
        public string ConnStr
        {
            //连接字串分两段加密，首先前8个字符为加密的日期，做为实际连接字符串信息的加密密码。
            //解密连接字串：先用默认加密密码的加密类型解密出前8个字符的明文，再用一个密码为此明文的加密类解密出后续字符，得到连接字符的明文。
            get
            {
                string con = string.Empty;
                return (new DTEncrypt()).DSEncrypt(_ConnectString);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _ConnectString = (new DTEncrypt()).Encrypt(value);
                    SaveConfig("ConnectString", _ConnectString);
                }
                else
                {
                    SaveConfig("ConnectString", string.Empty);
                }
            }
        }

        public bool Debug { get; set; }

        /// <summary>
        /// 获取或设置登录时是否记录登录名
        /// </summary>
        public bool RememberLogID
        {
            get { return _RememberLogID; }
            set
            {
                if (_RememberLogID != value)
                {
                    _RememberLogID = value;
                    SaveConfig("RememberLogID", _RememberLogID.ToString());
                }
            }
        }

        public bool SaveConfig(string configName, string configContent)
        {
            if (_parent != null)
            {
                try
                {
                    XmlElement add = null;
                    XmlAttribute key = null;
                    XmlAttribute value = null;
                    XmlNodeList nodeList = _parent.ChildNodes;
                    foreach (XmlNode xn in nodeList)
                    {
                        if (xn is XmlElement)
                        {
                            XmlElement xe = (XmlElement)xn;
                            if (xe.GetAttribute("key") == configName)
                            {
                                xe.SetAttribute("value", configContent);
                                add = xe;
                                break;
                            }
                        } // end if
                    }
                    if (add == null)
                    {
                        add = _doc.CreateElement("add");
                        key = _doc.CreateAttribute("key");
                        key.Value = configName;
                        value = _doc.CreateAttribute("value");
                        value.Value = configContent;

                        add.Attributes.Append(key);
                        add.Attributes.Append(value);
                        _parent.AppendChild(add);
                    }
                    this._doc.Save(_path.ToString());
                    return true;
                }
                catch (Exception ex)
                {
                    LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
            return false;
        }

        public string GetConfigContent(string configName)
        {
            if (_parent != null)
            {
                try
                {
                    XmlNodeList nodeList = _parent.ChildNodes;
                    foreach (XmlNode xn in nodeList)
                    {
                        if (xn is XmlElement)
                        {
                            XmlElement xe = (XmlElement)xn;
                            if (xe.GetAttribute("key") == configName)
                            {
                                return xe.GetAttribute("value");
                            }
                        } // end if
                    }
                }
                catch (Exception ex)
                {
                    LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
            return "";
        }
        #endregion
    }
}
