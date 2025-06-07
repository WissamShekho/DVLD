using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_BusinessLayer.ApplicationType;

namespace DVLD_BusinessLayer
{
    public class Application
    {
        public enum EnMode { AddNewMode, UpdateMode };
        public enum EnApplicationStatus { New = 1, Cancelled, Completed};

        public EnMode enMode { get; set; }
        public int ApplicationID {  get; set; }
        public int PersonID { get; set; }

        public Person PersonInfo;
        public DateTime ApplicationDate { get; set; }
        public EnApplicationStatus enApplicationStatus { get; set; }
        public ApplicationType.EnApplicationType ApplicationTypeID { get; set; }

        public ApplicationType ApplicationTypeInfo;
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int UserID { get; set; }

        public User UserInfo;
        // public short ApplicationStatus {  get; set; }
    
        public Application()
        {
            this.enMode = EnMode.AddNewMode;
            this.enApplicationStatus = EnApplicationStatus.New;
            this.ApplicationID = -1;
            this.PersonID = -1;
            this.ApplicationDate = DateTime.Today;
            this.ApplicationTypeID = EnApplicationType.LocalDrivingLicense;
            this.LastStatusDate = DateTime.Today;
            this.PaidFees = 0;
            this.UserID = -1;
        }

        private Application(int iD, int personID, DateTime applicationDate, EnApplicationStatus enApplicationStatus, ApplicationType.EnApplicationType applicationTypeID, DateTime lastStatusDate, float paidFees, int userID)
        {
            ApplicationID = iD;
            PersonID = personID;
            PersonInfo = Person.Find(PersonID);
            ApplicationDate = applicationDate;
            this.enApplicationStatus = enApplicationStatus;
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeInfo = ApplicationType.Find(applicationTypeID);
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            UserID = userID;
            UserInfo = User.FindByUserID(UserID);

            enMode = EnMode.UpdateMode;
        }


        private bool _AddNew()
        {
            this.ApplicationID = ApplicationData.AddApplication(this.PersonID, (int)this.ApplicationTypeID, (short)this.enApplicationStatus, this.PaidFees, this.UserID);
            return ApplicationID != -1;
        }
        
        private bool _Update()
        {
            return ApplicationData.UpdateApplication(this.ApplicationID, (short)enApplicationStatus);
        }
        
        
        public static DataTable GetAllApplications()
        {
            return ApplicationData.GetAllApplications();
        }
        
        public static Application Find(int ID)
        {
            int PersonID = -1;
            DateTime ApplicationDate = DateTime.Today;
            int ApplicationTypeID = -1;
            short enApplicationStatus = -1;
            DateTime LastStatusDate = DateTime.Today;
            float PaidFees = 0;
            int UserID = -1;

            if (ApplicationData.FindApplicationByID(ID, ref PersonID, ref ApplicationDate, ref ApplicationTypeID,
                ref enApplicationStatus, ref LastStatusDate, ref PaidFees, ref UserID))
                return new Application(ID, PersonID, ApplicationDate, (EnApplicationStatus)enApplicationStatus, (ApplicationType.EnApplicationType) ApplicationTypeID, LastStatusDate, PaidFees, UserID);
            return null;
        }

        public bool Delete()
        {
            return ApplicationData.DeleteApplication(ApplicationID);
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

        public bool Cancel()
        {
            this.enApplicationStatus = EnApplicationStatus.Cancelled;
            return Save();
        }

        public bool Completed()
        {
            this.enApplicationStatus = EnApplicationStatus.Completed;
            return Save();
        }
    
    }
}
