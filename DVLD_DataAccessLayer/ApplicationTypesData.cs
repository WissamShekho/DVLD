using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class ApplicationTypesData
    {
        public static DataTable GetAllApplicationsTypes()
        {
            DataTable dtAllApplicationsTypes = new DataTable();

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @"Select * from ApplicationTypes";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                    dtAllApplicationsTypes.Load(sqlDataReader);

                sqlDataReader.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return dtAllApplicationsTypes;
        }
    
        public static bool UpdateApplicationType(int ApplicationTypeID, string Title, float Fees)
        {
            int RowsAffected = 0;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @" Update ApplicationTypes 
                        set ApplicationTypes.ApplicationTypeTitle = @Title,
                        ApplicationTypes.ApplicationFees = @Fees
                        where ApplicationTypes.ApplicationTypeID = @ApplicationTypeID;";


            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Title", Title);
            sqlCommand.Parameters.AddWithValue("@Fees", Fees);
            sqlCommand.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                sqlConnection.Open();
                int.TryParse(sqlCommand.ExecuteNonQuery().ToString(), out RowsAffected);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

                return RowsAffected > 0;
        }
    
        public static bool FindApplicationTypeByID(int ID, ref string Title, ref float Fees)
        { 
            bool IsFound = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @"Select * from ApplicationTypes Where ApplicationTypes.ApplicationTypeID = @ID";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    Title = reader["ApplicationTypeTitle"].ToString();
                    Fees = float.Parse(reader["ApplicationFees"].ToString());

                    IsFound = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return IsFound;
        }

        public static bool FindApplicationTypeByTitle(string Title, ref int ID, ref float Fees)
        {
            bool IsFound = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @"Select * from ApplicationTypes Where ApplicationTypeTitle = @Title";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Title", Title);
            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    ID = (int)reader["ApplicationTypeID"];
                    Fees = (float)reader["ApplicationFees"];

                    IsFound = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return IsFound;
        }

        public static int AddApplicationType(string Title, float Fees)
        {
            int ID = -1;

            string Query = @"insert into ApplicationTypes values(@Title, @Fees)
                        select SCOPE_IDENTITY()";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                Connection.Open();
                //ID = (int)Command.ExecuteScalar();
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

        public static bool DeleteApplicationType(int ID)
        {
            int RowsAffected = 0;
            string Query = @"Delete from ApplicationTypes where ApplicationTypesID = @ID";

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
