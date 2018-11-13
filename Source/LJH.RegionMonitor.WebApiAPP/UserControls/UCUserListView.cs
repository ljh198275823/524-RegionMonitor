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
    public partial class UCUserListView : UserControl
    {
        public UCUserListView()
        {
            InitializeComponent();
        }

        #region 私有变量
        private readonly int ITEMWIDTH = 127; //宽度
        private readonly int ITEMHEIGHT = 40; //高度
        private int _Columns = 0;
        private List<UCUser> _Controls = new List<UCUser>();
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置列间距
        /// </summary>
        public int ColSpliter { get; set; }
        #endregion

        #region 私有方法
        private void PerformLayoutItems(List<UCUser> items)
        {
            if (_Columns == 0) return;
            int row = 0;
            int col = 0;
            this.SuspendLayout();
            foreach (var dept in items)
            {
                ShowUser(row, col, dept);
                if (col < _Columns - 1)
                {
                    col++;
                }
                else
                {
                    col = 0;
                    row++;
                }
            }
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ShowUser(int row, int col, UCUser ucd)
        {
            if (!this.Controls.Contains(ucd)) this.Controls.Add(ucd);
            Point p = new Point(ITEMWIDTH * col + ColSpliter * (col + 1), ITEMHEIGHT * row + ColSpliter * (row + 1));
            ucd.Visible = true;
            ucd.BorderStyle = BorderStyle.FixedSingle;
            ucd.Location = p;
            ucd.Width = ITEMWIDTH;
            ucd.Height = ITEMHEIGHT;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据要显示的项数量和最大宽度计算控件的真实大小
        /// </summary>
        /// <param name="person"></param>
        /// <param name="maxWidth"></param>
        /// <returns></returns>
        public Size CalSpace(int itemCount, int maxWidth)
        {
            _Columns = (maxWidth - ColSpliter) / (ITEMWIDTH + ColSpliter);
            if (_Columns == 0) _Columns = 1; //如果不够显示一行，也按一行来显示
            int row = (int)(Math.Ceiling((double)itemCount / _Columns));
            return new Size((ITEMWIDTH + ColSpliter) * _Columns + ColSpliter, (ITEMHEIGHT + ColSpliter) * row + ColSpliter);
        }

        public void ShowInregionPerson(List<InRegionPerson> person)
        {
            int pCount = person != null ? person.Count : 0;
            int ucdCount = _Controls != null ? _Controls.Count : 0;

            for (int i = pCount; i < ucdCount; i++) //多余的控件删除掉
            {
                this.Controls.RemoveAt(pCount);
                _Controls.RemoveAt(pCount);
            }
            for (int i = ucdCount; i < pCount; i++)
            {
                UCUser ucd = new UCUser();
                ucd.Visible = false;
                ucd.DoubleClick += new EventHandler(ucd_DoubleClick);
                _Controls.Add(ucd);
            }

            if (person != null && person.Count > 0)
            {
                for (int i = 0; i < person.Count; i++)
                {
                    UCUser ucd = _Controls[i];
                    ucd.Tag = person[i];
                    ucd.Title = person[i].UserName;
                    ucd.BackColor = person[i].IsTimeout ? Color.Red : Color.Blue ;
                    ucd.ForeColor = Color.White;
                }
                PerformLayoutItems(_Controls);
            }
        }

        private void ucd_DoubleClick(object sender, EventArgs e)
        {
            FrmUserDetail frm = FrmUserDetail.GetInstance();
            frm.ShowUserDetail((sender as Control).Tag as InRegionPerson);
        }
        #endregion
    }
}
