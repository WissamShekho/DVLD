using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.People;
using DVLD_BusinessLayer;

namespace DVLD.Applications.Controls
{
    public partial class ctrlDrivingLicenseApplicationCard : UserControl
    {
        public static Action DataUpdated;
        private int _LicenseID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public int LocalDrivingApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        public LocalDrivingLicenseApplication SelectedLocalDrivingApplicationInfo
        {
            get { return _LocalDrivingLicenseApplication; }
        }

        public ctrlDrivingLicenseApplicationCard()
        {
            InitializeComponent();
        }

        public void LoadLocalDrivingLicenseApplicationInfo(int ldlAppID)
        {
            _LocalDrivingLicenseApplicationID = ldlAppID;
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                ResetInfo();
                MessageBox.Show("No Local Driving License Application with ID = " + ldlAppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();
        }

        public void LoadLocalDrivingLicenseApplicationInfoByApplicationID(int AppID)
        {
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByApplicationID(AppID);
            if (_LocalDrivingLicenseApplication == null)
            {
                ResetInfo();
                MessageBox.Show("No Driving License Application with ID = " + AppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.ID;
            _FillApplicationInfo();
        }

        public void ResetInfo()
        {
            lblDrivingLicenseApplicationID.Text = "[???]";
            lblLicenseClassName.Text = "[???]";
            lblPassedTests.Text = "[]";

            ctrlApplicationCard1.ResetInfo();
            llLicenseInfo.Enabled = false;
        }

        private void _FillApplicationInfo()
        {

            lblDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.ID.ToString();
            lblLicenseClassName.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = _LocalDrivingLicenseApplication.PassedTestsCount().ToString();

            DVLD_BusinessLayer.License license = DVLD_BusinessLayer.License.FindByApplicationID(_LocalDrivingLicenseApplication.ApplicationID);
            if (license != null)
            {
                _LicenseID = license.ID;
                llLicenseInfo.Enabled = true;
            }
            else
                llLicenseInfo.Enabled = false;

            ctrlApplicationCard.DataUpdated += _DataBack;
            ctrlApplicationCard1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);
        }

        private void _DataBack()
        {
            LoadLocalDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplicationID);
            DataUpdated?.Invoke();
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("\"License\" Class Is Not Implemented Yet");
        }
    }
}
