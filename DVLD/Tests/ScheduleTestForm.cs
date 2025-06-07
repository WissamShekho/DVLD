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


namespace DVLD.Tests
{
    public partial class ScheduleTestForm : Form
    {
        private enum EnFormMode{ AddNew, Update };
        EnFormMode _FormMode;

        private int _LDLAppID;
        private TestType.EnTestType _EnTestTypeID;
        private int _TestAppointmentID = -1;

        public static Action ListUpdated;

        public ScheduleTestForm(int LDLAppID, TestType.EnTestType TestTypeID)
        {
            InitializeComponent();
            _EnTestTypeID = TestTypeID;
            _LDLAppID = LDLAppID;
            _FormMode = EnFormMode.AddNew;
        
        }

        public ScheduleTestForm(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _FormMode = EnFormMode.Update;
        }

        private void ScheduleTestForm_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest.DataUpdated += _DataUpdated;

            if (_FormMode == EnFormMode.AddNew)
                ctrlScheduleTest1.LoadTestAppointment(_LDLAppID, _EnTestTypeID);
            
            else
                ctrlScheduleTest1.LoadTestAppointment(_TestAppointmentID);

        }

        private void _DataUpdated()
        {
            ListUpdated?.Invoke();
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
