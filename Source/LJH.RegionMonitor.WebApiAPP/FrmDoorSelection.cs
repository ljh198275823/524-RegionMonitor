using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.RegionMonitor.WebAPIClient;
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class FrmDoorSelection : Form
    {
        public FrmDoorSelection()
        {
            InitializeComponent();
        }

        #region 公共方法
        private List<Door> _SelectedDoors;
        public List<Door> SelectedDoors
        {
            get
            {
                List<Door> ret = null;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["colChk"].FormattedValue))
                    {
                        if (ret == null) ret = new List<Door>();
                        ret.Add(row.Tag as Door);
                    }
                }
                return ret;
            }
            set
            {
                _SelectedDoors = value;
            }
        }
        #endregion

        #region 事件处理
        private void FrmDoorSelection_Load(object sender, EventArgs e)
        {
            List<Door> doors = new DoorClient(AppSettings.Current.ConnStr).GetItems(null, true).QueryObjects;
            if (doors != null && doors.Count > 0)
            {
                doors = (from item in doors
                         orderby item.Name ascending
                         select item).ToList();
                foreach (var door in doors)
                {
                    int row = dataGridView1.Rows.Add();
                    ShowItemInGridViewRow(dataGridView1.Rows[row], door);
                }
            }
        }

        private void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            Door ct = item as Door;
            row.Tag = ct;
            row.Cells["colChk"].Value = _SelectedDoors != null && _SelectedDoors.Exists(it => it.ID == ct.ID);
            row.Cells["colID"].Value = ct.ID;
            row.Cells["colName"].Value = ct.Name;
        }


        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["colChk"].Value = true;
            }
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["colChk"].Value = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (SelectedDoors == null || SelectedDoors.Count == 0)
            {
                MessageBox.Show("请选择至少一个门禁点");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
