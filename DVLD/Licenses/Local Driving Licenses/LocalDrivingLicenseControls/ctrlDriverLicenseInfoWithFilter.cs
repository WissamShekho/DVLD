using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Driving_Licenses.LocalDrivingLicenseControls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {

        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }

        public DVLD_BusinessLayer.License SelectedLicenseInfo
        { 
            get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }

        public void LoadLicenseInfo(int LicenseID)
        { 


            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadInfo(LicenseID);
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected?.Invoke(LicenseID);
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }

            _LicenseID = int.Parse(txtLicenseID.Text);
            
            if (!DVLD_BusinessLayer.License.IsLicenseExist(_LicenseID))
            {
                MessageBox.Show($"No License With ID: {_LicenseID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadLicenseInfo(_LicenseID);
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        public void ResetInfo()
        {
            ctrlDriverLicenseInfo1.ResetInfo();
        }

    }
}
