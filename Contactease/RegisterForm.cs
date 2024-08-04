using MySql.Data.MySqlClient;
using System;
using System.Drawing;
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
            string fotoPath = txtFotoPath.Text;

            // Verificar si el nombre de usuario o el correo electrónico ya existen
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

                // Insertar el nuevo usuario
                var command = new MySqlCommand("INSERT INTO Users (Username, Password, FirstName, LastName, Phone, Email, FotoPath) VALUES (@Username, @Password, @FirstName, @LastName, @Phone, @Email, @FotoPath)", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(password));
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@FotoPath", fotoPath);
                command.ExecuteNonQuery();

                MessageBox.Show("Usuario registrado exitosamente.");
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

       
    }
}
