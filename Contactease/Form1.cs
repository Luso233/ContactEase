using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;


namespace ContactEase
{
    public partial class Form1 : Form
    {
        private readonly int _userId;
        private readonly List<Contact> contactos;
        private TableLayoutPanel tableLayoutPanel;
        private ComboBox comboBoxOrden;
        private TextBox searchBar;
        private readonly string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

        public Form1(int userId)
        {
            InitializeComponent();
            InitializeCustomComponents();
            InitializeNavigationBar();
            contactos = new List<Contact>();
            CargarContactosDesdeBaseDeDatos();
           
        }

        private void AddContactToList(Contact newContact)
        {
            contactos.Add(newContact);
            ActualizarInterfaz(contactos);
        }

        private void BtnAddContact_Click(object sender, EventArgs e)
        {
            using (var addContactForm = new AddContactForm())
            {
                if (addContactForm.ShowDialog() == DialogResult.OK)
                {
                    var newContact = new Contact
                    {
                        FirstName = addContactForm.FirstName,
                        LastName = addContactForm.LastName,
                        Phone = addContactForm.Phone,
                        Email = addContactForm.Email,
                        IsFavorite = addContactForm.IsFavorite,
                        FotoPath = addContactForm.FotoPath
                    };

                    AddContactToList(newContact);
                    GuardarContactoEnBaseDeDatos(newContact);
                }
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV Files|*.csv|VCF Files|*.vcf|All Files|*.*";
                saveFileDialog.Title = "Guardar Contactos";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string format = Path.GetExtension(saveFileDialog.FileName).ToLower() == ".csv" ? "csv" : "vcf";
                    ExportContacts(saveFileDialog.FileName, format);
                }
            }
        }

