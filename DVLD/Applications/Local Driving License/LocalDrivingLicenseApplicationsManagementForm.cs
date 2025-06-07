using DVLD.Licenses;
using DVLD.Licenses.Local_Driving_Licenses;
using DVLD.People.PersonControls;
using DVLD.Tests;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DVLD.Applications.Local_Driving_License
{
    public partial class LocalDrivingLicenseApplicationsManagementForm : Form
    {
        private DataTable _dtAllApplications;

        public LocalDrivingLicenseApplicationsManagementForm()
        {
            InitializeComponent();
        }


        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {

            AddUpdateNewLocalDrivingLicenseApplicationForm addNewLocalDrivingLicenseApplicationForm = new AddUpdateNewLocalDrivingLicenseApplicationForm();
            AddUpdateNewLocalDrivingLicenseApplicationForm.DataUpdated += _LoadData;
            addNewLocalDrivingLicenseApplicationForm.ShowDialog();
        }

        private void _LoadData()
        {
            _dtAllApplications = LocalDrivingLicenseApplication.GetAllLocalDrivingLicensesApplications();
            dgvLocalDrivingLicenseApplicationsList.DataSource = _dtAllApplications;
            
            dgvLocalDrivingLicenseApplicationsList.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "L.D.L. App ID";
            dgvLocalDrivingLicenseApplicationsList.Columns["LocalDrivingLicenseApplicationID"].Width = 75;
            dgvLocalDrivingLicenseApplicationsList.Columns["ClassName"].HeaderText = "Class Name";
            dgvLocalDrivingLicenseApplicationsList.Columns["ClassName"].Width = 220;
            dgvLocalDrivingLicenseApplicationsList.Columns["NationalNo"].HeaderText = "National No";
            dgvLocalDrivingLicenseApplicationsList.Columns["NationalNo"].Width = 100;
            dgvLocalDrivingLicenseApplicationsList.Columns["FullName"].HeaderText = "Full Name";
            dgvLocalDrivingLicenseApplicationsList.Columns["FullName"].Width = 220;
            dgvLocalDrivingLicenseApplicationsList.Columns["ApplicationDate"].HeaderText = "Application Date";
            dgvLocalDrivingLicenseApplicationsList.Columns["ApplicationDate"].Width = 120;
            dgvLocalDrivingLicenseApplicationsList.Columns["PassedTestCount"].HeaderText = "Passed Tests";
            dgvLocalDrivingLicenseApplicationsList.Columns["PassedTestCount"].Width = 50;
            dgvLocalDrivingLicenseApplicationsList.Columns["Status"].Width = 75;

            cbFilters.SelectedIndex = cbFilters.Items.IndexOf("None");
        }

        private void LocalDrivingLicenseApplicationsManagementForm_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtAllApplications.DefaultView.RowFilter = "";
            string SelectedItem = cbFilters.Text.Trim();

            switch (SelectedItem)
            {
                case "None":
                    tbSearchApplication.Visible = false;
                    cbStatusFilter.Visible = false;
                    break;

                case "Status":
                    tbSearchApplication.Visible = false;
                    cbStatusFilter.Visible = true;
                    cbStatusFilter.Focus();
                    cbStatusFilter.SelectedIndex = cbStatusFilter.Items.IndexOf("All");
                    break;

                default:
                    tbSearchApplication.Visible = true;
                    cbStatusFilter.Visible = false;
                    tbSearchApplication.Text = "";
                    tbSearchApplication.Focus();
                    break;

            }
        }

        private void tbSearchApplication_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilters.Text)
            {
                case "L.D.L. App ID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "Class Name":
                    FilterColumn = "ClassName";
                    break;
                case "National Number":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Passed Tests":
                    FilterColumn = "PassedTestCount";
                    break;
                default:
                    break;
            }


            if (cbFilters.Text.Trim() == "" || cbFilters.Text == "None" || tbSearchApplication.Text.Trim() == "")
                _dtAllApplications.DefaultView.RowFilter = "";


            else if (FilterColumn == "LocalDrivingLicenseApplicationID" || FilterColumn == "PassedTestCount")
            {
                if (!int.TryParse(tbSearchApplication.Text, out int SearchValue))
                    return;
                
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbSearchApplication.Text.Trim());
            }
            else
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, tbSearchApplication.Text.Trim());

            RecordsCountlbl.Text = "#Records:    " + dgvLocalDrivingLicenseApplicationsList.RowCount;
        }

        private void tbSearchApplication_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.Text == "L.D.L. App ID" || cbFilters.Text == "Passed Tests")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            else
                e.Handled = char.IsPunctuation(e.KeyChar); 
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StatusValue = cbStatusFilter.Text.Trim();
        
            if (StatusValue != "All")
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", "Status", StatusValue);

            else
                _dtAllApplications.DefaultView.RowFilter = "";

            RecordsCountlbl.Text = "#Records:    " + dgvLocalDrivingLicenseApplicationsList.RowCount;
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;
            ShowLocalDrivingLicenseApplicationInfoForm form = new ShowLocalDrivingLicenseApplicationInfoForm(AppID);
            ShowLocalDrivingLicenseApplicationInfoForm.DataUpdated += _LoadData;
            form.ShowDialog();
        }

        private void cmsApplicationsList_Opening(object sender, CancelEventArgs e)
        {
            editApplicationToolStripMenuItem.Enabled = _IsEditingAllowed();
            cancelApplicationToolStripMenuItem.Enabled = _IsStatusNew();
            scheduleTestsToolStripMenuItem.Enabled = _IsSchedulingTestsAllowed();
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = _IsIssuingLinceseAllowed();
            showLicenseToolStripMenuItem.Enabled = _IsLicenseIssued();
            showPersonLicensesHistoryToolStripMenuItem.Enabled = _DoesPersonHaveAnyLicense();
            deleteApplicationToolStripMenuItem.Enabled = _IsStatusNew();
        }

        private bool _DoesPersonHaveAnyLicense()
        {
            int LDLappID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value;
            LocalDrivingLicenseApplication localDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByID(LDLappID);

            return (Driver.IsDriverExistByPersonID(localDrivingLicenseApplication.ApplicationInfo.PersonID));
        }

        private bool _IsLicenseIssued()
        {
            return dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells["Status"].Value.ToString() == "Completed";

        }

        private bool _IsIssuingLinceseAllowed()
        {
            return (!_IsSchedulingTestsAllowed() && _IsStatusNew());
        }

        private bool _IsSchedulingTestsAllowed()
        {
            int PassedTests = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells["PassedTestCount"].Value;

            return (PassedTests < 3 && _IsStatusNew());
        }

        private bool _IsStatusNew()
        {
            return dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells["Status"].Value.ToString() == "New"; 
        }

        private bool _IsEditingAllowed()
        {
            // we can't Edit LicenseClass After Passing the Second Test
            int PassedTests = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells["PassedTestCount"].Value;
            return (PassedTests < 2 && _IsStatusNew());
        }

        private void scheduleTestsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (!_IsSchedulingTestsAllowed())
                return;
         
            visionTestToolStripMenuItem.Enabled = false;
            writtenTestToolStripMenuItem.Enabled = false;
            streetTestToolStripMenuItem.Enabled = false;

            int PassedTests = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells["PassedTestCount"].Value;

            switch (PassedTests)
            {
                case 0:
                    visionTestToolStripMenuItem.Enabled = true;
                    break;

                case 1:
                    writtenTestToolStripMenuItem.Enabled = true;
                    break;

                case 2:
                    streetTestToolStripMenuItem.Enabled = true;
                    break;

                case 3:
                    break;
            }
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;
            AddUpdateNewLocalDrivingLicenseApplicationForm form = new AddUpdateNewLocalDrivingLicenseApplicationForm(AppID);
            AddUpdateNewLocalDrivingLicenseApplicationForm.DataUpdated += _LoadData;
            form.ShowDialog();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;

            LocalDrivingLicenseApplication LocalApp = LocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);

            if (LocalApp != null)
            {
                if (LocalApp.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _LoadData();
                }
                else
                {
                    MessageBox.Show("Could not Delete application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;

            LocalDrivingLicenseApplication LocalApp = LocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);

            if (LocalApp != null)
            {
                if (LocalApp.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _LoadData();
                }
                else
                {
                    MessageBox.Show("Could not cancel application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;
            TestsAppointmentsManagementForm Form = new TestsAppointmentsManagementForm(LDLAppID, TestType.EnTestType.VisionTest);
            TestsAppointmentsManagementForm.ListUpdated += _LoadData;
            Form.ShowDialog();
        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;
            TestsAppointmentsManagementForm Form = new TestsAppointmentsManagementForm(LDLAppID, TestType.EnTestType.WrittenTest);
            TestsAppointmentsManagementForm.ListUpdated += _LoadData;
            Form.ShowDialog();
        }

        private void streetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;
            TestsAppointmentsManagementForm Form = new TestsAppointmentsManagementForm(LDLAppID, TestType.EnTestType.StreetTest);
            TestsAppointmentsManagementForm.ListUpdated += _LoadData;
            Form.ShowDialog();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;
            IssueDrivingLicenseForTheFirstTimeForm form = new IssueDrivingLicenseForTheFirstTimeForm(LDLAppID);
            IssueDrivingLicenseForTheFirstTimeForm.DataUpdated += _LoadData;
            form.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;
            DVLD_BusinessLayer.License license = DVLD_BusinessLayer.License.FindByLocalDrivingLicenseApplicationID(LDLAppID);

            if (license == null)
            {
                MessageBox.Show($"No License For Local Driving License Application ID: {LDLAppID}", "Validation");
                return;
            }

            ShowDriverLicenseInfoForm form = new ShowDriverLicenseInfoForm(license.ID);
            form.ShowDialog();
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLocalDrivingLicenseApplicationsList.CurrentRow.Cells[0].Value;
            LocalDrivingLicenseApplication LDLApp = LocalDrivingLicenseApplication.FindByID(LDLAppID);
            int PersonID = LDLApp.ApplicationInfo.PersonID;

            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(PersonID);
            ctrlPersonCard.DataUpdated += _LoadData;
            form.ShowDialog();
        }
    }
}
