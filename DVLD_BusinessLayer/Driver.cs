using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class Driver
    {
        public enum EnMode { AddNewMode, UpdateMode };
        private EnMode enMode;
        public int ID { get; set; }
        public int PersonID { get; set; }
        public Person PersonInfo { get; set; }
        public int CreatedByUserID {  get; set; }
        public DateTime CreatedDate { get; set; }

        public Driver()
        {
            ID = -1;
            PersonID = -1;
            PersonInfo = new Person();
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
            enMode = EnMode.AddNewMode;
        }

        private Driver(int ID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.PersonInfo = Person.Find(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllDrivers()
        {
            return DriverData.GetAllDrivers();
        }

        public static Driver FindDriverByID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (DriverData.FindDriverByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            return null;
        }

        public static Driver FindDriverByPersonID(int PersonID)
        {
            int ID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (DriverData.FindDriverByPersonID(PersonID, ref ID, ref CreatedByUserID, ref CreatedDate))
                return new Driver(ID, PersonID, CreatedByUserID, CreatedDate);
            return null;
        }

        private bool _AddNew()
        {
            this.ID = DriverData.AddDriver(this.PersonID, this.CreatedByUserID);
            return ID != -1;
        }

        private bool _Update()
        {
            return DriverData.UpdateDriver(this.ID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
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

        public DataTable GetLocalLicenses()
        {
            return DriverData.GetLocalLicenses(this.ID);
        }

        public DataTable GetDriverActiveLicenses()
        {
            return DriverData.GetDriverActiveLicenses(this.ID);
        }

        public DataTable GetDriverNotActiveLicenses()
        {
            return DriverData.GetDriverNotActiveLicenses(this.ID);
        }

        public int DriverLicensesCount()
        {
            return DriverData.DriverLicensesCount(this.ID);
        }

        public int DriverActiveLicensesCount()
        {
            return DriverData.DriverActiveLicensesCount(this.ID);
        }

        public int DriverNotActiveLicensesCount()
        {
            return DriverLicensesCount() - DriverActiveLicensesCount();
        }

        static public bool IsDriverExistByPersonID(int PersonID)
        {
            return DriverData.IsDriverExistByPersonID(PersonID);
        }
        
        static public bool IsDriverExistByDriverID(int DriverID)
        {
            return DriverData.IsDriverExistByDriverID(DriverID);
        }

        public DataTable GetInternationalLicenses()
        {
            return InternationalDrivingLicenseData.GetDriverInternationalLicenses(this.ID);
        }
    }

}
