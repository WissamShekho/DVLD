using System;
using System.CodeDom;
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
    public partial class ctrlDriverLicenseCardWithFilter : UserControl
    {
        // Create my Own EventHandler (Method Signature)
        public delegate void OnLicenseSelectedEventHandler(int LicenseID);

        // Create The Event With Handler Of Type OnLicenseSelectedEventHandler
        public static event OnLicenseSelectedEventHandler OnLicenseSelected;


        public ctrlDriverLicenseCardWithFilter()
        {
            InitializeComponent();
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get
            {
                return ctrlDriverLicenseCard1.LicenseID;
            }
        }
       
        public DVLD_BusinessLayer.License License
        {
            get
            {
                return ctrlDriverLicenseCard1.SelectedLicenseInfo;
            }
        }
        public bool FilterEnabled
        {
            set
            {
                gbFilter.Enabled = value;
            }

            get
            {
                return FilterEnabled;
            }
        }

        public void ResetInfo()
        {
            ctrlDriverLicenseCard1.ResetInfo();
        }
        public void LoadLicenseInfo(int LicenseID)
        {
/*
            if (!DVLD_BusinessLayer.License.IsLicenseExist(LicenseID))
            {
                MessageBox.Show($"No License With ID {LicenseID}!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
*/
            _LicenseID = LicenseID;
            tbLicenseID.Text = LicenseID.ToString();
            gbFilter.Enabled = false;
            ctrlDriverLicenseCard1.LoadLicenseInfo(LicenseID);
            OnLicenseSelected?.Invoke(LicenseID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;


            if (!int.TryParse(tbLicenseID.Text, out _LicenseID))
            {
                MessageBox.Show("Invalid License ID!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*
            if (DVLD_BusinessLayer.License.IsLicenseExist(_LicenseID))
            {
                MessageBox.Show($"No License With ID: {_LicenseID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            */

            LoadLicenseInfo(_LicenseID);
        }

        private void tbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
                btnSearch.PerformClick();

        }

        private void tbLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLicenseID.Text.Trim()))
            {
                epValidateLicenseID.SetError(tbLicenseID, "Enter A Valid License ID");
                e.Cancel = true;
            }
            else
            {
                epValidateLicenseID.SetError(tbLicenseID, "");
                e.Cancel = false;
            }
        }
    
        public void FilterFocus()
        {
            tbLicenseID.Focus();
        }
    
    }
}
