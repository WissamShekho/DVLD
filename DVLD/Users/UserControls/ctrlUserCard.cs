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


namespace DVLD.Users.UserControls
{

    public partial class ctrlUserCard : UserControl
    {
        private User _User;
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            if (!User.IsUserExist(UserID))
            {
                MessageBox.Show($"the User With ID {UserID} Not Exist");
                ctrlPersonCard1.ResetPersonInfo();
                return;
            }

            _User = User.FindByUserID(UserID);

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {

            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive == true ? "Yes" : "No";
        }

        public void ResetUserInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();

            lblUserID.Text = "????";
            lblUserName.Text = "????";
            lblIsActive.Text = "????";
        }
    }
}
