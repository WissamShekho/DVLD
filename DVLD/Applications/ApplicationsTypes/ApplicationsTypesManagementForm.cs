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
    public partial class ApplicationsTypesManagementForm : Form
    {
        public ApplicationsTypesManagementForm()
        {
            InitializeComponent();
        }

        private void _RefreshData()
        {
            dgvApplicationsTypesList.DataSource = ApplicationType.GetAllApplicationsTypes();

            dgvApplicationsTypesList.Columns["ApplicationTypeTitle"].HeaderText = "Title";
            dgvApplicationsTypesList.Columns["ApplicationTypeTitle"].Width = 250;

            dgvApplicationsTypesList.Columns["ApplicationTypeID"].HeaderText = "ID";
            dgvApplicationsTypesList.Columns["ApplicationTypeID"].Width = 50;

            dgvApplicationsTypesList.Columns["ApplicationFees"].HeaderText = "Fees";
            dgvApplicationsTypesList.Columns["ApplicationFees"].Width = 100;

            lblAppsTypesCount.Text = dgvApplicationsTypesList.RowCount.ToString();
        }

        private void ApplicationsTypesManagementFom_Load(object sender, EventArgs e)
        {
            _RefreshData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationTypeForm editApplicationTypeForm = new EditApplicationTypeForm((int)dgvApplicationsTypesList.CurrentRow.Cells[0].Value);
            editApplicationTypeForm.ShowDialog();

            //refresh Data
            dgvApplicationsTypesList.DataSource = ApplicationType.GetAllApplicationsTypes();
        }
    }
}
