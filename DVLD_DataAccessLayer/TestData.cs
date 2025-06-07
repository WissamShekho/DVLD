using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class TestData
    {
        public static DataTable GetAllTests()
        {
            DataTable dtAllTests = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * From Tests";
            SqlCommand Command = new SqlCommand(Query, Connection);


            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtAllTests.Load(Reader);

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

            return dtAllTests;

        }

        public static bool FindTestByID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;

            string Query = @"Select * from Tests where TestID = @TestID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    TestAppointmentID = int.Parse(Reader["TestAppointmentID"].ToString());
                    TestResult = bool.Parse(Reader["TestResult"].ToString());
                    Notes = (Reader["Notes"].ToString());
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

        public static bool FindTestByTestAppointmentID(int TestAppointmentID, ref int TestID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;
            string Query = @"Select * from Tests where TestAppointmentID = @TestAppointmentID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    TestID = int.Parse(Reader["TestID"].ToString());
                    TestResult = bool.Parse(Reader["TestResult"].ToString());
                    Notes = Reader["Notes"].ToString();
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

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int RowsAffected = 0;

            string Query = @" Update Tests set TestAppointmentID = @TestAppointmentID, TestResult = @TestResult ,
            Notes = @Notes, CreatedByUserID = @CreatedByUserID where TestID = @TestID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestID", TestID);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            Command.Parameters.AddWithValue("@Notes", Notes);
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

        public static int AddTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            string Query = @"insert into Tests values(@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID)
            Update TestAppointments set IsLocked = 1 where TestAppointments.TestAppointmentID = @TestAppointmentID 
                        select SCOPE_IDENTITY()";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int NewID))
                {
                    TestID = NewID;
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
            return TestID;

        }
    }

}
