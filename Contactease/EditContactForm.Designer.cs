using System.Windows.Forms;

namespace ContactEase
{
    partial class EditContactForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.CheckBox chkIsFavorite;

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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.chkIsFavorite = new System.Windows.Forms.CheckBox();
            this.pbProfilePicture = new System.Windows.Forms.PictureBox();
            this.BtnUploadImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfilePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(566, 100);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 531);
            this.bottomPanel.Size = new System.Drawing.Size(566, 70);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(168, 389);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(322, 389);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(180, 187);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 25);
            this.txtFirstName.TabIndex = 2;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(180, 213);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 25);
            this.txtLastName.TabIndex = 3;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(180, 239);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 25);
            this.txtPhone.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(180, 265);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 25);
            this.txtEmail.TabIndex = 5;
            // 
            // chkIsFavorite
            // 
            this.chkIsFavorite.AutoSize = true;
            this.chkIsFavorite.Location = new System.Drawing.Point(156, 326);
            this.chkIsFavorite.Name = "chkIsFavorite";
            this.chkIsFavorite.Size = new System.Drawing.Size(77, 23);
            this.chkIsFavorite.TabIndex = 6;
            this.chkIsFavorite.Text = "Favorite";
            this.chkIsFavorite.UseVisualStyleBackColor = true;
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
            this.BtnUploadImage.ForeColor = System.Drawing.Color.White;
            this.BtnUploadImage.Location = new System.Drawing.Point(222, 317);
            this.BtnUploadImage.Margin = new System.Windows.Forms.Padding(4);
            this.BtnUploadImage.Name = "BtnUploadImage";
            this.BtnUploadImage.Size = new System.Drawing.Size(117, 39);
            this.BtnUploadImage.TabIndex = 18;
            this.BtnUploadImage.Text = "Subir Imagen";
            this.BtnUploadImage.UseVisualStyleBackColor = false;
            this.BtnUploadImage.Click += new System.EventHandler(this.BtnUploadImage_Click);
            // 
            // EditContactForm
            // 
            this.ClientSize = new System.Drawing.Size(566, 601);
            this.Controls.Add(this.BtnUploadImage);
            this.Controls.Add(this.chkIsFavorite);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Name = "EditContactForm";
            this.Text = "Edit Contact";
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.txtFirstName, 0);
            this.Controls.SetChildIndex(this.txtLastName, 0);
            this.Controls.SetChildIndex(this.txtPhone, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.chkIsFavorite, 0);
            this.Controls.SetChildIndex(this.BtnUploadImage, 0);
            this.Controls.SetChildIndex(this.bottomPanel, 0);
            this.Controls.SetChildIndex(this.topPanel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbProfilePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private PictureBox pbProfilePicture;
        private System.Windows.Forms.Button BtnUploadImage;
    }
}
