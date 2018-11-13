using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Windows.Forms;
using LJH.GeneralLibrary;
using LJH.GeneralLibrary.SoftDog;
using LJH.RegionMonitor.Model;
using LJH.RegionMonitor.WebAPIClient;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class FrmMonitor : Form
    {
        public FrmMonitor()
        {
            InitializeComponent();
        }

        #region 私有变量

        private MonitorRegion _CurrentRegion = null;
        private Thread _ReadCardEventThread = null;

        private bool _First = false;
        private CardEvent _LastEvent = null;
        private DateTime _LastDateTime = DateTime.Now.AddDays(-3);
        private int _Days = -3; //从某个时间点的刷卡记录开始算起,一般来说人员不会在区域里面呆超过三天
        #endregion

        #region 私有方法
        private void InitCurrentRegion()
        {
            var region = new RegionClient(AppSettings.Current.ConnStr).GetByID(1, false).QueryObject;
            if (region == null) //获取失败再根据设置的当前区域从数据库获取
            {
                lblRegion.Text = "没有设置当前区域";
                lblRegion.ForeColor = Color.Red;
                return;
            }
            _CurrentRegion = new MonitorRegion(region);
            lblRegion.Text = string.Format("{0} 在场人数", _CurrentRegion.Name);
            lblRegion.ForeColor = Color.Black;
            _First = true;


            if (_ReadCardEventThread == null)
            {
                _ReadCardEventThread = new Thread(new ThreadStart(FreshRegion_Thread));
                _ReadCardEventThread.IsBackground = true;
                _ReadCardEventThread.Start();
            }
            tmrGetEvents.Enabled = _CurrentRegion != null;
            tmrTimeout.Enabled = _CurrentRegion != null;
        }

        private void FreshRegion_Thread()
        {
            //while (true)
            //{
            //    Thread.Sleep(1000);
            //    if (_CurrentRegion == null) continue;
            //    List<CardEvent> events = new CardEventBLL(AppSettings.Current.ConnStr).GetEvents(_LastLogID, DateTime.Now.AddDays(_Days));
            //    if (events != null && events.Count > 0)
            //    {
            //        foreach (var item in events)
            //        {
            //            _CurrentRegion.HandleCardEvent(item);
            //        }
            //        if (!_First) _LastEvent = events.LastOrDefault(it => it.InorOut == 1 || it.InorOut == 2);   // events.LastOrDefault(it => _CurrentRegion.IsMyDoor(it.DoorID));
            //        _LastLogID = events.Max(it => it.ID) + 1;
            //    }
            //    _First = false;
            //}
        }
        #endregion

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblCurTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            InitCurrentRegion();

            string temp = AppSettings.Current.GetConfigContent("pTimeoutHeight");
            int pTimeoutHeight = 0;
            if (!int.TryParse(temp, out pTimeoutHeight)) pTimeoutHeight = 120;
            ucTimeout.Height = pTimeoutHeight;
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            this.toolStrip1.Visible = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnOver48_Click(object sender, EventArgs e)
        {
            FrmUserView frm = new FrmUserView();
            frm.Text = "超48小时未出场人员";
            List<InRegionPerson> over48 = _CurrentRegion.InregionUsers.Where(it => it.IsOver48).ToList();
            frm.InregionPerson = over48;
            frm.ShowDialog();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.toolStrip1.Visible = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void ucTimeout_Resize(object sender, EventArgs e)
        {
            AppSettings.Current.SaveConfig("pTimeoutHeight", ucTimeout.Height.ToString());
        }

        private void tmrGetEvents_Tick(object sender, EventArgs e)
        {
            lblCurTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (_CurrentRegion != null && _CurrentRegion.InregionUsersChanged)
            {
                List<InRegionPerson> users = _CurrentRegion.InregionUsers;
                this.lblInregionCount.Text = (users == null || users.Count == 0) ? "0" : users.Count(it => it.IsInRegion || it.IsTimeout).ToString();
                List<InRegionPerson> person = _CurrentRegion.InregionUsers;

                List<InregionDept> depts = null;
                if (person != null && person.Count > 0)
                {
                    depts = new List<InregionDept>();
                    var temp = (from item in person
                                where item.IsInRegion
                                orderby item.Department ascending
                                group item by item.Department);
                    foreach (var g in temp)
                    {
                        depts.Add(new InregionDept(g.Key, g.ToList()));
                    }
                }
                ucDeptView.ShowInregionDept(depts);
            }
        }

        private void tmrTimeout_Tick(object sender, EventArgs e)
        {
            List<InRegionPerson> timeout = _CurrentRegion.InregionUsers.Where(it => it.IsTimeout).ToList();
            InregionDept d = new InregionDept("超时未出人员", timeout);
            List<InregionDept> ds = new List<InregionDept>();
            ds.Add(d);
            ucTimeout.ShowInregionDept(ds);
        }

        private void FrmMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_ReadCardEventThread != null)
            {
                _ReadCardEventThread.Abort();
                _ReadCardEventThread = null;
            }
        }
        #endregion
    }
}
