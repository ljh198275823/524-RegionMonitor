using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    public partial class UCDateTimeInterval : UserControl
    {
        public UCDateTimeInterval()
        {
            InitializeComponent();
        }

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (!ShowTime)
            {
                this.dtStart.CustomFormat = "yyyy-MM-dd";
                this.dtEnd.CustomFormat = "yyyy-MM-dd";
            }
            else
            {
                this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            }
            this.rdToday.Checked = true;
        }
        /// <summary>
        /// 是否显示时间
        /// </summary>
        public bool ShowTime { get; set; }

        /// <summary>
        /// 获取或设置开始时间
        /// </summary>
        public DateTime StartDateTime
        {
            get
            {
                return this.dtStart.Value;
            }
            set
            {
                this.dtStart.Value = value;
            }
        }
        /// <summary>
        /// 获取或设置结束时间
        /// </summary>
        public DateTime EndDateTime
        {
            get
            {
                return this.dtEnd.Value;
            }
            set
            {
                this.dtEnd.Value = value;
            }
        }
        /// <summary>
        /// 选择今天
        /// </summary>
        public void SelectToday()
        {
            rdToday.Checked = true;
        }
        /// <summary>
        /// 选择本月
        /// </summary>
        public void SelectThisMonth()
        {
            rdThisMonth.Checked = true;
        }
        /// <summary>
        /// 选择本季度
        /// </summary>
        public void SelectThisQuarter()
        {
            rdThisQuarter.Checked = true;
        }
        /// <summary>
        /// 选择本年
        /// </summary>
        public void SelectThisYear()
        {
            rdThisYear.Checked = true;
        }
        #endregion

        #region 事件处理程序
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (dtEnd.Value < dtStart.Value)
            {
                dtEnd.Value = dtStart.Value;
            }
            this.rdToday.Checked = false;
            this.rdThisMonth.Checked = false;
            this.rdThisQuarter.Checked = false;
            this.rdThisYear.Checked = false;
        }

        private void rdToday_CheckedChanged(object sender, EventArgs e)
        {
            if (rdToday.Checked)
            {
                this.dtStart.ValueChanged -= new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.ValueChanged -= new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtStart.Value = DateTime.Today;
                this.dtEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
                this.dtStart.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            }
        }

        private void rdThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdThisMonth.Checked )
            {
                this.dtStart.ValueChanged -= new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.ValueChanged -= new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
                this.dtStart.Value = DateTime.Today .AddDays(1 - DateTime.Today.Day);
                this.dtStart.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            }
        }

        private void rdThisQuarter_CheckedChanged(object sender, EventArgs e)
        {
            if (rdThisQuarter.Checked)
            {
                this.dtStart.ValueChanged -= new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.ValueChanged -= new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
                switch (DateTime.Now.Month)
                {
                    case 1:
                    case 2:
                    case 3:
                        dtStart.Value = new DateTime(DateTime.Now.Year, 1, 1);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        dtStart.Value = new DateTime(DateTime.Now.Year, 4, 1);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        dtStart.Value = new DateTime(DateTime.Now.Year, 7, 1);
                        break;
                    case 10:
                    case 11:
                    case 12:
                        dtStart.Value = new DateTime(DateTime.Now.Year, 10, 1);
                        break;
                }
                this.dtStart.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            }
        }

        private void rdThisYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdThisYear.Checked)
            {
                this.dtStart.ValueChanged -= new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.ValueChanged -= new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
                this.dtStart.Value = DateTime.Today .AddDays(1 - DateTime.Today .DayOfYear);
                this.dtStart.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
                this.dtEnd.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            }
        }
        #endregion
    }
}
