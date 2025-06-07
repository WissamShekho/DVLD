using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Driving_Licenses;
using DVLD.People.PersonControls;
using DVLD_BusinessLayer;

namespace DVLD.Applications.Renew_Local_Driving_License
{
    public partial class RenewLocalDrivingLicenseApplicationForm : Form
    {
        private DVLD_BusinessLayer.License _License;
        
        public RenewLocalDrivingLicenseApplicationForm()
        {
            InitializeComponent();
        }

        private void RenewLocalDrivingLicenseApplicationForm_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.ResetInfo();
            _SetDefaultValues();
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
         
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += _FillNewLicenseInfo;
        }

        private void _SetDefaultValues()
        {
            lblApplicationDate.Text = DateTime.Today.ToShortDateString();
            lblIssueDate.Text = DateTime.Today.ToShortDateString();
            lblApplicationFees.Text = ApplicationType.Find(ApplicationType.EnApplicationType.RenewDrivingLicense).Fees.ToString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblExpirationDate.Text = "[??/??/????]";
            lblApplicationID.Text = "[???]";
            lblLicenseFees.Text = "[$$$]";
            txtNotes.Text = "";
            lblRenewedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblTotalFees.Text = "[$$$]";


            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnRenewLicense.Enabled = false;
        }

        private void _FillNewLicenseInfo(int LicenseID)
        {
            _SetDefaultValues();
            _License = DVLD_BusinessLayer.License.FindByID(LicenseID);

            llShowLicenseHistory.Enabled = true;

            lblLicenseFees.Text = _License.PaidFees.ToString();
            lblOldLicenseID.Text = _License.ID.ToString();
            lblExpirationDate.Text = DateTime.Today.AddYears(_License.LicenseClassInfo.DefaultValidityLength).ToShortDateString();
            lblTotalFees.Text = (int.Parse(lblApplicationFees.Text) + _License.PaidFees).ToString();
            txtNotes.Text = _License.Notes;

            if (!_License.IsActive)
            {
                MessageBox.Show($"Selected License Is Not Active", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (_License.IsDetained)
            {
                MessageBox.Show($"Selected License Is Detained, You Should Release It First", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!_License.IsExpired())
            {
                MessageBox.Show($"Selected License Is Not Expired Yet, It Will Expire On: {_License.ExpirationDate.ToShortDateString()}");
                return;
            }

            btnRenewLicense.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            DVLD_BusinessLayer.License license = _License.Renew(txtNotes.Text.Trim(), Global.CurrentUser.UserID);
            if (MessageBox.Show("Are You Sure About Renew Driving License?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (license != null)
                {
                    MessageBox.Show("License Renewed Successfully", "Success");
                    lblApplicationID.Text = license.ApplicationID.ToString();
                    lblRenewedLicenseID.Text = license.ID.ToString();
                    llShowLicenseInfo.Enabled = true;
                    btnRenewLicense.Enabled = false;
                }
                else
                    MessageBox.Show("Failed To Renew License", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDriverLicenseInfoForm form = new ShowDriverLicenseInfoForm(int.Parse(lblRenewedLicenseID.Text));
            form.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(_License.DriverInfo.PersonID);
            form.ShowDialog();
        }
    }
}
