using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.RegionMonitor.WebAPIClient;
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class FrmMonitorSetting : Form
    {
        public FrmMonitorSetting()
        {
            InitializeComponent();
        }

        public Region MonitorRegion { get; set; }

        #region 私有方法
        private void ShowRegion(Region region)
        {
            txtName.Text = region.Name;
            chk不显示超时未出人员.Checked = region.HideTimeOutPerson;
            var doors = new DoorClient(AppSettings.Current.ConnStr).GetItems(null, true).QueryObjects;
            if (doors == null || doors.Count == 0) return;
            viewEntrance.Rows.Clear();
            if (region.EnterDoors != null && region.EnterDoors.Count > 0)
            {
                foreach (var doorID in region.EnterDoors)
                {
                    var door = doors.SingleOrDefault(it => it.ID == doorID);
                    if (door != null)
                    {
                        var row = viewEntrance.Rows.Add();
                        viewEntrance.Rows[row].Tag = door;
                        viewEntrance.Rows[row].Cells["colIDEnter"].Value = door.ID;
                        viewEntrance.Rows[row].Cells["colNameEnter"].Value = door.Name;
                    }
                }
            }
            viewExit.Rows.Clear();
            if (region.ExitDoors != null && region.ExitDoors.Count > 0)
            {
                foreach (var doorID in region.ExitDoors)
                {
                    var door = doors.SingleOrDefault(it => it.ID == doorID);
                    if (door != null)
                    {
                        var row = viewExit.Rows.Add();
                        viewExit.Rows[row].Tag = door;
                        viewExit.Rows[row].Cells["colIDExit"].Value = door.ID;
                        viewExit.Rows[row].Cells["colNameExit"].Value = door.Name;
                    }
                }
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("区域名称不能为空");
                return false;
            }
            //if (viewEntrance.Rows.Count == 0)
            //{
            //    MessageBox.Show("请至少设置一个入口门禁点");
            //    return false;
            //}
            //if (viewExit.Rows.Count == 0)
            //{
            //    MessageBox.Show("请至少设置一个出口门禁点");
            //    return false;
            //}
            return true;
        }
        #endregion

        #region 事件处理程序
        private void FrmMonitorSetting_Load(object sender, EventArgs e)
        {
            MonitorRegion = new RegionClient(AppSettings.Current.ConnStr).GetByID(1, true).QueryObject;
            if (MonitorRegion != null) ShowRegion(MonitorRegion);
        }

        private void mnu_AddEnter_Click(object sender, EventArgs e)
        {
            List<Door> doors = new List<Door>();
            foreach (DataGridViewRow row in viewEntrance.Rows)
            {
                doors.Add(row.Tag as Door);
            }
            FrmDoorSelection frm = new FrmDoorSelection();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.SelectedDoors = doors;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                viewEntrance.Rows.Clear();
                foreach (var door in frm.SelectedDoors)
                {
                    var row = viewEntrance.Rows.Add();
                    viewEntrance.Rows[row].Tag = door;
                    viewEntrance.Rows[row].Cells["colIDEnter"].Value = door.ID;
                    viewEntrance.Rows[row].Cells["colNameEnter"].Value = door.Name;
                }
            }
        }

        private void mnu_DelEnter_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in viewEntrance.SelectedRows)
            {
                viewEntrance.Rows.Remove(row);
            }
        }

        private void mnu_AddExit_Click(object sender, EventArgs e)
        {
            List<Door> doors = new List<Door>();
            foreach (DataGridViewRow row in viewExit.Rows)
            {
                doors.Add(row.Tag as Door);
            }
            FrmDoorSelection frm = new FrmDoorSelection();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.SelectedDoors = doors;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                viewExit.Rows.Clear();
                foreach (var door in frm.SelectedDoors)
                {
                    var row = viewExit.Rows.Add();
                    viewExit.Rows[row].Tag = door;
                    viewExit.Rows[row].Cells["colIDExit"].Value = door.ID;
                    viewExit.Rows[row].Cells["colNameExit"].Value = door.Name;
                }
            }

        }

        private void mnu_DelExit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in viewExit.SelectedRows)
            {
                viewExit.Rows.Remove(row);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!CheckInput()) return;
            var region = MonitorRegion != null ? MonitorRegion : new Region();
            region.ID = 1;
            region.Name = txtName.Text;
            region.HideTimeOutPerson = chk不显示超时未出人员.Checked;
            region.EnterDoors = new List<string>();
            foreach (DataGridViewRow row in viewEntrance.Rows)
            {
                region.EnterDoors.Add((row.Tag as Door).ID);
            }
            region.ExitDoors = new List<string>();
            foreach (DataGridViewRow row in viewExit.Rows)
            {
                region.ExitDoors.Add((row.Tag as Door).ID);
            }
            var ret = new RegionClient(AppSettings.Current.ConnStr).Add(region, true);
            if (ret.Result == GeneralLibrary.Core.ResultCode.Successful) this.DialogResult = DialogResult.OK;
            else
            {
                MessageBox.Show(ret.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
