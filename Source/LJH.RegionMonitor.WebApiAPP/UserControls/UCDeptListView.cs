using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class UCDeptListView : UserControl
    {
        public UCDeptListView()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置列间距
        /// </summary>
        public int ColSpliter { get; set; }
        #endregion

        #region 私有方法
        private void PerformLayoutItems()
        {
            if (this.VerticalScroll != null && this.VerticalScroll.Visible) this.VerticalScroll.Value = 0;
            Point p = new Point(ColSpliter, ColSpliter);
            this.SuspendLayout();
            foreach (var c in this.Controls)
            {
                UCDepartment ucd = c as UCDepartment;
                ucd.BorderStyle = BorderStyle.FixedSingle;
                ucd.Location = p;
                ucd.Width = this.Width - ColSpliter * 2 - 15; //这里减15是用于当有滚动栏时不产生水平方向的滚动栏
                ucd.Prerender();
                ucd.Visible = true;
                ucd.FreshInfo();
                p = new Point(ColSpliter, ucd.Top + ucd.Height + ColSpliter);
            }
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        #region 公共方法
        public void ShowInregionDept(List<InregionDept> depts)
        {
            int pCount = depts != null ? depts.Count : 0;
            int ucdCount = this.Controls.Count;

            for (int i = pCount; i < ucdCount; i++) //多余的控件删除掉
            {
                this.Controls.RemoveAt(pCount);
            }

            for (int i = ucdCount; i < pCount; i++)
            {
                UCDepartment ucd = new UCDepartment();
                ucd.Visible = false;
                this.Controls.Add(ucd);
            }

            if (depts != null && depts.Count > 0)
            {
                for (int i = 0; i < depts.Count; i++)
                {
                    UCDepartment ucd = this.Controls[i] as UCDepartment;
                    ucd.Tag = depts[i];
                }
                PerformLayoutItems();
            }
        }
        #endregion

        #region 重写基类方法
        protected override void OnSizeChanged(EventArgs e)
        {
            PerformLayoutItems();
            base.OnSizeChanged(e);
        }
        #endregion
    }
}
