namespace DVLD.Applications.ApplicationsTypes
{
    partial class ApplicationsTypesManagementForm
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
            this.dgvApplicationsTypesList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAppsTypesCount = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmsApplicationTypesList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editApplicationTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicationsTypesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsApplicationTypesList.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvApplicationsTypesList
            // 
            this.dgvApplicationsTypesList.AllowUserToAddRows = false;
            this.dgvApplicationsTypesList.AllowUserToDeleteRows = false;
            this.dgvApplicationsTypesList.AllowUserToOrderColumns = true;
            this.dgvApplicationsTypesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApplicationsTypesList.GridColor = System.Drawing.Color.White;
            this.dgvApplicationsTypesList.Location = new System.Drawing.Point(12, 217);
            this.dgvApplicationsTypesList.Name = "dgvApplicationsTypesList";
            this.dgvApplicationsTypesList.ReadOnly = true;
            this.dgvApplicationsTypesList.Size = new System.Drawing.Size(457, 203);
            this.dgvApplicationsTypesList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(56, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage Applications Types";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(362, 426);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 438);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "#Applications Types: ";
            // 
            // lblAppsTypesCount
            // 
            this.lblAppsTypesCount.AutoSize = true;
            this.lblAppsTypesCount.Location = new System.Drawing.Point(124, 438);
            this.lblAppsTypesCount.Name = "lblAppsTypesCount";
            this.lblAppsTypesCount.Size = new System.Drawing.Size(13, 13);
            this.lblAppsTypesCount.TabIndex = 5;
            this.lblAppsTypesCount.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Application_Types_5121;
            this.pictureBox1.Location = new System.Drawing.Point(163, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(171, 149);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // cmsApplicationTypesList
            // 
            this.cmsApplicationTypesList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editApplicationTypeToolStripMenuItem});
            this.cmsApplicationTypesList.Name = "cmsApplicationTypesList";
            this.cmsApplicationTypesList.Size = new System.Drawing.Size(186, 48);
            // 
            // editApplicationTypeToolStripMenuItem
            // 
            this.editApplicationTypeToolStripMenuItem.Name = "editApplicationTypeToolStripMenuItem";
            this.editApplicationTypeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.editApplicationTypeToolStripMenuItem.Text = "Edit Application Type";
            this.editApplicationTypeToolStripMenuItem.Click += new System.EventHandler(this.editApplicationTypeToolStripMenuItem_Click);
            // 
            // ApplicationsTypesManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 471);
            this.ContextMenuStrip = this.cmsApplicationTypesList;
            this.Controls.Add(this.lblAppsTypesCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvApplicationsTypesList);
            this.Name = "ApplicationsTypesManagementForm";
            this.Text = "ApplicationsTypesManagementFom";
            this.Load += new System.EventHandler(this.ApplicationsTypesManagementFom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicationsTypesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsApplicationTypesList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvApplicationsTypesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAppsTypesCount;
        private System.Windows.Forms.ContextMenuStrip cmsApplicationTypesList;
        private System.Windows.Forms.ToolStripMenuItem editApplicationTypeToolStripMenuItem;
    }
}