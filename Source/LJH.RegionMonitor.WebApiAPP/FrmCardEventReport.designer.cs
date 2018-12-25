namespace LJH.RegionMonitor.WebApiAPP
{
    partial class FrmFullLogReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFullLogReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new LJH.GeneralLibrary.WinformControl.UCDateTimeInterval();
            this.viewEvent = new System.Windows.Forms.DataGridView();
            this.colEventDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPermited = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewEvent)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(291, 18);
            this.btnSearch.Size = new System.Drawing.Size(176, 74);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDateTimeInterval1);
            this.groupBox1.Location = new System.Drawing.Point(8, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 99);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "刷卡日期";
            // 
            // ucDateTimeInterval1
            // 
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2012, 6, 2, 10, 42, 8, 482);
            this.ucDateTimeInterval1.Location = new System.Drawing.Point(8, 16);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.Size = new System.Drawing.Size(221, 74);
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2012, 6, 2, 10, 42, 8, 482);
            this.ucDateTimeInterval1.TabIndex = 1;
            // 
            // viewEvent
            // 
            this.viewEvent.AllowUserToAddRows = false;
            this.viewEvent.AllowUserToDeleteRows = false;
            this.viewEvent.AllowUserToResizeColumns = false;
            this.viewEvent.AllowUserToResizeRows = false;
            this.viewEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.viewEvent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEventDateTime,
            this.colEventType,
            this.colCardID,
            this.colUserName,
            this.colDepartment,
            this.colDoorName,
            this.colPermited,
            this.colFill});
            this.viewEvent.Location = new System.Drawing.Point(8, 107);
            this.viewEvent.Name = "viewEvent";
            this.viewEvent.RowHeadersVisible = false;
            this.viewEvent.RowTemplate.Height = 23;
            this.viewEvent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.viewEvent.Size = new System.Drawing.Size(975, 350);
            this.viewEvent.TabIndex = 38;
            // 
            // colEventDateTime
            // 
            this.colEventDateTime.HeaderText = "刷卡时间";
            this.colEventDateTime.Name = "colEventDateTime";
            this.colEventDateTime.ReadOnly = true;
            this.colEventDateTime.Width = 130;
            // 
            // colEventType
            // 
            this.colEventType.HeaderText = "事件类别";
            this.colEventType.Name = "colEventType";
            this.colEventType.ReadOnly = true;
            // 
            // colCardID
            // 
            this.colCardID.HeaderText = "卡号";
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colUserName
            // 
            this.colUserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colUserName.HeaderText = "姓名";
            this.colUserName.MinimumWidth = 120;
            this.colUserName.Name = "colUserName";
            this.colUserName.ReadOnly = true;
            this.colUserName.Width = 120;
            // 
            // colDepartment
            // 
            this.colDepartment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colDepartment.HeaderText = "单位";
            this.colDepartment.MinimumWidth = 120;
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.ReadOnly = true;
            this.colDepartment.Width = 120;
            // 
            // colDoorName
            // 
            this.colDoorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colDoorName.HeaderText = "刷卡点";
            this.colDoorName.MinimumWidth = 100;
            this.colDoorName.Name = "colDoorName";
            this.colDoorName.ReadOnly = true;
            // 
            // colPermited
            // 
            this.colPermited.HeaderText = "有效";
            this.colPermited.Name = "colPermited";
            this.colPermited.ReadOnly = true;
            // 
            // colFill
            // 
            this.colFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFill.HeaderText = "";
            this.colFill.Name = "colFill";
            this.colFill.ReadOnly = true;
            // 
            // FrmFullLogReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 482);
            this.Controls.Add(this.viewEvent);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFullLogReport";
            this.Text = "门禁记录查询";
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.viewEvent, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewEvent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private LJH.GeneralLibrary.WinformControl.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.DataGridView viewEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPermited;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFill;
    }
}