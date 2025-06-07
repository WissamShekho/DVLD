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
    public class LicenseClassData
    {
        public static DataTable GetAllLicensesClasses()
        {
            DataTable dtAllLicensesClasses = new DataTable();

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * from LicenseClasses";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    dtAllLicensesClasses.Load(sqlDataReader);
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

            return dtAllLicensesClasses;
        }

        public static bool FindLicenseClassByID(int ID, ref string ClassName, ref string ClassDescription, ref short MinimumAllowedAge
            , ref short DefaultValidityLength ,ref float ClassFees)
        {
            bool IsFound = false;

            string Query = @"Select * from LicenseClasses where LicenseClassID = @ID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    ClassName = Reader["ClassName"].ToString();
                    ClassDescription = Reader["ClassDescription"].ToString();
                    float.TryParse(Reader["ClassFees"].ToString(), out ClassFees);
                    short.TryParse(Reader["MinimumAllowedAge"].ToString(), out MinimumAllowedAge);
                    short.TryParse(Reader["DefaultValidityLength"].ToString(), out DefaultValidityLength);

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

        public static bool FindLicenseClassByClassName(string ClassName, ref int ID, ref string ClassDescription, ref short MinimumAllowedAge
                    , ref short DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;

            string Query = @"Select * from LicenseClasses where ClassName = @ClassName";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    ID = int.Parse(Reader["LicenseCLassID"].ToString());
                    ClassDescription = Reader["ClassDescription"].ToString();
                    MinimumAllowedAge = short.Parse(Reader["MinimumAllowedAge"].ToString());
                    DefaultValidityLength = short.Parse(Reader["DefaultValidityLength"].ToString());
                    ClassFees = float.Parse(Reader["ClassFees"].ToString());

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
        
        public static bool UpdateLicenseClass(int ID, string ClassName, string ClassDescription, short MinimumAllowedAge
            , short DefaultValidityLength, float ClassFees)
        {
            int RowsAffected = 0;
            /*INSERT INTO newTable
            SELECT * FROM oldTable*/
            string Query = @"Update LicenseClasses set ClassName = @ClassName, ClassDescription = @ClassDescription, 
                MinimumAllowedAge = @MinimumAllowedAge, DefaultValidityLength = @DefaultValidityLength, ClassFees = @ClassFees 
                Where LicenseClassID = @ID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@ClassName", ClassName);
            Command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            Command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            Command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            Command.Parameters.AddWithValue("@ClassFees", ClassFees);

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

        public static int AddLicenseClass(string ClassName, string ClassDescription, short MinimumAllowedAge
            , short DefaultValidityLength, float ClassFees)
        {
            int ID = -1;

            string Query = @"insert into LicenseClasses values(@ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees)
                        select SCOPE_IDENTITY()";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ClassName", ClassName);
            Command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            Command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            Command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            Command.Parameters.AddWithValue("@ClassFees", ClassFees);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int NewID))
                {
                    ID = NewID;
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
            return ID;

        }

        public static bool DeleteLicenseClass(int ID)
        {
            int RowsAffected = 0;
            string Query = @"Delete from LicenseClasses where LicenseClassID = @ID";

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


    }

}
