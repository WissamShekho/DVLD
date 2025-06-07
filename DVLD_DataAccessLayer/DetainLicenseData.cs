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
    public class DetainLicenseData
    {
       
        public static DataTable GetAllDetains()
        {
            DataTable dtAllDetains = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "select * from DetainedLicenses_View";
            SqlCommand Command = new SqlCommand(Query, Connection);


            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtAllDetains.Load(Reader);

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

            return dtAllDetains;

        }

        public static bool FindDetainByID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref float FineFees,
            ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            string Query = @"Select * from Detains where DetainID = @DetainID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    LicenseID = int.Parse(Reader["LicenseID"].ToString());
                    DetainDate = DateTime.Parse(Reader["DetainDate"].ToString());
                    FineFees = float.Parse(Reader["FineFees"].ToString());
                    CreatedByUserID = int.Parse(Reader["CreatedByUserID"].ToString());
                    IsReleased = bool.Parse(Reader["IsReleased"].ToString());

                    if (Reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = DateTime.Parse(Reader["ReleaseDate"].ToString());
                    else
                        ReleaseDate = DateTime.MinValue;

                    if (Reader["ReleaseByUserID"] != DBNull.Value)
                        ReleasedByUserID = int.Parse(Reader["ReleasedByUserID"].ToString());
                    else
                        ReleasedByUserID = -1;

                    if (Reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseApplicationID = int.Parse(Reader["ReleaseApplicationID"].ToString());
                    else
                        ReleaseApplicationID = -1;


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


        public static bool FindDetainByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate, ref float FineFees,
            ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            string Query = @"select top 1 * from DetainedLicenses where LicenseID = @LicenseID order by DetainID desc;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    DetainID = int.Parse(Reader["DetainID"].ToString());
                    DetainDate = DateTime.Parse(Reader["DetainDate"].ToString());
                    FineFees = float.Parse(Reader["FineFees"].ToString());
                    CreatedByUserID = int.Parse(Reader["CreatedByUserID"].ToString());
                    IsReleased = bool.Parse(Reader["IsReleased"].ToString());

                    if (Reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = DateTime.Parse(Reader["ReleaseDate"].ToString());
                    else
                        ReleaseDate = DateTime.MinValue;

                    if (Reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedByUserID = int.Parse(Reader["ReleasedByUserID"].ToString());
                    else
                        ReleasedByUserID = -1;

                    if (Reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseApplicationID = int.Parse(Reader["ReleaseApplicationID"].ToString());
                    else
                        ReleaseApplicationID = -1;


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


        public static bool UpdateDetain(int DetainID, int LicenseID, DateTime DetainDate, float FineFees,
            int CreatedByUserID, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int RowsAffected = 0;

            string Query = @"Update DetainedLicenses set LicenseID = @LicenseID, DetainDate = DetainDate,
            FineFees = @FineFees, CreatedByUserID = CreatedByUserID, IsReleased = @IsReleased, ReleaseDate = @ReleaseDate,
            ReleasedByUserID = @ReleasedByUserID, ReleaseApplicationID = ReleaseApplicationID
            where DetainID = @DetainID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            
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

        public static int AddDetain(int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int DetainID = -1;

            string Query = @"insert into DetainedLicenses values (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID,
                0, NULL, NULL, NULL)
                select SCOPE_IDENTITY();";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int NewID))
                {
                    DetainID = NewID;
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
            return DetainID;

        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool LicenseFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"select 1 IsReleased from DetainedLicenses where LicenseID = @LicenseID 
            and IsReleased = 0;";

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

        public static bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int RowsAffected = 0;

            string Query = @"Update DetainedLicenses set IsReleased = 1, ReleaseDate = @ReleaseDate,
            ReleasedByUserID = @ReleasedByUserID, ReleaseApplicationID = @ReleaseApplicationID
            where DetainID = @DetainID;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);
            Command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

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
    }
}
