using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class Person
    {
        private enum enMode { NewMode, UpdateMode };
        private enMode _Mode;

        public enum EnGender { Male, Female}
        
        public int ID { get; set; }
        public string NationalNumber { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }

        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }

        public DateTime DateOfBirth { set; get; }
        public EnGender Gender { set; get; }
        
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public Country CountryInfo { set; get; }
        public int NationalityCountryID { set; get; }
        public string ImagePath { set; get; }



        public Person()
        {
            ID = -1;
            _Mode = enMode.NewMode;

            NationalNumber = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gender = EnGender.Male;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";
        }

        public Person(string nationalNumber, string firstName, string secondName, string thirdName, string lastName,
        DateTime dateOfBirth, EnGender gender, string address, string phone, string email, int countryID, string imagePath)
        {
            NationalNumber = nationalNumber;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            Phone = phone;
            Email = email;
            NationalityCountryID = countryID;
            CountryInfo = Country.Find(NationalityCountryID);
            ImagePath = imagePath;
            _Mode = enMode.UpdateMode;
        }

        private Person(int id, string nationalNumber, string firstName, string secondName, string thirdName, string lastName,
        DateTime dateOfBirth, EnGender gender, string address, string phone, string email, int countryID, string imagePath)
        {
            ID = id;
            NationalNumber = nationalNumber;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            Phone = phone;
            Email = email;
            NationalityCountryID = countryID;
            CountryInfo = Country.Find(NationalityCountryID);
            ImagePath = imagePath;
            _Mode = enMode.UpdateMode;
        }




        public static DataTable GetAllPeople()
        {
            return PersonData.GetAllPeople();
        }

        private bool _AddNewPerson()
        {
            this.ID = PersonData.AddPerson(this.NationalNumber, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                this.DateOfBirth, (byte)this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return this.ID != -1;
        }

        private bool _UpdatePerson()
        {
            return PersonData.UpdatePerson(this.ID, this.NationalNumber, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                this.DateOfBirth, (byte)this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }

        public static Person Find(int ID)
        {
            string NationalNumber = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int CountryID = -1;
            string ImagePath = "";
            if (PersonData.FindPersonByID(ID, ref NationalNumber, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref CountryID, ref ImagePath))
            {
                return new Person(ID, NationalNumber, FirstName, SecondName, ThirdName, LastName, DateOfBirth, (EnGender)Gender,
                    Address, Phone, Email, CountryID, ImagePath);
            }

            return null;
        }

        public static Person Find(string NationalNo)
        {
            int ID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int CountryID = -1;
            string ImagePath = "";
            if (PersonData.FindPersonByNationalNo(NationalNo, ref ID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref CountryID, ref ImagePath))
            {
                return new Person(ID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, (EnGender)Gender,
                    Address, Phone, Email, CountryID, ImagePath);
            }

            return null;
        }


        public static bool Delete(int ID)
        {
            return PersonData.DeletePerson(ID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.NewMode:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.UpdateMode;
                        return true;
                    }

                    return false;

                case enMode.UpdateMode:
                    return _UpdatePerson();

                default:
                    return false;
            }
        }

        public static bool IsPersonExist(int ID)
        {
            return PersonData.IsPersonExistByID(ID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return PersonData.IsPersonExistByNationalNo(NationalNo);
        }

        public static string GetGuid()
        {
            return PersonData.GetGuid();
        }
    }
}
