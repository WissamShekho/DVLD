using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.People.PersonControls;
using DVLD_BusinessLayer;
namespace DVLD.Licenses
{
    public partial class ShowPersonLicensesHistoryForm : Form
    {
        private int _PersonID;

        public static Action DataUpdated;

        public ShowPersonLicensesHistoryForm(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void ShowPersonLicensesHistoryForm_Load(object sender, EventArgs e)
        {
            if (!Person.IsPersonExist(_PersonID))
            {
                MessageBox.Show($"No Licenses History For Person With ID: {_PersonID}", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.OnPersonSelected += LoadPersonLicenses;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
        }

        private void LoadPersonLicenses(int PersonID)
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            ctrlDriverLicenses1.LoadPersonLicenses(_PersonID);
        }

    }
}
