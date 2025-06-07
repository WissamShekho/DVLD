namespace DVLD.People
{
    partial class PeopleManagementForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.peopleDGV = new System.Windows.Forms.DataGridView();
            this.PeopleListCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phoneCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Closebtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFilters = new System.Windows.Forms.ComboBox();
            this.RecordsCountlbl = new System.Windows.Forms.Label();
            this.tbSearchPerson = new System.Windows.Forms.TextBox();
            this.AddPersonBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.peopleDGV)).BeginInit();
            this.PeopleListCms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(329, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage People";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // peopleDGV
            // 
            this.peopleDGV.AllowUserToAddRows = false;
            this.peopleDGV.AllowUserToDeleteRows = false;
            this.peopleDGV.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.peopleDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.peopleDGV.ContextMenuStrip = this.PeopleListCms;
            this.peopleDGV.Location = new System.Drawing.Point(12, 179);
            this.peopleDGV.Name = "peopleDGV";
            this.peopleDGV.ReadOnly = true;
            this.peopleDGV.Size = new System.Drawing.Size(776, 234);
            this.peopleDGV.TabIndex = 2;
            // 
            // PeopleListCms
            // 
            this.PeopleListCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowDetailsToolStripMenuItem,
            this.toolStripSeparator1,
            this.addNewPersonToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator2,
            this.sendEmailToolStripMenuItem,
            this.phoneCallToolStripMenuItem});
            this.PeopleListCms.Name = "contextMenuStrip1";
            this.PeopleListCms.Size = new System.Drawing.Size(142, 148);
            // 
            // ShowDetailsToolStripMenuItem
            // 
            this.ShowDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowDetailsToolStripMenuItem.Name = "ShowDetailsToolStripMenuItem";
            this.ShowDetailsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.ShowDetailsToolStripMenuItem.Text = "Show Details";
            this.ShowDetailsToolStripMenuItem.Click += new System.EventHandler(this.ShowDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // addNewPersonToolStripMenuItem
            // 
            this.addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            this.addNewPersonToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addNewPersonToolStripMenuItem.Text = "Add New";
            this.addNewPersonToolStripMenuItem.Click += new System.EventHandler(this.addNewPersonToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(138, 6);
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            // 
            // phoneCallToolStripMenuItem
            // 
            this.phoneCallToolStripMenuItem.Name = "phoneCallToolStripMenuItem";
            this.phoneCallToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.phoneCallToolStripMenuItem.Text = "Phone Call";
            // 
            // Closebtn
            // 
            this.Closebtn.Location = new System.Drawing.Point(690, 419);
            this.Closebtn.Name = "Closebtn";
            this.Closebtn.Size = new System.Drawing.Size(98, 26);
            this.Closebtn.TabIndex = 3;
            this.Closebtn.Text = "Close";
            this.Closebtn.UseVisualStyleBackColor = true;
            this.Closebtn.Click += new System.EventHandler(this.Closebtn_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Filter By:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbFilters
            // 
            this.cbFilters.FormattingEnabled = true;
            this.cbFilters.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No",
            "FirstName",
            "Second Name",
            "Third Name",
            "Last Name",
            "Gender",
            "Address",
            "Phone",
            "Email",
            "Country Name"});
            this.cbFilters.Location = new System.Drawing.Point(81, 152);
            this.cbFilters.Name = "cbFilters";
            this.cbFilters.Size = new System.Drawing.Size(118, 21);
            this.cbFilters.TabIndex = 5;
            this.cbFilters.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // RecordsCountlbl
            // 
            this.RecordsCountlbl.AutoSize = true;
            this.RecordsCountlbl.Location = new System.Drawing.Point(21, 432);
            this.RecordsCountlbl.Name = "RecordsCountlbl";
            this.RecordsCountlbl.Size = new System.Drawing.Size(70, 13);
            this.RecordsCountlbl.TabIndex = 7;
            this.RecordsCountlbl.Text = "#Records:    ";
            // 
            // tbSearchPerson
            // 
            this.tbSearchPerson.Location = new System.Drawing.Point(205, 152);
            this.tbSearchPerson.Name = "tbSearchPerson";
            this.tbSearchPerson.Size = new System.Drawing.Size(118, 20);
            this.tbSearchPerson.TabIndex = 8;
            this.tbSearchPerson.TextChanged += new System.EventHandler(this.tbSearchPerson_TextChanged);
            this.tbSearchPerson.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchPerson_KeyPress);
            // 
            // AddPersonBtn
            // 
            this.AddPersonBtn.BackgroundImage = global::DVLD.Properties.Resources.Add_Person_721;
            this.AddPersonBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddPersonBtn.Location = new System.Drawing.Point(740, 140);
            this.AddPersonBtn.Name = "AddPersonBtn";
            this.AddPersonBtn.Size = new System.Drawing.Size(48, 33);
            this.AddPersonBtn.TabIndex = 6;
            this.AddPersonBtn.UseVisualStyleBackColor = true;
            this.AddPersonBtn.Click += new System.EventHandler(this.AddPersonBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.People_4001;
            this.pictureBox1.Location = new System.Drawing.Point(342, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PeopleManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbSearchPerson);
            this.Controls.Add(this.RecordsCountlbl);
            this.Controls.Add(this.AddPersonBtn);
            this.Controls.Add(this.cbFilters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Closebtn);
            this.Controls.Add(this.peopleDGV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PeopleManagementForm";
            this.Text = "PeopleManagementForm";
            this.Load += new System.EventHandler(this.PeopleManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.peopleDGV)).EndInit();
            this.PeopleListCms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView peopleDGV;
        private System.Windows.Forms.Button Closebtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFilters;
        private System.Windows.Forms.Button AddPersonBtn;
        private System.Windows.Forms.Label RecordsCountlbl;
        private System.Windows.Forms.ContextMenuStrip PeopleListCms;
        private System.Windows.Forms.ToolStripMenuItem ShowDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem addNewPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phoneCallToolStripMenuItem;
        private System.Windows.Forms.TextBox tbSearchPerson;
    }
}