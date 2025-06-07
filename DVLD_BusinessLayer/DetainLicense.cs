using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class DetainLicense
    {
        public enum EnMode { AddNewMode, UpdateMode };
        private EnMode enMode;
        public int ID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleasedDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID{ get; set; }
        public Application ReleaseApplicationInfo { get; set; }

        public DetainLicense()
        {
            ID = -1;
            LicenseID = -1;
            DetainDate = DateTime.MinValue;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleasedDate = DateTime.MinValue;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
            ReleaseApplicationInfo = new Application();

            enMode = EnMode.AddNewMode;
        }

        private DetainLicense(int iD, int licenseID, DateTime detainDate, float fineFees, int createdByUserID,
            bool isReleased, DateTime releasedDate, int releasedByUserID, int releaseApplicationID)
        {
            ID = iD;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleasedDate = releasedDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
            ReleaseApplicationInfo = Application.Find(ReleaseApplicationID);

            enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllDetains()
        {
            return DetainLicenseData.GetAllDetains();
        }

        public static DetainLicense FindDetainByID(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.MinValue, ReleasedDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;


            if (DetainLicenseData.FindDetainByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID,
                ref IsReleased, ref ReleasedDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new DetainLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleasedDate, 
                    ReleasedByUserID, ReleaseApplicationID);
            return null;
        }

        public static DetainLicense FindDetainByLicenseID(int LicenseID)
        {
            int ID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.MinValue, ReleasedDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;


            if (DetainLicenseData.FindDetainByLicenseID(LicenseID, ref ID, ref DetainDate, ref FineFees, ref CreatedByUserID,
                ref IsReleased, ref ReleasedDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new DetainLicense(ID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleasedDate,
                    ReleasedByUserID, ReleaseApplicationID);
            return null;
        }

        private bool _AddNew()
        {
            this.ID = DetainLicenseData.AddDetain(LicenseID, DetainDate, FineFees, CreatedByUserID);
            return ID != -1;
        }

        private bool _Update()
        {
            return DetainLicenseData.UpdateDetain(ID, LicenseID, DetainDate, FineFees, CreatedByUserID, ReleasedDate,
                ReleasedByUserID, ReleaseApplicationID);
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

        public static bool IsLicenseDetained(int LicenseID)
        {
            return DetainLicenseData.IsLicenseDetained(LicenseID);
        }
    
        public bool ReleaseLicense(Application ReleaseApplication, int ReleasedByUserID)
        {  
            this.ReleaseApplicationID = ReleaseApplication.ApplicationID;
            this.ReleasedByUserID = ReleaseApplication.UserID;
            this.ReleaseApplicationInfo = ReleaseApplication;

            return DetainLicenseData.ReleaseDetainedLicense(ID, ReleasedByUserID, ReleaseApplicationID);

        }
    
    }
}
