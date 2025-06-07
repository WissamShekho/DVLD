using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class LocalDrivingLicenseApplicationData
    {
        public static DataTable GetAllLocalDrivingLicensesApplications()
        {
            DataTable dtAllLDLApplications = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "select * from LocalDrivingLicenseApplications_View";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtAllLDLApplications.Load(Reader);

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

            return dtAllLDLApplications;
        }

        static public int AddLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int ID = -1;
            string Query = @"insert into LocalDrivingLicenseApplications values(@ApplicationID, @LicenseClassID) 
                            Select SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        static public bool FindLocalDrivingLicenseApplicationByID(int ID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;

            string Query = @"select * from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @ID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    ApplicationID = (int)Reader["ApplicationID"];
                    LicenseClassID = (int)Reader["LicenseClassID"];

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

        static public bool FindLocalDrivingLicenseApplicationByApplicationID(int ApplicationID, ref int ID, ref int LicenseClassID)
        {
            bool IsFound = false;

            string Query = @"select * from LocalDrivingLicenseApplications where ApplicationID = @ApplicationID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    ID = (int)Reader["ID"];
                    LicenseClassID = (int)Reader["LicenseClassID"];

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

        public static bool UpdateLocalDrivingLicenseApplication(int ID, int ApplicationID, int LicenseClassID)
        {
            int RowsAffected = 0;

            string Query = @" update LocalDrivingLicenseApplications set ApplicationID = @ApplicationID, LicenseClassID = @LicenseClassID 
                where LocalDrivingLicenseApplicationID = @ID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool DeleteLocalDrivingLicenseApplication(int ID)
        {
            int RowsAffected = 0;
            string Query = @"delete from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @ID";

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

        public static bool DoesPersonHaveAnActiveApplicationforLicenseClassID(int PersonID, int LicenseClassID)
        {
            bool IsFound = false;

            string Query = @" select 1 from LocalDrivingLicenseApplications
            inner join Applications on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
            where ApplicantPersonID = @personID and LicenseClassID = @LicenseClassID and ApplicationStatus = 1";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();

                if (Command.ExecuteScalar() != null)
                    IsFound = true;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }


            finally
            {
                Connection.Close();

            }

            return IsFound;
        }

        public static bool IsLocalDrivingLicenseApplicationExistByID(int LocalDrivingLicenseApplicationID)
        {
            bool ApplicationFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"Select 1 from LocalDrivingLicenseApplications Where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    ApplicationFound = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return ApplicationFound;
        }

        static public bool DoesTestPassed(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool TestPassed = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @" select TestPassed = 1
                from Tests inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID and TestResult = 1";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            sqlCommand.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    TestPassed = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return TestPassed;
        }

        static public int PassedTestsCount(int LocalDrivingLicenseApplicationID)
        {
            int PassedTestsCount = -1;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"Select Count(TestAppointments.TestTypeID) as PassedTestsCount
                    from Tests inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                     where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestResult = 1";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                sqlConnection.Open();

                object obj = sqlCommand.ExecuteScalar();

                if (obj != null)
                    PassedTestsCount = (int)obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return PassedTestsCount;
        }

        static public int TestTrialsCountByTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int TrialsCount = -1;
            string Query = @"select Count(Tests.TestResult) as trailsCount from Tests
                    inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                    where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                    and TestTypeID = @TestTypeID and Tests.TestResult = 0;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null)
                    TrialsCount = int.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return TrialsCount;
        }

        static public bool DoesAttendTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool TestAttended = false;
            string Query = @"SELECT 
            TestAppointments.TestAppointmentID, TestAppointments.LocalDrivingLicenseApplicationID, TestTypeID, Tests.TestResult
            FROM LocalDrivingLicenseApplications 
            INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
            INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
            
             WHERE (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
            AND(TestAppointments.TestTypeID = @TestTypeID) AND (Tests.TestResult = 0) 
             ORDER BY TestAppointments.TestAppointmentID desc;"; // هل يجب إزالة الشرط الأخير ؟

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null)
                    TestAttended = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return TestAttended;
        }

        static public bool IsThereAnyActiveTestAppointmentByTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool AcvtiveAppointmentFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @" select TestAppointments.LocalDrivingLicenseApplicationID, TestAppointments.TestAppointmentID,
                TestAppointments.TestTypeID, TestAppointments.IsLocked
                from TestAppointments
                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID and IsLocked = 0;";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            sqlCommand.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    AcvtiveAppointmentFound = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return AcvtiveAppointmentFound;
        }

        static public DateTime GetLastAppointmentDateByTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DateTime AppointmentDate = DateTime.MinValue;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"select Top 1 TestAppointments.AppointmentDate from testAppointments
            where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            and TestAppointments.TestTypeID = @TestTypeID
            order by TestAppointmentID desc;";
            SqlCommand Command = new SqlCommand(Query, sqlConnection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                sqlConnection.Open();

                object obj = Command.ExecuteScalar();

                if (obj != null)
                    AppointmentDate = DateTime.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return AppointmentDate;
        }

        public static DataTable GetAllTestAppointmentsForTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dtAllAppiontments = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @"select TestAppointmentID, AppointmentDate, PaidFees, IsLocked from TestAppointments
            where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID
            Order by TestAppointmentID desc";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtAllAppiontments.Load(Reader);

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

            return dtAllAppiontments;
        }

        public static int GetLastAppointmentIDByTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int ID = -1;
            string Query = @"select top 1 TestAppointmentID from TestAppointments 
                where LocalDrivingLicenseApplicationID = 36 and TestTypeID = 1
                order by TestAppointmentID desc;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

    }
}
