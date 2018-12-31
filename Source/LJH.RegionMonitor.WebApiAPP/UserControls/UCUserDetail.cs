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
            try
            {
                txtName.Text = p.UserName;
                txtCardID.Text = p.CardID;
                txtDepartment.Text = p.Department;
                txtDoor.Text = p.DoorName;
                txtEventDT.Text = p.EnterDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                txtPhone.Text = string.Empty;
                picPhoto.Image = null;
                var person = new WebAPIClient.PersonDetailClient(AppSettings.Current.ConnStr).GetByID(p.UserID, true).QueryObject;
                if (person != null)
                {
                    txtPhone.Text = person.Phone;
                    if (!string.IsNullOrEmpty(person.PhotoUrl))
                    {
                        using (var client = new System.Net.WebClient())
                        {
                            var data = client.DownloadData(person.PhotoUrl);
                            if (data != null && data.Length > 0)
                            {
                                using (var fs = new MemoryStream(data))
                                {
                                    picPhoto.Image = Image.FromStream(fs);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
