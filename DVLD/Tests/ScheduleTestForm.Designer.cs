namespace DVLD.Tests
{
    partial class ScheduleTestForm
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
            this.btnCLose = new System.Windows.Forms.Button();
            this.ctrlScheduleTest1 = new DVLD.Tests.TestControls.ctrlScheduleTest();
            this.SuspendLayout();
            // 
            // btnCLose
            // 
            this.btnCLose.Location = new System.Drawing.Point(183, 730);
            this.btnCLose.Name = "btnCLose";
            this.btnCLose.Size = new System.Drawing.Size(154, 43);
            this.btnCLose.TabIndex = 1;
            this.btnCLose.Text = "Close";
            this.btnCLose.UseVisualStyleBackColor = true;
            this.btnCLose.Click += new System.EventHandler(this.btnCLose_Click);
            // 
            // ctrlScheduleTest1
            // 
            this.ctrlScheduleTest1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlScheduleTest1.Location = new System.Drawing.Point(-2, 14);
            this.ctrlScheduleTest1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlScheduleTest1.Name = "ctrlScheduleTest1";
            this.ctrlScheduleTest1.Size = new System.Drawing.Size(533, 714);
            this.ctrlScheduleTest1.TabIndex = 0;
            this.ctrlScheduleTest1.TestTypeID = DVLD_BusinessLayer.TestType.EnTestType.VisionTest;
            // 
            // ScheduleTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 749);
            this.Controls.Add(this.btnCLose);
            this.Controls.Add(this.ctrlScheduleTest1);
            this.Name = "ScheduleTestForm";
            this.Text = "ScheduleTestForm";
            this.Load += new System.EventHandler(this.ScheduleTestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TestControls.ctrlScheduleTest ctrlScheduleTest1;
        private System.Windows.Forms.Button btnCLose;
    }
}