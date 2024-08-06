using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ContactEase
{
    public partial class AddContactForm : StyledForm
    {
        private readonly int _userID;
        public Contact NewContact { get; private set; }

        public AddContactForm(int userID)
        {
            _userID = userID;
            InitializeComponent();
            ApplyCustomDesign();
        }

        private void ApplyCustomDesign()
        {
            this.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.ForeColor = System.Drawing.Color.White;
            foreach (Control control in this.Controls)
            {
                if (control is Label)
                {
                    control.ForeColor = System.Drawing.Color.White;
                }
                if (control is TextBox)
                {
                    control.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
                    control.ForeColor = System.Drawing.Color.White;
                }
                if (control is Button)
                {
                    control.BackColor = System.Drawing.Color.FromArgb(63, 63, 70);
                    control.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            byte[] foto = null;
            if (pbProfilePicture.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pbProfilePicture.Image.Save(ms, pbProfilePicture.Image.RawFormat);
                    foto = ms.ToArray();
                }
            }

            NewContact = new Contact
            {
                UserID = _userID,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Foto = foto,
                IsFavorite = chkIsFavorite.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;",
                Title = "Select a Profile Picture"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                pbProfilePicture.Image = Image.FromFile(filePath);
            }
        }
    }
}
