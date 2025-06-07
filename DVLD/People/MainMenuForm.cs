using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Applications.ApplicationsTypes;
using DVLD.Applications.Detain_License;
using DVLD.Applications.Local_Driving_License;
using DVLD.Applications.Renew_Local_Driving_License;
using DVLD.Applications.Replace_Damaged_And_Lost_License;
using DVLD.Drivers;
using DVLD.Global_Classes;
using DVLD.People;
using DVLD.Tests.TestsTypes;
using DVLD.Users;
using DVLD.Applications.International_Driving_License;

namespace DVLD
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        public static Action WhenFormClosed;

        private void PeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PeopleManagementForm peopleManagementForm = new PeopleManagementForm();
            peopleManagementForm.ShowDialog();
        }


        private void DriversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DriversListForm driversListForm = new DriversListForm();
            driversListForm.ShowDialog();
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
           UsersManagementForm usersManagementForm = new UsersManagementForm();
            usersManagementForm.ShowDialog();
        }



        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.CurrentUser = null;
            WhenFormClosed?.Invoke();
            this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserInfoForm showUserInfoForm = new ShowUserInfoForm(Global.CurrentUser.UserID);
            showUserInfoForm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(Global.CurrentUser.UserID);
            changePasswordForm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationsTypesManagementForm applicationsTypesManagementForm = new ApplicationsTypesManagementForm();
            applicationsTypesManagementForm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestsTypesManagementForm testsTypesManagementForm = new TestsTypesManagementForm();
            testsTypesManagementForm.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateNewLocalDrivingLicenseApplicationForm addNewLocalDrivingLicenseApplicationForm = new AddUpdateNewLocalDrivingLicenseApplicationForm();
            addNewLocalDrivingLicenseApplicationForm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationsManagementForm localDrivingLicenseApplicationsManagementForm = new LocalDrivingLicenseApplicationsManagementForm();
            localDrivingLicenseApplicationsManagementForm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLocalDrivingLicenseApplicationForm renewLocalDrivingLicenseApplication = new RenewLocalDrivingLicenseApplicationForm();
            renewLocalDrivingLicenseApplication.ShowDialog();
        }

        private void replacementForToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceDamagedAndLostLicenseForm replaceDamagedAndLostLicense = new ReplaceDamagedAndLostLicenseForm();
            replaceDamagedAndLostLicense.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainLicenseForm DetainLicense = new DetainLicenseForm();
            DetainLicense.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseLicenseForm ReleaseLicense = new ReleaseLicenseForm();
            ReleaseLicense.ShowDialog();
        }

        private void manageReleaseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseAndDetainedLicensesMangamentForm releaseAndDetainedLicensesMangamentForm = new ReleaseAndDetainedLicensesMangamentForm();
            releaseAndDetainedLicensesMangamentForm.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternationalDrivingLicenseApplicationsManagementForm internationalDrivingLicenseApplicationsManagementForm = new InternationalDrivingLicenseApplicationsManagementForm();
            internationalDrivingLicenseApplicationsManagementForm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationsManagementForm localDrivingLicenseApplicationsManagementForm = new LocalDrivingLicenseApplicationsManagementForm();
            localDrivingLicenseApplicationsManagementForm.ShowDialog();
        }

        private void InternationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueNewInternationalLicenseForm issueNewInternationalLicenseForm = new IssueNewInternationalLicenseForm();
            issueNewInternationalLicenseForm.ShowDialog();
        }
    }
}
