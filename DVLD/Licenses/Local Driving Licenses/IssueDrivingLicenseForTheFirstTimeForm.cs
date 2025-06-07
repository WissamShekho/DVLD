using DVLD.Global_Classes;
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

namespace DVLD.Licenses.Local_Driving_Licenses
{
    public partial class IssueDrivingLicenseForTheFirstTimeForm : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public static Action DataUpdated;

        public IssueDrivingLicenseForTheFirstTimeForm(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IssueDrivingLicenseForTheFirstTimeForm_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);
            
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_LocalDrivingLicenseApplication.DoesPassedAllTests())
            {
                MessageBox.Show("Error: Person Should Pass All Tests First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DVLD_BusinessLayer.License.DoesPersonHaveActiveLicenseByLicenseClass(_LocalDrivingLicenseApplication.ApplicationInfo.PersonID, _LocalDrivingLicenseApplication.LicenseClassID))
            {
                MessageBox.Show("Error: Person Have This Class Of Licenses Already", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlDrivingLicenseApplicationCard1.LoadLocalDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplicationID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure About Issuing The License?", "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LicenseID = _LocalDrivingLicenseApplication.IssueDrivingLicenseForTheFirstTime(tbNotes.Text.Trim(), Global.CurrentUser.UserID);
            

            if (LicenseID != -1)
            {
                MessageBox.Show($"License Issued Successfully, License ID = {LicenseID}", "Succeded");
                ctrlDrivingLicenseApplicationCard1.LoadLocalDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplicationID);
                DataUpdated?.Invoke();
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show($"Error: Failed To Save license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
