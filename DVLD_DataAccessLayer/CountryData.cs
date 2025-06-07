using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{

    public class CountryData
    {
        public static DataTable GetAllCountries()
        {
            DataTable CountriesDt = new DataTable();

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * from Countries";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    CountriesDt.Load(sqlDataReader);
                }

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return CountriesDt;
        }

        public static bool FindCountryByCountryID(int CountryID, ref string CountryName)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * from Countries where CountryID = @CountryID";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    result = true;
                    CountryName = sqlDataReader["CountryName"].ToString();
                }

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }

        public static bool FindCountryByCountryName(ref int ID, string CountryName)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * from Countries where CountryName = @CountryName";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    ID = (int)sqlDataReader["CountryID"];
                    result = true;
                }

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }

        public static bool IsCountryExistsByID(int ID)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select Found = 1 from Countries where CountryID = @CountryID";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CountryID", ID);
            try
            {
                sqlConnection.Open();
                object obj = sqlCommand.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out var result2))
                {
                    result = result2 == 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }

        public static bool IsCountryExistsByName(string CountryName)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select Found = 1 from Countries where CountryName = @CountryName";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                sqlConnection.Open();
                object obj = sqlCommand.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out var result2))
                {
                    result = result2 == 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }

    }

    }
