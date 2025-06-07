using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class Country
    {
        public int ID { get; }
        public string Name { get; set; }

        public Country()
        {
            ID = -1;
            Name = "";
        }

        private Country(int id, string name)
        {
            ID = id;
            Name = name;
        }



        public static DataTable GetAllCountries()
        {
            return CountryData.GetAllCountries();
        }

        public static Country Find(int ID)
        {
            string CountryName = "";

            if (CountryData.FindCountryByCountryID(ID, ref CountryName))
                return new Country(ID, CountryName);
            return null;
        }

        public static Country Find(string Name)
        {
            int ID = -1;
            if (CountryData.FindCountryByCountryName(ref ID, Name))
                return new Country(ID, Name);
            return null;
        }

    }
}
