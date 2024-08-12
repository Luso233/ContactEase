using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Contactease;
using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;

namespace ContactEase
{
    public partial class Form1 : Form
    {
        private readonly int UserID;
        private readonly int ContactUserID;
        private readonly List<Contact> contactos;
        private readonly string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

        public Form1(int userID, int contactUserID)
        {
            UserID = userID;
            ContactUserID = contactUserID;
            contactos = new List<Contact>();

            InitializeComponent();
            LoadContacts();
            DisplayContacts(contactos);
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            searchBar.TextChanged += (sender, e) => SearchContacts();
            comboBoxOrden.SelectedIndexChanged += (sender, e) => SortContacts();
            btnAddContact.Click += (sender, e) => OpenAddContactForm();
            btnExport.Click += (sender, e) => ExportContacts();
            btnImport.Click += (sender, e) => ImportContacts();
            btnLogout.Click += (sender, e) => Logout();
        }

        private void LoadContacts()
        {
            contactos.Clear();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM contacts WHERE UserID = @UserID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
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
            DisplayContacts(contactos);
        }

        private void DisplayContacts(IEnumerable<Contact> contactList)
        {
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowStyles.Clear(); // Limpiar estilos de fila
            tableLayoutPanel.RowCount = 0; // Reiniciar el contador de filas

            foreach (var contact in contactList)
            {
                var contactPanel = CreateContactPanel(contact);
                tableLayoutPanel.Controls.Add(contactPanel);
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Agregar estilo de fila
                tableLayoutPanel.SetRow(contactPanel, tableLayoutPanel.RowCount++); // Incrementar el contador de filas
            }
        }

        private Panel CreateContactPanel(Contact contact)
        {
            Panel contactPanel = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                Margin = new Padding(10),
                BackColor = Color.FromArgb(45, 45, 45)
            };

            PictureBox fotoPictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = contact.Foto != null ? Image.FromStream(new MemoryStream(contact.Foto)) : Image.FromFile(@"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Logo.jpg")
            };

            Label nombreLabel = new Label
            {
                Text = $"{contact.FirstName} {contact.LastName}",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(100, 10),
                Font = new Font("Arial", 14, FontStyle.Bold)
            };

            Label telefonoLabel = new Label
            {
                Text = contact.Phone,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(100, 40),
                Font = new Font("Arial", 12)
            };

            Label emailLabel = new Label
            {
                Text = contact.Email,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(100, 70),
                Font = new Font("Arial", 12)
            };


            contactPanel.Click += (s, e) =>
            {
                OpenEditContactForm(contact);
            };

            contactPanel.Controls.Add(fotoPictureBox);
            contactPanel.Controls.Add(nombreLabel);
            contactPanel.Controls.Add(telefonoLabel);
            contactPanel.Controls.Add(emailLabel);
            

            return contactPanel;
        }

        private void SearchContacts()
        {
            var filteredContacts = contactos.Where(c => c.FirstName.ToLower().Contains(searchBar.Text.ToLower())).ToList();
            DisplayContacts(filteredContacts);
        }

        private void SortContacts()
        {
            var sortedContacts = contactos.OrderBy(c => c.FirstName).ToList();
            if (comboBoxOrden.SelectedItem.ToString() == "Favoritos")
            {
                sortedContacts = sortedContacts.OrderByDescending(c => c.IsFavorite).ThenBy(c => c.FirstName).ToList();
            }
            DisplayContacts(sortedContacts);
        }

        private void OpenAddContactForm()
        {
            var addContactForm = new AddContactForm(UserID, ContactUserID);
            if (addContactForm.ShowDialog() == DialogResult.OK)
            {
                var newContact = addContactForm.NewContact;
                if (newContact != null)
                {
                    AddContactToDatabase(newContact);
                    LoadContacts();
                    DisplayContacts(contactos);
                }
            }
        }

        private void OpenEditContactForm(Contact contact)
        {
            var editContactForm = new EditContactForm(contact);
            if (editContactForm.ShowDialog() == DialogResult.OK)
            {
                contact.FirstName = editContactForm.FirstName;
                contact.LastName = editContactForm.LastName;
                contact.Phone = editContactForm.Phone;
                contact.Email = editContactForm.Email;
                contact.IsFavorite = editContactForm.IsFavorite;
                contact.Foto = editContactForm.Foto;
                UpdateContactInDatabase(contact); // Usar el objeto contact aquí directamente
                LoadContacts();
                DisplayContacts(contactos);
            }
        }


        private void ExportContacts()
        {
            using (var sfd = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv", FileName = "contacts.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(sfd.FileName))
                    using (var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                    {
                        csv.WriteRecords(contactos);
                    }
                }
            }
        }

        private void ImportContacts()
        {
            using (var ofd = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var reader = new StreamReader(ofd.FileName))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                    {
                        var importedContacts = csv.GetRecords<Contact>().ToList();
                        foreach (var contact in importedContacts)
                        {
                            contact.UserID = UserID;
                            AddContactToDatabase(contact);
                        }
                        LoadContacts();
                        DisplayContacts(contactos);
                    }
                }
            }
        }

        private void OpenDMForm(int ReceiverID)
        {
            DMForm dmForm = new DMForm(UserID, ReceiverID);
            dmForm.StartPosition = FormStartPosition.Manual;
            dmForm.Location = new Point(0, Screen.PrimaryScreen.WorkingArea.Height - dmForm.Height);
            dmForm.Show();

        }




        private void AddContactToDatabase(Contact contact)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Verificar si ContactUserID es válido
                string checkQuery = "SELECT COUNT(*) FROM users WHERE UserID = @ContactUserID";
                using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ContactUserID", contact.ContactUserID);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("El usuario de contacto no existe.");
                        return;
                    }
                }

                // Insertar contacto en la base de datos
                string query = "INSERT INTO contacts (UserID, ContactUserID, FirstName, LastName, Phone, Email, Foto, IsFavorite) VALUES (@UserID, @ContactUserID, @FirstName, @LastName, @Phone, @Email, @Foto, @IsFavorite)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", contact.UserID);
                    command.Parameters.AddWithValue("@ContactUserID", contact.ContactUserID);
                    command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    command.Parameters.AddWithValue("@LastName", contact.LastName);
                    command.Parameters.AddWithValue("@Phone", contact.Phone);
                    command.Parameters.AddWithValue("@Email", contact.Email);
                    command.Parameters.AddWithValue("@Foto", contact.Foto);
                    command.Parameters.AddWithValue("@IsFavorite", contact.IsFavorite);
                    command.ExecuteNonQuery();
                }
            }
        }


        private void UpdateContactInDatabase(Contact contact)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE contacts SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @Email, Foto = @Foto, IsFavorite = @IsFavorite WHERE ContactID = @ContactID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    command.Parameters.AddWithValue("@LastName", contact.LastName);
                    command.Parameters.AddWithValue("@Phone", contact.Phone);
                    command.Parameters.AddWithValue("@Email", contact.Email);
                    command.Parameters.AddWithValue("@Foto", contact.Foto);
                    command.Parameters.AddWithValue("@IsFavorite", contact.IsFavorite);
                    command.Parameters.AddWithValue("@ContactID", contact.ContactID);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Logout()
        {
            var landingForm = new LandingForm();
            landingForm.Show();
            this.Close();
        }

        private void lblMensajes_Click(object sender, EventArgs e)
        {
            int ReceiverID = ContactUserID; // Puedes seleccionar un contacto específico aquí
            OpenDMForm(ReceiverID);
        }
    }
}
