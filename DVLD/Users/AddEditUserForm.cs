using DVLD.Global_Classes;
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

namespace DVLD.Users
{
    public partial class AddEditUserForm : Form
    {

        public static Action ListChanged;
        public event EventHandler<int> DataBack;

        private int _UserID = -1;
        private User _User;

        private enum EnFormMode { AddNew, Update };
        EnFormMode _FormMode;

        public AddEditUserForm()
        {
            InitializeComponent();
            _FormMode = EnFormMode.AddNew;
        }

        public AddEditUserForm(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;

            _FormMode = EnFormMode.Update;
        }


        private void AddEditUserForm_Load(object sender, EventArgs e)
        {
            SetDefaultValues();

            if (_FormMode == EnFormMode.Update)
            {
                _User = User.FindByUserID(_UserID);

                if (_User == null)
                {
                    MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }
                UpdateModeSettingsActivate();
            }
            else
                AddNewModeSettingsActivate();
        }

        private void SetDefaultValues()
        {
            tpLoginInfo.Enabled = ctrlPersonCardWithFilter1.PersonID != -1;
            btnSave.Enabled = ctrlPersonCardWithFilter1.PersonID != -1;
        }

        private void AddNewModeSettingsActivate()
        {
            lblFormTitle.Text = "Add New User";
            this.Text = "Add New User";


            lblUserID.Text = "????";
            tbUserName.Text = "";
            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
            chkbIsActive.Checked = true;

            ctrlPersonCardWithFilter1.FilterFocus();



            _User = new User();
            ctrlPersonCardWithFilter1.FilterEnabled = true;
            ctrlPersonCardWithFilter1.ResetPersonInfo();
        }

        private void UpdateModeSettingsActivate()
        {
            lblFormTitle.Text = "Update User";
            this.Text = "Update User";

            ctrlPersonCardWithFilter1.FilterEnabled = false;

            //Loading User Data
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();
            tbUserName.Text = _User.UserName;
            tbPassword.Text = _User.Password;
            tbConfirmPassword.Text = _User.Password;
            chkbIsActive.Checked = _User.IsActive;

            btnNext.Focus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.SelectedPersonInfo == null)
            {
                MessageBox.Show("You Should Select A Person First");
                return;
            }

            if (_FormMode == EnFormMode.AddNew && User.IsUserExistByPersonID(ctrlPersonCardWithFilter1.PersonID))
            {
                MessageBox.Show("This Person Is Already A User, Select Another Person");
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }

            tcUserInfo.SelectTab("tpLoginInfo");
            tpLoginInfo.Enabled = ctrlPersonCardWithFilter1.PersonID != -1;
            btnSave.Enabled = ctrlPersonCardWithFilter1.PersonID != -1;
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserName.Text.Trim()))
            {
                e.Cancel = true;
                ErrorProvider1.SetError(tbUserName, "This Field Should Not Be Empty");
                return;
            }

            else if (User.IsUserExist(tbUserName.Text.Trim()) && tbUserName.Text != _User.UserName)
            {
                e.Cancel = true;
                ErrorProvider1.SetError(tbUserName, "This Username Is Already Used, Select Another One");
                return;
            }
            else
            {
                e.Cancel = false;
                ErrorProvider1.SetError(tbUserName, "");
            }

        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                ErrorProvider1.SetError(tbConfirmPassword, "This Field Should Not Be Empty");

            }

            else if (tbConfirmPassword.Text != tbPassword.Text)
            {
                e.Cancel = true;
                ErrorProvider1.SetError(tbConfirmPassword, "Password Not Matched");
            }
            else
            {
                e.Cancel = false;
                ErrorProvider1.SetError(tbConfirmPassword, "");
            }

        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                e.Cancel = true;
                ErrorProvider1.SetError(tbPassword, "This Field Should Not Be Empty");
            }
            else
            {
                e.Cancel = false;
                ErrorProvider1.SetError(tbConfirmPassword, "");
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

            _User.PersonInfo = ctrlPersonCardWithFilter1.SelectedPersonInfo;
            _User.PersonID = _User.PersonInfo.ID;

            _User.UserName = tbUserName.Text;
            _User.Password = Global.HashingEncryption(tbPassword.Text.Trim()); // Hashing Password
            _User.IsActive = chkbIsActive.Checked;

            if (_User.Save())
            {
                MessageBox.Show("User Saved Successfully");

                lblFormTitle.Text = "Update User";
                this.Text = "Update User";
                lblUserID.Text = _User.UserID.ToString();
                _FormMode = EnFormMode.Update;

                DataBack?.Invoke(this, _User.UserID);
                ListChanged?.Invoke();
            }
            else
                MessageBox.Show("Error: Data Is Not Saved!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
