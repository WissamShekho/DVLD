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
    public partial class ShowUserInfoForm : Form
    {

        private int _UserID = -1;

        public ShowUserInfoForm(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }

        private void UserInfoForm_Load(object sender, EventArgs e)
        {
            if (!User.IsUserExist(_UserID))
            {
                MessageBox.Show($"The User With User ID: {_UserID} Not Found");
                return;
            }
            ctrlUserCard1.LoadUserInfo(_UserID);

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
