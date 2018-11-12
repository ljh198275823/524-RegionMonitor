namespace LJH.RegionMonitor.WebApiAPP
{
    partial class FrmMonitorSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitorSetting));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.viewEntrance = new System.Windows.Forms.DataGridView();
            this.menuEntrance = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_AddEnter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_DelEnter = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.viewExit = new System.Windows.Forms.DataGridView();
            this.mnuExit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_AddExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.colIDExit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNameExit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDEnter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNameEnter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewEntrance)).BeginInit();
            this.menuEntrance.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewExit)).BeginInit();
            this.mnuExit.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "区域名称";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(88, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(358, 26);
            this.txtName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.viewEntrance);
            this.groupBox1.Location = new System.Drawing.Point(11, 58);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(435, 395);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "入口门禁点";
            // 
            // viewEntrance
            // 
            this.viewEntrance.AllowUserToAddRows = false;
            this.viewEntrance.AllowUserToDeleteRows = false;
            this.viewEntrance.AllowUserToResizeRows = false;
            this.viewEntrance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.viewEntrance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.viewEntrance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.viewEntrance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIDEnter,
            this.colNameEnter});
            this.viewEntrance.ContextMenuStrip = this.menuEntrance;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.viewEntrance.DefaultCellStyle = dataGridViewCellStyle2;
            this.viewEntrance.Location = new System.Drawing.Point(5, 24);
            this.viewEntrance.Name = "viewEntrance";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.viewEntrance.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.viewEntrance.RowHeadersVisible = false;
            this.viewEntrance.RowTemplate.Height = 23;
            this.viewEntrance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.viewEntrance.Size = new System.Drawing.Size(425, 366);
            this.viewEntrance.TabIndex = 57;
            // 
            // menuEntrance
            // 
            this.menuEntrance.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_AddEnter,
            this.mnu_DelEnter});
            this.menuEntrance.Name = "menuEntrance";
            this.menuEntrance.Size = new System.Drawing.Size(161, 48);
            // 
            // mnu_AddEnter
            // 
            this.mnu_AddEnter.Name = "mnu_AddEnter";
            this.mnu_AddEnter.Size = new System.Drawing.Size(160, 22);
            this.mnu_AddEnter.Text = "选择入口门禁点";
            this.mnu_AddEnter.Click += new System.EventHandler(this.mnu_AddEnter_Click);
            // 
            // mnu_DelEnter
            // 
            this.mnu_DelEnter.Name = "mnu_DelEnter";
            this.mnu_DelEnter.Size = new System.Drawing.Size(160, 22);
            this.mnu_DelEnter.Text = "删除入口门禁点";
            this.mnu_DelEnter.Click += new System.EventHandler(this.mnu_DelEnter_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.viewExit);
            this.groupBox2.Location = new System.Drawing.Point(464, 58);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(435, 395);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出口门禁点";
            // 
            // viewExit
            // 
            this.viewExit.AllowUserToAddRows = false;
            this.viewExit.AllowUserToDeleteRows = false;
            this.viewExit.AllowUserToResizeRows = false;
            this.viewExit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.viewExit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.viewExit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.viewExit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIDExit,
            this.colNameExit});
            this.viewExit.ContextMenuStrip = this.mnuExit;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.viewExit.DefaultCellStyle = dataGridViewCellStyle5;
            this.viewExit.Location = new System.Drawing.Point(6, 24);
            this.viewExit.Name = "viewExit";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.viewExit.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.viewExit.RowHeadersVisible = false;
            this.viewExit.RowTemplate.Height = 23;
            this.viewExit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.viewExit.Size = new System.Drawing.Size(424, 366);
            this.viewExit.TabIndex = 58;
            // 
            // mnuExit
            // 
            this.mnuExit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_AddExit,
            this.mnuDelExit});
            this.mnuExit.Name = "menuEntrance";
            this.mnuExit.Size = new System.Drawing.Size(161, 48);
            // 
            // mnu_AddExit
            // 
            this.mnu_AddExit.Name = "mnu_AddExit";
            this.mnu_AddExit.Size = new System.Drawing.Size(160, 22);
            this.mnu_AddExit.Text = "选择出口门禁点";
            this.mnu_AddExit.Click += new System.EventHandler(this.mnu_AddExit_Click);
            // 
            // mnuDelExit
            // 
            this.mnuDelExit.Name = "mnuDelExit";
            this.mnuDelExit.Size = new System.Drawing.Size(160, 22);
            this.mnuDelExit.Text = "删除出口门禁点";
            this.mnuDelExit.Click += new System.EventHandler(this.mnu_DelExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(452, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "*";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(763, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(125, 38);
            this.btnClose.TabIndex = 60;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOk.Location = new System.Drawing.Point(623, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(125, 38);
            this.btnOk.TabIndex = 59;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // colIDExit
            // 
            this.colIDExit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colIDExit.HeaderText = "编号";
            this.colIDExit.MinimumWidth = 80;
            this.colIDExit.Name = "colIDExit";
            this.colIDExit.ReadOnly = true;
            this.colIDExit.Width = 80;
            // 
            // colNameExit
            // 
            this.colNameExit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNameExit.HeaderText = "名称";
            this.colNameExit.MinimumWidth = 100;
            this.colNameExit.Name = "colNameExit";
            this.colNameExit.ReadOnly = true;
            // 
            // colIDEnter
            // 
            this.colIDEnter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colIDEnter.HeaderText = "编号";
            this.colIDEnter.MinimumWidth = 80;
            this.colIDEnter.Name = "colIDEnter";
            this.colIDEnter.ReadOnly = true;
            this.colIDEnter.Width = 80;
            // 
            // colNameEnter
            // 
            this.colNameEnter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNameEnter.HeaderText = "名称";
            this.colNameEnter.MinimumWidth = 100;
            this.colNameEnter.Name = "colNameEnter";
            this.colNameEnter.ReadOnly = true;
            // 
            // FrmMonitorSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 464);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMonitorSetting";
            this.Text = "监控区域设置";
            this.Load += new System.EventHandler(this.FrmMonitorSetting_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewEntrance)).EndInit();
            this.menuEntrance.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewExit)).EndInit();
            this.mnuExit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView viewEntrance;
        private System.Windows.Forms.DataGridView viewExit;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ContextMenuStrip menuEntrance;
        private System.Windows.Forms.ContextMenuStrip mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnu_AddEnter;
        private System.Windows.Forms.ToolStripMenuItem mnu_DelEnter;
        private System.Windows.Forms.ToolStripMenuItem mnu_AddExit;
        private System.Windows.Forms.ToolStripMenuItem mnuDelExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDEnter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameEnter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameExit;
    }
}