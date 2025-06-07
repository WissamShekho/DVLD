using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class AddEditPersonForm : Form
    {

        private Person _Person;
        private int _PersonID = -1;
        string ImagesFolderPath = "C:\\programming1\\programming_advices\\19- Full Real Project\\DVLD_People_Images";

        private enum EnGender { Male, Female };
        EnGender _Gender;

        enum EnFormMode { AddNew, Update };
        private EnFormMode _FormMode;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Loading Form

        public AddEditPersonForm()
        {
            InitializeComponent();
            _FormMode = EnFormMode.AddNew;
        }

        public AddEditPersonForm(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _FormMode = EnFormMode.Update;
        }

        public static Action ListChanged;

        public event EventHandler<int> DataBack;


        private void AddEditPersonForm_Load(object sender, EventArgs e)
        {
            SetDefaultValues();

            if (_FormMode == EnFormMode.Update)
            {
                _Person = Person.Find(_PersonID);

                if (_Person == null)
                {
                    MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                UpdateModeSettingsActivate();
            }
            else
                AddNewModeSettingsActivate();


        }

        private void SetDefaultValues()
        {
            _loadAllCountries();

            BirthDateDtp.MaxDate = DateTime.Now.AddYears(-18);
            BirthDateDtp.MinDate = DateTime.Now.AddYears(-100);

        }

        private void UpdateModeSettingsActivate()
        {
            FormTitleL.Text = "Update Person";
            PersonIDlbl.Text = _Person.ID.ToString();

            FirstNameTb.Text = _Person.FirstName;
            SecondNameTb.Text = _Person.SecondName;
            ThirdNameTb.Text = _Person.ThirdName;
            LastNameTb.Text = _Person.LastName;
            NationalNumberTb.Text = _Person.NationalNumber;
            BirthDateDtp.Value = _Person.DateOfBirth;
            AddressTb.Text = _Person.Address;
            EmailTb.Text = _Person.Email;
            PhoneTb.Text = _Person.Phone;
            CountriesCb.SelectedIndex = CountriesCb.FindString(_Person.CountryInfo.Name);

            if (_Person.ImagePath != "")
                PersonImagePB.ImageLocation = _Person.ImagePath;

            RemoveImageLl.Visible = _Person.ImagePath != "";


            if ((EnGender)_Person.Gender == EnGender.Male)
            {
                _Gender = EnGender.Male;
                MaleRB.Checked = true;
            }
            else
            {
                _Gender = EnGender.Female;
                FemaleRB.Checked = true;
            }
        }

        private void AddNewModeSettingsActivate()
        {
            _Person = new Person();
            FormTitleL.Text = "Add New Person";
            PersonIDlbl.Text = "N/A";


            MaleRB.Checked = true;
            PersonImagePB.Image = Resources.Male_512;
            _Gender = EnGender.Male;
            RemoveImageLl.Visible = false;

            CountriesCb.SelectedIndex = CountriesCb.FindString("Turkey");
        }

        private void _loadAllCountries()
        {
            DataTable tb1 = Country.GetAllCountries();

            foreach (DataRow row in tb1.Rows)
                CountriesCb.Items.Add(row["CountryName"]);
        }


        ////////////////////////////////////////////  Images  ////////////////////////////////////////////////////////////////////

        private void MaleRB_CheckedChanged(object sender, EventArgs e)
        {

            if (PersonImagePB.ImageLocation == null)
                PersonImagePB.Image = Resources.Male_512;

            _Gender = EnGender.Male;
        }

        private void FemaleRB_CheckedChanged(object sender, EventArgs e)
        {

            if (PersonImagePB.ImageLocation == null)
                PersonImagePB.Image = Resources.Female_512;

            _Gender = EnGender.Female;
        }


        private void SetImageLl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetImageD.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.gif;*.bmp";
            SetImageD.FilterIndex = 1;
            SetImageD.RestoreDirectory = true;
            SetImageD.InitialDirectory = "C:\\Users\\SPECIAL TEK\\Pictures";

            SetImageD.DefaultExt = "png";

            if (SetImageD.ShowDialog() == DialogResult.OK)
            {
                PersonImagePB.Load(SetImageD.FileName);
                RemoveImageLl.Visible = true;
            }
        }

        private void RemoveImageLl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonImagePB.ImageLocation = null;

            if (_Gender == EnGender.Male)
                PersonImagePB.Image = Resources.Male_512;
            else
                PersonImagePB.Image = Resources.Female_512;

            RemoveImageLl.Visible = false;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields Are not Filled Correctly, Put the Mouse On the Red Icon to See the Errors",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!HandleImage())
            {
                MessageBox.Show("Error handiling Image");
                return;
            }

            if (_FormMode == EnFormMode.AddNew)
                _Person = new Person();

            _Person.NationalNumber = NationalNumberTb.Text.Trim();
            _Person.FirstName = FirstNameTb.Text.Trim();
            _Person.SecondName = SecondNameTb.Text.Trim();
            _Person.ThirdName = ThirdNameTb.Text.Trim();
            _Person.LastName = LastNameTb.Text.Trim();
            _Person.Address = AddressTb.Text.Trim();
            _Person.Email = EmailTb.Text.Trim();
            _Person.Phone = PhoneTb.Text.Trim();
            _Person.CountryInfo = Country.Find(CountriesCb.Text);
            _Person.NationalityCountryID = _Person.CountryInfo.ID;
            _Person.Gender = (Person.EnGender) _Gender;
            _Person.DateOfBirth = BirthDateDtp.Value;

            if (PersonImagePB.ImageLocation != null)
                _Person.ImagePath = PersonImagePB.ImageLocation;
            else
                _Person.ImagePath = "";


            if (_Person.Save())
            {
                MessageBox.Show("Person Saved Successfully");

                FormTitleL.Text = "Update Person";
                PersonIDlbl.Text = _Person.ID.ToString();
                _FormMode = EnFormMode.Update;
                DataBack?.Invoke(this, _Person.ID);
                ListChanged?.Invoke();
            }
            else
                MessageBox.Show("Error: Data Is Not Saved!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private bool HandleImage()
        {
            // if _Person image Changed
            if (_Person.ImagePath != PersonImagePB.ImageLocation)
            {
                // if _Person has an image before
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch
                    {
                        MessageBox.Show("Error Can not Change Image!");
                    }
                }

                if (PersonImagePB.ImageLocation != null)
                {
                    string ImagePath = PersonImagePB.ImageLocation.ToString();

                    if (CopyToImagesFolder(ref ImagePath))
                    {
                        PersonImagePB.ImageLocation = ImagePath;
                        return true;
                    }
                    MessageBox.Show("Coping Mission Failed!");
                    return false;
                }
            }

            return true;
        }


        private bool CopyToImagesFolder(ref string OldImagePath)
        {
            if (!CreateFolderIfNotExist(ImagesFolderPath))
                return false;


            FileInfo FI = new FileInfo(OldImagePath);
            string NewImagePath = ImagesFolderPath + "\\" + Person.GetGuid() + FI.Extension;
            try
            {
                File.Copy(OldImagePath, NewImagePath, true);
            }
            catch
            {
                MessageBox.Show("Error: Cannot Copy Image");
            }

            OldImagePath = NewImagePath;
            return true;
        }



        private bool CreateFolderIfNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Error: Cannot Create Directory");
                    return false; ;
                }
            }
            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        // Validatings
        private void FirstNameTb_Validating(object sender, CancelEventArgs e)
        {
            string FirstName = FirstNameTb.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(FirstName))
            {
                e.Cancel = true;
                FirstNameValidationEp.SetError(FirstNameTb, "First Name Should Not Be Empty");
            }
            else
            {
                e.Cancel = false;
                FirstNameValidationEp.SetError(FirstNameTb, "");
            }
        }

        private void SecondNameTb_Validating(object sender, CancelEventArgs e)
        {

            string SecondName = SecondNameTb.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(SecondName))
            {
                e.Cancel = true;
                SecondNameValidationEp.SetError(SecondNameTb, "Second Name Should Not Be Empty");
            }
            else
            {
                e.Cancel = false;
                SecondNameValidationEp.SetError(SecondNameTb, "");
            }
        }

        private void LastNameTb_Validating(object sender, CancelEventArgs e)
        {
            string LastName = LastNameTb.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(LastName))
            {
                e.Cancel = true;
                LastNameValidationEp.SetError(LastNameTb, "last Name Should Not Be Empty");
            }
            else
            {
                e.Cancel = false;
                LastNameValidationEp.SetError(LastNameTb, "");
            }
        }

        private void NationalNumberTb_Validating(object sender, CancelEventArgs e)
        {
            if ((Person.IsPersonExist(NationalNumberTb.Text) && NationalNumberTb.Text != _Person.NationalNumber) || string.IsNullOrEmpty(NationalNumberTb.Text))
            {
                e.Cancel = true;
                NationalNumberValidationEp.SetError(NationalNumberTb, "This National Number Is Used For Another Person");
            }
            else
            {
                e.Cancel = false;
                NationalNumberValidationEp.SetError(NationalNumberTb, "");
            }
        }

        private void AddressTb_Validating(object sender, CancelEventArgs e)
        {
            string Address = AddressTb.Text.Trim();
            if (string.IsNullOrEmpty(Address))
            {
                e.Cancel = true;
                AddressValidationEp.SetError(AddressTb, "Address Field Should Not Be Empty!");
            }
            else
            {
                e.Cancel = false;
                AddressValidationEp.SetError(AddressTb, "");

            }
        }

        private void PhoneTb_Validating(object sender, CancelEventArgs e)
        {
            string Phone = PhoneTb.Text.Trim();
            if (string.IsNullOrEmpty(Phone))
            {
                e.Cancel = true;
                PhoneValidationEp.SetError(PhoneTb, "Invalid Phone Number!");
            }
            else
            {
                e.Cancel = false;
                PhoneValidationEp.SetError(PhoneTb, "");
            }
        }

        private void EmailTb_Validating(object sender, CancelEventArgs e)
        {
            string Email = EmailTb.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(Email) && !Email.EndsWith("gmail.com"))
            {
                e.Cancel = true;
                EmailValidationEp.SetError(EmailTb, "Email Format Is Not Valid!");
            }
            else
            {
                e.Cancel = false;
                EmailValidationEp.SetError(EmailTb, "");
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
