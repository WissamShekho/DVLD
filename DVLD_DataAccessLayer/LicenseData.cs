using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public class LicenseData
    {
        public static DataTable GetAllLicenses()
        {
            DataTable dtAllLicenses = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "select * from Licenses";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                    dtAllLicenses.Load(Reader);

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

            return dtAllLicenses;
        }

        static public int AddLicense(int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate,
            DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            string Query = @"insert into Licenses values(@ApplicationID, @DriverID, @LicenseClassID, @IssueDate, 
            @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID) 
                            Select SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (string.IsNullOrEmpty(Notes))
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Notes", Notes);

            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null)
                   LicenseID = int.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return LicenseID;
        }

        static public bool FindLicenseByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, 
            ref int IssueReason, ref int CreatedByUserID)

        {
            bool IsFound = false;

            string Query = @"select * from Licenses where LicenseID = @LicenseID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID",LicenseID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = int.Parse(Reader["DriverID"].ToString());
                    LicenseClassID = int.Parse(Reader["LicenseClassID"].ToString());
                    IssueDate = DateTime.Parse(Reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(Reader["ExpirationDate"].ToString());

                    if (string.IsNullOrEmpty(Reader["notes"].ToString()))
                        Notes = "";
                    else
                        Notes = Reader["notes"].ToString();
                    
                    PaidFees = float.Parse(Reader["PaidFees"].ToString());
                    IsActive = bool.Parse(Reader["IsActive"].ToString());
                    IssueReason = int.Parse(Reader["IssueReason"].ToString());
                    CreatedByUserID = int.Parse(Reader["CreatedByUserID"].ToString());



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

        static public bool FindLicenseByApplicationID(int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClassID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive,
            ref int IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            string Query = @"select * from Licenses where ApplicationID = @ApplicationID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    LicenseID= int.Parse(Reader["LicenseID"].ToString());
                    DriverID = int.Parse(Reader["DriverID"].ToString());
                    LicenseClassID = int.Parse(Reader["LicenseClassID"].ToString());
                    IssueDate = DateTime.Parse(Reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(Reader["ExpirationDate"].ToString());

                    if (string.IsNullOrEmpty(Reader["notes"].ToString()))
                        Notes = "";
                    else
                        Notes = Reader["notes"].ToString();
                    
                    PaidFees = float.Parse(Reader["PaidFees"].ToString());
                    IsActive = bool.Parse(Reader["IsActive"].ToString());
                    IssueReason = int.Parse(Reader["IssueReason"].ToString());
                    CreatedByUserID = int.Parse(Reader["CreatedByUserID"].ToString());



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

        static public bool FindLicenseByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID,
            ref int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate,
            ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref int IssueReason,
            ref int CreatedByUserID)
        {
            bool IsFound = false;

            string Query = @"select * from Licenses
            inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID = Licenses.ApplicationID
            where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    LicenseID = (int)Reader["LicenseID"];
                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = int.Parse(Reader["DriverID"].ToString());
                    LicenseClassID = int.Parse(Reader["LicenseClassID"].ToString());
                    IssueDate = DateTime.Parse(Reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(Reader["ExpirationDate"].ToString());

                    if (string.IsNullOrEmpty(Reader["notes"].ToString()))
                        Notes = "";
                    else
                        Notes = Reader["notes"].ToString();

                    PaidFees = float.Parse(Reader["PaidFees"].ToString());
                    IsActive = bool.Parse(Reader["IsActive"].ToString());
                    IssueReason = int.Parse(Reader["IssueReason"].ToString());
                    CreatedByUserID = int.Parse(Reader["CreatedByUserID"].ToString());



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

        public static bool FindActiveLicenseForThisPersonByLicenseClass(int PersonID, int LicenseClassID, ref int LicenseID,
    ref int ApplicationID, ref int DriverID, ref DateTime IssueDate, ref DateTime ExpirationDate,
    ref string Notes, ref float PaidFees, ref int IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            string Query = @"select Licenses.* from Licenses
            inner Join Drivers on Drivers.DriverID = Licenses.DriverID
            where PersonID = @PersonID and LicenseClassID = @LicenseClassID and IsActive = 1;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    LicenseID = (int)Reader["LicenseID"];
                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = int.Parse(Reader["DriverID"].ToString());
                    IssueDate = DateTime.Parse(Reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(Reader["ExpirationDate"].ToString());

                    if (string.IsNullOrEmpty(Reader["notes"].ToString()))
                        Notes = "";
                    else
                        Notes = Reader["notes"].ToString();

                    PaidFees = float.Parse(Reader["PaidFees"].ToString());
                    IssueReason = int.Parse(Reader["IssueReason"].ToString());
                    CreatedByUserID = int.Parse(Reader["CreatedByUserID"].ToString());



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


        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, 
            DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int RowsAffected = 0;

            string Query = @" update Licenses set ApplicationID = @ApplicationID, DriverID = @DriverID, 
            LicenseClassID = @LicenseClassID, IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, Notes = @Notes,
            PaidFees = @PaidFees, IsActive = @IsActive, IssueReason = @IssueReason, CreatedByUserID = @CreatedByUserID
            where LicenseID = @LicenseID;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID",LicenseID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (string.IsNullOrEmpty(Notes))
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Notes", Notes);
            
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool DeleteLicense(int LicenseID)
        {
            int RowsAffected = 0;
            string Query = @"delete from Licenses where LicenseID = @LicenseID";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID",LicenseID);

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

        public static bool IsLicenseExist(int LicenseID)
        {
            bool LicenseFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"Select 1 from Licenses Where LicenseID = @LicenseID";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    LicenseFound = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return LicenseFound;
        }

        public static bool DeActivateLicense(int LicenseID)
        {
            int RowsAffected = 0;

            string Query = @" update Licenses set IsActive = 0 where LicenseID = @LicenseID;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool ActivateLicense(int LicenseID)
        {
            int RowsAffected = 0;

            string Query = @" update Licenses set IsActive = 1 where LicenseID = @LicenseID;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool DoesPersonHaveActiveLicenseByLicenseClass(int PersonID, int LicenseClassID)
        {
            bool LicenseFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"select 1 from Licenses inner Join Drivers on Drivers.DriverID = Licenses.DriverID
            where PersonID = @PersonID and Licenses.LicenseClassID = @LicenseClassID and IsActive = 1;";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    LicenseFound = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return LicenseFound;
        }
        
    }
}
