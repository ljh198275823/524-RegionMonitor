using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class FrmUserDetail : Form
    {
        #region 静态属性
        private static FrmUserDetail _Instance;
        public static FrmUserDetail GetInstance()
        {
            if (_Instance == null) _Instance = new FrmUserDetail();
            return _Instance;
        }
        #endregion

        #region 构造函数
        public FrmUserDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        public int _Seconds = 0;
        #endregion

        #region 公共方法
        public void ShowUserDetail(InRegionPerson p)
        {
            this.ucUserDetail1.ShowPeople(p);
            _Seconds = 30;
        }
        #endregion

        #region 事件处理
        private void FrmUserDetail_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_Seconds > 0)
            {
                this.Show();
                this.Activate();
                _Seconds--;
            }
            else
            {
                this.Hide();
            }
        }

        private void FrmUserDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                _Seconds = 0;
                this.Hide();
                e.Cancel = true;
            }
        }

        private void FrmUserDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            _Instance = null;
        }
        #endregion
    }
}
