using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class User
    {
        private enum EnMode { NewMode, UpdateMode };
        private EnMode _enMode;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public Person PersonInfo;
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { get; set; }
        private bool IsAdmin { get; }


        public User()
        {
            UserID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            _enMode = EnMode.NewMode;
        }

        private User(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = Person.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            _enMode = EnMode.UpdateMode;
        }

        public static DataTable GetAllUsers()
        {
            return UserData.GetAllUsers();
        }

        private bool _AddNewUser()
        {
            this.UserID = UserData.AddUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return this.UserID != -1;
        }

        private bool _updateUser()
        {
            return UserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static User FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;

            if (UserData.FindUserByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
                return new User(UserID, PersonID, UserName, Password, IsActive);

            return null;
        }

        public static User FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;


            if (UserData.FindUserByPersonID(ref UserID, PersonID, ref UserName, ref Password, ref IsActive))
                return new User(UserID, PersonID, UserName, Password, IsActive);

            return null;
        }

        public static User FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;

            if (UserData.FindUserByUserNameAndPassword(ref UserID, ref PersonID, UserName, Password, ref IsActive))
                return new User(UserID, PersonID, UserName, Password, IsActive);

            return null;
        }
        public static bool Delete(int UserID)
        {
            return UserData.DeleteUser(UserID);
        }

        public bool Save()
        {
            switch (_enMode)
            {
                case EnMode.NewMode:
                    if (_AddNewUser())
                    {
                        _enMode = EnMode.UpdateMode;
                        return true;
                    }
                    return false;

                case EnMode.UpdateMode:
                    return _updateUser();

                default:
                    return false;
            }
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return UserData.IsUserExistByPersonID(PersonID);
        }
        public static bool IsUserExist(int UserID)
        {
            return UserData.IsUserExistByUserID(UserID);
        }
        public static bool IsUserExist(string UserName)
        {
            return UserData.IsUserExistByUsername(UserName);
        }

        public bool ChangePassword(String newPassword)
        {
            return UserData.ChangePassword(this.UserID, newPassword);
        }
    }
}
