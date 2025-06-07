using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DVLD.Licenses;
using System.Windows.Forms;
using DVLD.People.PersonControls;
using DVLD.People;
using DVLD.Licenses.Local_Driving_Licenses;

namespace DVLD.Applications.Detain_License
{
    public partial class ReleaseAndDetainedLicensesMangamentForm : Form
    {
        private DataTable _dtAllDetains;
        public ReleaseAndDetainedLicensesMangamentForm()
        {
            InitializeComponent();
        }

        private void ReleaseAndDetainedLicensesMangamentForm_Load(object sender, EventArgs e)
        {
            _LoadData();


            lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;

                cbFilterBy.SelectedIndex = cbFilterBy.Items.IndexOf("None");

            }
        }
        private void _LoadData()
        {
            _dtAllDetains = DetainLicense.GetAllDetains();
            dgvDetainedLicenses.DataSource = _dtAllDetains;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedItem = cbFilterBy.Text.Trim();

            switch (SelectedItem)
            {
                case "None":

                    txtFilterValue.Visible = false;
                    cbIsReleased.Visible = false;
                    _dtAllDetains.DefaultView.RowFilter = "";
                    break;

                case "Is Released":
                    txtFilterValue.Visible = false;
                    cbIsReleased.Visible = true;
                    cbIsReleased.Focus();
                    cbIsReleased.SelectedIndex = cbIsReleased.Items.IndexOf("All");
                    break;

                default:
                    cbIsReleased.Visible = false;
                    txtFilterValue.Visible = true;
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                    break;

            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IsReleased = cbIsReleased.Text.Trim();
            switch (IsReleased)
            {
                case "All":
                    break;
                case "Yes":
                    IsReleased = "1";
                    break;
                case "No":
                    IsReleased = "0";
                    break;
            }

            if (IsReleased != "All")
                _dtAllDetains.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsReleased", IsReleased);

            else
                _dtAllDetains.DefaultView.RowFilter = "";

            lblTotalRecords.Text = "#Records:    " + dgvDetainedLicenses.RowCount;
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            string txtValue = txtFilterValue.Text.Trim();

            if (cbFilterBy.Text.Trim() == "" || cbFilterBy.Text == "None" || txtValue == "")
                _dtAllDetains.DefaultView.RowFilter = "";

            else if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
            {
                if (!int.TryParse(txtValue, out int SearchValue))
                    return;

                _dtAllDetains.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtValue);
            }
            else
                _dtAllDetains.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtValue);

            lblTotalRecords.Text = "#Records:    " + dgvDetainedLicenses.RowCount;
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "ReleaseApplication ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            else
                e.Handled = char.IsPunctuation(e.KeyChar);
        }

        

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID =  DVLD_BusinessLayer.License.FindByID(LicenseID).DriverInfo.PersonID;
            ShowPersonLicensesHistoryForm frm = new ShowPersonLicensesHistoryForm(PersonID);
            ctrlPersonCard.DataUpdated += _LoadData;
            frm.ShowDialog();
        }


        private void PersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = DVLD_BusinessLayer.License.FindByID(LicenseID).DriverInfo.PersonID;

            ShowPersonInfoForm frm = new ShowPersonInfoForm(PersonID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;

            ShowDriverLicenseInfoForm frm = new ShowDriverLicenseInfoForm(LicenseID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;

            ReleaseLicenseForm frm = new ReleaseLicenseForm(LicenseID);
            frm.DataUpdated += _LoadData;
            frm.ShowDialog();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            DetainLicenseForm frm = new DetainLicenseForm();
            frm.DataUpdated += _LoadData;
            frm.ShowDialog();

        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            ReleaseLicenseForm frm = new ReleaseLicenseForm();
            frm.DataUpdated += _LoadData;
            frm.ShowDialog();
        }
    }
}
