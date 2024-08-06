using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ContactEase
{
    public partial class EditContactForm : StyledForm
    {
        public Contact Contact { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsFavorite { get; set; }
        public byte[] Foto { get; set; }
        public Contact contact { get; set; }
        private readonly string connectionString;

        public EditContactForm(Contact contact)
        {
            InitializeComponent();
            this.Contact = contact;
            connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

            // Initialize fields with contact information
            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;
            txtPhone.Text = contact.Phone;
            txtEmail.Text = contact.Email;
            chkIsFavorite.Checked = contact.IsFavorite;

            if (contact.Foto != null)
            {
                using (MemoryStream ms = new MemoryStream(contact.Foto))
                {
                    pbProfilePicture.Image = Image.FromStream(ms);
                }
            }
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

                using (var ms = new MemoryStream())
                {
                    pbProfilePicture.Image.Save(ms, pbProfilePicture.Image.RawFormat);
                    Contact.Foto = ms.ToArray();
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Contact.FirstName = txtFirstName.Text;
            Contact.LastName = txtLastName.Text;
            Contact.Phone = txtPhone.Text;
            Contact.Email = txtEmail.Text;
            Contact.IsFavorite = chkIsFavorite.Checked;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE contacts SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @Email, IsFavorite = @IsFavorite, Foto = @Foto WHERE ContactID = @ContactID", connection);
                command.Parameters.AddWithValue("@FirstName", Contact.FirstName);
                command.Parameters.AddWithValue("@LastName", Contact.LastName);
                command.Parameters.AddWithValue("@Phone", Contact.Phone);
                command.Parameters.AddWithValue("@Email", Contact.Email);
                command.Parameters.AddWithValue("@IsFavorite", Contact.IsFavorite);
                command.Parameters.AddWithValue("@Foto", Contact.Foto);
                command.Parameters.AddWithValue("@ContactID", Contact.ContactID);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Contacto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Cierra el formulario después de guardar
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM contacts WHERE ContactID = @ContactID", connection);
                command.Parameters.AddWithValue("@ContactID", Contact.ContactID);
                command.ExecuteNonQuery();
            }
            this.Close();
        }

        private void BtnUploadImage_Click_1(object sender, EventArgs e)
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
