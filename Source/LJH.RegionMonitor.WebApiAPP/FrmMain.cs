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
        private bool CheckConnect(string conStr)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = conStr;
                    con.Open();
                    con.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void StartWebHost()
        {
            try
            {
                WebHostStarter.StartWebListenerHost(_URL,AppSettings .Current .ConnStr );
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
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion
    }
}
