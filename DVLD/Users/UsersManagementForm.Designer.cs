namespace DVLD.Users
{
    partial class UsersManagementForm
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
            this.tbSearchUser = new System.Windows.Forms.TextBox();
            this.RecordsCountlbl = new System.Windows.Forms.Label();
            this.cbFilters = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Closebtn = new System.Windows.Forms.Button();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbIsActiveFilters = new System.Windows.Forms.ComboBox();
            this.cmsUsersList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowUserInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AddNewUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeUserPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsUsersList.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSearchUser
            // 
            this.tbSearchUser.Location = new System.Drawing.Point(205, 185);
            this.tbSearchUser.Name = "tbSearchUser";
            this.tbSearchUser.Size = new System.Drawing.Size(118, 20);
            this.tbSearchUser.TabIndex = 17;
            this.tbSearchUser.TextChanged += new System.EventHandler(this.tbSearchUser_TextChanged);
            this.tbSearchUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchUser_KeyPress);
            // 
            // RecordsCountlbl
            // 
            this.RecordsCountlbl.AutoSize = true;
            this.RecordsCountlbl.Location = new System.Drawing.Point(21, 465);
            this.RecordsCountlbl.Name = "RecordsCountlbl";
            this.RecordsCountlbl.Size = new System.Drawing.Size(70, 13);
            this.RecordsCountlbl.TabIndex = 16;
            this.RecordsCountlbl.Text = "#Records:    ";
            // 
            // cbFilters
            // 
            this.cbFilters.FormattingEnabled = true;
            this.cbFilters.Items.AddRange(new object[] {
            "None",
            "User ID",
            "Person ID",
            "Full Name",
            "Username",
            "Is Active"});
            this.cbFilters.Location = new System.Drawing.Point(81, 185);
            this.cbFilters.Name = "cbFilters";
            this.cbFilters.Size = new System.Drawing.Size(118, 21);
            this.cbFilters.TabIndex = 14;
            this.cbFilters.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 22);
            this.label2.TabIndex = 13;
            this.label2.Text = "Filter By:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Closebtn
            // 
            this.Closebtn.Location = new System.Drawing.Point(690, 452);
            this.Closebtn.Name = "Closebtn";
            this.Closebtn.Size = new System.Drawing.Size(98, 26);
            this.Closebtn.TabIndex = 12;
            this.Closebtn.Text = "Close";
            this.Closebtn.UseVisualStyleBackColor = true;
            this.Closebtn.Click += new System.EventHandler(this.Closebtn_Click);
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.ContextMenuStrip = this.cmsUsersList;
            this.dgvUsers.Location = new System.Drawing.Point(12, 212);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.Size = new System.Drawing.Size(776, 234);
            this.dgvUsers.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(284, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 40);
            this.label1.TabIndex = 10;
            this.label1.Text = "Manage Users";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddUser
            // 
            this.btnAddUser.BackgroundImage = global::DVLD.Properties.Resources.Users_2_64;
            this.btnAddUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddUser.Location = new System.Drawing.Point(736, 158);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(52, 48);
            this.btnAddUser.TabIndex = 15;
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Users_2_400;
            this.pictureBox1.Location = new System.Drawing.Point(295, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // cbIsActiveFilters
            // 
            this.cbIsActiveFilters.FormattingEnabled = true;
            this.cbIsActiveFilters.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsActiveFilters.Location = new System.Drawing.Point(222, 184);
            this.cbIsActiveFilters.Name = "cbIsActiveFilters";
            this.cbIsActiveFilters.Size = new System.Drawing.Size(101, 21);
            this.cbIsActiveFilters.TabIndex = 18;
            this.cbIsActiveFilters.SelectedIndexChanged += new System.EventHandler(this.cbIsActiveFilters_SelectedIndexChanged);
            // 
            // cmsUsersList
            // 
            this.cmsUsersList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowUserInfoToolStripMenuItem,
            this.toolStripSeparator2,
            this.AddNewUserToolStripMenuItem,
            this.EditUserToolStripMenuItem,
            this.DeleteUserToolStripMenuItem,
            this.ChangeUserPasswordToolStripMenuItem});
            this.cmsUsersList.Name = "cmsUsersList";
            this.cmsUsersList.Size = new System.Drawing.Size(195, 120);
            // 
            // ShowUserInfoToolStripMenuItem
            // 
            this.ShowUserInfoToolStripMenuItem.Name = "ShowUserInfoToolStripMenuItem";
            this.ShowUserInfoToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.ShowUserInfoToolStripMenuItem.Text = "Show User Info";
            this.ShowUserInfoToolStripMenuItem.Click += new System.EventHandler(this.ShowUserInfoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
            // 
            // AddNewUserToolStripMenuItem
            // 
            this.AddNewUserToolStripMenuItem.Name = "AddNewUserToolStripMenuItem";
            this.AddNewUserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.AddNewUserToolStripMenuItem.Text = "Add New User";
            this.AddNewUserToolStripMenuItem.Click += new System.EventHandler(this.AddNewUserToolStripMenuItem_Click);
            // 
            // EditUserToolStripMenuItem
            // 
            this.EditUserToolStripMenuItem.Name = "EditUserToolStripMenuItem";
            this.EditUserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.EditUserToolStripMenuItem.Text = "Edit User";
            this.EditUserToolStripMenuItem.Click += new System.EventHandler(this.EditUserToolStripMenuItem_Click);
            // 
            // DeleteUserToolStripMenuItem
            // 
            this.DeleteUserToolStripMenuItem.Name = "DeleteUserToolStripMenuItem";
            this.DeleteUserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.DeleteUserToolStripMenuItem.Text = "Delete User";
            this.DeleteUserToolStripMenuItem.Click += new System.EventHandler(this.DeleteUserToolStripMenuItem_Click);
            // 
            // ChangeUserPasswordToolStripMenuItem
            // 
            this.ChangeUserPasswordToolStripMenuItem.Name = "ChangeUserPasswordToolStripMenuItem";
            this.ChangeUserPasswordToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.ChangeUserPasswordToolStripMenuItem.Text = "Change User Password";
            this.ChangeUserPasswordToolStripMenuItem.Click += new System.EventHandler(this.ChangeUserPasswordToolStripMenuItem_Click);
            // 
            // UsersManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 495);
            this.Controls.Add(this.cbIsActiveFilters);
            this.Controls.Add(this.tbSearchUser);
            this.Controls.Add(this.RecordsCountlbl);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.cbFilters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Closebtn);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "UsersManagementForm";
            this.Text = "UsersManagementForm";
            this.Load += new System.EventHandler(this.UsersManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsUsersList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSearchUser;
        private System.Windows.Forms.Label RecordsCountlbl;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ComboBox cbFilters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Closebtn;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbIsActiveFilters;
        private System.Windows.Forms.ContextMenuStrip cmsUsersList;
        private System.Windows.Forms.ToolStripMenuItem AddNewUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem EditUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeUserPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowUserInfoToolStripMenuItem;
    }
}