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
        #endregion

        #region 私有方法
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
    }
}
