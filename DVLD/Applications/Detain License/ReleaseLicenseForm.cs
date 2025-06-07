using DVLD.Global_Classes;
using DVLD.Licenses.Local_Driving_Licenses;
using DVLD.Licenses;
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

namespace DVLD.Applications.Detain_License
{
    public partial class ReleaseLicenseForm : Form
    {
        private int _licenseID = -1;
        private DVLD_BusinessLayer.License _license;
        public Action DataUpdated;
        public ReleaseLicenseForm()
        {
            InitializeComponent();
        }

        public ReleaseLicenseForm(int LicenseID)
        {
            _licenseID = LicenseID;
            InitializeComponent();
        }

        private void ReleaseLicenseForm_Load(object sender, EventArgs e)
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
            lblApplicationFees.Text = ApplicationType.Find(ApplicationType.EnApplicationType.ReleaseDetianedDrivingLicense).Fees.ToString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblApplicationID.Text = "[???]";
            lblDetainDate.Text = "[??/??/????]";
            lblFineFees.Text = "[$$$]";
            lblTotalFees.Text = "[$$$]";

            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnReleaseLicense.Enabled = false;
        }

        private void _FillNewLicenseInfo(int LicenseID)
        {
            _SetDefaultValues();
            _license = DVLD_BusinessLayer.License.FindByID(LicenseID);
            llShowLicenseHistory.Enabled = true;

            if (!_license.IsActive)
            {
                MessageBox.Show($"Selected License Is Not Active", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!_license.IsDetained)
            {
                MessageBox.Show($"Selected License Is not Detained", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            

            lblDetainID.Text = _license.DetainInfo.ID.ToString();
            lblDetainDate.Text = _license.DetainInfo.DetainDate.ToShortDateString();
            lblLicenseID.Text = _license.DetainInfo.LicenseID.ToString();
            lblFineFees.Text = _license.DetainInfo.FineFees.ToString();
            lblTotalFees.Text = (_license.DetainInfo.FineFees + ApplicationType.Find(ApplicationType.EnApplicationType.ReleaseDetianedDrivingLicense).Fees).ToString();

            btnReleaseLicense.Enabled = true;
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure About Releasing Driving License?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_license.Release(Global.CurrentUser.UserID))
                {
                    MessageBox.Show("License Released Successfully", "Success");
                    lblApplicationID.Text = _license.DetainInfo.ReleaseApplicationID.ToString();
                    btnReleaseLicense.Enabled = false;
                    llShowLicenseInfo.Enabled = true;

                    DataUpdated?.Invoke();
                }
                else
                    MessageBox.Show("Failed To Relaease License", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDriverLicenseInfoForm form = new ShowDriverLicenseInfoForm(_license.ID);
            form.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(_license.DriverInfo.PersonID);
            form.ShowDialog();
        }

    }
}
