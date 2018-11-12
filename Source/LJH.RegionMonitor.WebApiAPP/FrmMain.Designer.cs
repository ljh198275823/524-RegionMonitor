namespace LJH.RegionMonitor.WebApiAPP
{
    partial class FrmMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblWebhostState = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUrl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRegion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCardEvent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblWebhostState,
            this.lblUrl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 481);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(744, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblWebhostState
            // 
            this.lblWebhostState.Name = "lblWebhostState";
            this.lblWebhostState.Size = new System.Drawing.Size(0, 17);
            // 
            // lblUrl
            // 
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRegion,
            this.toolStripSeparator1,
            this.btnCardEvent,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(744, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRegion
            // 
            this.btnRegion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRegion.Image = ((System.Drawing.Image)(resources.GetObject("btnRegion.Image")));
            this.btnRegion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRegion.Name = "btnRegion";
            this.btnRegion.Size = new System.Drawing.Size(84, 22);
            this.btnRegion.Text = "人员区域设置";
            this.btnRegion.Click += new System.EventHandler(this.btnRegion_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCardEvent
            // 
            this.btnCardEvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCardEvent.Image = ((System.Drawing.Image)(resources.GetObject("btnCardEvent.Image")));
            this.btnCardEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardEvent.Name = "btnCardEvent";
            this.btnCardEvent.Size = new System.Drawing.Size(84, 22);
            this.btnCardEvent.Text = "刷卡记录查询";
            this.btnCardEvent.Click += new System.EventHandler(this.btnCardEvent_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 503);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "天荒坪水电站区域人员统计API服务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblWebhostState;
        private System.Windows.Forms.ToolStripStatusLabel lblUrl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRegion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCardEvent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

