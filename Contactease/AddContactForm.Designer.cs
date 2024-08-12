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
        private Button BtnSave;
        private Button BtnCancel;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblFoto;


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
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.chkIsFavorite = new System.Windows.Forms.CheckBox();
            this.lblFoto = new System.Windows.Forms.Label();
            this.pbProfilePicture = new System.Windows.Forms.PictureBox();
            this.BtnUploadImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfilePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.topPanel.Size = new System.Drawing.Size(561, 100);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 472);
            this.bottomPanel.Size = new System.Drawing.Size(561, 70);
            // 
            // txtFirstName
            // 
            this.txtFirstName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.txtFirstName.Location = new System.Drawing.Point(189, 145);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 25);
            this.txtFirstName.TabIndex = 5;
            // 
            // txtLastName
            // 
            this.txtLastName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.txtLastName.Location = new System.Drawing.Point(189, 175);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 25);
            this.txtLastName.TabIndex = 6;
            // 
            // txtPhone
            // 
            this.txtPhone.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.txtPhone.Location = new System.Drawing.Point(189, 205);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 25);
            this.txtPhone.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.txtEmail.Location = new System.Drawing.Point(189, 235);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 25);
            this.txtEmail.TabIndex = 8;
            // 
            // BtnSave
            // 
            this.BtnSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnSave.Location = new System.Drawing.Point(165, 335);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 30);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnCancel.Location = new System.Drawing.Point(319, 335);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(80, 30);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "Cancel";
            // 
            // lblFirstName
            // 
            this.lblFirstName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFirstName.Location = new System.Drawing.Point(109, 145);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(80, 23);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "First Name:";
            // 
            // lblLastName
            // 
            this.lblLastName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblLastName.Location = new System.Drawing.Point(109, 175);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(80, 23);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Last Name:";
            // 
            // lblPhone
            // 
            this.lblPhone.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblPhone.Location = new System.Drawing.Point(109, 205);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(80, 23);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Phone:";
            // 
            // lblEmail
            // 
            this.lblEmail.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblEmail.Location = new System.Drawing.Point(109, 235);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(80, 23);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // chkIsFavorite
            // 
            this.chkIsFavorite.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.chkIsFavorite.Location = new System.Drawing.Point(165, 390);
            this.chkIsFavorite.Name = "chkIsFavorite";
            this.chkIsFavorite.Size = new System.Drawing.Size(104, 24);
            this.chkIsFavorite.TabIndex = 0;
            this.chkIsFavorite.Text = "Favorito";
            // 
            // lblFoto
            // 
            this.lblFoto.AutoSize = true;
            this.lblFoto.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFoto.Location = new System.Drawing.Point(112, 262);
            this.lblFoto.Name = "lblFoto";
            this.lblFoto.Size = new System.Drawing.Size(37, 19);
            this.lblFoto.TabIndex = 12;
            this.lblFoto.Text = "Foto";
            // 
            // pbProfilePicture
            // 
            this.pbProfilePicture.BackgroundImage = global::Contactease.Properties.Resources.Logo;
            this.pbProfilePicture.Location = new System.Drawing.Point(588, -35);
            this.pbProfilePicture.Name = "pbProfilePicture";
            this.pbProfilePicture.Size = new System.Drawing.Size(896, 900);
            this.pbProfilePicture.TabIndex = 16;
            this.pbProfilePicture.TabStop = false;
            // 
            // BtnUploadImage
            // 
            this.BtnUploadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnUploadImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BtnUploadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUploadImage.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnUploadImage.Location = new System.Drawing.Point(226, 289);
            this.BtnUploadImage.Margin = new System.Windows.Forms.Padding(4);
            this.BtnUploadImage.Name = "BtnUploadImage";
            this.BtnUploadImage.Size = new System.Drawing.Size(117, 39);
            this.BtnUploadImage.TabIndex = 18;
            this.BtnUploadImage.Text = "Subir Imagen";
            this.BtnUploadImage.UseVisualStyleBackColor = false;
            this.BtnUploadImage.Click += new System.EventHandler(this.BtnUploadImage_Click);
            // 
            // AddContactForm
            // 
            this.ClientSize = new System.Drawing.Size(561, 542);
            this.Controls.Add(this.BtnUploadImage);
            this.Controls.Add(this.lblFoto);
            this.Controls.Add(this.pbProfilePicture);
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
            this.Name = "AddContactForm";
            this.Text = "Add Contact";
            this.Controls.SetChildIndex(this.BtnCancel, 0);
            this.Controls.SetChildIndex(this.BtnSave, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.txtPhone, 0);
            this.Controls.SetChildIndex(this.txtLastName, 0);
            this.Controls.SetChildIndex(this.txtFirstName, 0);
            this.Controls.SetChildIndex(this.lblEmail, 0);
            this.Controls.SetChildIndex(this.lblPhone, 0);
            this.Controls.SetChildIndex(this.lblLastName, 0);
            this.Controls.SetChildIndex(this.lblFirstName, 0);
            this.Controls.SetChildIndex(this.chkIsFavorite, 0);
            this.Controls.SetChildIndex(this.pbProfilePicture, 0);
            this.Controls.SetChildIndex(this.lblFoto, 0);
            this.Controls.SetChildIndex(this.BtnUploadImage, 0);
            this.Controls.SetChildIndex(this.bottomPanel, 0);
            this.Controls.SetChildIndex(this.topPanel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbProfilePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button BtnUploadImage;
        private PictureBox pbProfilePicture;
    }
}