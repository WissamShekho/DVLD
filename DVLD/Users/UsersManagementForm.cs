using DVLD.People;
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

namespace DVLD.Users
{
    public partial class UsersManagementForm : Form
    {
        private DataTable _dtAllUsers = User.GetAllUsers();


        public UsersManagementForm()
        {
            InitializeComponent();
        }

        private void UsersManagementForm_Load(object sender, EventArgs e)
        {
            _RefreshData();
            
            cbFilters.SelectedIndex = 0;

             if (dgvUsers.Rows.Count > 0)
             {
                 dgvUsers.Columns["UserID"].HeaderText = "User ID";
                 dgvUsers.Columns["UserID"].Width = 75;

                 dgvUsers.Columns["PersonID"].HeaderText = "PersonID";
                 dgvUsers.Columns["PersonID"].Width = 75;


                 dgvUsers.Columns["FullName"].HeaderText = "Full Name";
                 dgvUsers.Columns["FullName"].Width = 200;

                 dgvUsers.Columns["UserName"].HeaderText = "Username";
                 dgvUsers.Columns["UserName"].Width = 120;

                 dgvUsers.Columns["IsActive"].HeaderText = "Is Active";
                 dgvUsers.Columns["IsActive"].Width = 100;
             }

            cbIsActiveFilters.Visible = cbFilters.Text.Trim() == "Is Active";

            cbFilters.SelectedIndex = 0;
        }

        private void _RefreshData()
        {
            _dtAllUsers = User.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;

            RecordsCountlbl.Text = "#Records:    " + dgvUsers.RowCount;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddEditUserForm addEditUserForm = new AddEditUserForm();
            AddEditPersonForm.ListChanged += _RefreshData;
            addEditUserForm.ShowDialog();
        }


        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtAllUsers.DefaultView.RowFilter = "";

            if (cbFilters.Text.Trim() == "None")
            {
                tbSearchUser.Visible = false;
                cbIsActiveFilters.Visible = false;
            }
            else if (cbFilters.Text.Trim() == "Is Active")
            {
                tbSearchUser.Visible = false;
                cbIsActiveFilters.Visible = true;
                cbIsActiveFilters.SelectedIndex = cbIsActiveFilters.Items.IndexOf("All");
            }
            else
            {
                tbSearchUser.Visible = true;
                cbIsActiveFilters.Visible = false;
                tbSearchUser.Text = "";
                tbSearchUser.Focus();
            }
        }

        private void tbSearchUser_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilters.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;


                case "Username":
                    FilterColumn = "UserName";
                    break;

                default:
                    break;
            }


            if (cbFilters.Text.Trim() == "" || cbFilters.Text == "None")
                _dtAllUsers.DefaultView.RowFilter = "";

            else if (FilterColumn == "PersonID" || FilterColumn == "UserID")
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbSearchUser.Text.Trim());

            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, tbSearchUser.Text.Trim());


            RecordsCountlbl.Text = "#Records:    " + dgvUsers.RowCount;
            dgvUsers.DataSource = _dtAllUsers;
        }

        private void tbSearchUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.Text == "Person ID" || cbFilters.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            else
                e.Handled = char.IsPunctuation(e.KeyChar);
        }


        private void Closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbIsActiveFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string Value = cbIsActiveFilters.Text.Trim() == "Yes" ? "1" : "0";

            if (cbIsActiveFilters.Text != "All")
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, Value);

            else
                _dtAllUsers.DefaultView.RowFilter = "";



            RecordsCountlbl.Text = "#Records:    " + dgvUsers.RowCount;
        }

        private void ShowUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserInfoForm showUserInfoForm = new ShowUserInfoForm((int)dgvUsers.CurrentRow.Cells[0].Value);
            showUserInfoForm.ShowDialog();
            AddEditPersonForm.ListChanged += _RefreshData;
        }

        private void AddNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddUser_Click(sender, e);
        }

        private void EditUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditUserForm addEditUserForm = new AddEditUserForm((int)dgvUsers.CurrentRow.Cells[0].Value);
            AddEditUserForm.ListChanged += _RefreshData;
            addEditUserForm.ShowDialog();
        }

        private void DeleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to delete this User?", "deleting User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Person.Delete((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully", "Successful", MessageBoxButtons.OK);
                    _RefreshData();
                }
                else
                    MessageBox.Show("This User Can't Be Deleted Becuase It's has Data Linked To It", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ChangeUserPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm((int)dgvUsers.CurrentRow.Cells[0].Value);
            changePasswordForm.ShowDialog();

        }
    }
}
