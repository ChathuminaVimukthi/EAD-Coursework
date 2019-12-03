namespace EADCourseworkTwo
{
    partial class AddContact
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.saveContact = new System.Windows.Forms.Button();
            this.contactNameTxtBox = new System.Windows.Forms.TextBox();
            this.numberTxtBox = new System.Windows.Forms.TextBox();
            this.emailTxtBox = new System.Windows.Forms.TextBox();
            this.appointmentsDataSet1 = new EADCourseworkTwo.AppointmentsDataSet();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.saveContact, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.contactNameTxtBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numberTxtBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.emailTxtBox, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(445, 158);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 33);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mobile Number";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 33);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveContact
            // 
            this.saveContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveContact.Location = new System.Drawing.Point(3, 120);
            this.saveContact.Name = "saveContact";
            this.saveContact.Size = new System.Drawing.Size(127, 35);
            this.saveContact.TabIndex = 3;
            this.saveContact.Text = "Add";
            this.saveContact.UseVisualStyleBackColor = true;
            // 
            // contactNameTxtBox
            // 
            this.contactNameTxtBox.Location = new System.Drawing.Point(136, 10);
            this.contactNameTxtBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.contactNameTxtBox.Name = "contactNameTxtBox";
            this.contactNameTxtBox.Size = new System.Drawing.Size(306, 20);
            this.contactNameTxtBox.TabIndex = 4;
            // 
            // numberTxtBox
            // 
            this.numberTxtBox.Location = new System.Drawing.Point(136, 49);
            this.numberTxtBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.numberTxtBox.Name = "numberTxtBox";
            this.numberTxtBox.Size = new System.Drawing.Size(306, 20);
            this.numberTxtBox.TabIndex = 5;
            this.numberTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numTextBox_KeyPress);
            // 
            // emailTxtBox
            // 
            this.emailTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailTxtBox.Location = new System.Drawing.Point(136, 88);
            this.emailTxtBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.emailTxtBox.Name = "emailTxtBox";
            this.emailTxtBox.Size = new System.Drawing.Size(306, 20);
            this.emailTxtBox.TabIndex = 6;
            // 
            // appointmentsDataSet1
            // 
            this.appointmentsDataSet1.DataSetName = "AppointmentsDataSet";
            this.appointmentsDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AddContact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AddContact";
            this.Size = new System.Drawing.Size(445, 158);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button saveContact;
        private System.Windows.Forms.TextBox contactNameTxtBox;
        private System.Windows.Forms.TextBox numberTxtBox;
        private System.Windows.Forms.TextBox emailTxtBox;
        private AppointmentsDataSet appointmentsDataSet1;
    }
}
