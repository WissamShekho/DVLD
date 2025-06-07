namespace DVLD.Licenses.Local_Driving_Licenses.LocalDrivingLicenseControls
{
    partial class ctrlDriverLicenseCardWithFilter
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
            this.components = new System.ComponentModel.Container();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.tbLicenseID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlDriverLicenseCard1 = new DVLD.Licenses.Local_Driving_Licenses.LocalDrivingLicenseControls.ctrlDriverLicenseCard();
            this.epValidateLicenseID = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epValidateLicenseID)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.tbLicenseID);
            this.gbFilter.Controls.Add(this.btnSearch);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Location = new System.Drawing.Point(94, 7);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(451, 72);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // tbLicenseID
            // 
            this.tbLicenseID.Location = new System.Drawing.Point(121, 29);
            this.tbLicenseID.Name = "tbLicenseID";
            this.tbLicenseID.Size = new System.Drawing.Size(194, 20);
            this.tbLicenseID.TabIndex = 2;
            this.tbLicenseID.TextChanged += new System.EventHandler(this.btnSearch_Click);
            this.tbLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbLicenseID_KeyPress);
            this.tbLicenseID.Validating += new System.ComponentModel.CancelEventHandler(this.tbLicenseID_Validating);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Image = global::DVLD.Properties.Resources.License_View_32;
            this.btnSearch.Location = new System.Drawing.Point(359, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(47, 43);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "License ID:";
            // 
            // ctrlDriverLicenseCard1
            // 
            if (!this.DesignMode)
            {
                this.ctrlDriverLicenseCard1.Location = new System.Drawing.Point(3, 85);
                this.ctrlDriverLicenseCard1.Name = "ctrlDriverLicenseCard1";
                this.ctrlDriverLicenseCard1.Size = new System.Drawing.Size(660, 241);
                this.ctrlDriverLicenseCard1.TabIndex = 0;
            }
            // 
            // epValidateLicenseID
            // 
            this.epValidateLicenseID.ContainerControl = this;
            // 
            // ctrlDriverLicenseCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.ctrlDriverLicenseCard1);
            this.Name = "ctrlDriverLicenseCardWithFilter";
            this.Size = new System.Drawing.Size(664, 328);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epValidateLicenseID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlDriverLicenseCard ctrlDriverLicenseCard1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox tbLicenseID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider epValidateLicenseID;
    }
}
