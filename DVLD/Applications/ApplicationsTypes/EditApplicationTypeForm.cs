using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD.Applications.ApplicationsTypes
{
    public partial class EditApplicationTypeForm : Form
    {
        private int _AppID;
        private ApplicationType _App;
        public EditApplicationTypeForm(int appID)
        {
            InitializeComponent();
            _AppID = appID;
        }

        private void EditApplicationTypeForm_Load(object sender, EventArgs e)
        {
            _App = ApplicationType.Find((ApplicationType.EnApplicationType) _AppID);

            if (_App != null)
            {
                lblApplicationTypeID.Text = _App.ID.ToString();
                tbApplicationTypeTitle.Text = _App.Title.ToString();
                tbApplicationTypeFees.Text = $"{(float)_App.Fees}";

                //tbApplicationTypeFees.Focus();
            }
            else
            {
                MessageBox.Show("Invalid Application Type ID", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tbApplicationTypeFees_Validating(object sender, CancelEventArgs e)
        {

            float Fees;
            string result = tbApplicationTypeFees.Text.Trim();

            if (float.TryParse(result, out Fees) && Fees < 0)
            {
                errorProvider1.SetError(tbApplicationTypeFees, "Fees Can Not Be Smaller Than 0");
                e.Cancel = true;
            }
            
            else if (string.IsNullOrEmpty(result))
            {
                errorProvider1.SetError(tbApplicationTypeFees, "This Field Should Not Be Empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbApplicationTypeFees, "");
                e.Cancel = false;
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbApplicationTypeFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled => Bolocked Characters
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Fields Are Not Filled Correctly", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            _App.Title = tbApplicationTypeTitle.Text;
            if (float.TryParse(tbApplicationTypeFees.Text, out float fees))
                _App.Fees = fees;

            if (_App.Save())
            {
                MessageBox.Show("Application Type Updated Successfully", "Success", MessageBoxButtons.OK);
                this.Close();
            }
            else
                MessageBox.Show("An Error Has Occurred", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tbApplicationTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbApplicationTypeTitle.Text.Trim()))
            {
                errorProvider1.SetError(tbApplicationTypeTitle, "This Field Should Not Be Empty");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbApplicationTypeTitle, "");

            }

        }
    }
}
