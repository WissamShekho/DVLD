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
using DVLD.Tests.TestControls;
using DVLD.Properties;
using static DVLD_BusinessLayer.TestType;

namespace DVLD.Tests
{
    public partial class TestsAppointmentsManagementForm : Form
    {
        private DataTable _AppointmentsTable;
        private int _LocalDrivingLicenseApplicationID;
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private TestType.EnTestType _TestTypeID;
        private TestType _TestType;
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
                            lblTitle.Text = "Vision Test";
                            pbTestImage.Image = Resources.Vision_512;
                            break;
                        }

                    case TestType.EnTestType.WrittenTest:
                        {
                            lblTitle.Text = "Written Test";
                            pbTestImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case TestType.EnTestType.StreetTest:
                        {
                            lblTitle.Text = "Street Test";
                            pbTestImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }

        public static Action ListUpdated;

        public TestsAppointmentsManagementForm(int LocalDrivingLicenseApplicationID, TestType.EnTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestTypeID = TestTypeID;
        }


        private void TestsAppointmentsManagementForm_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);
            _TestType = TestType.Find(TestTypeID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show($"Error: No Application with ID: {_LocalDrivingLicenseApplicationID}", "Validation", MessageBoxButtons.OK,
                     MessageBoxIcon.Information);

                ctrlDrivingLicenseApplicationCard1.ResetInfo();
                btnAddAppointment.Enabled = false;
                btnClose.Focus();
                return;
            }

            else if (_TestType == null)
            {
                MessageBox.Show($"Error: No Test With Name: {TestTypeID.ToString().Replace("Test","")}", "Validation", MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                ctrlDrivingLicenseApplicationCard1.ResetInfo();
                btnAddAppointment.Enabled = false;
                btnClose.Focus();
                return;
            }


            _LoadAppointmentsData();
        }

        private void _LoadAppointmentsData()
        {
            ctrlDrivingLicenseApplicationCard1.LoadLocalDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplicationID);
            _AppointmentsTable = _LocalDrivingLicenseApplication.GetAllTestAppointmentsForTestType(TestTypeID);
            dgvAppointmentsList.DataSource = _AppointmentsTable;

            if (dgvAppointmentsList.Rows.Count < 0)
            {
                dgvAppointmentsList.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                dgvAppointmentsList.Columns["TestAppointmentID"].Width = 120;
                dgvAppointmentsList.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                dgvAppointmentsList.Columns["AppointmentDate"].Width = 125;
                dgvAppointmentsList.Columns["PaidFees"].HeaderText = "Paid Fees";
                dgvAppointmentsList.Columns["PaidFees"].Width = 80;
                dgvAppointmentsList.Columns["IsLocked"].HeaderText = "Is Locked";
                dgvAppointmentsList.Columns["IsLocked"].Width = 80;
            }
            lblRecords.Text = dgvAppointmentsList.RowCount.ToString();

            ListUpdated?.Invoke();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (!_LocalDrivingLicenseApplication.DoesPassedPreviousTests(TestTypeID))
            {
                MessageBox.Show("Person Does Not Pass The Previous Tests Yet", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesTestPassed(TestTypeID))
            {
                MessageBox.Show("Person Already Passed This Test", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_LocalDrivingLicenseApplication.IsThereAnyActiveTestAppointmentByTestType(TestTypeID))
            {
                MessageBox.Show("There Is An Active Appointment For this Test ALready", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }


            ScheduleTestForm scheduleTestForm = new ScheduleTestForm(_LocalDrivingLicenseApplicationID, TestTypeID);
            ScheduleTestForm.ListUpdated += _LoadAppointmentsData;
            scheduleTestForm.ShowDialog();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int AppointmentID = int.Parse(dgvAppointmentsList.CurrentRow.Cells["TestAppointmentID"].Value.ToString());
            bool IsLocked = bool.Parse(dgvAppointmentsList.CurrentRow.Cells["IsLocked"].Value.ToString());

            if (IsLocked)
            {
                MessageBox.Show("Person already sat for the test, appointment locked.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            ScheduleTestForm scheduleTestForm = new ScheduleTestForm(AppointmentID);
            ScheduleTestForm.ListUpdated += _LoadAppointmentsData;
            scheduleTestForm.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsLocked= (bool)dgvAppointmentsList.CurrentRow.Cells["IsLocked"].Value;

            if (IsLocked)
            {
                MessageBox.Show("Error: Person already Sat For This Test, Appointment Locked", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int AppointmentID = (int)dgvAppointmentsList.CurrentRow.Cells["TestAppointmentID"].Value;
            TakeTestForm takeTestForm = new TakeTestForm(AppointmentID);
            TakeTestForm.ListUpdated += _LoadAppointmentsData;
            takeTestForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
