using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class Test
    {
        public enum EnMode { AddNewMode, UpdateMode };
        private EnMode enMode;
        public int ID { get; set; }
        public int TestAppointmentID { get; set; }
        public TestAppointment TestAppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public String Notes { get; set; }
        public int CreatedByUserID {  get; set; }

        public Test()
        {
            ID = -1;
            TestAppointmentID = -1;
            TestAppointmentInfo = new TestAppointment();
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;
            enMode = EnMode.AddNewMode;
        }

        private Test(int ID, int TestAppointmentID, bool TestResult, String Notes, int CreatedByUserID)
        {
            this.ID = ID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = TestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllTestsTypes()
        {
            return TestData.GetAllTests();
        }

        public static Test FindTestByID(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            String Notes = "";
            int CreatedByUserID = -1;

            if (TestData.FindTestByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new Test(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            return null;
        }

        public static Test Find(int TestAppointmentID)
        {
            int ID = -1;
            bool TestResult = false;
            String Notes = "";
            int CreatedByUserID = -1;

            if (TestData.FindTestByTestAppointmentID(TestAppointmentID, ref ID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new Test(ID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            return null;
        }

        private bool _AddNew()
        {
            this.ID = TestData.AddTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return ID != -1;
        }

        private bool _Update()
        {
            return TestData.UpdateTest(this.ID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (enMode)
            {
                case EnMode.AddNewMode:
                    if (_AddNew())
                    {
                        this.enMode = EnMode.UpdateMode;
                        return true;
                    }

                    return false;
                    
                case EnMode.UpdateMode:
                    return _Update();

                default:
                    return false;
            }
        }

    }

}
