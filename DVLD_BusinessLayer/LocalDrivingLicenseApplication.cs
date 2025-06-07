using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class LocalDrivingLicenseApplication
    {
  
        public enum EnMode { AddNewMode, UpdateMode };
        public EnMode enMode {  get; set; }
        public int ID { get; set; }

        public int ApplicationID;
        
        public Application ApplicationInfo;
        public LicenseClass.EnLicenseClass LicenseClassID { get; set; }

        public LicenseClass LicenseClassInfo;
   

        public LocalDrivingLicenseApplication()
        {
            ID = -1;
            ApplicationID = -1;
            ApplicationInfo = new Application();
            LicenseClassID = LicenseClass.EnLicenseClass.OrdinaryDrivingLicense;
            LicenseClassInfo = new LicenseClass();
            this.enMode = EnMode.AddNewMode;
        }

        private LocalDrivingLicenseApplication(int iD, int applicationID, LicenseClass.EnLicenseClass licenseClassID)
        {
            ID = iD;
            this.ApplicationID = applicationID;
            this.LicenseClassID = licenseClassID;
            this.ApplicationInfo = Application.Find(this.ApplicationID);
            this.LicenseClassInfo = LicenseClass.Find(licenseClassID);
            this.enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllLocalDrivingLicensesApplications()
        {
            return LocalDrivingLicenseApplicationData.GetAllLocalDrivingLicensesApplications();
        }

        public static LocalDrivingLicenseApplication FindByID(int ID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (LocalDrivingLicenseApplicationData.FindLocalDrivingLicenseApplicationByID(ID, ref ApplicationID, ref LicenseClassID))
                return new LocalDrivingLicenseApplication(ID, ApplicationID, (LicenseClass.EnLicenseClass)LicenseClassID);
            return null;
        }

        public static LocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int ID = -1, LicenseClassID = -1;

            if (LocalDrivingLicenseApplicationData.FindLocalDrivingLicenseApplicationByApplicationID(ApplicationID, ref ID, ref LicenseClassID))
                return new LocalDrivingLicenseApplication(ID, ApplicationID, (LicenseClass.EnLicenseClass)LicenseClassID);
            return null;
        }

        private bool _AddNew()
        {

            this.ID = LocalDrivingLicenseApplicationData.AddLocalDrivingLicenseApplication(this.ApplicationID, (int)this.LicenseClassID) ;
            return this.ID != -1;
        }

        private bool _Update()
        {
            return LocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.ID, ApplicationID, (int)LicenseClassID);
        }

        public bool Delete()
        {
            bool LocalDrivingLicenseApplicationDeleted = LocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.ID);
            return LocalDrivingLicenseApplicationDeleted ? this.ApplicationInfo.Delete() : false;
        }
       
        public bool Cancel()
        {
            return this.ApplicationInfo.Cancel();
        }

        public bool Save()
        {
            ApplicationInfo.enMode = (Application.EnMode)this.enMode; 
            if (!ApplicationInfo.Save())
                return false;

            ApplicationID = ApplicationInfo.ApplicationID;

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

        public int PassedTestsCount()
        {
            return LocalDrivingLicenseApplicationData.PassedTestsCount(this.ID);
        }
        
        public bool DoesPassedAllTests()
        {
            return PassedTestsCount() == TestType.TestsTypesCount();
        }
        public bool DoesTestPassed(TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.DoesTestPassed(this.ID, (int)TestTypeID);
        }

        public static int TestTrialsCountForLocalDrivingLicenseApplicationByTestType(int LocalDrivingLicenseApplicationID, TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.TestTrialsCountByTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public int TrailsCount(TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.TestTrialsCountByTestType(this.ID, (int)TestTypeID);
        }

        public static bool DoesAttendTest(int LocalDrivingLicenseApplicationID, TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.DoesAttendTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTest(TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.DoesAttendTest(this.ID, (int)TestTypeID);
        }

        public bool IsThereAnyActiveTestAppointmentByTestType(TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.IsThereAnyActiveTestAppointmentByTestType(this.ID, (int)TestTypeID);
        }

        public static bool IsLocalDrivingApplicationExist(int LocalDrivingApplicationID)
        {
            return LocalDrivingLicenseApplicationData.IsLocalDrivingLicenseApplicationExistByID(LocalDrivingApplicationID);
        }

        public static bool DoesPersonHaveAnActiveApplicationforLicenseClassID(int PersonID, int LicenseClassID)
        {
            return LocalDrivingLicenseApplicationData.DoesPersonHaveAnActiveApplicationforLicenseClassID(PersonID, LicenseClassID);
        }

        public static DateTime GetLastAppointmentDateForLocalDrivingLicenseApplicationByTestType(int LocalDrivingLicenseApplicationID, TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.GetLastAppointmentDateByTestType(LocalDrivingLicenseApplicationID, ((int)TestTypeID));
        }

        public DateTime GetLastAppointmentDateByTestType(TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.GetLastAppointmentDateByTestType(this.ID, (int)TestTypeID);
        }

        public DataTable GetAllTestAppointmentsForTestType(TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.GetAllTestAppointmentsForTestType(this.ID, ((int)TestTypeID));
        }

        public static DataTable GetAllTestAppointmentsForLocalDrivingLicenseApplicationByTestType(int LocalDrivingLicenseApplicationID, TestType.EnTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.GetAllTestAppointmentsForTestType(LocalDrivingLicenseApplicationID, ((int)TestTypeID));
        }

        public bool DoesPassedPreviousTests(TestType.EnTestType TestTypeID)
        {
            if (TestTypeID == TestType.EnTestType.VisionTest)
                return true;

            switch (TestTypeID)
            {
                case TestType.EnTestType.WrittenTest:
                    return DoesTestPassed(TestType.EnTestType.VisionTest);

                case TestType.EnTestType.StreetTest:
                    return (DoesTestPassed(TestType.EnTestType.VisionTest) && DoesTestPassed(TestType.EnTestType.WrittenTest));

                default:
                    return false;
            }
        }

        public int GetLastAppointmentIDByTestType(TestType.EnTestType TestTypeID)
        {
            
            return LocalDrivingLicenseApplicationData.GetLastAppointmentIDByTestType(this.ID, (int)TestTypeID);
        }

        public int IssueDrivingLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {
            Driver driver = Driver.FindDriverByPersonID(this.ApplicationInfo.PersonID);

            if (driver == null)
            {
                driver = new Driver();
                driver.PersonID = this.ApplicationInfo.PersonID;
                driver.PersonInfo = this.ApplicationInfo.PersonInfo;
                driver.CreatedDate = DateTime.Now;
                driver.CreatedByUserID = CreatedByUserID;

                if (!driver.Save())
                    return -1;
            }

            License license = new License();

            license.ApplicationID = this.ApplicationID;
            license.ApplicationInfo = this.ApplicationInfo;
            license.LicenseClassID = this.LicenseClassID;
            license.LicenseClassInfo = this.LicenseClassInfo;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = (license.IssueDate.AddYears(this.LicenseClassInfo.DefaultValidityLength));
            license.PaidFees = this.LicenseClassInfo.Fees;
            license.Notes = Notes;
            license.enIssueReason = License.EnIssueReason.FirstTime;
            license.IssueReasonApplicationTypeInfo = this.ApplicationInfo.ApplicationTypeInfo;
            license.IsActive = true;
            license.CreatedByUserID = CreatedByUserID;
            license.DriverID = driver.ID;
            license.DriverInfo = driver;

            if (license.Save())
            {
                this.Complete();
                return license.ID;
            }
            return -1;

        }

        private void Complete()
        {
            this.ApplicationInfo.enApplicationStatus = Application.EnApplicationStatus.Completed;
            Save();
        }
    }
}
