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
    public partial class FrmUserView : Form
    {
        public FrmUserView()
        {
            InitializeComponent();
        }

        #region 公共属性
        public List<InRegionPerson> InregionPerson { get; set; }
        #endregion

        private void FrmUserView_Load(object sender, EventArgs e)
        {
            if (InregionPerson != null && InregionPerson.Count > 0)
            {
                foreach (var p in InregionPerson)
                {
                    int row = dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells["colCardID"].Value = p.CardID;
                    dataGridView1.Rows[row].Cells["colUserName"].Value = p.UserName;
                    dataGridView1.Rows[row].Cells["colDepartment"].Value = p.Department;
                    //dataGridView1.Rows[row].Cells["colPhone"].Value = new UserBLL(AppSettings.Current.ConnStr).GetPhone(p.CardID);
                    dataGridView1.Rows[row].Cells["colEnterDateTime"].Value = p.EnterDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                lblMSG.Text = string.Format("共 {0} 项", InregionPerson.Count);
            }
        }
    }
}
