using System;
using System.Drawing;
using System.Windows.Forms;

namespace ContactEase
{
    public partial class AddContactForm : Form
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public bool IsFavorite { get; private set; }
        public string FotoPath { get; private set; }

        public AddContactForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AddContactForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.Name = "AddContactForm";
            this.Text = "Agregar Contacto";
            this.ResumeLayout(false);
        }

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.FromArgb(15, 15, 15);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            this.ForeColor = Color.White;

            var lblFirstName = new Label
            {
                Text = "Nombre",
                Location = new Point(30, 30)
            };

            var txtFirstName = new TextBox
            {
                Location = new Point(150, 30),
                Width = 200,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };

            var lblLastName = new Label
            {
                Text = "Apellido",
                Location = new Point(30, 80)
            };

            var txtLastName = new TextBox
            {
                Location = new Point(150, 80),
                Width = 200,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };

            var lblPhone = new Label
            {
                Text = "Teléfono",
                Location = new Point(30, 130)
            };

            var txtPhone = new TextBox
            {
                Location = new Point(150, 130),
                Width = 200,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };

            var lblEmail = new Label
            {
                Text = "Correo",
                Location = new Point(30, 180)
            };

            var txtEmail = new TextBox
            {
                Location = new Point(150, 180),
                Width = 200,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };

            var lblIsFavorite = new Label
            {
                Text = "Favorito",
                Location = new Point(30, 230)
            };

            var chkIsFavorite = new CheckBox
            {
                Location = new Point(150, 230),
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };

            var lblFotoPath = new Label
            {
                Text = "Foto",
                Location = new Point(30, 280)
            };

            var txtFotoPath = new TextBox
            {
                Location = new Point(150, 280),
                Width = 200,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };

            var btnBrowse = new Button
            {
                Text = "Examinar",
                Location = new Point(360, 280),
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };
            btnBrowse.Click += (sender, e) =>
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtFotoPath.Text = openFileDialog.FileName;
                    }
                }
            };

            var btnSave = new Button
            {
                Text = "Guardar",
                Location = new Point(150, 350),
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };
            btnSave.Click += (sender, e) =>
            {
                FirstName = txtFirstName.Text;
                LastName = txtLastName.Text;
                Phone = txtPhone.Text;
                Email = txtEmail.Text;
                IsFavorite = chkIsFavorite.Checked;
                FotoPath = txtFotoPath.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.Controls.Add(lblFirstName);
            this.Controls.Add(txtFirstName);
            this.Controls.Add(lblLastName);
            this.Controls.Add(txtLastName);
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblIsFavorite);
            this.Controls.Add(chkIsFavorite);
            this.Controls.Add(lblFotoPath);
            this.Controls.Add(txtFotoPath);
            this.Controls.Add(btnBrowse);
            this.Controls.Add(btnSave);
        }
    }
}
