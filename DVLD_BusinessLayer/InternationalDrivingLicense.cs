using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_BusinessLayer.ApplicationType;

namespace DVLD_BusinessLayer
{
    public class InternationalDrivingLicense
    {
        public enum EnMode { AddNewMode, UpdateMode };
        private EnMode enMode { get; set; }
        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public Application ApplicationInfo { get; set; }
        public int DriverID { get; set; }
        public Driver DriverInfo { get; set; }
        public int LocalLicenseID { get; set; }
        public License LocalLicenseInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        

        public InternationalDrivingLicense()
        {
            ID = -1;
            ApplicationID = -1;
            ApplicationInfo = new Application();
            DriverID = -1;
            DriverInfo = new Driver();
            LocalLicenseID = -1;
            LocalLicenseInfo = new License();
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = false;
            CreatedByUserID = CreatedByUserID;
            
            this.enMode = EnMode.AddNewMode;
        }

        private InternationalDrivingLicense(int ID, int ApplicationID, int DriverID, int LocalLicenseID, DateTime IssueDate,
            DateTime ExpirationDate,bool IsActive, int CreatedByUserID)
        {
            this.ID = ID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = Application.Find(ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = Driver.FindDriverByID(DriverID);
            this.LocalLicenseID = LocalLicenseID;
            this.LocalLicenseInfo = License.FindByID(LocalLicenseID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive; 
            this.CreatedByUserID = CreatedByUserID;
            
            this.enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return InternationalDrivingLicenseData.GetAllInternationalLicenses();
        }

        public static InternationalDrivingLicense FindByID(int ID)
        {
            int ApplicationID = -1, DriverID = -1, CreatedByUserID = -1, LocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;


            if (InternationalDrivingLicenseData.FindInternationalLicenseByID(ID, ref ApplicationID, ref DriverID, ref LocalLicenseID, ref IssueDate,
                ref ExpirationDate, ref IsActive, ref CreatedByUserID))
                return new InternationalDrivingLicense(ID, ApplicationID, DriverID, LocalLicenseID, IssueDate, ExpirationDate,
                    IsActive, CreatedByUserID);
            return null;
        }

        public static InternationalDrivingLicense FindByApplicationID(int ApplicationID)
        {
            int ID = -1, DriverID = -1, CreatedByUserID = -1, LocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;


            if (InternationalDrivingLicenseData.FindInternationalLicenseByApplicationID(ApplicationID, ref ID, ref DriverID,
                ref LocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
                return new InternationalDrivingLicense(ID, ApplicationID, DriverID, LocalLicenseID, IssueDate, ExpirationDate,
                    IsActive, CreatedByUserID);
            return null;
        }

        public static InternationalDrivingLicense FindDriverActiveInternationalLicense(int DriverID)
        {
            int ID = -1, ApplicationID = -1, LocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;

            if (InternationalDrivingLicenseData.FindDriverActiveInternationalLicense(DriverID, ref ID, ref ApplicationID,
                ref LocalLicenseID, ref IssueDate, ref ExpirationDate, ref CreatedByUserID))

                return new InternationalDrivingLicense(ID, ApplicationID, DriverID, LocalLicenseID, IssueDate, ExpirationDate, 
                    true, CreatedByUserID);
            return null;
        }
        private bool _AddNew()
        {

            this.ID = InternationalDrivingLicenseData.AddInternationalLicense(ApplicationID, DriverID, LocalLicenseID, IssueDate,
                ExpirationDate, CreatedByUserID);
            return this.ID != -1;
        }

        private bool _Update()
        {
            return InternationalDrivingLicenseData.UpdateInternationalLicense(ID, ApplicationID, DriverID, LocalLicenseID,
                IssueDate, ExpirationDate, IsActive, CreatedByUserID);
        }

        public bool Delete()
        {
            return InternationalDrivingLicenseData.DeleteInternationalLicense(this.ID);
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

        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            return InternationalDrivingLicenseData.IsInternationalLicenseExist(InternationalLicenseID);
        }

        public static bool DoesDriverHaveActiveInternationalLicense(int DriverID)
        {
            return InternationalDrivingLicenseData.DoesDriverHaveActiveInternationalLicense(DriverID);
        }

        public bool DoesDriverHaveActiveInternationalLicense()
        {
            return InternationalDrivingLicenseData.DoesDriverHaveActiveInternationalLicense(this.DriverID);
        }

        public bool DeActivate()
        {
            return InternationalDrivingLicenseData.DeActivateInternationalLicense(this.ID);
        }

        public bool IsExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool IssueInternationalDrivingLicense(License LocalLicense, int UserID)
        {
            Application application = new Application();
            application.ApplicationDate = DateTime.Now;
            application.enApplicationStatus = Application.EnApplicationStatus.Completed;
            application.ApplicationTypeID = EnApplicationType.InternationalDrivingLicense;
            application.ApplicationTypeInfo = ApplicationType.Find(ApplicationType.EnApplicationType.InternationalDrivingLicense);
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = ApplicationType.Find(ApplicationType.EnApplicationType.InternationalDrivingLicense).Fees;
            application.PersonID = LocalLicense.DriverInfo.PersonID;
            application.PersonInfo = LocalLicense.DriverInfo.PersonInfo;
            application.UserID = UserID;
            application.UserInfo = User.FindByUserID(UserID);
            
            if (application.Save())
            {
                this.ApplicationID = application.ApplicationID;
                this.ApplicationInfo = application;
                this.LocalLicenseID = LocalLicense.ID;
                this.LocalLicenseInfo = LocalLicense;
                this.DriverID = LocalLicense.DriverID;
                this.DriverInfo = LocalLicense.DriverInfo;
                this.IssueDate = DateTime.Now;
                this.ExpirationDate = IssueDate.AddYears(1);
                this.CreatedByUserID = UserID;

                return Save();

            }
            return false;
        }
    }
}
