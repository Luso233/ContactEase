using System;
using System.Windows.Forms;

namespace ContactEase
{
    partial class AddContactForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckBox chkIsFavorite;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private TextBox txtFotoPath;
        private Button BtnSave;
        private Button BtnCancel;
        private CheckBox chkFavorite;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblFotoPath;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.chkFavorite = new System.Windows.Forms.CheckBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.chkIsFavorite = new System.Windows.Forms.CheckBox();
            this.lblFotoPath = new System.Windows.Forms.Label();
            this.txtFotoPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(100, 50);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 20);
            this.txtFirstName.TabIndex = 5;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(100, 80);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 20);
            this.txtLastName.TabIndex = 6;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(100, 110);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            this.txtPhone.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(100, 140);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 8;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(100, 209);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 30);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(220, 209);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(80, 30);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // chkFavorite
            // 
            this.chkFavorite.Location = new System.Drawing.Point(100, 245);
            this.chkFavorite.Name = "chkFavorite";
            this.chkFavorite.Size = new System.Drawing.Size(80, 24);
            this.chkFavorite.TabIndex = 11;
            this.chkFavorite.Text = "Favorite";
            // 
            // lblFirstName
            // 
            this.lblFirstName.Location = new System.Drawing.Point(20, 50);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(80, 23);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "First Name:";
            // 
            // lblLastName
            // 
            this.lblLastName.Location = new System.Drawing.Point(20, 80);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(80, 23);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Last Name:";
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(20, 110);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(80, 23);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Phone:";
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(20, 140);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(80, 23);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // chkIsFavorite
            // 
            this.chkIsFavorite.Location = new System.Drawing.Point(0, 0);
            this.chkIsFavorite.Name = "chkIsFavorite";
            this.chkIsFavorite.Size = new System.Drawing.Size(104, 24);
            this.chkIsFavorite.TabIndex = 0;
            // 
            // lblFotoPath
            // 
            this.lblFotoPath.AutoSize = true;
            this.lblFotoPath.Location = new System.Drawing.Point(23, 167);
            this.lblFotoPath.Name = "FotoPath";
            this.lblFotoPath.Size = new System.Drawing.Size(35, 13);
            this.lblFotoPath.TabIndex = 12;
            this.lblFotoPath.Text = "FotoPath";
            // 
            // txtFotoPath
            // 
            this.txtFotoPath.Location = new System.Drawing.Point(100, 167);
            this.txtFotoPath.Name = "FotoPath";
            this.txtFotoPath.Size = new System.Drawing.Size(200, 20);
            this.txtFotoPath.TabIndex = 13;
            // 
            // AddContactForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.txtFotoPath);
            this.Controls.Add(this.lblFotoPath);
            this.Controls.Add(this.chkIsFavorite);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.chkFavorite);
            this.Name = "AddContactForm";
            this.Text = "Add Contact";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}