using DVLD.Applications.ApplicationsTypes;
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
    public partial class TestsTypesManagementForm : Form
    {
        public TestsTypesManagementForm()
        {
            InitializeComponent();
        }

        private void TestsTypesManagement_Load(object sender, EventArgs e)
        {
            DataTable dtAllTestsTypes = TestType.GetAllTestsTypes();

            dgvTestsTypesList.DataSource = dtAllTestsTypes;

            dgvTestsTypesList.Columns["TestTypeTitle"].HeaderText = "Title";
            dgvTestsTypesList.Columns["TestTypeTitle"].Width = 150;

            dgvTestsTypesList.Columns["TestTypeDescription"].HeaderText = "Description";
            dgvTestsTypesList.Columns["TestTypeDescription"].Width = 250;

            dgvTestsTypesList.Columns["TestTypeFees"].HeaderText = "Fees";
            dgvTestsTypesList.Columns["TestTypeFees"].Width = 60;
            
            dgvTestsTypesList.Columns["TestTypeID"].HeaderText = "ID";
            dgvTestsTypesList.Columns["TestTypeID"].Width = 25;



            lblTestsTypesCount.Text = dtAllTestsTypes.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestTypeForm editTestTypeForm = new EditTestTypeForm((TestType.EnTestType)dgvTestsTypesList.CurrentRow.Cells[0].Value);
            editTestTypeForm.ShowDialog();

            dgvTestsTypesList.DataSource = TestType.GetAllTestsTypes();
        }
    }
}
