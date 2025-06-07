using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DVLD_BusinessLayer
{
    public class LicenseClass
    {
        public enum EnMode { AddNewMode, UpdateMode };
        public enum EnLicenseClass { SmallMotorcycle=1, HeavyMotorcylce, OrdinaryDrivingLicense, Commercial, 
            Agricultural, SmallAndMediumBus, TruckAndHeavyVehicle };

        private EnMode enMode { get; set; }
        public EnLicenseClass ID { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public short MinimumAllowedAge {  get; set; }
        public short DefaultValidityLength {  get; set; }
        public float Fees { get; set; }

        public LicenseClass()
        {
            ID = EnLicenseClass.OrdinaryDrivingLicense;
            ClassName = "";
            Description = "";
            MinimumAllowedAge = short.MaxValue;
            DefaultValidityLength = short.MinValue;
            Fees = float.MinValue;
            enMode = EnMode.AddNewMode;
        }

        private LicenseClass(EnLicenseClass ID, string ClassName, string Description, short MinimumAllowedAge, 
            short DefaultValidityLength, float Fees)
        {                                                                             
            this.ID = ID;
            this.ClassName = ClassName;
            this.Description = Description;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.Fees = Fees;
            this.enMode = EnMode.UpdateMode;
        }


        public static LicenseClass Find(EnLicenseClass ID)
        {
            string ClassName = "";
            string Description = "";
            short MinimumAllowedAge = short.MaxValue;
            short DefaultValidityLength = short.MinValue;
            float Fees = float.MinValue;

            if (LicenseClassData.FindLicenseClassByID((int)ID, ref ClassName, ref Description, ref MinimumAllowedAge ,ref DefaultValidityLength, ref Fees))
                return new LicenseClass(ID, ClassName, Description, MinimumAllowedAge, DefaultValidityLength, Fees);
            return null;
        }

        public static LicenseClass Find(string ClassName)
        {
            int ID = -1;
            string Description = "";
            short MinimumAllowedAge = short.MaxValue;
            short DefaultValidityLength = short.MinValue;
            float Fees = float.MinValue;

            if (LicenseClassData.FindLicenseClassByClassName(ClassName, ref ID, ref Description, ref MinimumAllowedAge, ref DefaultValidityLength, ref Fees))
                return new LicenseClass((EnLicenseClass)ID, ClassName, Description, MinimumAllowedAge, DefaultValidityLength, Fees);
            return null;
        }

        private bool _AddNew()
        {
            int ID = LicenseClassData.AddLicenseClass(this.ClassName, this.Description, this.MinimumAllowedAge, this.DefaultValidityLength, this.Fees);

            if (ID != -1)
            {
                this.ID = (EnLicenseClass)ID;
                return true;
            }
            return false;



        }

        private bool _Update()
        {
            return LicenseClassData.UpdateLicenseClass((int)this.ID, this.ClassName, this.Description, MinimumAllowedAge, DefaultValidityLength, this.Fees);
        }

        public bool Delete(EnLicenseClass ID)
        {
            return LicenseClassData.DeleteLicenseClass((int)ID);
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


        public static DataTable GetAllLicensesClasses()
        {
            return LicenseClassData.GetAllLicensesClasses();
        }
    }
}
