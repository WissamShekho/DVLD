namespace DVLD.Tests.TestsTypes
{
    partial class TestsTypesManagementForm
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
            this.lblTestsTypesCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTestsTypesList = new System.Windows.Forms.DataGridView();
            this.cmsTestsTypesList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editTestTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestsTypesList)).BeginInit();
            this.cmsTestsTypesList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTestsTypesCount
            // 
            this.lblTestsTypesCount.AutoSize = true;
            this.lblTestsTypesCount.Location = new System.Drawing.Point(124, 432);
            this.lblTestsTypesCount.Name = "lblTestsTypesCount";
            this.lblTestsTypesCount.Size = new System.Drawing.Size(13, 13);
            this.lblTestsTypesCount.TabIndex = 11;
            this.lblTestsTypesCount.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 432);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "#Tests Types: ";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(362, 420);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 25);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(107, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 33);
            this.label1.TabIndex = 7;
            this.label1.Text = "Manage Tests Types";
            // 
            // dgvTestsTypesList
            // 
            this.dgvTestsTypesList.AllowUserToAddRows = false;
            this.dgvTestsTypesList.AllowUserToDeleteRows = false;
            this.dgvTestsTypesList.AllowUserToOrderColumns = true;
            this.dgvTestsTypesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestsTypesList.ContextMenuStrip = this.cmsTestsTypesList;
            this.dgvTestsTypesList.GridColor = System.Drawing.Color.White;
            this.dgvTestsTypesList.Location = new System.Drawing.Point(12, 211);
            this.dgvTestsTypesList.Name = "dgvTestsTypesList";
            this.dgvTestsTypesList.ReadOnly = true;
            this.dgvTestsTypesList.Size = new System.Drawing.Size(457, 203);
            this.dgvTestsTypesList.TabIndex = 6;
            // 
            // cmsTestsTypesList
            // 
            this.cmsTestsTypesList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTestTypeToolStripMenuItem});
            this.cmsTestsTypesList.Name = "cmsTestsTypesList";
            this.cmsTestsTypesList.Size = new System.Drawing.Size(181, 48);
            // 
            // editTestTypeToolStripMe
            // 
            this.editTestTypeToolStripMenuItem.Name = "editTestTypeToolStripMe";
            this.editTestTypeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editTestTypeToolStripMenuItem.Text = "Edit Test Type";
            this.editTestTypeToolStripMenuItem.Click += new System.EventHandler(this.editTestTypeToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.TestType_5121;
            this.pictureBox1.Location = new System.Drawing.Point(163, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(171, 149);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // TestsTypesManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 450);
            this.Controls.Add(this.lblTestsTypesCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTestsTypesList);
            this.Name = "TestsTypesManagement";
            this.Text = "Tests Types Managment";
            this.Load += new System.EventHandler(this.TestsTypesManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestsTypesList)).EndInit();
            this.cmsTestsTypesList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTestsTypesCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTestsTypesList;
        private System.Windows.Forms.ContextMenuStrip cmsTestsTypesList;
        private System.Windows.Forms.ToolStripMenuItem editTestTypeToolStripMenuItem;
    }
}