using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DVLD_DataAccessLayer
{
    public class InternationalDrivingLicenseData
    {
        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dtAllInternationalLicenses = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "select * from InternationalLicenses";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                    dtAllInternationalLicenses.Load(Reader);

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

            return dtAllInternationalLicenses;
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            DataTable dtAllInternationalLicenses = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "select * from InternationalLicenses where DriverID = @DriverID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtAllInternationalLicenses.Load(Reader);

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

            return dtAllInternationalLicenses;
        }

        

        static public int AddInternationalLicense(int ApplicationID, int DriverID, int LocalLicenseID, DateTime IssueDate,
            DateTime ExpirationDate, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;
            string Query = @"UPDATE InternationalLicenses SET IsActive = 0 where DriverID = @DriverID

            Insert into InternationalLicenses values(@ApplicationID, @DriverID, @LocalLicenseID,
            @IssueDate, @ExpirationDate, 1, @CreatedByUserID)
            Select SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null)
                    InternationalLicenseID = int.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return InternationalLicenseID;
        }

        static public bool FindInternationalLicenseByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID,
            ref int LocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool IsFound = false;

            string Query = @"select * from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    ApplicationID = (int)Reader["ApplicationID"];
                    DriverID = int.Parse(Reader["DriverID"].ToString());
                    LocalLicenseID = int.Parse(Reader["LocalLicenseID"].ToString());
                    IssueDate = DateTime.Parse(Reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(Reader["ExpirationDate"].ToString());
                    IsActive = bool.Parse(Reader["IsActive"].ToString());
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

        static public bool FindInternationalLicenseByApplicationID(int ApplicationID, ref int InternationalLicenseID,
            ref int DriverID, ref int LocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,
            ref bool IsActive, ref int CreatedByUserID)
        {
            bool IsFound = false;

            string Query = @"select * from InternationalLicenses where ApplicationID = @ApplicationID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    InternationalLicenseID = int.Parse(Reader["InternationalLicenseID"].ToString());
                    DriverID = int.Parse(Reader["DriverID"].ToString());
                    LocalLicenseID = int.Parse(Reader["LocalLicenseID"].ToString());
                    IssueDate = DateTime.Parse(Reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(Reader["ExpirationDate"].ToString());
                    IsActive = bool.Parse(Reader["IsActive"].ToString());
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

        public static bool FindDriverActiveInternationalLicense(int DriverID, ref int InternationalLicenseID, ref int ApplicationID,
            ref int LocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref int CreatedByUserID)
        {
            bool IsFound = false;

            string Query = @"select * from InternationalLicenses where DriverID = @DriverID and IsActive = 1;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);


            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    InternationalLicenseID = (int)Reader["InternationalLicenseID"];
                    ApplicationID = (int)Reader["ApplicationID"];
                    LocalLicenseID = int.Parse(Reader["LocalLicenseID"].ToString());
                    IssueDate = DateTime.Parse(Reader["IssueDate"].ToString());
                    ExpirationDate = DateTime.Parse(Reader["ExpirationDate"].ToString());
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


        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
            int LocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int RowsAffected = 0;

            string Query = @"Update InternationalLicenses SET ApplicationID = @ApplicationID, DriverID = @DriverID,
            LocalLicenseID = @LocalLicenseID, IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, 
            IsActive = @IsActive, CreatedByUserID = @CreatedByUserID
            where InternationalLicenseID = @InternationalLicenseID;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
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

        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            int RowsAffected = 0;
            string Query = @"delete from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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


        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            bool InternationalLicenseFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"Select 1 from InternationalLicenses Where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    InternationalLicenseFound = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return InternationalLicenseFound;
        }

        public static bool IsInternationalLicenseExistByLocalLicenseID(int LocalLicenseID)
        {
            bool InternationalLicenseFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"select * from InternationalLicenses where LocalLicenseID = @LocalLicenseID;";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    InternationalLicenseFound = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return InternationalLicenseFound;
        }


        public static bool DeActivateInternationalLicense(int InternationalLicenseID)
        {
            int RowsAffected = 0;

            string Query = @" update InternationalLicenses set IsActive = 0 where InternationalLicenseID = @InternationalLicenseID;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool ActivateInternationalLicense(int InternationalLicenseID)
        {
            int RowsAffected = 0;

            string Query = @" update InternationalLicenses set IsActive = 1 where InternationalLicenseID = @InternationalLicenseID;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool DoesDriverHaveActiveInternationalLicense(int DriverID)
        {
            bool InternationalLicenseFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"select 1 from InternationalLicenses
			where DriverID = @DriverID and IsActive = 1;";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    InternationalLicenseFound = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return InternationalLicenseFound;
        }
    }
}
