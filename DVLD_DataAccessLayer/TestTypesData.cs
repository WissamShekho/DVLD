using Microsoft.SqlServer.Server;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class TestTypesData
    {
        public static DataTable GetAllTestsTypes()
        {
            DataTable dtAllTestsTypes = new DataTable();

            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * From TestTypes";
            SqlCommand Command = new SqlCommand(Query, Connection);


            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                    dtAllTestsTypes.Load(Reader);

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

            return dtAllTestsTypes;

        }
    
        public static bool FindTestTypeByID(int ID, ref string Title, ref string Description, ref float Fees)
        {
            bool IsFound = false;

            string Query = @"Select * from TestTypes where TestTypeID = @ID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    Title = Reader["TestTypeTitle"].ToString();
                    Description = Reader["TestTypeDescription"].ToString();
                    Fees = float.Parse(Reader["TestTypeFees"].ToString());



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

        public static bool FindTestTypeByTitle(string Title, ref int ID, ref string Description, ref float Fees)
        {
            bool IsFound = false;                             
            string Query = @"Select * from TestTypes where TestTypeTitle = @Title";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Title", Title);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                
                if (Reader.Read())
                {
                    ID = (int)Reader["TestTypeID"];
                    Description = Reader["TestTypeDescription"].ToString();
                    Fees = (float)Reader["TestTypeFees"];

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
    
        public static bool UpdateTestType(int ID, string Title, string Description, float Fees)
        {
            int RowsAffected = 0;

            string Query = @" Update TestTypes set TestTypeTitle = @Title, TestTypeDescription = @Description ,TestTypeFees = @Fees where TestTypeID = @ID";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Description", Description);
            Command.Parameters.AddWithValue("@Fees", Fees);

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
    
        public static int AddTestType(string Title, string Description, float Fees)
        {
            int ID = -1;

            string Query = @"insert into TestTypes values(@Title, @Description, @Fees)
                        select SCOPE_IDENTITY()";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Description", Description);
            Command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int NewID))
                {
                    ID = NewID;
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return ID;

        }
    
        public static bool DeleteTestType(int ID)
        {
            int RowsAffected = 0;
            string Query = @"Delete from TestTypes where TestTypesID = @ID";
            
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

        public static int GetTestsTypesCount()
        {
            int TestsTypesCount = 0;

            string Query = @"select Count(TestTypeID) as TestsTypesCount from TestTypes;";
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null)
                    TestsTypesCount = int.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return TestsTypesCount;

        }

    }
    
}
