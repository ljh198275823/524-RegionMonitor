using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LJH.RegionMonitor.Model;

namespace LJH.RegionMonitor.WebApiAPP
{
    public partial class UCUserDetail : UserControl
    {
        public UCUserDetail()
        {
            InitializeComponent();
        }

        public void ShowPeople(InRegionPerson p)
        {
            txtName.Text = p.UserName;
            txtCardID.Text = p.CardID;
            txtDepartment.Text = p.Department;
            txtDoor.Text = p.DoorName;
            txtEventDT.Text = p.EnterDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            //if (!string.IsNullOrEmpty(AppSettings.Current.PhotoPath) && !string.IsNullOrEmpty(p.PhotoPath))
            //{
            //    string photo = Path.Combine(AppSettings.Current.PhotoPath, p.PhotoPath);
            //    if (File.Exists(photo)) picPhoto.Image = Image.FromFile(photo);
            //}
            //string phone = new UserBLL(AppSettings.Current.ConnStr).GetPhone(p.CardID);
            //txtPhone.Text = phone;
        }
    }
}
