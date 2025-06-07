using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public class TestAppointmentData
    {
        static public bool FindTestAppointment(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsFound = false;

            string Query = @"select * from TestAppointments where TestAppointmentID = @TestAppointmentID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    TestAppointmentID = (int)Reader["TestAppointmentID"];
                    TestTypeID = (int)Reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)Reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)Reader["AppointmentDate"];
                    PaidFees = float.Parse(Reader["PaidFees"].ToString());
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    IsLocked = (bool)Reader["IsLocked"];

                    if (Reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeTestApplicationID = (int)Reader["RetakeTestApplicationID"];



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

        static public DataTable GetAllTestsAppointments()
        {
            DataTable dtAllTestsAppiontments = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "select * from TestAppointments_View";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                    dtAllTestsAppiontments.Load(Reader);

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

            return dtAllTestsAppiontments;
        }

        static public DataTable GetAllTestsAppointmentsforLocalDrivingLicenseApplicationOfTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dtAllTestsAppiontments = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @"Select TestAppointmentID, AppointmentDate, PaidFees, IsLocked from TestAppointments_View
                        where TestTypeID = @TestTypeID and LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows) // Should I Replace 'HasRows' With 'Read()' ?
                    dtAllTestsAppiontments.Load(Reader);

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

            return dtAllTestsAppiontments;
        }

        static public bool UpdateTestAppointment(int TestAppointmentID, DateTime AppointmentDate)
        {
            int RowsAffected = 0;

            string Query = @"update TestAppointments set AppointmentDate = @AppointmentDate where TestAppointmentID = @TestAppointmentID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

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

        static public int AddTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
            float PaidFees, int CreatedByUserID, int RetakeTestApplicationID)
        {
            int ID = -1;
            string Query = @"insert into TestAppointments values(@TestTypeID, @LocalDrivingLicenseApplicationID,
                @AppointmentDate, @PaidFees, @CreatedByUserID, 0, @RetakeTestApplicationID)
                            Select SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (RetakeTestApplicationID == -1)

                Command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
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


        static public int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            string Query = @"select * from Tests where TestAppointmentID = @TestAppointmentID;";

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null)
                    TestID = int.Parse(obj.ToString());
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
