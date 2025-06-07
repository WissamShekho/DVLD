namespace DVLD.Licenses.Local_Driving_Licenses
{
    partial class IssueDrivingLicenseForTheFirstTimeForm
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
            this.ctrlDrivingLicenseApplicationCard1 = new DVLD.Applications.Controls.ctrlDrivingLicenseApplicationCard();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ctrlDrivingLicenseApplicationCard1
            // 
            this.ctrlDrivingLicenseApplicationCard1.Location = new System.Drawing.Point(12, 12);
            this.ctrlDrivingLicenseApplicationCard1.Name = "ctrlDrivingLicenseApplicationCard1";
            this.ctrlDrivingLicenseApplicationCard1.Size = new System.Drawing.Size(462, 293);
            this.ctrlDrivingLicenseApplicationCard1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Notes:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(232, 425);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 31);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(348, 425);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbNotes
            // 
            this.tbNotes.Location = new System.Drawing.Point(67, 311);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(402, 98);
            this.tbNotes.TabIndex = 4;
            // 
            // IssueDrivingLicenseForTheFirstTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 468);
            this.Controls.Add(this.tbNotes);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlDrivingLicenseApplicationCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "IssueDrivingLicenseForTheFirstTimeForm";
            this.Text = "IssueDrivingLicenseForTheFirstTimeForm";
            this.Load += new System.EventHandler(this.IssueDrivingLicenseForTheFirstTimeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Applications.Controls.ctrlDrivingLicenseApplicationCard ctrlDrivingLicenseApplicationCard1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbNotes;
    }
}