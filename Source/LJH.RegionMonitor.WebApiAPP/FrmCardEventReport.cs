using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LJH.RegionMonitor.WebAPIClient;
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class FrmFullLogReport : FrmReportBase
    {
        public FrmFullLogReport()
        {
            InitializeComponent();
        }

        #region 重写基类方法
        protected override void Init()
        {
            base.Init();
            this.ucDateTimeInterval1.Init();
            this.ucDateTimeInterval1.SelectToday();
        }

        protected override List<object> GetDataSource()
        {
            CardEventSearchCondition con = new CardEventSearchCondition();
            con.EventTime = new GeneralLibrary.DateTimeRange(ucDateTimeInterval1.StartDateTime, ucDateTimeInterval1.EndDateTime);
            var record = new CardEventClient(AppSettings.Current.ConnStr).GetItems(con,true).QueryObjects;
            if (record != null && record.Count > 0)
            {
                return (from it in record
                        orderby it.EventTime descending
                        select (object)it).ToList();
            }
            return null;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            var record = item as CardEvent;
            row.Tag = record;
            row.Cells["colCardID"].Value = record.CardID;
            row.Cells["colEventType"].Value = record.EventType;
            row.Cells["colUserName"].Value = record.UserName;
            row.Cells["colDoorName"].Value = record.DoorName;
            row.Cells["colDepartment"].Value = record.Department;
            row.Cells["colEventDateTime"].Value = record.EventTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colPermited"].Value = record.Permitted ? "有效刷卡" : "无效刷卡";
            row.DefaultCellStyle.ForeColor = record.Permitted ? Color.Blue : Color.Red;
        }
        #endregion
    }
}
