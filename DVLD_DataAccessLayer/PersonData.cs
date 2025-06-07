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
    public class PersonData
    {
        static public DataTable GetAllPeople()
        {
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth,  
				  CASE
                  WHEN People.Gender = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GenderCaption ,
			  People.Address, People.Phone, People.Email, 
              Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.PersonID";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    dataTable.Load(sqlDataReader);
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

            return dataTable;
        }

        static public bool IsPersonExistByNationalNo(String NationalNo)
        {
            bool IsFound = false;
            string Query = "Select 1 from People where NationalNo = @NationalNo";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    IsFound = true;


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

        static public bool IsPersonExistByID(int ID)
        {
            bool IsFound = false;
            string Query = "Select 1 from People where PersonID = @PersonID";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                sqlConnection.Open();

                if (sqlCommand.ExecuteScalar() != null)
                    IsFound = true;


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

        public static bool FindPersonByID(int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string cmdText = "Select * from People where PersonID = @PersonID";
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    result = true;
                    NationalNo = sqlDataReader["NationalNo"].ToString();
                    FirstName = sqlDataReader["FirstName"].ToString();
                    SecondName = sqlDataReader["SecondName"].ToString();

                    if (sqlDataReader["ThirdName"] != DBNull.Value)
                        ThirdName = sqlDataReader["ThirdName"].ToString();
                    else
                        ThirdName = "";


                    LastName = sqlDataReader["LastName"].ToString();
                    DateOfBirth = (DateTime)sqlDataReader["DateOfBirth"];
                    Gender = (byte)sqlDataReader["Gender"];
                    Address = sqlDataReader["Address"].ToString();
                    Phone = sqlDataReader["Phone"].ToString();



                    if (sqlDataReader["Email"] != DBNull.Value)
                        Email = sqlDataReader["Email"].ToString();
                    else
                        Email = "";


                    NationalityCountryID = (int)sqlDataReader["NationalityCountryID"];


                    if (sqlDataReader["ImagePath"] != DBNull.Value)
                        ImagePath = sqlDataReader["ImagePath"].ToString();
                    else
                        ImagePath = "";
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

            return result;
        }

        public static bool FindPersonByNationalNo(string NationalNo, ref int ID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string cmdText = "Select * from People where NationalNo = @NationalNo";
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    result = true;
                    ID = (int)sqlDataReader["PersonID"];
                    FirstName = sqlDataReader["FirstName"].ToString();
                    SecondName = sqlDataReader["SecondName"].ToString();
                    if (sqlDataReader["ThirdName"] != DBNull.Value)
                        ThirdName = sqlDataReader["ThirdName"].ToString();
                    else
                        ThirdName = "";


                    LastName = sqlDataReader["LastName"].ToString();
                    DateOfBirth = (DateTime)sqlDataReader["DateOfBirth"];
                    Gender = (byte)sqlDataReader["Gender"];
                    Address = sqlDataReader["Address"].ToString();
                    Phone = sqlDataReader["Phone"].ToString();



                    if (sqlDataReader["Email"] != DBNull.Value)
                        Email = sqlDataReader["Email"].ToString();
                    else
                        Email = "";


                    NationalityCountryID = (int)sqlDataReader["NationalityCountryID"];


                    if (sqlDataReader["ImagePath"] != DBNull.Value)
                        ImagePath = sqlDataReader["ImagePath"].ToString();
                    else
                        ImagePath = "";
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

            return result;
        }


        public static int AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
             DateTime DateOfBirth, Byte Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int result = -1;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = @"insert into People (NationalNo, FirstName, SecondName,ThirdName, LastName, DateOfBirth, Gender, " +
                "Address, Phone, Email,NationalityCountryID, ImagePath)values" +
                "(@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath)" +
                "select SCOPE_IDENTITY()";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@NationalNo", NationalNo);
            sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
            sqlCommand.Parameters.AddWithValue("@SecondName", SecondName);
            sqlCommand.Parameters.AddWithValue("@ThirdName", ThirdName);
            sqlCommand.Parameters.AddWithValue("@LastName", LastName);
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@Gender", Gender);
            sqlCommand.Parameters.AddWithValue("@Address", Address);
            sqlCommand.Parameters.AddWithValue("@Phone", Phone);
            sqlCommand.Parameters.AddWithValue("@Email", Email);
            sqlCommand.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != "")
            {
                sqlCommand.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }

            try
            {
                sqlConnection.Open();
                object obj = sqlCommand.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out var result2))
                {
                    result = result2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }

        public static bool UpdatePerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
             DateTime DateOfBirth, Byte Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int num = 0;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Update People set NationalNo = @NationalNo, FirstName = @FirstName, SecondName = @SecondName, ThirdName = @ThirdName, LastName = @lastName," +
                "DateOfBirth = @DateOfbirth, Gender = @Gender, Address = @Address, Phone = @phone, Email = @Email,NationalityCountryID = @NationalityCountryID, " +
                "ImagePath = @ImagePath where PersonID = @ID";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            sqlCommand.Parameters.AddWithValue("@NationalNo", NationalNo);
            sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
            sqlCommand.Parameters.AddWithValue("@SecondName", SecondName);
            sqlCommand.Parameters.AddWithValue("@ThirdName", ThirdName);
            sqlCommand.Parameters.AddWithValue("@LastName", LastName);
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@Gender", Gender);
            sqlCommand.Parameters.AddWithValue("@Address", Address);
            sqlCommand.Parameters.AddWithValue("@Phone", Phone);
            sqlCommand.Parameters.AddWithValue("@Email", Email);
            sqlCommand.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != "")
            {
                sqlCommand.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }

            try
            {
                sqlConnection.Open();
                num = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception arg)
            {
                Console.WriteLine($"Error: {arg}");
            }
            finally
            {
                sqlConnection.Close();
            }

            return num > 0;
        }

        public static bool DeletePerson(int ID)
        {
            int num = 0;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            string Query = "Delete From People Where PersonID = @PersonID";
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                sqlConnection.Open();
                num = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception arg)
            {
                Console.WriteLine($"Error: {arg}");
            }
            finally
            {
                sqlConnection.Close();
            }

            return num > 0;
        }

        static public string GetGuid()
        {
            string Guid = "";
            string Query = "Select NEWID()";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);


            try
            {
                sqlConnection.Open();
                Guid = sqlCommand.ExecuteScalar().ToString();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }


            finally
            {
                sqlConnection.Close();

            }

            return Guid;
        }

    }

}
