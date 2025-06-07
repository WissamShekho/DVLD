namespace DVLD.Tests
{
    partial class TestsAppointmentsManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvAppointmentsList = new System.Windows.Forms.DataGridView();
            this.cmsAppointmentsList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbTestImage = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ctrlDrivingLicenseApplicationCard1 = new DVLD.Applications.Controls.ctrlDrivingLicenseApplicationCard();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsList)).BeginInit();
            this.cmsAppointmentsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAppointmentsList
            // 
            this.dgvAppointmentsList.AllowUserToAddRows = false;
            this.dgvAppointmentsList.AllowUserToDeleteRows = false;
            this.dgvAppointmentsList.AllowUserToOrderColumns = true;
            this.dgvAppointmentsList.BackgroundColor = System.Drawing.Color.White;
            this.dgvAppointmentsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointmentsList.ContextMenuStrip = this.cmsAppointmentsList;
            this.dgvAppointmentsList.Location = new System.Drawing.Point(8, 511);
            this.dgvAppointmentsList.Name = "dgvAppointmentsList";
            this.dgvAppointmentsList.ReadOnly = true;
            this.dgvAppointmentsList.Size = new System.Drawing.Size(464, 167);
            this.dgvAppointmentsList.TabIndex = 1;
            // 
            // cmsAppointmentsList
            // 
            this.cmsAppointmentsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.cmsAppointmentsList.Name = "cmsAppointmentsList";
            this.cmsAppointmentsList.Size = new System.Drawing.Size(121, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem1_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 484);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Appointments: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 694);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "#Records: ";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(75, 694);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(13, 13);
            this.lblRecords.TabIndex = 4;
            this.lblRecords.Text = "0";
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.BackColor = System.Drawing.Color.Transparent;
            this.btnAddAppointment.Image = global::DVLD.Properties.Resources.AddAppointment_321;
            this.btnAddAppointment.Location = new System.Drawing.Point(423, 465);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(48, 40);
            this.btnAddAppointment.TabIndex = 5;
            this.btnAddAppointment.UseVisualStyleBackColor = false;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(368, 687);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 26);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbTestImage
            // 
            this.pbTestImage.BackColor = System.Drawing.Color.Transparent;
            this.pbTestImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbTestImage.Image = global::DVLD.Properties.Resources.Vision_5121;
            this.pbTestImage.Location = new System.Drawing.Point(167, 2);
            this.pbTestImage.Name = "pbTestImage";
            this.pbTestImage.Size = new System.Drawing.Size(155, 126);
            this.pbTestImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestImage.TabIndex = 7;
            this.pbTestImage.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(163, 133);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(163, 33);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Vision Test";
            // 
            // ctrlDrivingLicenseApplicationCard1
            // 
            this.ctrlDrivingLicenseApplicationCard1.Location = new System.Drawing.Point(8, 173);
            this.ctrlDrivingLicenseApplicationCard1.Name = "ctrlDrivingLicenseApplicationCard1";
            this.ctrlDrivingLicenseApplicationCard1.Size = new System.Drawing.Size(464, 288);
            this.ctrlDrivingLicenseApplicationCard1.TabIndex = 0;
            // 
            // TestsAppointmentsManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 720);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbTestImage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAppointmentsList);
            this.Controls.Add(this.ctrlDrivingLicenseApplicationCard1);
            this.Name = "TestsAppointmentsManagementForm";
            this.Text = "TestsAppoointmentsManagementForm";
            this.Load += new System.EventHandler(this.TestsAppointmentsManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsList)).EndInit();
            this.cmsAppointmentsList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTestImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Applications.Controls.ctrlDrivingLicenseApplicationCard ctrlDrivingLicenseApplicationCard1;
        private System.Windows.Forms.DataGridView dgvAppointmentsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbTestImage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ContextMenuStrip cmsAppointmentsList;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}