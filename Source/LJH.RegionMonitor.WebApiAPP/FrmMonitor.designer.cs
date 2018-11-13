namespace LJH.RegionMonitor.WebApiAPP
{
    partial class FrmMonitor
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOver48 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblRegion = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblInregionCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrGetEvents = new System.Windows.Forms.Timer(this.components);
            this.pMonitor = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tmrTimeout = new System.Windows.Forms.Timer(this.components);
            this.ucDeptView = new LJH.RegionMonitor.WebApiAPP.UCDeptListView();
            this.ucTimeout = new LJH.RegionMonitor.WebApiAPP.UCDeptListView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pMonitor.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullScreen,
            this.toolStripSeparator2,
            this.btnOver48});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(855, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(36, 22);
            this.btnFullScreen.Text = "全屏";
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOver48
            // 
            this.btnOver48.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOver48.Name = "btnOver48";
            this.btnOver48.Size = new System.Drawing.Size(110, 22);
            this.btnOver48.Text = "超48小时未出人员";
            this.btnOver48.Click += new System.EventHandler(this.btnOver48_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblRegion,
            this.lblInregionCount,
            this.lblCurTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 25);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(855, 71);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblRegion
            // 
            this.lblRegion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblRegion.Font = new System.Drawing.Font("微软雅黑", 30F, System.Drawing.FontStyle.Bold);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(187, 66);
            this.lblRegion.Text = "当前区域";
            // 
            // lblInregionCount
            // 
            this.lblInregionCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblInregionCount.Font = new System.Drawing.Font("微软雅黑", 35F, System.Drawing.FontStyle.Bold);
            this.lblInregionCount.ForeColor = System.Drawing.Color.Red;
            this.lblInregionCount.Name = "lblInregionCount";
            this.lblInregionCount.Size = new System.Drawing.Size(60, 66);
            this.lblInregionCount.Text = "0";
            // 
            // lblCurTime
            // 
            this.lblCurTime.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.lblCurTime.Name = "lblCurTime";
            this.lblCurTime.Size = new System.Drawing.Size(593, 66);
            this.lblCurTime.Spring = true;
            this.lblCurTime.Text = "2015-09-14 12:00:00";
            this.lblCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrGetEvents
            // 
            this.tmrGetEvents.Interval = 1000;
            this.tmrGetEvents.Tick += new System.EventHandler(this.tmrGetEvents_Tick);
            // 
            // pMonitor
            // 
            this.pMonitor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMonitor.Controls.Add(this.ucDeptView);
            this.pMonitor.Controls.Add(this.splitter1);
            this.pMonitor.Controls.Add(this.ucTimeout);
            this.pMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMonitor.Location = new System.Drawing.Point(0, 96);
            this.pMonitor.Name = "pMonitor";
            this.pMonitor.Size = new System.Drawing.Size(855, 491);
            this.pMonitor.TabIndex = 4;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 363);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(853, 6);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // tmrTimeout
            // 
            this.tmrTimeout.Interval = 30000;
            this.tmrTimeout.Tick += new System.EventHandler(this.tmrTimeout_Tick);
            // 
            // ucDeptView
            // 
            this.ucDeptView.AutoScroll = true;
            this.ucDeptView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucDeptView.ColSpliter = 5;
            this.ucDeptView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDeptView.Location = new System.Drawing.Point(0, 0);
            this.ucDeptView.Name = "ucDeptView";
            this.ucDeptView.Size = new System.Drawing.Size(853, 363);
            this.ucDeptView.TabIndex = 8;
            // 
            // ucTimeout
            // 
            this.ucTimeout.AutoScroll = true;
            this.ucTimeout.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucTimeout.ColSpliter = 5;
            this.ucTimeout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucTimeout.Location = new System.Drawing.Point(0, 369);
            this.ucTimeout.Name = "ucTimeout";
            this.ucTimeout.Size = new System.Drawing.Size(853, 120);
            this.ucTimeout.TabIndex = 6;
            this.ucTimeout.Resize += new System.EventHandler(this.ucTimeout_Resize);
            // 
            // FrmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 587);
            this.Controls.Add(this.pMonitor);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmMonitor";
            this.Text = "区域在场人员监测软件";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMonitor_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pMonitor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton btnFullScreen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmrGetEvents;
        private System.Windows.Forms.ToolStripStatusLabel lblRegion;
        private System.Windows.Forms.ToolStripStatusLabel lblInregionCount;
        private System.Windows.Forms.ToolStripStatusLabel lblCurTime;
        private System.Windows.Forms.Panel pMonitor;
        private UCDeptListView ucDeptView;
        private System.Windows.Forms.Splitter splitter1;
        private UCDeptListView ucTimeout;
        private System.Windows.Forms.Timer tmrTimeout;
        private System.Windows.Forms.ToolStripButton btnOver48;

    }
}

