using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLayer
{
    public class TestAppointment
    {
        public enum EnMode { AddNewMode, UpdateMode};
        public EnMode enMode { get; set; }    
        public int ID {  get; set; }
        public TestType.EnTestType TestTypeID { get; set; }
        public TestType TestTypeInfo { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public LocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;
        public DateTime AppointmentDate {  get; set; }
        public float PaidFees {  get; set; }
        public int CreatedByUserID {  get; set; }
        public bool IsLocked {  get; set; }
        public int RetakeTestApplicationID { get; set; }
        
        public TestAppointment()
        {
            ID = -1;
            TestTypeID = TestType.EnTestType.VisionTest;
            TestTypeInfo = TestType.Find(TestTypeID);
            LocalDrivingLicenseApplicationID = -1;
            LocalDrivingLicenseApplicationInfo = new LocalDrivingLicenseApplication();
            AppointmentDate = DateTime.MaxValue;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
            enMode = EnMode.AddNewMode;
        }

        private TestAppointment(int ID, TestType.EnTestType TestTypeID, int LocalDrivingLicenseApplicationID, 
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.ID = ID;
            this.TestTypeID = TestTypeID;
            this.TestTypeInfo = TestType.Find(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplicationInfo = LocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllTestsAppointments()
        {
            return TestAppointmentData.GetAllTestsAppointments();
        }
        
        public static TestAppointment Find(int ID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1 , RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.MaxValue;
            float PaidFees = 0;
            bool IsLocked = false;

            if (TestAppointmentData.FindTestAppointment(ID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate,
                ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new TestAppointment(ID, (TestType.EnTestType)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                    PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            return null;
        }
        
        public static DataTable GetAllTestAppointmentForLocalDrivingLicenseApplicationOfTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return TestAppointmentData.GetAllTestsAppointmentsforLocalDrivingLicenseApplicationOfTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static int GetTestID(int TestAppointmentID)
        {
            return TestAppointmentData.GetTestID(TestAppointmentID);
        }

        private bool _AddNew()
        {

            this.ID = TestAppointmentData.AddTestAppointment(((int)TestTypeID), LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
                CreatedByUserID, RetakeTestApplicationID);
            return this.ID != -1;
        }

        private bool _Update()
        {
            return TestAppointmentData.UpdateTestAppointment(ID, AppointmentDate);
        }

        public bool Save()
        {
            switch (this.enMode)
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