        private void ExportContacts(string filePath, string format)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    if (format == "csv")
                    {
                        writer.WriteLine("FirstName,LastName,Phone,Email,IsFavorite,FotoPath");
                        foreach (var contact in contactos)
                        {
                            writer.WriteLine($"{contact.FirstName},{contact.LastName},{contact.Phone},{contact.Email},{contact.IsFavorite},{contact.FotoPath}");
                        }
                    }
                    else if (format == "vcf")
                    {
                        foreach (var contact in contactos)
                        {
                            writer.WriteLine("BEGIN:VCARD");
                            writer.WriteLine($"FN:{contact.FirstName} {contact.LastName}");
                            writer.WriteLine($"TEL:{contact.Phone}");
                            writer.WriteLine($"EMAIL:{contact.Email}");
                            writer.WriteLine("END:VCARD");
                        }
                    }
                }
                MessageBox.Show("Contactos exportados exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar contactos: " + ex.Message);
            }
        }

        public class Importer
        {
            private readonly Form1 formInstance;
            private readonly string connectionString;

            public Importer(Form1 form, string dbConnectionString)
            {
                formInstance = form;
                connectionString = dbConnectionString;
            }

            public void ImportContacts(string filePath, string format)
            {
                try
                {
                    if (format == "csv")
                    {
                        ImportContactsFromCsv(filePath);
                    }
                    else if (format == "vcf")
                    {
                        ImportContactsFromVcf(filePath);
                    }

                    MessageBox.Show("Contactos importados exitosamente.");
                    formInstance.CargarContactosDesdeBaseDeDatos(); // Actualizar la interfaz después de la importación
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al importar contactos: " + ex.Message);
                }
            }

            private void ImportContactsFromCsv(string filePath)
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                };

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<Contact>().ToList();
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        foreach (var contact in records)
                        {
                            var command = new MySqlCommand("INSERT INTO contacts (FirstName, LastName, Phone, Email, IsFavorite, FotoPath) VALUES (@FirstName, @LastName, @Phone, @Email, @IsFavorite, @FotoPath)", connection);
                            command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                            command.Parameters.AddWithValue("@LastName", contact.LastName);
                            command.Parameters.AddWithValue("@Phone", contact.Phone);
                            command.Parameters.AddWithValue("@Email", contact.Email);
                            command.Parameters.AddWithValue("@IsFavorite", contact.IsFavorite);
                            command.Parameters.AddWithValue("@FotoPath", contact.FotoPath);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            private void ImportContactsFromVcf(string filePath)
            {
                var contacts = new List<Contact>();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    Contact newContact = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("BEGIN:VCARD"))
                        {
                            newContact = new Contact();
                        }
                        else if (line.StartsWith("FN:"))
                        {
                            var names = line.Substring(3).Split(' ');
                            newContact.FirstName = names[0];
                            newContact.LastName = names.Length > 1 ? names[1] : string.Empty;
                        }
                        else if (line.StartsWith("TEL:"))
                        {
                            newContact.Phone = line.Substring(4);
                        }
                        else if (line.StartsWith("EMAIL:"))
                        {
                            newContact.Email = line.Substring(6);
                        }
                        else if (line.StartsWith("END:VCARD"))
                        {
                            contacts.Add(newContact);
                        }
                    }
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (var contact in contacts)
                    {
                        var command = new MySqlCommand("INSERT INTO contacts (FirstName, LastName, Phone, Email, IsFavorite, FotoPath) VALUES (@FirstName, @LastName, @Phone, @Email, @IsFavorite, @FotoPath)", connection);
                        command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                        command.Parameters.AddWithValue("@LastName", contact.LastName);
                        command.Parameters.AddWithValue("@Phone", contact.Phone);
                        command.Parameters.AddWithValue("@Email", contact.Email);
                        command.Parameters.AddWithValue("@IsFavorite", contact.IsFavorite);
                        command.Parameters.AddWithValue("@FotoPath", contact.FotoPath);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV Files|*.csv|VCF Files|*.vcf|All Files|*.*";
                openFileDialog.Title = "Importar Contactos";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string format = Path.GetExtension(openFileDialog.FileName).ToLower() == ".csv" ? "csv" : "vcf";
                    Importer importer = new Importer(this, connectionString); // Pasar 'this' para la instancia de Form1
                    importer.ImportContacts(openFileDialog.FileName, format);
                }
            }
        }




        private void BtnEditContact_Click(object sender, EventArgs e)
        {
            Contact selectedContact = GetSelectedContact();
            if (selectedContact != null)
            {
                EditContactForm editForm = new EditContactForm(selectedContact);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Actualizar los valores del contacto con los valores del formulario de edición
                    selectedContact.FirstName = editForm.FirstName;
                    selectedContact.LastName = editForm.LastName;
                    selectedContact.Phone = editForm.Phone;
                    selectedContact.Email = editForm.Email;
                    selectedContact.IsFavorite = editForm.IsFavorite;
                    selectedContact.FotoPath = editForm.FotoPath;

                    // Llamar al método para actualizar el contacto en la base de datos
                    ActualizarContactoEnBaseDeDatos(selectedContact);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un contacto para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void InitializeCustomComponents()
        {
            this.Text = "ContactEase";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(15, 15, 15);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Cambiar la fuente

            Panel topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100, // Incrementar la altura del panel superior
                BackColor = Color.FromArgb(29, 29, 29),
                Padding = new Padding(10)
            };

            PictureBox logoPictureBox = new PictureBox
            {
                Image = Image.FromFile(@"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Favoriteicon.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(50, 50),
                Location = new Point(10, 25) // Ajustar la posición
            };

            searchBar = new TextBox
            {
                Width = 200,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                Location = new Point(70, 35), // Ajustar la posición
                                              //PlaceholderText = "Buscar..."
            };
            searchBar.TextChanged += (s, e) => BuscarContactos(searchBar.Text);

            comboBoxOrden = new ComboBox
            {
                Width = 150,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                Location = new Point(280, 35) // Ajustar la posición
            };
            comboBoxOrden.Items.AddRange(new[] { "Alfabético", "Favoritos" });
            comboBoxOrden.SelectedIndexChanged += (s, e) => OrdenarContactos(comboBoxOrden.SelectedItem.ToString());

            Button btnAddContact = new Button
            {
                Text = "Agregar Contacto",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(450, 35) // Ajustar la posición
            };
            btnAddContact.FlatAppearance.BorderColor = Color.FromArgb(192, 0, 0);
            btnAddContact.Click += BtnAddContact_Click;
            EstilizarBoton(btnAddContact); // Estilizar botón

            Button btnExport = new Button
            {
                Text = "Exportar",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(600, 35) // Ajustar la posición
            };
            btnExport.FlatAppearance.BorderColor = Color.FromArgb(192, 0, 0);
            btnExport.Click += BtnExport_Click;
            EstilizarBoton(btnExport); // Estilizar botón

            Button btnImport = new Button
            {
                Text = "Importar",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(700, 35) // Ajustar la posición
            };
            btnImport.FlatAppearance.BorderColor = Color.FromArgb(192, 0, 0);
            btnImport.Click += BtnImport_Click;
            EstilizarBoton(btnImport); // Estilizar botón

            Button btnLogout = new Button
            {
                Text = "Cerrar sesión",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(800, 35), // Ajustar la posición
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnLogout.FlatAppearance.BorderColor = Color.FromArgb(192, 0, 0);
            btnLogout.Click += (s, e) => this.Close();
            EstilizarBoton(btnLogout); // Estilizar botón

            topPanel.Controls.Add(logoPictureBox);
            topPanel.Controls.Add(searchBar);
            topPanel.Controls.Add(comboBoxOrden);
            topPanel.Controls.Add(btnAddContact);
            topPanel.Controls.Add(btnExport);
            topPanel.Controls.Add(btnImport);
            topPanel.Controls.Add(btnLogout);
            this.Controls.Add(topPanel);

            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                AutoScroll = true,
                Padding = new Padding(10, 100, 10, 100)
            };

            for (int i = 0; i < 4; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            this.Controls.Add(tableLayoutPanel);
        }



        private void InitializeNavigationBar()
        {
            Panel bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70, // Incrementar la altura del panel inferior
                BackColor = Color.FromArgb(29, 29, 29),
                Padding = new Padding(10)
            };

            Label lblContactos = new Label
            {
                Text = "Contactos",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20) // Ajustar la posición
            };

            Label lblMensajes = new Label
            {
                Text = "Mensajes",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(this.Width / 2 - 30, 20) // Ajustar la posición
            };

            PictureBox profilePictureBox = new PictureBox
            {
                Image = Image.FromFile(@"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Favoriteicon.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(30, 30),
                Location = new Point(this.Width - 50, 20) // Ajustar la posición
            };

            bottomPanel.Controls.Add(lblContactos);
            bottomPanel.Controls.Add(lblMensajes);
            bottomPanel.Controls.Add(profilePictureBox);
            this.Controls.Add(bottomPanel);
        }


        private void ActualizarInterfaz(List<Contact> contactos)
        {
            tableLayoutPanel.Controls.Clear();
            foreach (var contacto in contactos)
            {
                AgregarContactoATabla(contacto);
            }
        }

        private void EstilizarBoton(Button button)
        {
            button.FlatAppearance.BorderColor = Color.FromArgb(192, 0, 0);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Fuente profesional
            button.Padding = new Padding(5);
        }


        private void GuardarContactoEnBaseDeDatos(Contact contact)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO contacts (FirstName, LastName, Phone, Email, IsFavorite, FotoPath) VALUES (@FirstName, @LastName, @Phone, @Email, @IsFavorite, @FotoPath)", connection);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Phone", contact.Phone);
                command.Parameters.AddWithValue("@Email", contact.Email);
                command.Parameters.AddWithValue("@IsFavorite", contact.IsFavorite);
                command.Parameters.AddWithValue("@FotoPath", contact.FotoPath);
                command.ExecuteNonQuery();
            }
        }

        private void AgregarContactoATabla(Contact contact)
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
                Image = Image.FromFile(string.IsNullOrEmpty(contact.FotoPath) ? @"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Favoriteicon.jpg" : contact.FotoPath)
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
                using (var editContactForm = new EditContactForm(contact))
                {
                    if (editContactForm.ShowDialog() == DialogResult.OK)
                    {
                        contact.FirstName = editContactForm.FirstName;
                        contact.LastName = editContactForm.LastName;
                        contact.Phone = editContactForm.Phone;
                        contact.Email = editContactForm.Email;
                        contact.IsFavorite = editContactForm.IsFavorite;
                        contact.FotoPath = editContactForm.FotoPath;

                        ActualizarInterfaz(contactos);
                        ActualizarContactoEnBaseDeDatos(contact);
                    }
                }
            };

            contactPanel.Controls.Add(fotoPictureBox);
            contactPanel.Controls.Add(nombreLabel);
            contactPanel.Controls.Add(telefonoLabel);
            contactPanel.Controls.Add(emailLabel);
            tableLayoutPanel.Controls.Add(contactPanel);
        }

        private void CargarContactosDesdeBaseDeDatos()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM contacts", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contact = new Contact
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            IsFavorite = Convert.ToBoolean(reader["IsFavorite"]),
                            FotoPath = reader["FotoPath"].ToString()
                        };
                        contactos.Add(contact);
                    }
                }
            }
            ActualizarInterfaz(contactos);
        }


        private void ActualizarContactoEnBaseDeDatos(Contact contact)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE contacts SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @Email, IsFavorite = @IsFavorite, FotoPath = @FotoPath WHERE ContactID = @ContactID", connection);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Phone", contact.Phone);
                command.Parameters.AddWithValue("@Email", contact.Email);
                command.Parameters.AddWithValue("@IsFavorite", contact.IsFavorite);
                command.Parameters.AddWithValue("@FotoPath", contact.FotoPath);
                command.Parameters.AddWithValue("@ContactID", contact.ContactID);
                command.ExecuteNonQuery();
            }
            ActualizarInterfaz(contactos); // Actualizar la interfaz después de la edición
        }

        private Contact GetSelectedContact()
        {
            // Implementa la lógica para obtener el contacto seleccionado de la lista
            return new Contact();
        }

        private void BuscarContactos(string searchTerm)
        {
            var filteredContacts = contactos.Where(c => c.FirstName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                        c.LastName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                        c.Phone.IndexOf(searchTerm) >= 0 ||
                                                        c.Email.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            ActualizarInterfaz(filteredContacts);
        }


        private void OrdenarContactos(string orden)
        {
            List<Contact> sortedContacts;
            if (orden == "Alfabético")
            {
                sortedContacts = contactos.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).ToList();
            }
            else // "Favoritos"
            {
                sortedContacts = contactos.OrderByDescending(c => c.IsFavorite).ThenBy(c => c.FirstName).ThenBy(c => c.LastName).ToList();
            }
            ActualizarInterfaz(sortedContacts);
        }




        
    }

    
}
