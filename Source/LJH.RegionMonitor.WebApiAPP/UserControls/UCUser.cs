using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class UCUser : UserControl
    {
        public UCUser()
        {
            InitializeComponent();
        }

        public string Title
        {
            set
            {
                label1.Text = value;
            }
        }

        public new Color BackColor
        {
            get
            {
                return label1.BackColor;
            }
            set
            {
                label1.BackColor = value;
            }
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }
    }
}
