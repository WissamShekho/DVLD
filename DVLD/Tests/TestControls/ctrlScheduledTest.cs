using DVLD.Properties;
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
using static DVLD.Tests.TestControls.ctrlScheduleTest;

namespace DVLD.Tests.TestControls
{
    public partial class ctrlScheduledTest : UserControl
    {
        private int _TestAppointmentID;
        private TestAppointment _TestAppointment;

        private int _TestID;
        public int TestID
        {
            get
            {
                return _TestID;
            }

            set
            {
                _TestID = value;
                lblTestID.Text = _TestID.ToString();
            }
        }

        private TestType.EnTestType _TestTypeID;
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


        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        

        public void LoadTestAppointment(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = TestAppointment.Find(_TestAppointmentID);
            TestTypeID = _TestAppointment.TestTypeID;

            if (_TestAppointment == null)
            {
                MessageBox.Show($"Error: No Appointment with ID: {_TestAppointmentID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetInfo();
                return;
            }

            _FillTestAppointmentInfo();
        }

        private void _FillTestAppointmentInfo()
        {
            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblFullName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicationInfo.PersonInfo.FullName;
            lblTrial.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.TrailsCount(_TestAppointment.TestTypeID).ToString();
            lblDate.Text= _TestAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = "Not Taken Yet";
        }

        public void ResetInfo()
        {
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblDrivingClass.Text = "[????]";
            lblFullName.Text = "[????]";
            lblTrial.Text = "[????]";
            lblDate.Text= DateTime.Today.ToShortDateString();
            lblFees.Text = "[$$$$]";
            lblTestID.Text = "Not Taken Yet";
        }

    }
}
