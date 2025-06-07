using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{

    public class UserData
    {
        public static DataTable GetAllUsers()
        {
            DataTable dtAllUsers = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"Select Users.UserID, Users.PersonID,
                FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName
                , Users.UserName, Users.Password, Users.IsActive 
                from Users Inner join People On Users.PersonID = People.PersonID";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    dtAllUsers.Load(sqlDataReader);
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

            return dtAllUsers;
        }

        public static bool IsUserExistByUserID(int UserID)
        {
            bool IsUserExist = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"Select 1 from Users Where UserID = @UserID";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    IsUserExist = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return IsUserExist;
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            bool IsUserExist = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"Select 1 from Users Where PersonID = @PersonID";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    IsUserExist = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return IsUserExist;
        }

        public static bool IsUserExistByUsername(String UserName)
        {
            bool IsUserExist = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            string Query = @"Select 1 from Users Where UserName = @UserName";

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    IsUserExist = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);

            }
            finally
            {
                sqlConnection.Close();
            }

            return IsUserExist;
        }



        public static bool FindUserByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password,
        ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * from Users where UserID = @UserID";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    PersonID = (int)sqlDataReader["PersonID"];
                    UserName = sqlDataReader["UserName"].ToString();
                    Password = sqlDataReader["Password"].ToString();
                    IsActive = (bool)sqlDataReader["IsActive"];

                    IsFound = true;
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

            return IsFound;
        }

        public static bool FindUserByPersonID(ref int UserID, int PersonID, ref string UserName, ref string Password,
        ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * from Users where PersonID = @PersonID";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    UserID = (int)sqlDataReader["UserID"];
                    UserName = sqlDataReader["UserName"].ToString();
                    Password = sqlDataReader["Password"].ToString();
                    IsActive = (bool)sqlDataReader["IsActive"];

                    IsFound = true;
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

            return IsFound;
        }

        public static bool FindUserByUserNameAndPassword(ref int UserID, ref int PersonID, string UserName, string Password,
        ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Select * from Users where UserName = @UserName And Password = @Password";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@UserName", UserName);
            sqlCommand.Parameters.AddWithValue("@Password", Password);

            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    UserID = (int)sqlDataReader["UserID"];
                    PersonID = (int)sqlDataReader["PersonID"];
                    IsActive = (bool)sqlDataReader["IsActive"];

                    IsFound = true;
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

            return IsFound;
        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowsAffected = 0;

            string Query = @" Update Users Set PersonID = @PersonID, UserName = @UserName, Password = @Password, IsActive = @IsActive
                where UserID = @UserID";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);
            sqlCommand.Parameters.AddWithValue("@UserID", UserID);
            sqlCommand.Parameters.AddWithValue("@UserName", UserName);
            sqlCommand.Parameters.AddWithValue("@Password", Password);
            sqlCommand.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                sqlConnection.Open();
                RowsAffected = sqlCommand.ExecuteNonQuery();


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

        public static int AddUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;

            string Query = @"Insert Into Users values (@PersonID, @UserName, @Password, @IsActive)
                Select SCOPE_IDENTITY()";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);
            sqlCommand.Parameters.AddWithValue("@UserName", UserName);
            sqlCommand.Parameters.AddWithValue("@Password", Password);
            sqlCommand.Parameters.AddWithValue("@IsActive", IsActive);


            try
            {
                sqlConnection.Open();
                object obj = sqlCommand.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int NewID))
                {
                    UserID = NewID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return UserID;
        }

        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;

            string Query = @"Delete From Users Where UserID = @UserID";

            SqlConnection sqlConnection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                sqlConnection.Open();

                RowsAffected = sqlCommand.ExecuteNonQuery();
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

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            int RowsAffected = 0;

            string Query = @" Update Users Set Password = @NewPassword where UserID = @UserID";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@UserID", UserID);
            sqlCommand.Parameters.AddWithValue("@NewPassword", NewPassword);

            try
            {
                sqlConnection.Open();
                RowsAffected = sqlCommand.ExecuteNonQuery();


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
    }
}
