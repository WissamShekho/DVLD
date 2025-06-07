using DVLD.Licenses;
using DVLD.Licenses.Local_Driving_Licenses;
using DVLD.People;
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

namespace DVLD.Drivers
{
    public partial class DriversListForm : Form
    {
        public DriversListForm()
        {
            InitializeComponent();
        }

        private DataTable _dtAllDrivers;

        private void cmsUsersList_Opening(object sender, CancelEventArgs e)
        {

        }

        private void DriversListForm_Load(object sender, EventArgs e)
        {
            _LoadData();

            dgvDrivers.Columns["DriverID"].HeaderText = "Driver ID";
            dgvDrivers.Columns["DriverID"].Width = 75;

            dgvDrivers.Columns["PersonID"].HeaderText = "Person ID";
            dgvDrivers.Columns["PersonID"].Width = 75;

            dgvDrivers.Columns["NationalNo"].HeaderText = "National Number";
            dgvDrivers.Columns["NationalNo"].Width = 150;

            dgvDrivers.Columns["FullName"].HeaderText = "Full Name";
            dgvDrivers.Columns["FullName"].Width = 150;


            dgvDrivers.Columns["CreatedDate"].HeaderText = "Created Date";
            dgvDrivers.Columns["CreatedDate"].Width = 100;

            dgvDrivers.Columns["NumberofActiveLicenses"].HeaderText = "Number Of Active Licenses";

            cbFilters.SelectedIndex = cbFilters.Items.IndexOf("None");
        }

        private void _LoadData()
        {
            _dtAllDrivers = Driver.GetAllDrivers();
            dgvDrivers.DataSource = _dtAllDrivers;
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtAllDrivers.DefaultView.RowFilter = "";

            if (cbFilters.Text.Trim() == "None")
                tbSearchDriver.Visible = false;

            else
            {
                tbSearchDriver.Visible = true;
                tbSearchDriver.Text = "";
                tbSearchDriver.Focus();
            }
        }

        private void tbSearchDriver_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilters.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;


                case "National Number":
                    FilterColumn = "NationalNo";
                    break;

                default:
                    break;
            }


            if (cbFilters.Text.Trim() == "" || cbFilters.Text == "None" || tbSearchDriver.Text.Trim() == "")
                _dtAllDrivers.DefaultView.RowFilter = "";

            else if (FilterColumn == "PersonID" || FilterColumn == "DriverID")
            {
                if (!int.TryParse(tbSearchDriver.Text, out int SearchValue))
                    return;

                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbSearchDriver.Text.Trim());
            }

            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, tbSearchDriver.Text.Trim());


            RecordsCountlbl.Text = "#Records:    " + dgvDrivers.RowCount;
        }

        private void tbSearchDriver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.Text == "Person ID" || cbFilters.Text == "Driver ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            else
                e.Handled = char.IsPunctuation(e.KeyChar);
        }

        private void Closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = int.Parse(dgvDrivers.CurrentRow.Cells["PersonID"].Value.ToString());
            ShowPersonInfoForm form = new ShowPersonInfoForm(personID);
            ShowPersonInfoForm.DataUpdated += _LoadData;
            form.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = int.Parse(dgvDrivers.CurrentRow.Cells["PersonID"].Value.ToString());
            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(personID);
            ctrlPersonCard.DataUpdated += _LoadData;
            form.ShowDialog();
        }
    }
}
