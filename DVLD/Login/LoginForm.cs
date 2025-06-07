using DVLD.Global_Classes;
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

namespace DVLD.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            MainMenuForm.WhenFormClosed += ReOpenLoginForm;
        }
        
        public void CheckStoredCredintial()
        {
            string UserName = "", Password = "";

            if (Global.GetStoredRegister(ref UserName, ref Password))
            {
                tbUserName.Text = UserName;
                tbPassword.Text = Password;
                chkbRememberMe.Checked = true;
            }
            else
                chkbRememberMe.Checked = false;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CheckStoredCredintial();
            btnLogin.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string HashedPassword = Global.HashingEncryption(tbPassword.Text.Trim()); // Hashing Password
            User user = User.FindByUserNameAndPassword(tbUserName.Text.Trim(), HashedPassword);

            if (user != null)
            {
                if (user.IsActive)
                {
                    if (chkbRememberMe.Checked)
                        Global.SaveRegister(tbUserName.Text.Trim(), tbPassword.Text.Trim());
                    
                    else
                        Global.SaveRegister("", "");


                    Global.CurrentUser = user;
                    MainMenuForm mainMenuForm = new MainMenuForm();
                    mainMenuForm.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Your Account Is Valid But Not Active, Please Contact Your Admin", "Login Failed"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Invalid Username Or Password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    
        private void ReOpenLoginForm()
        {
            tbUserName.Text = "";
            tbPassword.Text = "";
            chkbRememberMe.Checked = true;
         
            this.Show();

            CheckStoredCredintial();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
