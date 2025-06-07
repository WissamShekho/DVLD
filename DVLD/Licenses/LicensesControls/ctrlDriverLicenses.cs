using DVLD.Licenses.Local_Driving_Licenses;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.LicensesControls
{
    
    public partial class ctrlDriverLicenses : UserControl
    {
        private DataTable _dtLocalLicenses = new DataTable();
        private DataTable _dtInternationalLicenses = new DataTable();
        private Driver _driver;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadDriverLicenses(int DriverID)
        {
            _driver = Driver.FindDriverByID(DriverID);
            if (_driver == null)
            {
                MessageBox.Show($"No Driver With ID: {DriverID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            _LoadData();
            _SetTablesSettings();
        }

        public void LoadPersonLicenses(int PersonID)
        {
            _driver = Driver.FindDriverByPersonID(PersonID);
            if (_driver == null)
            {
                MessageBox.Show($"No Driver Recorded With Person ID: {PersonID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            _LoadData();
            _SetTablesSettings();
        }

        public void Clear()
        {
            _dtLocalLicenses.Clear();
            _dtInternationalLicenses.Clear();
        }

        private void _SetTablesSettings()
        {
            if (dgvLocalLicenses.Rows.Count > 0)
            {
                dgvLocalLicenses.Columns["LicenseID"].HeaderText = "License ID";
                dgvLocalLicenses.Columns["LicenseID"].Width = 100;

                dgvLocalLicenses.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvLocalLicenses.Columns["ApplicationID"].Width = 100;

                dgvLocalLicenses.Columns["ClassName"].HeaderText = "Class Name";
                dgvLocalLicenses.Columns["ClassName"].Width = 200;

                dgvLocalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns["IssueDate"].Width = 125;

                dgvLocalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns["ExpirationDate"].Width = 125;

                dgvLocalLicenses.Columns["IsActive"].HeaderText = "Is Active";
                dgvLocalLicenses.Columns["IsActive"].Width = 50;
            }
            lblLocalLicensesRecords.Text = "#Records: " + dgvLocalLicenses.Rows.Count.ToString();



            if (dgvInternationalLicenses.Rows.Count > 0)
            {



                dgvInternationalLicenses.Columns["InternationalLicenseID"].HeaderText = "International License ID";
                dgvInternationalLicenses.Columns["InternationalLicenseID"].Width = 200;

                dgvInternationalLicenses.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns["ApplicationID"].Width = 100;

                dgvInternationalLicenses.Columns["DriverID"].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns["DriverID"].Width = 150;

                dgvInternationalLicenses.Columns["LocalLicenseID"].HeaderText = "Local License ID";
                dgvInternationalLicenses.Columns["LocalLicenseID"].Width = 125;


                dgvInternationalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns["IssueDate"].Width = 125;

                dgvInternationalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns["ExpirationDate"].Width = 125;

                dgvInternationalLicenses.Columns["IsActive"].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns["IsActive"].Width = 50;


            }
            lblInternationalLicensesRecords.Text = "#Records: " + dgvInternationalLicenses.Rows.Count.ToString();

        }

        private void _LoadData()
        {
            _dtLocalLicenses = _driver.GetLocalLicenses();
            _dtInternationalLicenses = _driver.GetInternationalLicenses();

            dgvLocalLicenses.DataSource = _dtLocalLicenses;
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;
        }

        private void showLocalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicenses.CurrentRow.Cells["LicenseID"].Value;
            ShowDriverLicenseInfoForm form = new ShowDriverLicenseInfoForm(LicenseID);
            form.ShowDialog();
        }
    }
}
