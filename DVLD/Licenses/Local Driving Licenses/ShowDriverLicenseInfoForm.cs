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
    public partial class ShowDriverLicenseInfoForm : Form
    {
        private int _LicenseID = -1;
        public ShowDriverLicenseInfoForm(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void ShowDriverLicenseInfoForm_Load(object sender, EventArgs e)
        {
            if (DVLD_BusinessLayer.License.IsLicenseExist(_LicenseID))
                ctrlDriverLicenseCard1.LoadLicenseInfo(_LicenseID);
            else
                MessageBox.Show($"No License With ID {_LicenseID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
