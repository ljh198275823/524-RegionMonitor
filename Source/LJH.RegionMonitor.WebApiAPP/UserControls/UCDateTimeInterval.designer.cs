namespace LJH.GeneralLibrary.WinformControl
{
    partial class UCDateTimeInterval
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDateTimeInterval));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.rdToday = new System.Windows.Forms.RadioButton();
            this.rdThisMonth = new System.Windows.Forms.RadioButton();
            this.rdThisQuarter = new System.Windows.Forms.RadioButton();
            this.rdThisYear = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dtStart
            // 
            resources.ApplyResources(this.dtStart, "dtStart");
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Name = "dtStart";
            // 
            // dtEnd
            // 
            resources.ApplyResources(this.dtEnd, "dtEnd");
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Name = "dtEnd";
            // 
            // rdToday
            // 
            resources.ApplyResources(this.rdToday, "rdToday");
            this.rdToday.Name = "rdToday";
            this.rdToday.UseVisualStyleBackColor = true;
            this.rdToday.CheckedChanged += new System.EventHandler(this.rdToday_CheckedChanged);
            // 
            // rdThisMonth
            // 
            resources.ApplyResources(this.rdThisMonth, "rdThisMonth");
            this.rdThisMonth.Name = "rdThisMonth";
            this.rdThisMonth.TabStop = true;
            this.rdThisMonth.UseVisualStyleBackColor = true;
            this.rdThisMonth.CheckedChanged += new System.EventHandler(this.rdThisMonth_CheckedChanged);
            // 
            // rdThisQuarter
            // 
            resources.ApplyResources(this.rdThisQuarter, "rdThisQuarter");
            this.rdThisQuarter.Name = "rdThisQuarter";
            this.rdThisQuarter.TabStop = true;
            this.rdThisQuarter.UseVisualStyleBackColor = true;
            this.rdThisQuarter.CheckedChanged += new System.EventHandler(this.rdThisQuarter_CheckedChanged);
            // 
            // rdThisYear
            // 
            resources.ApplyResources(this.rdThisYear, "rdThisYear");
            this.rdThisYear.Name = "rdThisYear";
            this.rdThisYear.TabStop = true;
            this.rdThisYear.UseVisualStyleBackColor = true;
            this.rdThisYear.CheckedChanged += new System.EventHandler(this.rdThisYear_CheckedChanged);
            // 
            // UCDateTimeInterval
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdThisYear);
            this.Controls.Add(this.rdThisQuarter);
            this.Controls.Add(this.rdThisMonth);
            this.Controls.Add(this.rdToday);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCDateTimeInterval";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.RadioButton rdToday;
        private System.Windows.Forms.RadioButton rdThisMonth;
        private System.Windows.Forms.RadioButton rdThisQuarter;
        private System.Windows.Forms.RadioButton rdThisYear;
    }
}
