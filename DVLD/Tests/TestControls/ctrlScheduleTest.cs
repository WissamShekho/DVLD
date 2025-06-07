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
using DVLD.Properties;
using DVLD_BusinessLayer;


namespace DVLD.Tests.TestControls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }


        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode;

        private DVLD_BusinessLayer.Application _RetakeTestApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _TestAppointmentID = -1;
        private TestAppointment _TestAppointment;
        private TestType.EnTestType _TestTypeID;
        private TestType _TestTypeInfo;
        public TestType.EnTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case TestType.EnTestType.VisionTest:
                        {
                            lblTitle.Text = gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case TestType.EnTestType.WrittenTest:
                        {
                            lblTitle.Text = gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case TestType.EnTestType.StreetTest:
                        {
                            lblTitle.Text = gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }

        private float _RetakeTestAppFees;
        public static Action DataUpdated;



        public void LoadTestAppointment(int TestAppointmentID)
        {
            _Mode = enMode.Update;

            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = TestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show($"Error: No Appointment with ID: {_TestAppointmentID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetInfo();
                btnSave.Enabled = false;
                return;
            }

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = _TestAppointment.LocalDrivingLicenseApplicationInfo;
            TestTypeID = _TestAppointment.TestTypeID;
            _TestTypeInfo = _TestAppointment.TestTypeInfo;

            _CreationMode = _TestAppointment.LocalDrivingLicenseApplicationInfo.DoesAttendTest(TestTypeID) ? enCreationMode.RetakeTestSchedule : enCreationMode.FirstTimeSchedule;

            _FillTestAppointmentInfo();


            if (AlreadySatThisTest())
                return;
        }

        private void _FillTestAppointmentInfo()
        {
            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblFullName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicationInfo.PersonInfo.FullName;
            lblTrial.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.TrailsCount(TestTypeID).ToString();
            dtpTestDate.Value = _TestAppointment.AppointmentDate;
            lblFees.Text = _TestAppointment.TestTypeInfo.Fees.ToString();

            if (_CreationMode == enCreationMode.FirstTimeSchedule)
            {
                lblRetakeTestAppID.Text = "[N/A]";

                _RetakeTestAppFees = 0;
                
                gbRetakeTestInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = ("Retake " + lblTitle.Text);

                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

                _RetakeTestAppFees = ApplicationType.Find(ApplicationType.EnApplicationType.RetakeTest).Fees;
                lblRetakeAppFees.Text = _RetakeTestAppFees.ToString();

                gbRetakeTestInfo.Enabled = true;
            }

            lblRetakeAppFees.Text = _RetakeTestAppFees.ToString();
            lblTotalFees.Text = (_RetakeTestAppFees + _TestAppointment.TestTypeInfo.Fees).ToString();
        }

        private bool AlreadySatThisTest()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment locked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return true;
            }

            lblUserMessage.Visible = false;
            return false;
        }







        public void LoadTestAppointment(int LocalDrivingLicenseApplicationID, TestType.EnTestType TestTypeID)
        {
            _Mode = enMode.AddNew;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show($"Error: No Application with ID: {_LocalDrivingLicenseApplicationID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetInfo();
                btnSave.Enabled = false;
                return;
            }


            this.TestTypeID = TestTypeID;
            _TestTypeInfo = TestType.Find(_TestTypeID);

            if (_TestTypeInfo == null)
            {
                MessageBox.Show($"Error: No Test Of Type: {TestTypeID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetInfo();
                btnSave.Enabled = false;
                return;
            }


            _CreationMode = _LocalDrivingLicenseApplication.DoesAttendTest(TestTypeID) ? enCreationMode.RetakeTestSchedule: enCreationMode.FirstTimeSchedule;

            _FillNewTestAppointmentInfo();


            if (ActiveAppointmentAlreadyExist())
                return;

            if (DoesNotPassPreviousTest())
                return;

        }

        private void _FillNewTestAppointmentInfo()
        {
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.ID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicationInfo.PersonInfo.FullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TrailsCount(TestTypeID).ToString();
            dtpTestDate.Value = DateTime.Today;
            lblFees.Text = _TestTypeInfo.Fees.ToString();
            lblRetakeTestAppID.Text = "[N/A]";

            if (_CreationMode == enCreationMode.FirstTimeSchedule)
            {
                _RetakeTestAppFees = 0;
                gbRetakeTestInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = ("Retake " + lblTitle.Text);

                _RetakeTestAppFees = ApplicationType.Find(ApplicationType.EnApplicationType.RetakeTest).Fees;
                gbRetakeTestInfo.Enabled = true;
            }

            lblRetakeAppFees.Text = _RetakeTestAppFees.ToString();
            lblTotalFees.Text = (_RetakeTestAppFees + _TestTypeInfo.Fees).ToString();
        }

        private bool DoesNotPassPreviousTest()
        {
            switch (TestTypeID)
            {
                case TestType.EnTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return false;

                case TestType.EnTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplication.DoesTestPassed(TestType.EnTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule Written Test, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }

                    lblUserMessage.Visible = false;
                    btnSave.Enabled = true;
                    dtpTestDate.Enabled = true;
                    return true;


                case TestType.EnTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesTestPassed(TestType.EnTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule Street Test, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }

                    lblUserMessage.Visible = false;
                    btnSave.Enabled = true;
                    dtpTestDate.Enabled = true;
                    return true;

                default:
                    return true;
            }


        }

        private bool ActiveAppointmentAlreadyExist()
        {
            if (_LocalDrivingLicenseApplication.IsThereAnyActiveTestAppointmentByTestType(TestTypeID))
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Value = _LocalDrivingLicenseApplication.GetLastAppointmentDateByTestType(_TestTypeID);
                dtpTestDate.Enabled = false;
                return true;
            }
            lblUserMessage.Visible = false;
            return false;
        }








        public void ResetInfo()
        {
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblDrivingClass.Text = "[????]";
            lblFullName.Text = "[????]";
            lblTrial.Text = "[????]";
            dtpTestDate.Value = DateTime.Today;
            lblFees.Text = "[$$$$]";
            lblRetakeTestAppID.Text = "[????]";
            lblRetakeAppFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsDateValid())
                return;


            if (_Mode == enMode.AddNew)
            { 
                _TestAppointment = new TestAppointment();
                
                _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
                _TestAppointment.LocalDrivingLicenseApplicationInfo = _LocalDrivingLicenseApplication;
                _TestAppointment.TestTypeID = TestTypeID;
                _TestAppointment.AppointmentDate = dtpTestDate.Value;
                _TestAppointment.CreatedByUserID = Global_Classes.Global.CurrentUser.UserID;
                _TestAppointment.PaidFees = (_TestTypeInfo.Fees + _RetakeTestAppFees);
                _TestAppointment.IsLocked = false;
                _TestAppointment.RetakeTestApplicationID = _GetRetakeTestApplicationID();
            }

            else
                _TestAppointment.AppointmentDate = dtpTestDate.Value;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;

                if (_TestAppointment.RetakeTestApplicationID != -1)
                    lblRetakeTestAppID.Text =  _TestAppointment.RetakeTestApplicationID.ToString();

                MessageBox.Show("Test Appointment Saved Successfully");
                DataUpdated?.Invoke();
            }
        
            else
                MessageBox.Show("Error: Data Is Not Saved!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private int _GetRetakeTestApplicationID()
        {
            if (_CreationMode == enCreationMode.FirstTimeSchedule)
                return -1;

            _RetakeTestApplication = new DVLD_BusinessLayer.Application();

            _RetakeTestApplication.ApplicationDate = DateTime.Today;
            _RetakeTestApplication.ApplicationTypeID = ApplicationType.EnApplicationType.RetakeTest;
            _RetakeTestApplication.LastStatusDate = DateTime.Today;
            _RetakeTestApplication.PaidFees = ApplicationType.Find(ApplicationType.EnApplicationType.RetakeTest).Fees;
            _RetakeTestApplication.PersonID = _LocalDrivingLicenseApplication.ApplicationInfo.PersonID;
            _RetakeTestApplication.UserID = Global.CurrentUser.UserID;
            _RetakeTestApplication.enApplicationStatus = DVLD_BusinessLayer.Application.EnApplicationStatus.Completed;

            if (!_RetakeTestApplication.Save())
            {
                MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return _RetakeTestApplication.ApplicationID;
        }

        private bool IsDateValid()
        {
            if (DateTime.Compare(dtpTestDate.Value, DateTime.Today) < 0)
            {
                MessageBox.Show("Invalid Date: Choose A Date In The Future", "Validation");
                return false;
            }
            return true;
        }
    }
}
