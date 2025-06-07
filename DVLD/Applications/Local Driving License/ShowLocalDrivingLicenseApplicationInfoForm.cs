using DVLD.Applications.Controls;
using DVLD.Users.UserControls;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class ShowLocalDrivingLicenseApplicationInfoForm : Form
    {
        public static Action DataUpdated;
        private int _LocalDrivingLicenseApplicationID = -1;
        public ShowLocalDrivingLicenseApplicationInfoForm(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void ShowLocalDrivingLicenseApplicationInfoForm_Load(object sender, EventArgs e)
        {
            if (!LocalDrivingLicenseApplication.IsLocalDrivingApplicationExist(_LocalDrivingLicenseApplicationID))
            {
                MessageBox.Show($"There Is No Local Driving License Application With ID = {_LocalDrivingLicenseApplicationID} ");
                return;
            }

            ctrlDrivingLicenseApplicationCard.DataUpdated += _Databack;
            ctrlLocalDrivingLicenseApplicationCard1.LoadLocalDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplicationID);
        }

        private void _Databack()
        {
            DataUpdated?.Invoke();
            ctrlLocalDrivingLicenseApplicationCard1.LoadLocalDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
