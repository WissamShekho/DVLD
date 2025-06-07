using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Driving_Licenses.LocalDrivingLicenseControls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID;
        private DVLD_BusinessLayer.License _License;

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();

        }

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public DVLD_BusinessLayer.License SelectedLicenseInfo
        { get { return _License; } }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = DVLD_BusinessLayer.License.FindByID(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblLicenseID.Text = _License.ID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNumber;
            lblGender.Text = _License.DriverInfo.PersonInfo.Gender.ToString();
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();



        }

        public void ResetInfo()
        {
            lblClass.Text = "[????]";
            lblFullName.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "[??/??/????]";
            lblIssueReason.Text = "[????]";
            lblNotes.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblDateOfBirth.Text = "[??/??/????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[??/??/????]";
            lblIsDetained.Text = "[????]";

            pbPersonImage.ImageLocation = Resources.Male_5121.ToString();
        }
    }
}
