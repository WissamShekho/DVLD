using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.TestsTypes
{
    public partial class EditTestTypeForm : Form
    {
        private TestType.EnTestType _TestTypeID;
        private TestType _TestType;

        public EditTestTypeForm(TestType.EnTestType TestTypeID)
        {
            _TestTypeID = TestTypeID;
            InitializeComponent();
        }

        private void EditTestTypeForm_Load(object sender, EventArgs e)
        {
            _TestType = TestType.Find(_TestTypeID);

            if (_TestType != null )
                _LoadDataToForm();
            else
            {
                MessageBox.Show("Invalid Test Type ID", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void _LoadDataToForm()
        {
            lblTestTypeID.Text = ((int)_TestType.ID).ToString();
            tbTestTypeTitle.Text = _TestType.Title;
            tbTestTypeDescription.Text = _TestType.Description;
            tbTestTypeFees.Text = _TestType.Fees.ToString();

            tbTestTypeFees.Focus();
        }

        private void tbTestTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            string Title = tbTestTypeTitle.Text;

            if (string.IsNullOrEmpty(Title))
            {
                errorProvider1.SetError(tbTestTypeTitle, "Title Should Not Be Empty!");
                e.Cancel = true;
            }

            else if (TestType.Find(Title) != null && Title != _TestType.Title)
            {
                errorProvider1.SetError(tbTestTypeTitle, "This Test Is Already Exist, Chose Another Title");
                e.Cancel = true;
            }

            else
            {
                errorProvider1.SetError(tbTestTypeTitle, "");
                e.Cancel = false;
            }

        }

        private void tbTestTypeFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTestTypeFees.Text))
            {
                errorProvider1.SetError(tbTestTypeFees, "Fees Should Not Be Empty");
                e.Cancel = true;
            }

            else if (float.TryParse(tbTestTypeFees.Text, out float Fees))
            {
                if (Fees < 0)
                {
                   errorProvider1.SetError(tbTestTypeFees, "Fees Should Not Be Less Than Zero");
                    e.Cancel = true;
                }
            }
            else
            {
                errorProvider1.SetError(tbTestTypeFees, "");
                e.Cancel = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Fields Are Not Filled Correctly", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.Title = tbTestTypeTitle.Text;
            _TestType.Description = tbTestTypeDescription.Text;
            _TestType.Fees = float.Parse(tbTestTypeFees.Text);

            if (_TestType.Save())
            {
                MessageBox.Show("Test Type Updated Successfully", "Success", MessageBoxButtons.OK);
                this.Close();
            }
            else
                MessageBox.Show("An Error Has Occurred", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tbTestTypeFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled => Bolocked Characters
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }
    }
}
