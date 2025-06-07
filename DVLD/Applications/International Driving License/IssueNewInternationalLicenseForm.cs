using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.International_Driving_Licenses;
using DVLD.Licenses.Local_Driving_Licenses;
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

namespace DVLD.Applications.International_Driving_License
{
    public partial class IssueNewInternationalLicenseForm : Form
    {
        private int _licenseID = -1;
        private DVLD_BusinessLayer.License _license;
        private InternationalDrivingLicense _internationalDrivingLicense = new InternationalDrivingLicense();
        public Action DataUpdated;

        public IssueNewInternationalLicenseForm()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
            _SetDefaultValues();

            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += _FillNewLicenseInfo;

            if (_licenseID != -1)
            {
                ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_licenseID);
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            }
        }


        private void _SetDefaultValues()
        {
            lblFees.Text = ApplicationType.Find(ApplicationType.EnApplicationType.InternationalDrivingLicense).Fees.ToString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            
            lblApplicationID.Text = "[???]";
            lblApplicationDate.Text = DateTime.Today.ToString();
            lblIssueDate.Text = DateTime.Today.ToString();
            lblInternationalLicenseID.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
            lblExpirationDate.Text = DateTime.Today.AddYears(1).ToString();

            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnIssue.Enabled = false;
        }

        private void _FillNewLicenseInfo(int LicenseID)
        {
            _SetDefaultValues();
            _license = DVLD_BusinessLayer.License.FindByID(LicenseID);
            llShowLicenseHistory.Enabled = true;


            if (InternationalDrivingLicense.DoesDriverHaveActiveInternationalLicense(_license.DriverID))
            {
                MessageBox.Show($"Selected Driver Is Is Already Have An Active International Driving License", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (_license.LicenseClassID != LicenseClass.EnLicenseClass.OrdinaryDrivingLicense)
            {
                MessageBox.Show($"Selected License Should Be Class 3", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!_license.IsActive)
            {
                MessageBox.Show($"Selected License Is Not Active", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (_license.IsDetained)
            {
                MessageBox.Show($"Selected License Is Detained", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            lblLocalLicenseID.Text = _license.ID.ToString();
            
            btnIssue.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure About Issuing International Driving License?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                if (_internationalDrivingLicense.IssueInternationalDrivingLicense(_license, Global.CurrentUser.UserID))
                {
                    MessageBox.Show("International Driving Licnese Issued Successfully", "Success");
                    lblApplicationID.Text = _internationalDrivingLicense.ApplicationID.ToString();
                    lblInternationalLicenseID.Text = _internationalDrivingLicense.ID.ToString();
                    btnIssue.Enabled = false;
                    llShowLicenseInfo.Enabled = true;
                    ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                    DataUpdated?.Invoke();
                }
                else
                    MessageBox.Show("Failed To issuing International Driving License", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            ShowInternationalDrivingLicenseInfoForm form = new ShowInternationalDrivingLicenseInfoForm(_internationalDrivingLicense.ID);
            form.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(_license.DriverInfo.PersonID);
            form.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
