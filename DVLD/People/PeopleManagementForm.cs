using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD.People
{
    public partial class PeopleManagementForm : Form
    {

        private DataTable _dtAllPeople = Person.GetAllPeople();


        public PeopleManagementForm()
        {
            InitializeComponent();
        }

        private void _RefreshData()
        {
            _dtAllPeople = Person.GetAllPeople();
            peopleDGV.DataSource = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                "FirstName", "SecondName", "ThirdName", "LastName", "DateOfBirth", "GenderCaption", "Phone", "Email", "CountryName");

            RecordsCountlbl.Text = "#Records:    " + peopleDGV.RowCount;
        }

        private void PeopleManagementForm_Load(object sender, EventArgs e)
        {
            _RefreshData();

            cbFilters.SelectedIndex = 0;

            if (peopleDGV.Rows.Count > 0)
            {
                peopleDGV.Columns["PersonID"].HeaderText = "Person ID";
                peopleDGV.Columns["PersonID"].Width = 75;

                peopleDGV.Columns["NationalNo"].HeaderText = "National No";
                peopleDGV.Columns["NationalNo"].Width = 130;


                peopleDGV.Columns["FirstName"].HeaderText = "First Name";
                peopleDGV.Columns["FirstName"].Width = 100;

                peopleDGV.Columns["SecondName"].HeaderText = "Second Name";
                peopleDGV.Columns["SecondName"].Width = 100;

                peopleDGV.Columns["ThirdName"].HeaderText = "Third Name";
                peopleDGV.Columns["ThirdName"].Width = 100;

                peopleDGV.Columns["LastName"].HeaderText = "Last Name";
                peopleDGV.Columns["LastName"].Width = 100;


                peopleDGV.Columns["DateOfBirth"].HeaderText = "Birthdate";
                peopleDGV.Columns["DateOfBirth"].Width = 150;

                peopleDGV.Columns["GenderCaption"].HeaderText = "Gender";
                peopleDGV.Columns["GenderCaption"].Width = 75;


                peopleDGV.Columns["Phone"].Width = 120;

                peopleDGV.Columns["Email"].Width = 170;
            }

        }

        private void Closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddPersonBtn_Click(object sender, EventArgs e)
        {
            AddEditPersonForm addEditPersonForm = new AddEditPersonForm();
            AddEditPersonForm.ListChanged += _RefreshData;
            addEditPersonForm.ShowDialog();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPersonBtn_Click(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPersonForm AddEditPersonForm = new AddEditPersonForm((int)peopleDGV.CurrentRow.Cells[0].Value);
            AddEditPersonForm.ListChanged += _RefreshData;
            AddEditPersonForm.ShowDialog();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to delete this person?", "deleting Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Person.Delete((int)peopleDGV.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully", "Successful Deletion", MessageBoxButtons.OK);
                    _RefreshData();
                }
                else
                    MessageBox.Show("This Person Can't Be Deleted Becuase It's has Data Linked To It", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void ShowDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonInfoForm showPersonInfoForm = new ShowPersonInfoForm((int)peopleDGV.CurrentRow.Cells[0].Value);
            showPersonInfoForm.ShowDialog();
            AddEditPersonForm.ListChanged += _RefreshData;
        }

        private void tbSearchPerson_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilters.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;


                case "Country Name":
                    FilterColumn = "CountryName";
                    break;

                default:
                    break;
            }

            if (tbSearchPerson.Text.Trim() == "" || cbFilters.Text == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";

                RecordsCountlbl.Text = peopleDGV.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbSearchPerson.Text.Trim());

            else
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, tbSearchPerson.Text.Trim());


            RecordsCountlbl.Text = peopleDGV.Rows.Count.ToString();
            peopleDGV.DataSource = _dtAllPeople;
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSearchPerson.Visible = cbFilters.Text != "None";

            if (tbSearchPerson.Visible)
            {
                tbSearchPerson.Text = "";
                tbSearchPerson.Focus();
            }
        }

        private void tbSearchPerson_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            else
                e.Handled = char.IsPunctuation(e.KeyChar);
        }
    }
}
