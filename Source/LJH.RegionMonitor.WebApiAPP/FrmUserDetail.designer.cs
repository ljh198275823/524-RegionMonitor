namespace LJH.RegionMonitor.WebApiAPP
{
    partial class FrmUserDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserDetail));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ucUserDetail1 = new UCUserDetail();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucUserDetail1
            // 
            this.ucUserDetail1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ucUserDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserDetail1.Location = new System.Drawing.Point(0, 0);
            this.ucUserDetail1.Name = "ucUserDetail1";
            this.ucUserDetail1.Size = new System.Drawing.Size(459, 196);
            this.ucUserDetail1.TabIndex = 0;
            // 
            // FrmUserDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 196);
            this.Controls.Add(this.ucUserDetail1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人员明细";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmUserDetail_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmUserDetail_FormClosed);
            this.Load += new System.EventHandler(this.FrmUserDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UCUserDetail ucUserDetail1;
        private System.Windows.Forms.Timer timer1;
    }
}