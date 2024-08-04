using System;
using System.Windows.Forms;


namespace ContactEase
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;

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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(160, 100);
            this.txtUsername.Size = new System.Drawing.Size(180, 20);
            this.txtUsername.Name = "txtUsername";
            this.Controls.Add(this.txtUsername);

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(160, 150);
            this.txtPassword.Size = new System.Drawing.Size(180, 20);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            this.Controls.Add(this.txtPassword);

            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(150, 200);
            this.btnLogin.Size = new System.Drawing.Size(100, 30);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Text = "Iniciar Sesión";
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            this.Controls.Add(this.btnLogin);

            // LoginForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Name = "LoginForm";
            this.Text = "Inicio de Sesión";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

