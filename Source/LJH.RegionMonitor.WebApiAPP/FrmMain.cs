using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using LJH.RegionMonitor.WebAPI;
using LJH.GeneralLibrary.SoftDog;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region 私有变量
        private string _URL = @"http://+:13002/rm/";
        private SoftDogInfo _SoftDog;
        private DateTime _ExpireDate = new DateTime(2020, 12, 31);
        #endregion

        #region 私有方法
        //private void ReadDog()
        //{
        //    string lic = Path.Combine(Application.StartupPath, "ljh.lic");
        //    string skey = AppSettings.Current.GetConfigContent("SKey");
        //    if (string.IsNullOrEmpty(skey))
        //    {
        //        skey = @"#i~xnUc4RH1G@\)$&7z6qv9xy@~<mTR5nUR?OU}jh`4r<qN>:*xZwz~E$0";
        //        AppSettings.Current.SaveConfig("SKey", skey);
        //    }
        //    try
        //    {
        //        SoftDogReader reader = new SoftDogReader(skey);
        //        _SoftDog = reader.ReadDog();
        //    }
        //    catch
        //    {
        //    }
        //    try
        //    {
        //        if (_SoftDog == null && File.Exists(lic))
        //        {
        //            _SoftDog = LICReader.ReadDog(lic);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        //private void CheckDog()
        //{
        //    ReadDog();
        //    if (_SoftDog == null)
        //    {
        //        FrmContactUs frm = new FrmContactUs();
        //        frm.txtMessage.Text = "没有找到加密狗";
        //        frm.ShowDialog();
        //        System.Environment.Exit(0);
        //    }
        //    else if ((_SoftDog.SoftwareList & SoftwareType.TYPE_ACS) == 0)  //没有开放进销存软件权限
        //    {
        //        FrmContactUs frm = new FrmContactUs();
        //        frm.txtMessage.Text = "加密狗权限不足";
        //        frm.ShowDialog();
        //        System.Environment.Exit(0);
        //    }
        //    else if (_SoftDog.ExpiredDate < DateTime.Today && _SoftDog.ExpiredDate.AddDays(15) >= DateTime.Today) //已经过期
        //    {
        //        DateTime expire = _SoftDog.ExpiredDate.AddDays(15);
        //        TimeSpan ts = new TimeSpan(expire.Ticks - DateTime.Today.Ticks);
        //        MessageBox.Show(string.Format("软件已经过期，还可以再试用 {0} 天，请尽快与供应商联系延长您的软件使用期!", (int)(ts.TotalDays + 1)), "注意");
        //    }
        //    else if (_SoftDog.ExpiredDate.AddDays(15) < DateTime.Today)
        //    {
        //        FrmContactUs frm = new FrmContactUs();
        //        frm.txtMessage.Text = "软件已经过期";
        //        frm.ShowDialog();
        //        System.Environment.Exit(0);
        //    }
        //    else if (!string.IsNullOrEmpty(_SoftDog.MAC))
        //    {
        //        string[] auMac = _SoftDog.MAC.Split(',', '，');
        //        string local = LJH.GeneralLibrary.Net.NetTool.GetLocalMAC();
        //        if (string.IsNullOrEmpty(local))
        //        {
        //            MessageBox.Show("软件不允许在此电脑上使用!", "注意");
        //            System.Environment.Exit(0);
        //        }
        //        var locMac = local.Split(',', '，');
        //        if (locMac.Any(it => !string.IsNullOrEmpty(it) && auMac.Any(f => !string.IsNullOrEmpty(f) && it.Replace("-", string.Empty).ToUpper() == f.Replace("-", string.Empty).ToUpper())))
        //        {
        //        }
        //        else
        //        {
        //            MessageBox.Show("软件不允许在此电脑上使用!", "注意");
        //            System.Environment.Exit(0);
        //        }
        //    }
        //}

        private void StartWebHost()
        {
            try
            {
                WebHostStarter.StartWebListenerHost(_URL, AppSettings.Current.ConnStr);
                lblWebhostState.Text = "WebApi服务启动";
                lblWebhostState.ForeColor = Color.Blue;
            }
            catch (Exception)
            {
                lblWebhostState.Text = "WebApi服务异常";
                lblWebhostState.ForeColor = Color.Red;
            }
        }
        #endregion

        #region 事件处理程序 
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text += string.Format(" [{0}]", Application.ProductVersion);
            lblUrl.Text = _URL;
            StartWebHost();
            AppSettings.Current.ConnStr = @"http://localhost:13002/rm/api";
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion

        private void btnRegion_Click(object sender, EventArgs e)
        {
            FrmMonitorSetting frm = new FrmMonitorSetting();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btnCardEvent_Click(object sender, EventArgs e)
        {
            var frm = new FrmFullLogReport();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btn区域进出实时统计_Click(object sender, EventArgs e)
        {
            var frm = new FrmMonitor();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
