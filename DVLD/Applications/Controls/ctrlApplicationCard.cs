using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationCard : UserControl
    {
        public ctrlApplicationCard()
        {
            InitializeComponent();
        }

        public static Action DataUpdated;
        private int _ApplicationID = -1;
        private DVLD_BusinessLayer.Application _Application = null;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        public DVLD_BusinessLayer.Application SelectedApplicationInfo
        {
            get { return _Application; }
        }

        public void LoadApplicationInfo(int AppID)
        {
            _ApplicationID = AppID;
            _Application = DVLD_BusinessLayer.Application.Find(_ApplicationID);
            if (_Application == null)
            {
                ResetInfo();
                MessageBox.Show("No Application with ID = " + AppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();
        }

        public void ResetInfo()
        {
            lblApplicationID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblFees.Text = "[???]";
            lblApplicationTypeTitle.Text = "[???]";
            lblPersonFullName.Text = "[???]";
            lblDate.Text = "[???]";
            lblStatusDate.Text = "[???]";
            lblUserName.Text = "[???]";

            llPersonInfo.Enabled = false;
        }

        private void _FillApplicationInfo()
        {
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.enApplicationStatus.ToString();
            lblFees.Text = _Application.PaidFees.ToString();
            lblApplicationTypeTitle.Text = _Application.ApplicationTypeInfo.Title;
            lblPersonFullName.Text = _Application.PersonInfo.FullName;
            lblDate.Text = _Application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _Application.LastStatusDate.ToShortDateString();
            lblUserName.Text = _Application.UserInfo.UserName;

        }

        private void llPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddEditPersonForm addEditPersonForm = new AddEditPersonForm(_Application.PersonID);
            AddEditPersonForm.ListChanged += _DataBack;
            addEditPersonForm.ShowDialog();
        }

        private void _DataBack()
        {
            DataUpdated?.Invoke();
            LoadApplicationInfo(_ApplicationID);
        }
    }
}
