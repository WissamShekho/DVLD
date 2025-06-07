using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class ApplicationType
    {
        private enum EnMode { AddNewMode, UpdateMode };
        public enum EnApplicationType{ LocalDrivingLicense = 1, RenewDrivingLicense, ReplacementLostDrivingLicense, 
            ReplacementDamagedDrivingLicense, ReleaseDetianedDrivingLicense, InternationalDrivingLicense, RetakeTest}
        private EnMode enMode { get; set; }
        public EnApplicationType ID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }

        public ApplicationType()
        {
            ID = EnApplicationType.LocalDrivingLicense;
            Title = "";
            Fees = 0;
            enMode = EnMode.AddNewMode;
        }
        
        private ApplicationType(EnApplicationType ID, string Title, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
            this.enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllApplicationsTypes()
        {
            return ApplicationTypesData.GetAllApplicationsTypes();
        }

        public static ApplicationType Find(EnApplicationType ID)
        {
            string Title = "";
            float Fees = 0;

            if (ApplicationTypesData.FindApplicationTypeByID((int)ID, ref Title, ref Fees))
                return new ApplicationType(ID, Title, Fees);
            return null;
        }

        public static ApplicationType Find(string Title)
        {
            int ID = -1;
            float Fees = 0;

            if (ApplicationTypesData.FindApplicationTypeByTitle(Title, ref ID, ref Fees))
                return new ApplicationType((EnApplicationType)ID, Title, Fees);
            return null;
        }

        private bool _AddNew()
        {
            int ID = ApplicationTypesData.AddApplicationType(this.Title, this.Fees);
            if (ID != -1)
            {
                this.ID = (EnApplicationType)ID;
                return true;
            }
            return false;
        }

        private bool _Update()
        {
            return ApplicationTypesData.UpdateApplicationType((int)this.ID, this.Title, this.Fees);
        }

        public bool Delete(int ID)
        {
            return ApplicationTypesData.DeleteApplicationType(ID);
        }
        public bool Save()
        {
            switch (enMode)
            {
                case EnMode.AddNewMode:
                    {
                        if (_AddNew())
                        {
                            this.enMode = EnMode.UpdateMode;
                            return true;
                        }

                        return false;
                    }
                case EnMode.UpdateMode:
                    {
                        return _Update();
                    }
                default:
                    return false;
            }
        }

    }
}
