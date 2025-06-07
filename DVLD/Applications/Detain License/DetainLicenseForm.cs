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
    public partial class DetainLicenseForm : Form
    {
        public Action DataUpdated;
        private DVLD_BusinessLayer.License _License;

        public DetainLicenseForm()
        {
            InitializeComponent();
        }

        private void DetainLicenseForm_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            btnDetainLicense.Enabled = false;

            lblDetainDate.Text = DateTime.Today.ToShortDateString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;

            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += _FillNewLicenseInfo;
        }
        
        private void _SetDefaultValues()
        {
            lblDetainDate.Text = DateTime.Today.ToShortDateString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = "[???]";
            tbFineFees.Clear();

            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnDetainLicense.Enabled = false;
            tbFineFees.Enabled = false;
        }

        private void _FillNewLicenseInfo(int LicenseID)
        {
            _SetDefaultValues();

            _License = DVLD_BusinessLayer.License.FindByID(LicenseID);

            llShowLicenseHistory.Enabled = true;
            lblLicenseID.Text = _License.ID.ToString();

            if (!_License.IsActive)
            {
                MessageBox.Show($"Selected License Is Not Active", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (_License.IsDetained)
            {
                MessageBox.Show($"Selected License Is Already Detained", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }




            btnDetainLicense.Enabled = true;
            tbFineFees.Enabled = true;
        }


        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                return;
            }

            if (MessageBox.Show("Are You Sure About Detain Driving License?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_License.Detain(float.Parse(tbFineFees.Text), Global.CurrentUser.UserID))
                {
                    MessageBox.Show("License Detained Successfully", "Success");
                    lblDetainID.Text = _License.DetainInfo.ID.ToString();
                    lblDetainDate.Text = _License.DetainInfo.DetainDate.ToShortDateString();
                    llShowLicenseInfo.Enabled = true;
                    tbFineFees.Enabled = false;
                    btnDetainLicense.Enabled = false;

                    DataUpdated?.Invoke();
                }
                else
                    MessageBox.Show("Failed To Detain License", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDriverLicenseInfoForm form = new ShowDriverLicenseInfoForm(int.Parse(lblLicenseID.Text));
            form.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(_License.DriverInfo.PersonID);
            form.ShowDialog();
        }

        private void tbFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbFineFees.Text))
            {
                epFineFeesValidation.SetError(tbFineFees, "This Field Should Not Be Empty!");
                e.Cancel = true;
            }
            else
            {
                epFineFeesValidation.SetError(tbFineFees, "");
                e.Cancel = false;
            }
        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
