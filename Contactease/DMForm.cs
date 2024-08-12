using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ContactEase
{
    public partial class DMForm : Form
    {
        private readonly int senderID;
        private int receiverID;
        private readonly string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

        public DMForm(int senderID, int receiverID)
        {
            this.senderID = senderID;
            this.receiverID = receiverID;
            InitializeComponent();
            LoadContactsToComboBox();
            LoadMessages();
            UpdatePanelSuperior();
        }

        

        private void LoadContactsToComboBox()
        {
            List<Contact> contactos = GetContacts();

            comboBoxContacts.DisplayMember = "FirstName";
            comboBoxContacts.ValueMember = "ContactUserID"; // Se asegura de que el valor seleccionado sea el `ContactUserID`
            comboBoxContacts.DataSource = contactos;

            if (comboBoxContacts.Items.Count > 0)
            {
                comboBoxContacts.SelectedIndex = 0; // Seleccionar el primer contacto por defecto
                receiverID = contactos[0].ContactUserID; // Asegurar que receiverID sea correcto desde el inicio
            }
        }



        private void comboBoxContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedContactId = (int)comboBoxContacts.SelectedValue;
            LoadConversationForContact(selectedContactId);
        }

        private List<Contact> GetContacts()
        {
            var contactos = new List<Contact>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM contacts WHERE UserID = @UserID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", senderID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contactos.Add(new Contact
                            {
                                ContactID = reader.GetInt32("ContactID"),
                                UserID = reader.GetInt32("UserID"),
                                ContactUserID = reader.GetInt32("ContactUserID"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Phone = reader.GetString("Phone"),
                                Email = reader.GetString("Email"),
                                IsFavorite = reader.GetBoolean("IsFavorite"),
                                Foto = reader["Foto"] as byte[]
                            });
                        }
                    }
                }
            }

            return contactos;
        }

        private void LoadConversationForContact(int contactId)
        {
            receiverID = contactId; // Actualizar el receiverID con el contacto seleccionado
            UpdatePanelSuperior(); // Actualizar el panel superior con los detalles del contacto
            LoadMessages(); // Cargar mensajes para el contacto seleccionado
        }

        private void UpdatePanelSuperior()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users WHERE UserID = @ReceiverUserID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReceiverUserID", receiverID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            panelSuperior.Controls.Clear();

                            // Add profile picture
                            PictureBox profilePic = new PictureBox
                            {
                                Size = new Size(50, 50),
                                Location = new Point(5, 10),
                                Image = GetProfilePicture(receiverID),
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };
                            panelSuperior.Controls.Add(profilePic);

                            // Add user name
                            Label userNameLabel = new Label
                            {
                                Text = $"{reader.GetString("FirstName")} {reader.GetString("LastName")} / {reader.GetString("UserName")}",
                                AutoSize = true,
                                Location = new Point(60, 20),
                                ForeColor = Color.White
                            };
                            panelSuperior.Controls.Add(userNameLabel);
                        }
                    }
                }
            }
        }

        private void LoadMessages()
        {
            messagePanel.Controls.Clear();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM messages WHERE (SenderUserID = @SenderUserID AND ReceiverUserID = @ReceiverUserID) OR (SenderUserID = @ReceiverUserID AND ReceiverUserID = @SenderUserID) ORDER BY Timestamp";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderUserID", senderID);
                    command.Parameters.AddWithValue("@ReceiverUserID", receiverID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int currentSenderUserID = reader.GetInt32("SenderUserID");

                            Panel messagePanelItem = new Panel
                            {
                                Size = new Size(500, 50),
                                Padding = new Padding(5),
                                BorderStyle = BorderStyle.FixedSingle,
                                Margin = new Padding(0, 5, 0, 5)
                            };

                            // Add profile picture
                            PictureBox profilePic = new PictureBox
                            {
                                Size = new Size(40, 40),
                                Location = new Point(5, 5),
                                Image = GetProfilePicture(currentSenderUserID),
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };
                            messagePanelItem.Controls.Add(profilePic);

                            // Add timestamp
                            Label timestampLabel = new Label
                            {
                                Text = reader.GetDateTime("Timestamp").ToString("HH:mm"),
                                AutoSize = true,
                                Location = new Point(50, 5),
                                ForeColor = Color.Gray
                            };
                            messagePanelItem.Controls.Add(timestampLabel);

                            // Add message text
                            Label messageLabel = new Label
                            {
                                Text = reader.GetString("MessageText"),
                                AutoSize = true,
                                Location = new Point(50, 25),
                                ForeColor = currentSenderUserID == senderID ? Color.Blue : Color.Green
                            };
                            messagePanelItem.Controls.Add(messageLabel);

                            // Position message panel
                            messagePanelItem.Location = new Point(0, messagePanel.Controls.Count * 55);

                            // Add message panel to messagePanel
                            messagePanel.Controls.Add(messagePanelItem);
                        }
                    }
                }
            }
        }

        private void SendMessage()
        {
            string messageText = txtMensaje.Text;
            if (string.IsNullOrWhiteSpace(messageText)) return;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO messages (SenderUserID, ReceiverUserID, MessageText, Timestamp) VALUES (@SenderUserID, @ReceiverUserID, @MessageText, @Timestamp)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderUserID", senderID);
                    command.Parameters.AddWithValue("@ReceiverUserID", receiverID);
                    command.Parameters.AddWithValue("@MessageText", messageText);
                    command.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
            txtMensaje.Text = string.Empty;
            LoadMessages();
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private Image GetProfilePicture(int UserID)
        {
            byte[] fotoData = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Foto FROM users WHERE UserID = @UserID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            fotoData = (byte[])reader["Foto"];
                        }
                    }
                }
            }

            if (fotoData != null)
            {
                using (var ms = new MemoryStream(fotoData))
                {
                    return Image.FromStream(ms);
                }
            }
            else
            {
                // Si no hay foto en la base de datos, puedes retornar una imagen predeterminada
                return Image.FromFile(@"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Logo.jpg"); // Asegúrate de tener una imagen predeterminada en tu proyecto
            }
        }
    }
}
