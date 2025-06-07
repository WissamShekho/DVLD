using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_BusinessLayer.ApplicationType;
using static DVLD_BusinessLayer.LicenseClass;

namespace DVLD_BusinessLayer
{
    public class License
    {
        public enum EnMode { AddNewMode, UpdateMode };
        private EnMode enMode { get; set; }
        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public Application ApplicationInfo { get; set;  }
        public int DriverID {  get; set; }
        public Driver DriverInfo { get; set; }
        public LicenseClass.EnLicenseClass LicenseClassID { get; set; }
        public LicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public float PaidFees { get; set; }
        public enum EnIssueReason { FirstTime = 1, Renew, LostReplacement, DamagedReplacement };
        public EnIssueReason enIssueReason { get; set; }        
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText();
            }
        }
        private string GetIssueReasonText()
        {
            switch (enIssueReason)
            {
                case EnIssueReason.FirstTime:
                    return "First Time";

                case EnIssueReason.Renew:
                    return "Renew";

                case EnIssueReason.DamagedReplacement:
                    return "Damaged Replacement";

                case EnIssueReason.LostReplacement:
                    return "Lost Replacement";

                default:
                    return "";
            }
        }
        public ApplicationType IssueReasonApplicationTypeInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public DetainLicense DetainInfo {  get; set; }

        public bool IsDetained
        {
            get
            {
                if (this.DetainInfo == null)
                    return false;

                return this.DetainInfo.IsReleased == false;
                
            }
        }


        public License()
        {
            ID = -1;
            ApplicationID = -1;
            ApplicationInfo = new Application();
            DriverID = -1;
            DriverInfo = new Driver();
            LicenseClassID = LicenseClass.EnLicenseClass.OrdinaryDrivingLicense;
            LicenseClassInfo = new LicenseClass();
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = false;
            enIssueReason = EnIssueReason.FirstTime;
            IssueReasonApplicationTypeInfo = new ApplicationType();
            CreatedByUserID = CreatedByUserID;
            this.DetainInfo = new DetainLicense();
            
            this.enMode = EnMode.AddNewMode;
        }

        private License(int ID, int ApplicationID, int DriverID, LicenseClass.EnLicenseClass LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive,
            EnIssueReason enIssueReason, int CreatedByUserID)
        {
            this.ID = ID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = Application.Find(ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = Driver.FindDriverByID(DriverID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = LicenseClass.Find(LicenseClassID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;

            /*if (IsActive && IsExpired()) // Check if License Is Expired and Still Active
            {
                DeActivate(); // DeActivate the License In The Database
                this.IsActive = false; // Mark License As InActive
            }
            else*/
                this.IsActive = IsActive; // Else Store the Actual Value  (Not Necessarily to be True)

            this.enIssueReason = enIssueReason;
            this.IssueReasonApplicationTypeInfo = ApplicationType.Find( (EnApplicationType) enIssueReason);
            this.CreatedByUserID = CreatedByUserID;
            this.DetainInfo = DetainLicense.FindDetainByLicenseID(ID);

            this.enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllLicenses()
        {
            return LicenseData.GetAllLicenses();
        }

        public static License FindByID(int ID)
        {
            int ApplicationID = -1, DriverID = -1, CreatedByUserID = -1, LicenseClassID = -1, enIssueReason= -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
                

            if (LicenseData.FindLicenseByID(ID, ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref enIssueReason, ref CreatedByUserID))
                return new License(ID, ApplicationID, DriverID, (LicenseClass.EnLicenseClass)LicenseClassID, IssueDate,
                    ExpirationDate, Notes, PaidFees, IsActive, (EnIssueReason) enIssueReason,
                    CreatedByUserID);
            return null;
        }

        public static License FindByApplicationID(int ApplicationID)
        {
            int ID = -1, DriverID = -1, CreatedByUserID = -1, LicenseClassID = -1, enIssueReason= -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;


            if (LicenseData.FindLicenseByApplicationID(ApplicationID, ref ID, ref DriverID, ref LicenseClassID, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref enIssueReason, ref CreatedByUserID))
                return new License(ID, ApplicationID, DriverID, (LicenseClass.EnLicenseClass)LicenseClassID, IssueDate,
                    ExpirationDate, Notes, PaidFees, IsActive, (EnIssueReason)enIssueReason,
                    CreatedByUserID);
            return null;
        }

        public static License FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ID = -1, ApplicationID = -1, DriverID = -1, CreatedByUserID = -1, LicenseClassID = -1, enIssueReason = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;


            if (LicenseData.FindLicenseByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID, ref ID, 
                ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref enIssueReason, ref CreatedByUserID))
                return new License(ID, ApplicationID, DriverID, (LicenseClass.EnLicenseClass)LicenseClassID, IssueDate,
                    ExpirationDate, Notes, PaidFees, IsActive, (EnIssueReason)enIssueReason,
                    CreatedByUserID);
            return null;
        }

        public static License FindActiveLicenseForThisPersonByLicenseClass(int PersonID, LicenseClass.EnLicenseClass LicenseClassID)
        {
            int ID = -1, ApplicationID = -1, DriverID = -1, CreatedByUserID = -1, enIssueReason = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;


            if (LicenseData.FindActiveLicenseForThisPersonByLicenseClass(PersonID, (int)LicenseClassID, ref ID, ref ApplicationID,
                ref DriverID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref enIssueReason, ref CreatedByUserID))
                
                return new License(ID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes,
                    PaidFees, true, (EnIssueReason)enIssueReason,CreatedByUserID);
            return null;
        }
        private bool _AddNew()
        {

            this.ID = LicenseData.AddLicense(ApplicationID, DriverID, (int)LicenseClassID, IssueDate, ExpirationDate, Notes,
                PaidFees, IsActive, ((int)enIssueReason) ,CreatedByUserID);
            return this.ID != -1;
        }

        private bool _Update()
        {
            return LicenseData.UpdateLicense(ID, ApplicationID, DriverID, (int)LicenseClassID, IssueDate, ExpirationDate, Notes,
                PaidFees, IsActive, ((int)enIssueReason), CreatedByUserID);
        }

        public bool Delete()
        {
            return LicenseData.DeleteLicense(this.ID);    
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

        public static bool IsLicenseExist(int LicenseID)
        {
            return LicenseData.IsLicenseExist (LicenseID);
        }

        public static bool DoesPersonHaveActiveLicenseByLicenseClass(int PersonID, LicenseClass.EnLicenseClass LicenseClassID)
        {
            return LicenseData.DoesPersonHaveActiveLicenseByLicenseClass(PersonID, (int)LicenseClassID);
        }

        public bool DoesPersonHaveActiveLicenseOfSameClass()
        {
            return LicenseData.DoesPersonHaveActiveLicenseByLicenseClass(this.DriverInfo.PersonID, (int)this.LicenseClassID);
        }

        public bool DeActivate()
        {
            return LicenseData.DeActivateLicense(this.ID);
        }
    
        public bool IsExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }
    
        public License Renew(string Notes, int CreatedByUserID)
        {
            this.DeActivate();

            Application application = new Application();
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = EnApplicationType.RenewDrivingLicense;
            application.ApplicationTypeInfo = ApplicationType.Find(EnApplicationType.RenewDrivingLicense);
            application.enApplicationStatus = Application.EnApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = application.ApplicationTypeInfo.Fees;
            application.PersonID = this.DriverInfo.PersonID;
            application.PersonInfo = this.DriverInfo.PersonInfo;
            application.UserID = CreatedByUserID;
            application.UserInfo = User.FindByUserID(CreatedByUserID);


            License license = new License();
            license.DriverID = this.DriverID;
            license.DriverInfo = this.DriverInfo;
            license.LicenseClassID = this.LicenseClassID;
            license.LicenseClassInfo = this.LicenseClassInfo;
            license.PaidFees = this.PaidFees;
            license.CreatedByUserID = CreatedByUserID;
            license.enIssueReason = EnIssueReason.Renew;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            license.IsActive = this.IsActive;
            license.Notes = Notes;
            license.DetainInfo = this.DetainInfo;
            license.IssueReasonApplicationTypeInfo = this.IssueReasonApplicationTypeInfo;


            
            if (application.Save())
            {
                license.ApplicationID = application.ApplicationID;
                license.ApplicationInfo = application;

                if (license.Save())
                    return license;
            }
            return null;
        }

        public License Replace(EnIssueReason enIssueReason, int CreatedByUserID)
        {
            this.DeActivate();

            Application application = new Application();
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = EnApplicationType.RenewDrivingLicense;
            application.ApplicationTypeInfo = ApplicationType.Find((EnApplicationType) enIssueReason);
            application.enApplicationStatus = Application.EnApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = application.ApplicationTypeInfo.Fees;
            application.PersonID = this.DriverInfo.PersonID;
            application.PersonInfo = this.DriverInfo.PersonInfo;
            application.UserID = CreatedByUserID;
            application.UserInfo = User.FindByUserID(CreatedByUserID);


            License license = new License();
            license.DriverID = this.DriverID;
            license.DriverInfo = this.DriverInfo;
            license.LicenseClassID = this.LicenseClassID;
            license.LicenseClassInfo = this.LicenseClassInfo;
            license.PaidFees = 0;
            license.CreatedByUserID = CreatedByUserID;
            license.enIssueReason = enIssueReason;
            license.IssueDate = this.IssueDate;
            license.ExpirationDate = this.ExpirationDate;
            license.IsActive = this.IsActive;
            license.Notes = this.Notes;
            license.DetainInfo = this.DetainInfo;

            license.IssueReasonApplicationTypeInfo = ApplicationType.Find((EnApplicationType) enIssueReason);



            if (application.Save())
            {
                license.ApplicationID = application.ApplicationID;
                license.ApplicationInfo = application;
                if (license.Save())
                    return license;

                return null;
            }

            return null;
        }

        public bool Detain(float FineFees, int CreatedByUserID)
        {
            this.DetainInfo = new DetainLicense();
            this.DetainInfo.LicenseID = ID;
            this.DetainInfo.DetainDate = DateTime.Now;
            this.DetainInfo.FineFees = FineFees;
            this.DetainInfo.CreatedByUserID = CreatedByUserID;

            return this.DetainInfo.Save();
            
        }
    
        public bool Release(int ReleasedByUserID)
        {
            Application ReleaseApp = new Application();
            ReleaseApp.ApplicationDate = DateTime.Now;
            ReleaseApp.ApplicationTypeID = ApplicationType.EnApplicationType.ReleaseDetianedDrivingLicense;
            ReleaseApp.ApplicationTypeInfo = ApplicationType.Find(ApplicationType.EnApplicationType.ReleaseDetianedDrivingLicense);
            ReleaseApp.enApplicationStatus = Application.EnApplicationStatus.Completed;
            ReleaseApp.LastStatusDate = DateTime.Now;
            ReleaseApp.PaidFees = ReleaseApp.ApplicationTypeInfo.Fees;
            ReleaseApp.PersonID = this.DriverInfo.PersonID;
            ReleaseApp.PersonInfo = this.DriverInfo.PersonInfo;
            ReleaseApp.UserID = ReleasedByUserID;
            ReleaseApp.UserInfo = User.FindByUserID(ReleasedByUserID);

            if (ReleaseApp.Save())
                return this.DetainInfo.ReleaseLicense(ReleaseApp, ReleasedByUserID);
            
             
            return false;

        }
    }
}
