using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD.People.PersonControls;
using DVLD_BusinessLayer;

namespace DVLD.Users
{

    public partial class ChangePasswordForm : Form
    {
        private int _UserID;
        private User _User;
        private string _HashedPassword;

        public ChangePasswordForm(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            _User = User.FindByUserID(_UserID);
            
            if (_User == null)
            {
                MessageBox.Show($"the User With ID {_UserID} Not Exist");
                ctrlUserCard1.ResetUserInfo();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_UserID);
            tbCurrentPassword.Focus();
        }

        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCurrentPassword.Text.Trim()) || string.IsNullOrWhiteSpace(tbCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbCurrentPassword, "this Field Should Not Be Empty");

            }
            else if (tbCurrentPassword.Text.Trim() != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(tbCurrentPassword, "Current Password Is Incorrect!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbCurrentPassword, "");
            }
        }

        private void tbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            string NewPassword = tbNewPassword.Text.Trim();
            _HashedPassword = Global.HashingEncryption(NewPassword);// Hashing Password

            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrWhiteSpace(NewPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNewPassword, "this Field Should Not Be Empty");

            }
            else if (_HashedPassword == _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNewPassword, "New Password Should Not Be Same As The Current Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbNewPassword, "");
            }
        }

        private void tbConfrimNewPassword_Validating(object sender, CancelEventArgs e)
        {
            string ConfirmNewPassword = tbConfirmNewPassword.Text.Trim();
            if (string.IsNullOrEmpty(ConfirmNewPassword) || string.IsNullOrWhiteSpace(ConfirmNewPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmNewPassword, "this Field Should Not Be Empty");

            }
            else if (ConfirmNewPassword != tbNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmNewPassword, "Confirm New Password Field Should Match The New Password Field");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmNewPassword, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields Are not Filled Correctly, Put the Mouse On the Red Icon to See the Errors",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _User.Password = _HashedPassword; // HashedPassword

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully");

            }
            else
                MessageBox.Show("Error: Could Not Change Password!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
