using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ContactEase
{
    public partial class RegisterForm : StyledForm
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private readonly string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            byte[] foto = null;

            if (pbProfilePicture.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pbProfilePicture.Image.Save(ms, pbProfilePicture.Image.RawFormat);
                    foto = ms.ToArray();
                }
            }

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var checkCommand = new MySqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email", connection);
                checkCommand.Parameters.AddWithValue("@Username", username);
                checkCommand.Parameters.AddWithValue("@Email", email);
                int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (userCount > 0)
                {
                    MessageBox.Show("El nombre de usuario o el correo electrónico ya existen.");
                    return;
                }

                var command = new MySqlCommand("INSERT INTO Users (Username, Password, FirstName, LastName, Phone, Email, Foto) VALUES (@Username, @Password, @FirstName, @LastName, @Phone, @Email, @Foto)", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(password));
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Foto", foto);
                command.ExecuteNonQuery();

                MessageBox.Show("Usuario registrado exitosamente.");
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
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
            }
        }
    }
}
