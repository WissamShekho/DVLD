namespace DVLD.Applications.Controls
{
    partial class ctrlDrivingLicenseApplicationCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.lblPassedTests = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLicenseClassName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDrivingLicenseApplicationID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlApplicationCard1 = new DVLD.Applications.Controls.ctrlApplicationCard();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.llLicenseInfo);
            this.groupBox1.Controls.Add(this.lblPassedTests);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblLicenseClassName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblDrivingLicenseApplicationID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driving License Application Info";
            // 
            // llLicenseInfo
            // 
            this.llLicenseInfo.AutoSize = true;
            this.llLicenseInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llLicenseInfo.Location = new System.Drawing.Point(318, 60);
            this.llLicenseInfo.Name = "llLicenseInfo";
            this.llLicenseInfo.Size = new System.Drawing.Size(108, 13);
            this.llLicenseInfo.TabIndex = 6;
            this.llLicenseInfo.TabStop = true;
            this.llLicenseInfo.Text = "Show License Info";
            this.llLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLicenseInfo_LinkClicked);
            // 
            // lblPassedTests
            // 
            this.lblPassedTests.AutoSize = true;
            this.lblPassedTests.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassedTests.Location = new System.Drawing.Point(391, 35);
            this.lblPassedTests.Name = "lblPassedTests";
            this.lblPassedTests.Size = new System.Drawing.Size(35, 13);
            this.lblPassedTests.TabIndex = 5;
            this.lblPassedTests.Text = "[???]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(307, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Passed Tests: ";
            // 
            // lblLicenseClassName
            // 
            this.lblLicenseClassName.AutoSize = true;
            this.lblLicenseClassName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseClassName.Location = new System.Drawing.Point(93, 60);
            this.lblLicenseClassName.Name = "lblLicenseClassName";
            this.lblLicenseClassName.Size = new System.Drawing.Size(35, 13);
            this.lblLicenseClassName.TabIndex = 3;
            this.lblLicenseClassName.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "License CLass: ";
            // 
            // lblDrivingLicenseApplicationID
            // 
            this.lblDrivingLicenseApplicationID.AutoSize = true;
            this.lblDrivingLicenseApplicationID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrivingLicenseApplicationID.Location = new System.Drawing.Point(122, 35);
            this.lblDrivingLicenseApplicationID.Name = "lblDrivingLicenseApplicationID";
            this.lblDrivingLicenseApplicationID.Size = new System.Drawing.Size(35, 13);
            this.lblDrivingLicenseApplicationID.TabIndex = 1;
            this.lblDrivingLicenseApplicationID.Text = "[???]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "D. L. Application ID: ";
            // 
            // ctrlApplicationCard1
            // 
            this.ctrlApplicationCard1.Location = new System.Drawing.Point(1, 106);
            this.ctrlApplicationCard1.Name = "ctrlApplicationCard1";
            this.ctrlApplicationCard1.Size = new System.Drawing.Size(458, 183);
            this.ctrlApplicationCard1.TabIndex = 1;
            // 
            // ctrlDrivingLicenseApplicationCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlApplicationCard1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlDrivingLicenseApplicationCard";
            this.Size = new System.Drawing.Size(462, 293);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPassedTests;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLicenseClassName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDrivingLicenseApplicationID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llLicenseInfo;
        private DVLD.Applications.Controls.ctrlApplicationCard ctrlApplicationCard1;
    }
}
