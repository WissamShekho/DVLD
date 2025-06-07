using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
   public class TestType
   {
        public enum EnMode { AddNewMode, UpdateMode };
        public enum EnTestType { VisionTest =1 , WrittenTest, StreetTest};

        private EnMode enMode;
        public EnTestType ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

       public TestType()
       {
           ID = EnTestType.VisionTest;
           Title = "";
           Description = "";
           Fees = 0;
           enMode = EnMode.AddNewMode;
       }

       private TestType(EnTestType ID, string Title, string Description, float Fees)
       {
           this.ID = ID;
           this.Title = Title;
           this.Description = Description;
           this.Fees = Fees;
           this.enMode = EnMode.UpdateMode;
       }

       public static DataTable GetAllTestsTypes()
       {
           return TestTypesData.GetAllTestsTypes();
       }

       public static TestType Find(EnTestType ID)
       {
            string Title = "";
            string Description = "";
            float Fees = 0;

            if (TestTypesData.FindTestTypeByID((int)ID, ref Title,ref Description, ref Fees))
                return new TestType(ID, Title, Description, Fees);
            return null;
       }

       public static TestType Find(string Title)
       {
            int ID = -1;
            string Description = "";
             float Fees = 0;

            if (TestTypesData.FindTestTypeByTitle(Title, ref ID, ref Description, ref Fees))
                  return new TestType((EnTestType) ID, Title, Description, Fees);
             return null;
       }

        private bool _AddNew()
        {
            int ID = TestTypesData.AddTestType(this.Title, this.Description, this.Fees);

            if (ID != -1)
            {
                this.ID = (EnTestType)ID;
                return true;
            }
            return false;

            
            
        }

        private bool _Update()
       {
           return TestTypesData.UpdateTestType((int)this.ID, this.Title, this.Description, this.Fees);
       }

        public bool Delete(EnTestType ID)
        {
            return TestTypesData.DeleteTestType((int)ID);
        }

        public bool Save()
       {
           switch (enMode)
           {
               case EnMode.AddNewMode:
                   {
                       if (_AddNew())
                       {
                           this.enMode = EnMode.UpdateMode;
                           return true;
                       }

                       return false;
                   }
               case EnMode.UpdateMode:
                   {
                       return _Update();
                   }
               default:
                   return false;
           }
       }

        internal static int TestsTypesCount()
        {
            return TestTypesData.GetTestsTypesCount();
        }
    }
}
