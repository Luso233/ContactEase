using ContactEase;
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
    public partial class LoginForm : StyledForm
    {
        public LoginForm()
        {
            InitializeComponent();
            // Configura el diseño similar a Form1
        }

        private readonly string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Verificar en la base de datos
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT Password, UserID FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string storedHash = reader.GetString("Password");
                    int userId = reader.GetInt32("UserID");
                    if (VerifyPassword(password, storedHash))
                    {
                        // Guardar el ID del usuario en una variable global si es necesario
                        // Mostrar el formulario principal
                        Form1 mainForm = new Form1(userId);
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Nombre de usuario o contraseña incorrectos.");
                    }
                }
                else
                {
                    MessageBox.Show("Nombre de usuario no encontrado.");
                }
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Verificar la contraseña (deberías usar un método de hashing seguro)
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }

}
