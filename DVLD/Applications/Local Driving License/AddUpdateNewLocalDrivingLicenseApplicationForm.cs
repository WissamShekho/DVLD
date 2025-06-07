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

namespace DVLD.Applications.Local_Driving_License
{
    public partial class AddUpdateNewLocalDrivingLicenseApplicationForm : Form
    {
        enum EnMode { AddNew, Update};

        private EnMode _FormMode;

        public static Action DataUpdated;

        private int _LocalDrivingLicenseApplicationID;

        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public AddUpdateNewLocalDrivingLicenseApplicationForm(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _FormMode = EnMode.Update;
        }

        public AddUpdateNewLocalDrivingLicenseApplicationForm()
        {
            InitializeComponent();
            _FormMode = EnMode.AddNew;
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.SelectedPersonInfo != null) 
            {
                tpApplicationInfo.Enabled = true;
                //tpApplicationInfo.Show();
                tcLocalDrivingLicenseApplicationInfo.SelectTab("tpApplicationInfo");
                btnSave.Enabled = true;
            }
            else
                MessageBox.Show("You Should Select A Person First!");
             
        }


        private void tpApplicationInfo_Click(object sender, EventArgs e)
        {
            btnNext_Click(sender, e);
        }

        private void AddNewLocalDrivingLicenseApplicationForm_Load(object sender, EventArgs e)
        {
            if (_FormMode == EnMode.Update)
            {
                 _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);
                if (_LocalDrivingLicenseApplication == null)
                {
                    MessageBox.Show("No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }
                    _FillApplicationInfo();
            }
            else
                _SetDefaultValues();
        }

        private void _FillApplicationInfo()
        {
            this.Text = lblTitle.Text = "Update Local Driving License Application";

            ctrlPersonCardWithFilter1.FilterEnabled = false;
            tpApplicationInfo.Enabled = true;
            _LoadLicensesClassesIntoComboBox();

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicationInfo.PersonID);
            btnSave.Enabled = true;
            lblApplicationID.Text = _LocalDrivingLicenseApplication.ID.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationInfo.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.ApplicationInfo.PaidFees.ToString();
            lblUsername.Text = _LocalDrivingLicenseApplication.ApplicationInfo.UserInfo.UserName.ToString();
            cbLicensesClasses.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;


        }

        private void _SetDefaultValues()
        {
            this.Text = lblTitle.Text = "Add New Local Driving License Application";
            

            tpApplicationInfo.Enabled = false;
            ctrlPersonCardWithFilter1.FilterFocus();
            _LoadLicensesClassesIntoComboBox();

            ctrlPersonCardWithFilter1.ResetPersonInfo();
            btnSave.Enabled = false;
            lblApplicationID.Text = "[???]";
            lblApplicationDate.Text = DateTime.Today.ToShortDateString();
            lblApplicationFees.Text = ApplicationType.Find(ApplicationType.EnApplicationType.LocalDrivingLicense).Fees.ToString();
            lblUsername.Text = Global_Classes.Global.CurrentUser.UserName;

            cbLicensesClasses.SelectedIndex = cbLicensesClasses.FindString("Class 3");
        }

        private void _LoadLicensesClassesIntoComboBox()
        {
            DataTable dtLicensesClasses = LicenseClass.GetAllLicensesClasses();
            foreach (DataRow row in dtLicensesClasses.Rows)
                cbLicensesClasses.Items.Add(row["ClassName"]);
                
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_FormMode == EnMode.AddNew)
                _LocalDrivingLicenseApplication = new LocalDrivingLicenseApplication();


            if (DVLD_BusinessLayer.License.DoesPersonHaveActiveLicenseByLicenseClass(ctrlPersonCardWithFilter1.PersonID, LicenseClass.Find(cbLicensesClasses.Text).ID))
            {
                MessageBox.Show("Person Already Have An Active License Of This Class", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (LocalDrivingLicenseApplication.DoesPersonHaveAnActiveApplicationforLicenseClassID(ctrlPersonCardWithFilter1.PersonID, (int)LicenseClass.Find(cbLicensesClasses.Text).ID))
            {
                MessageBox.Show("The Person Is Already Have An Active Application Of this License Class", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicationInfo.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.ApplicationInfo.UserID = Global_Classes.Global.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.ApplicationInfo.ApplicationDate = DateTime.Parse(lblApplicationDate.Text);
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClass.Find(cbLicensesClasses.Text).ID;
            _LocalDrivingLicenseApplication.ApplicationInfo.PaidFees = float.Parse(lblApplicationFees.Text);
            _LocalDrivingLicenseApplication.ApplicationInfo.ApplicationTypeID = ApplicationType.EnApplicationType.LocalDrivingLicense;

            if (_LocalDrivingLicenseApplication.Save())
            {
                _FormMode = EnMode.Update;
                MessageBox.Show("Local Driving License Application Saved Successfully");
                lblApplicationID.Text = _LocalDrivingLicenseApplication.ID.ToString();
                this.Text = lblTitle.Text = "Update Local Driving License Application";
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                DataUpdated?.Invoke();
            }
            else
                MessageBox.Show("Error: Data Is Not Saved!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
