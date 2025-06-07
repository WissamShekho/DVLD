using DVLD.Properties;
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
    public partial class ctrlDriverLicenseCard : UserControl
    {
        public ctrlDriverLicenseCard()
        {
            InitializeComponent();
        }


        private int _LicenseID = -1;
        private DVLD_BusinessLayer.License _License;

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public DVLD_BusinessLayer.License SelectedLicenseInfo
        {
            get { return _License; }
        }

        public void LoadLicenseInfo(int LicenseID)
        {
                _LicenseID = LicenseID;
                _License = DVLD_BusinessLayer.License.FindByID(_LicenseID);
                if (_License == null)
                {
                    ResetInfo();
                    MessageBox.Show("No License with ID = " + LicenseID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _FillLicenseInfo();
        }

        public void ResetInfo()
        {
            lblLicenseClass.Text = "[????]";
            lblDriverName.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "[??/??/????]";
            lblIssueReason.Text = "[????]";
            lblNotes.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblBirthDate.Text = "[??/??/????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[??/??/????]";
            lblIsDetained.Text = "[????]";

           pbPersonImage.ImageLocation = Resources.Male_5121.ToString();
        }

        private void _FillLicenseInfo()
        {
            lblLicenseClass.Text = _License.LicenseClassInfo.ClassName;
            lblDriverName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _License.ID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNumber;
            lblGender.Text = _License.DriverInfo.PersonInfo.Gender.ToString();
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            lblIsActive.Text = _License.IsActive? "Yes" : "No";
            lblBirthDate.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = _License.IsDetained? "Yes" : "No";
            
            if (_License.DriverInfo.PersonInfo.ImagePath != "")
                pbPersonImage.ImageLocation = _License.DriverInfo.PersonInfo.ImagePath;
            else
            {
                if (_License.DriverInfo.PersonInfo.Gender == DVLD_BusinessLayer.Person.EnGender.Male)
                    pbPersonImage.ImageLocation = Resources.Male_5121.ToString();
                else
                    pbPersonImage.ImageLocation = Resources.Female_5121.ToString();
            }
        }

    }
}
