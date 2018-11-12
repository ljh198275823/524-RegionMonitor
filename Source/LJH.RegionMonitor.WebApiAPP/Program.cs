using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJH.RegionMonitor.WebApiAPP
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!LJH.GeneralLibrary.SingleInstance.ExistsProcess())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
            else
            {
                LJH.GeneralLibrary.SingleInstance.ShowSingleProcess();
            }
        }
    }
}
