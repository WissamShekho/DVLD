using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace DVLD.Global_Classes
{
    internal static class Global
    {
        public static User CurrentUser;
        private static readonly string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

        public static bool GetStoredRegister(ref string UserName, ref string Password)
        {
            // Reading From Registry
            try
            {
                string username = (string)Registry.GetValue(KeyPath, "Username", null);
                string password = (string)Registry.GetValue(KeyPath, "Password", null);
                UserName = username ?? "";
                Password = password ?? "";

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [Obsolete("This Feature Is Deprecated")]
        public static bool GetStoredCredential(ref string UserName, ref string Password)
        {
            string FilePath = System.IO.Directory.GetCurrentDirectory() + "//Data.txt";

            if (!File.Exists(FilePath))
                return false;

            try
            {
                using (StreamReader stream = File.OpenText(FilePath))
                {
                    string Line;
                    
                    while ((Line = stream.ReadLine()) != null)
                    {
                        string[] strings = Line.Split(new string[] {"//"}, StringSplitOptions.None);

                        UserName = strings[0];
                        Password = strings[1];
                    }

                    return true;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("An Error Has Accured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public static bool SaveRegister(string UserName, string Password)
        {
            // Writing To Registry
            try
            {
                Registry.SetValue(KeyPath, "Username", UserName, RegistryValueKind.String);
                Registry.SetValue(KeyPath, "Password", Password, RegistryValueKind.String);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [Obsolete("This Feature Is Deprecated")]
        public static bool RememberUserNameAndPassword(string UserName, string Password)
        {
            string UserNameAndPassword = UserName + "//" + Password;
            string FilePath = System.IO.Directory.GetCurrentDirectory() + "//Data.txt";

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            try
            {
                using (StreamWriter stream = File.CreateText(FilePath))
                {
                    stream.WriteLine(UserNameAndPassword);
                    return true;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("An Error Has Accured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public static string HashingEncryption(string Password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] HashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return BitConverter.ToString(HashedPassword).Replace("-", "");
            }
        }
    }
}
