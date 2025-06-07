using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Driving_Licenses
{
    public partial class ShowInternationalDrivingLicenseInfoForm : Form
    {
        private int _InternationalLicenseID;
        public ShowInternationalDrivingLicenseInfoForm(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowInternationalLicenseInfoForm_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.LoadInfo(_InternationalLicenseID);
        }
    }
}
