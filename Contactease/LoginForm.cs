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

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT Password, UserID FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedPassword = reader.GetString("Password");
                        int userID = reader.GetInt32("UserID");
                        int contactUserID = reader.GetInt32("ContactUserID");

                        // Compare passwords directly
                        if (password == storedPassword)
                        {
                            // Login successful
                            Form1 form1 = new Form1(userID,contactUserID);
                            form1.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.");
                    }
                }
            }
        }


    }
}
