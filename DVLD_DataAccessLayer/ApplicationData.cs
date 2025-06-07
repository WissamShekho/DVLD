using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace DVLD_DataAccessLayer
{
    public class ApplicationData
    {
        static public DataTable GetAllApplications()
        {
            DataTable dtAllApplications = new DataTable();
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * from Applications";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                    dtAllApplications.Load(Reader);
                
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

            return dtAllApplications;
        }
    
        static public int AddApplication(int PersonID, int ApplicationTypeID, short ApplicationStatus, float PaidFees, int UserID)
        {
            int ID = -1;
            string Query = @"insert into Applications values(@PersonID , GETDATE(), @ApplicationTypeID,
                    @ApplicationStatus, GETDATE(), @PaidFees, @UserID)
                    Select SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicationStatus", (short)ApplicationStatus);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();
                
                if (obj != null)
                    ID = int.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return ID;
        }

        static public bool FindApplicationByID(int ID, ref int PersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref short ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int UserID)
        {
            bool IsFound = false;

            string Query = @"select * from Applications where ApplicationID = @ID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    PersonID = int.Parse(Reader["ApplicantPersonID"].ToString());
                    ApplicationDate = (DateTime)Reader["ApplicationDate"];
                    ApplicationTypeID = int.Parse(Reader["ApplicationTypeID"].ToString());
                    ApplicationStatus = short.Parse(Reader["ApplicationStatus"].ToString());
                    LastStatusDate = (DateTime)Reader["LastStatusDate"];
                    PaidFees = float.Parse(Reader["PaidFees"].ToString());
                    UserID = int.Parse(Reader["CreatedByUserID"].ToString());

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

        public static bool UpdateApplication(int ID, short ApplicationStatus)
        {
            int RowsAffected = 0;

            string Query = @" update Applications set ApplicationStatus = @ApplicationStatus, LastStatusDate = GETDATE() where ApplicationID = @ID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

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

        public static bool DeleteApplication(int ID)
        {
            int RowsAffected = 0;
            string Query = @"delete from Applications where ApplicationID = @ID";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ID", ID);

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



        //IMPORTANT: 
        /*-- Does Person Have An Active Application Of A Specific Licenses class
        -- Public static bool Does_Person_Have_An_Active_Application_Of_A_Specific_Licenses_class(int PersonID, int ApplicationTypeID, int LicenseClassID)

        select ActiveApplications = Applications.ApplicationID , Applications.ApplicantPersonID, Applications.ApplicationTypeID
        , Applications.ApplicationStatus ,LocalDrivingLicenseApplications.LicenseClassID from Applications 
        inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.LicenseClassID = LicenseClassID 
        where ApplicantPersonID = 1  and  ApplicationTypeID = 1 and ApplicationStatus = 2 and LicenseClassID = 1
        */

    }
}
