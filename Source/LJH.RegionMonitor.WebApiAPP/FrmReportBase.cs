using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using LJH.GeneralLibrary.Core.DAL;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class FrmReportBase : Form
    {
        public FrmReportBase()
        {
            InitializeComponent();
        }

        #region 私有变量
        private DataGridView _gridView;
        #endregion.

        #region 私有方法
        private void InitGridView()
        {
            if (GridView != null)
            {
                GridView.BorderStyle = BorderStyle.FixedSingle;
                GridView.BackgroundColor = Color.White;
                GridView.Sorted += new EventHandler(GridView_Sorted);

                if (GridView.ContextMenuStrip != null)
                {
                    ContextMenuStrip menu = GridView.ContextMenuStrip;
                    if (menu.Items["cMnu_Export"] != null) menu.Items["cMnu_Export"].Click += btnExport_Click;
                }
            }
        }

        private void InitGridViewColumns()
        {
            DataGridView grid = this.GridView;
            if (grid == null) return;
            string temp = string.Empty;
            if (string.IsNullOrEmpty(temp)) return;
            string[] cols = temp.Split(',');
            int displayIndex = 0;
            for (int i = 0; i < cols.Length; i++)
            {
                string[] col_Temp = cols[i].Split(':');
                if (col_Temp.Length >= 1 && grid.Columns.Contains(col_Temp[0]))
                {
                    grid.Columns[col_Temp[0]].DisplayIndex = displayIndex;
                    displayIndex++;
                    if (col_Temp.Length >= 2 && col_Temp[1].Trim() == "0")
                    {
                        grid.Columns[col_Temp[0]].Visible = false;
                    }
                    else
                    {
                        grid.Columns[col_Temp[0]].Visible = true;
                    }
                }
            }
        }

        private string[] GetAllVisiableColumns()
        {
            if (GridView != null)
            {
                List<string> cols = new List<string>();
                foreach (DataGridViewColumn col in GridView.Columns)
                {
                    if (col.Visible) cols.Add(col.Name);
                }
                return cols.ToArray();
            }
            return null;
        }
        #endregion

        #region 保护方法
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        protected virtual List<object> GetDataSource()
        {
            return null;
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="items">要显示的数据</param>
        /// <param name="reload">是否重新加载数据，如果为真，则表示先会清空之前的数据，否则保留旧有数据</param>
        protected virtual void ShowItemsOnGrid(List<object> items)
        {
            GridView.Rows.Clear();
            if (items != null && items.Count > 0)
            {
                foreach (object item in items)
                {
                    int row = GridView.Rows.Add();
                    ShowItemInGridViewRow(GridView.Rows[row], item);
                    GridView.Rows[row].Tag = item;
                }
            }
            if (this.GridView.Rows.Count > 0)
            {
                ShowRowBackColor();
                this.GridView.Rows[0].Selected = false;
                FreshStatusBar();
            }
        }

        /// <summary>
        /// 在网格行中显示单个数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="item"></param>
        protected virtual void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {

        }

        /// <summary>
        /// 刷新状态栏
        /// </summary>
        protected virtual void FreshStatusBar()
        {
            this.toolStripStatusLabel1.Text = string.Format("总共 {0} 项", GridView.Rows.Count);
        }
        /// <summary>
        /// 显示数据行的颜色
        /// </summary>
        protected virtual void ShowRowBackColor()
        {
            int count = 0;
            foreach (DataGridViewRow row in this.GridView.Rows)
            {
                if (row.Visible)
                {
                    count++;
                    row.DefaultCellStyle.BackColor = (count % 2 == 1) ? Color.FromArgb(230, 230, 230) : Color.White;
                }
            }
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        protected virtual void ExportData()
        {
            //try
            //{
            //    DataGridView view = this.GridView;
            //    if (view != null)
            //    {
            //        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //        saveFileDialog1.Filter = "Excel文档|*.xls|所有文件(*.*)|*.*";
            //        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //        {
            //            string path = saveFileDialog1.FileName;
            //            if (LJH.GeneralLibrary.WinformControl.DataGridViewExporter.Export(view, path))
            //            {
            //                MessageBox.Show("导出成功");
            //            }
            //            else
            //            {
            //                MessageBox.Show("保存到电子表格时出现错误!");
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("保存到电子表格时出现错误!");
            //}
        }
        #endregion

        #region 子类要重写的方法
        protected virtual DataGridView GridView
        {
            get
            {
                if (_gridView == null)
                {
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is DataGridView)
                        {
                            _gridView = ctrl as DataGridView;
                        }
                    }
                }
                return _gridView;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            ShowOperatorRights();
            InitGridView();
            InitGridViewColumns();
        }
        /// <summary>
        /// 显示操作的权限
        /// </summary>
        public virtual void ShowOperatorRights()
        {

        }
        #endregion

        #region 事件处理
        private void FrmReportBase_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<object> items = GetDataSource();
            ShowItemsOnGrid(items);
        }

        private void GridView_Sorted(object sender, EventArgs e)
        {
            ShowRowBackColor();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportData();
        }
        #endregion
    }
}