using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ContactEase
{
    public partial class LoginForm : StyledForm
    {
        public LoginForm()
        {
            InitializeComponent();
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

                    try
                    {
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
                    catch (ArgumentOutOfRangeException ex)
                    {
                        MessageBox.Show("Error al verificar la contraseña: " + ex.Message);
                        // Opcionalmente, registra el error para su posterior análisis
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
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, storedHash);
            }
            catch (Exception ex)
            {
                // Manejo adicional de excepciones si es necesario
                MessageBox.Show("Error al verificar la contraseña: " + ex.Message);
                return false;
            }
        }

    }
}
