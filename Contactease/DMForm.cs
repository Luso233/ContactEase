using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ContactEase
{
    public partial class DMForm : Form
    {
        private readonly int senderID;
        private readonly int receiverID;
        private readonly string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

        public DMForm(int senderID, int receiverID)
        {
            this.senderID = senderID;
            this.receiverID = receiverID;
            InitializeComponent();
            LoadMessages();
            UpdatePanelSuperior();
        }

        private void LoadMessages()
        {
            messagePanel.Controls.Clear();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM messages WHERE (SenderID = @SenderID AND ReceiverID = @ReceiverID) OR (SenderID = @ReceiverID AND ReceiverID = @SenderID) ORDER BY Timestamp";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderID", senderID);
                    command.Parameters.AddWithValue("@ReceiverID", receiverID);
                    using (var reader = command.ExecuteReader())
                    {
                        int lastSenderID = -1;
                        while (reader.Read())
                        {
                            int currentSenderID = reader.GetInt32("SenderID");
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
                                Image = GetProfilePicture(currentSenderID),
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
                                ForeColor = currentSenderID == senderID ? Color.Blue : Color.Green
                            };
                            messagePanelItem.Controls.Add(messageLabel);

                            // Position message panel
                            messagePanelItem.Location = new Point(0, messagePanel.Controls.Count * 55);

                            // Add message panel to messagePanel
                            messagePanel.Controls.Add(messagePanelItem);

                            lastSenderID = currentSenderID;
                        }
                    }
                }
            }
        }

        private void UpdatePanelSuperior()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users WHERE UserID = @ReceiverID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReceiverID", receiverID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Clear previous content
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

        private void SendMessage()
        {
            string messageText = txtMensaje.Text;
            if (string.IsNullOrWhiteSpace(messageText)) return;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO messages (SenderID, ReceiverID, MessageText, Timestamp) VALUES (@SenderID, @ReceiverID, @MessageText, @Timestamp)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderID", senderID);
                    command.Parameters.AddWithValue("@ReceiverID", receiverID);
                    command.Parameters.AddWithValue("@MessageText", messageText);
                    command.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
            txtMensaje.Text = string.Empty;
            LoadMessages();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            SendMessage();
        }


        private Image GetProfilePicture(int UserID)
        {
            // Replace with actual logic to fetch profile picture
            // This is just a placeholder
            return Image.FromFile("C:\\Users\\luisf\\source\\repos\\Contactease\\Contactease\\Carpeta\\Favoriteicon.jpg");
        }
    }

        
    
}
