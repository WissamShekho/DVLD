using DVLD.Global_Classes;
using DVLD.Tests.TestControls;
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

namespace DVLD.Tests
{
    public partial class TakeTestForm : Form
    {
        private Test _Test;
        private int _TestAppointmentID;
        private TestAppointment _TestAppointment;
        private bool _TestPassed = false;
        public static Action ListUpdated;
        public TakeTestForm(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
        }

        private void TakeTestForm_Load(object sender, EventArgs e)
        {
            _TestAppointment = TestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show($"Error: No Appointment with ID: {_TestAppointmentID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ctrlScheduledTest1.ResetInfo();
                    btnSave.Enabled = false;
                    return;
            }

            lblMessage.Visible = false;
            rbFail.Checked = true;
            ctrlScheduledTest1.LoadTestAppointment(_TestAppointmentID);
        }

        private void rbPass_CheckedChanged(object sender, EventArgs e)
        {
            _TestPassed = true;
        }

        private void rbFail_CheckedChanged(object sender, EventArgs e)
        {
            _TestPassed = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure About The Result?", "Validation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            
            _Test = new Test();

            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestAppointmentInfo = TestAppointment.Find(_TestAppointmentID);
            _Test.TestResult = _TestPassed;
            _Test.Notes = tbNotes.Text.Trim();
            _Test.CreatedByUserID = Global.CurrentUser.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Test Saved Successfully");
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                lblMessage.Text = "You Can Not Change The Result";
                lblMessage.Visible = true;
                ctrlScheduledTest1.TestID = _Test.ID;
                tbNotes.Enabled = false;
                btnSave.Enabled = false;
                ListUpdated?.Invoke();
            }

            else
                MessageBox.Show("Error: Data Is Not Saved!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
