using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Driving_Licenses;
using DVLD.People.PersonControls;
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

namespace DVLD.Applications.Replace_Damaged_And_Lost_License
{
    public partial class ReplaceDamagedAndLostLicenseForm : Form
    {
        private DVLD_BusinessLayer.License _License;

        public ReplaceDamagedAndLostLicenseForm()
        {
            InitializeComponent();
        }

        private enum EnReplacementType { Damaged, Lost};
        private EnReplacementType enReplacement;

        private void ReplaceDamagedAndLostLicenseForm_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
            rbDamaged.Checked = true;
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            btnReplaceLicense.Enabled = false;

            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += _FillNewLicenseInfo;
        }

        private void _SetDefaultValues()
        {

            if (rbDamaged.Checked)
                enReplacement = EnReplacementType.Damaged;
            else
                enReplacement = EnReplacementType.Lost;

            lblApplicationDate.Text = DateTime.Today.ToShortDateString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblApplicationID.Text = "[???]";
            lblReplacedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";

            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnReplaceLicense.Enabled = false;
        }

        private void _FillNewLicenseInfo(int LicenseID)
        {
            _SetDefaultValues();
            
            _License = DVLD_BusinessLayer.License.FindByID(LicenseID);
            
            llShowLicenseInfo.Enabled = true;
            lblOldLicenseID.Text = _License.ID.ToString();

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

            btnReplaceLicense.Enabled = true;
        }

        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = lblTitle.Text = "Replace Damaged License";
            lblApplicationFees.Text = ApplicationType.Find(ApplicationType.EnApplicationType.ReplacementDamagedDrivingLicense).Fees.ToString();
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = lblTitle.Text = "Replace Lost License";
            lblApplicationFees.Text = ApplicationType.Find(ApplicationType.EnApplicationType.ReplacementLostDrivingLicense).Fees.ToString();
        }


        private void btnReplaceLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure About Replace Driving License?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DVLD_BusinessLayer.License license = _License.Replace(_GetIssueReason(), Global.CurrentUser.UserID);

                if (license != null)
                {
                    MessageBox.Show("License Replaced Successfully", "Success");
                    lblApplicationID.Text = license.ApplicationID.ToString();
                    lblReplacedLicenseID.Text = license.ID.ToString();
                    llShowLicenseHistory.Enabled = true;
                    btnReplaceLicense.Enabled = false;
                }
                else
                    MessageBox.Show("Failed To Replace License", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DVLD_BusinessLayer.License.EnIssueReason _GetIssueReason()
        {
            if (enReplacement == EnReplacementType.Damaged)
                return DVLD_BusinessLayer.License.EnIssueReason.DamagedReplacement;
            else
                return DVLD_BusinessLayer.License.EnIssueReason.LostReplacement;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDriverLicenseInfoForm form = new ShowDriverLicenseInfoForm(int.Parse(lblReplacedLicenseID.Text));
            form.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(_License.DriverInfo.PersonID);
            form.ShowDialog();
        }

    }
}
