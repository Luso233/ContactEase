using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactEase
{
    public partial class RegisterForm : StyledForm
    {
        public RegisterForm()
        {
            InitializeComponent();
            // Configura el diseño similar a Form1
        }

        private readonly string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;

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
                var command = new MySqlCommand("INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(password));
                command.Parameters.AddWithValue("@Email", email);
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

        private void RegisterForm_Load_1(object sender, EventArgs e)
        {

        }
    }

}
