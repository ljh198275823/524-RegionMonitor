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
    public partial class UCDepartment : UserControl
    {
        public UCDepartment()
        {
            InitializeComponent();
        }

        private readonly int MINITEMWIDTH = 215; //最小宽度

        #region 公共方法
        /// <summary>
        /// 确定显示之前各子控件的尺寸
        /// </summary>
        /// <param name="person"></param>
        /// <param name="maxWidth"></param>
        /// <returns></returns>
        public void Prerender()
        {
            var dept = this.Tag as InregionDept;
            var ret = this.ucUserListView1.CalSpace((dept != null && dept.InregionPerson != null && dept.InregionPerson.Count > 0) ? dept.InregionPerson.Count : 1, this.Width - MINITEMWIDTH);
            this.ucUserListView1.Width = ret.Width;
            this.Height = ret.Height;
        }

        public void FreshInfo()
        {
            var dept = this.Tag as InregionDept;
            lblTitle.Text = dept.Name;
            lblCount.Text = dept.InregionPerson != null ? dept.InregionPerson.Count.ToString() : "0";
            this.ucUserListView1.ShowInregionPerson(dept.InregionPerson);
        }
        #endregion
    }
}