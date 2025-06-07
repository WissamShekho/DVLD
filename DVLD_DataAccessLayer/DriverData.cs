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
    public class DriverData
    {
        public static DataTable GetAllDrivers()
        {
            DataTable dtAllDrivers = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * From Drivers_View";
            SqlCommand Command = new SqlCommand(Query, Connection);


            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtAllDrivers.Load(Reader);

                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return dtAllDrivers;

        }

        public static bool FindDriverByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            string Query = @"Select * from Drivers where DriverID = @DriverID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    PersonID = int.Parse(Reader["PersonID"].ToString());
                    CreatedByUserID = int.Parse(Reader["CreatedByUserID"].ToString());
                    CreatedDate = DateTime.Parse(Reader["CreatedDate"].ToString());

                    IsFound = true;
                    Reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool FindDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;
            string Query = @"Select * from Drivers where PersonID = @PersonID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    DriverID = int.Parse(Reader["DriverID"].ToString());
                    CreatedByUserID = int.Parse(Reader["CreatedByUserID"].ToString());
                    CreatedDate = DateTime.Parse(Reader["CreatedDate"].ToString());

                    IsFound = true;
                    Reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int RowsAffected = 0;

            string Query = @" Update Drivers set PersonID = @PersonID, CreatedByUserID = @CreatedByUserID, 
            CreatedDate = @CreatedDate where DriverID = @DriverID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return RowsAffected > 0;

        }

        public static int AddDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;

            string Query = @"insert into Drivers values(@PersonID, @CreatedByUserID, GETDATE())
                        select SCOPE_IDENTITY()";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int NewID))
                {
                    DriverID = NewID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return DriverID;

        }

        public static DataTable GetLocalLicenses(int DriverID)
        {
            DataTable dtDriverActiveLicenses = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @"select LicenseID, ApplicationID, LicenseClasses.ClassName, IssueDate, ExpirationDate, IsActive
            from Licenses inner join LicenseClasses on Licenses.LicenseClassID = LicenseClasses.LicenseClassID
            where DriverID = @DriverId order by ClassName asc, LicenseID desc;";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtDriverActiveLicenses.Load(Reader);

                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return dtDriverActiveLicenses;
        }

        public static DataTable GetDriverActiveLicenses(int DriverID)
        {
            DataTable dtDriverActiveLicenses = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * From Licenses where DriverID = @DriverID  and IsActive = 1";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtDriverActiveLicenses.Load(Reader);

                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return dtDriverActiveLicenses;
        }

        public static DataTable GetDriverNotActiveLicenses(int DriverID)
        {
            DataTable dtDriverActiveNotLicenses = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * From Licenses where DriverID = @DriverID  and IsActive = 0";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtDriverActiveNotLicenses.Load(Reader);

                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return dtDriverActiveNotLicenses;
        }

        public static int DriverLicensesCount(int DriverID)
        {
            int LicensesCount = 0;

            string Query = @"select Count(Licenses.licenseID) AS LicensesCount from Licenses where DriverID = @DriverID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);
            
            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null)
                    LicensesCount = int.Parse(obj.ToString());
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return LicensesCount;
        }

        public static int DriverActiveLicensesCount(int DriverID)
        {
            int ActiveLicensesCount = 0;

            string Query = @"select Count(Licenses.licenseID) AS LicensesCount 
            from Licenses where DriverID = @DriverID and IsActive = 1";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null)
                    ActiveLicensesCount = int.Parse(obj.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return ActiveLicensesCount;
        }

        public static bool IsDriverExistByPersonID(int PersonID)
        {
            bool IsFound = false;

            string Query = @"Select 1 from Drivers where PersonID = @PersonID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();

                if (Command.ExecuteScalar() != null)
                   IsFound = true;
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;

        }

        public static bool IsDriverExistByDriverID(int DriverID)
        {
            bool IsFound = false;

            string Query = @"Select 1 from Drivers where DriverID = @DriverID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();

                if (Command.ExecuteScalar() != null)
                    IsFound = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;

        }


    }

}
