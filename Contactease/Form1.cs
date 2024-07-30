using ContactEase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Contactease;
using System.Net.Sockets;

namespace ContactEase
{
    public partial class Form1 : Form
    {
        private readonly List<Contact> contactos; // Definir contactos como un campo de clase
        private TableLayoutPanel tableLayoutPanel; // Definir el TableLayoutPanel como un campo de clase
        private ComboBox comboBoxOrden; // Definir el ComboBox como un campo de clase
        private TextBox searchBar; // Definir la barra de búsqueda como un campo de clase

        public Form1()
        {
            InitializeComponent();
            TestDatabaseConnection();
            InitializeCustomComponents();
            // Inicializar la lista de contactos
            contactos = new List<Contact>();
            CargarContactos();
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
                    // Añadir más formatos según sea necesario
                }
                MessageBox.Show("Contactos exportados exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar contactos: " + ex.Message);
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

        private void ImportContacts(string filePath, string format)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    if (format == "csv")
                    {
                        string line;
                        reader.ReadLine(); // Saltar el encabezado
                        while ((line = reader.ReadLine()) != null)
                        {
                            var values = line.Split(',');
                            var newContact = new Contact
                            {
                                FirstName = values[0],
                                LastName = values[1],
                                Phone = values[2],
                                Email = values[3],
                                IsFavorite = bool.Parse(values[4]),
                                FotoPath = values[5]
                            };
                            AddContactToList(newContact);
                        }
                    }
                    else if (format == "vcf")
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
                                newContact.LastName = names[1];
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
                                AddContactToList(newContact);
                            }
                        }
                    }
                    // Añadir más formatos según sea necesario
                }
                MessageBox.Show("Contactos importados exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al importar contactos: " + ex.Message);
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV Files|*.csv|VCF Files|*.vcf|All Files|*.*";
                openFileDialog.Title = "Abrir Contactos";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string format = Path.GetExtension(openFileDialog.FileName).ToLower() == ".csv" ? "csv" : "vcf";
                    ImportContacts(openFileDialog.FileName, format);
                }
            }
        }

        private void InitializeCustomComponents()
        {
            // Set form properties
            this.Text = "ContactEase";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(15, 15, 15); // Dark background color

            // Create and configure the top panel
            Panel topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80, // Increased height to accommodate logo
                BackColor = Color.FromArgb(29, 29, 29), // Darker panel background
                Padding = new Padding(10) // Add padding to avoid overlapping
            };

            // Create and configure the logo
            PictureBox logoPictureBox = new PictureBox
            {
                Image = Image.FromFile(@"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Favoriteicon.jpg"), // Path to logo image file
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(50, 50),
                Location = new Point(10, 15)
            };

            // Create and configure the search bar
            searchBar = new TextBox
            {
                // PlaceholderText = "Buscar...",
                Width = 200,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                Font = new Font("Arial", 12, FontStyle.Regular),
                Top = 15,
                Left = 70
            };
            searchBar.TextChanged += SearchBar_TextChanged;

            // Create and configure the dropdown menu
            comboBoxOrden = new ComboBox
            {
                Top = 15,
                Left = searchBar.Right + 10,
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                Font = new Font("Arial", 12, FontStyle.Regular) // Set font
            };
            comboBoxOrden.Items.AddRange(new object[] { "Alfabético", "Favoritos" });
            comboBoxOrden.SelectedIndexChanged += ComboBoxOrden_SelectedIndexChanged;

            // Create and configure buttons with improved design
            Button addButton = CreateCustomButton("Agregar Contacto", searchBar.Right + 200);
            addButton.Click += BtnAddContact_Click;
            Button exportButton = CreateCustomButton("Exportar Contacto", addButton.Right + 10);
            exportButton.Click += BtnExport_Click;
            Button importButton = CreateCustomButton("Importar Contacto", exportButton.Right + 10);
            importButton.Click += BtnImport_Click;
            Button logoutButton = CreateCustomButton("Cerrar sesión", importButton.Right + 700); // Adjust position

            // Add controls to the top panel
            topPanel.Controls.Add(logoPictureBox);
            topPanel.Controls.Add(searchBar);
            topPanel.Controls.Add(comboBoxOrden);
            topPanel.Controls.Add(addButton);
            topPanel.Controls.Add(exportButton);
            topPanel.Controls.Add(importButton);
            topPanel.Controls.Add(logoutButton);

            // Create and configure the TableLayoutPanel
            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                AutoScroll = true,
                BackColor = Color.FromArgb(24, 24, 24) // Slightly lighter background for contrast
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Profile picture
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Name
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Phone
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Email

            // Add panels to the form
            this.Controls.Add(tableLayoutPanel);
            this.Controls.Add(topPanel);

            // Set TableLayoutPanel as a class field for easier access
            //this.tableLayoutPanel = tableLayoutPanel;
        }

        private Button CreateCustomButton(string text, int leftPosition)
        {
            return new Button
            {
                Text = text,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(35, 35, 35), // Dark button background
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Regular), // Font size reduced
                Size = new Size(150, 40), // Adjusted size
                Left = leftPosition,
                Top = 15
            };
        }

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = searchBar.Text.ToLower();
            var filteredContacts = contactos.Where(c => c.FirstName.ToLower().Contains(searchTerm) || c.LastName.ToLower().Contains(searchTerm)).ToList();
            ActualizarInterfaz(filteredContacts);
        }

        private void ComboBoxOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOrden.SelectedItem.ToString() == "Favoritos")
            {
                var favoriteContacts = contactos.Where(c => c.IsFavorite).ToList();
                ActualizarInterfaz(favoriteContacts);
            }
            else
            {
                ActualizarInterfaz(contactos.OrderBy(c => c.FirstName).ToList());
            }
        }

        private void ActualizarInterfaz(List<Contact> contactosActualizados)
        {
            tableLayoutPanel.Controls.Clear();

            foreach (var contact in contactosActualizados)
            {
                // Add profile picture
                PictureBox profilePicture = new PictureBox
                {
                    Size = new Size(50, 50),
                    ImageLocation = contact.FotoPath,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill
                };

                // Ensure all images are the same size
                profilePicture.SizeMode = PictureBoxSizeMode.Zoom;
                profilePicture.Width = 50;
                profilePicture.Height = 50;

                // Add name and surname
                Label nameLabel = new Label
                {
                    Text = $"{contact.FirstName} {contact.LastName}",
                    ForeColor = Color.White,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Arial", 12, FontStyle.Regular)
                };

                // Add phone number
                Label phoneLabel = new Label
                {
                    Text = contact.Phone,
                    ForeColor = Color.White,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Arial", 12, FontStyle.Regular)
                };

                // Add email address
                Label emailLabel = new Label
                {
                    Text = contact.Email,
                    ForeColor = Color.White,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Arial", 12, FontStyle.Regular)
                };

                tableLayoutPanel.Controls.Add(profilePicture);
                tableLayoutPanel.Controls.Add(nameLabel);
                tableLayoutPanel.Controls.Add(phoneLabel);
                tableLayoutPanel.Controls.Add(emailLabel);
            }
        }

        private void CargarContactos()
        {
            // Implementar la lógica para cargar contactos desde una base de datos u otra fuente
            // Por ahora, agregamos algunos contactos de ejemplo

            contactos.Add(new Contact
            {
                FirstName = "Juan",
                LastName = "Pérez",
                Phone = "1234567890",
                Email = "juan.perez@example.com",
                IsFavorite = false,
                FotoPath = @"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Default.jpg"
            });

            contactos.Add(new Contact
            {
                FirstName = "María",
                LastName = "González",
                Phone = "0987654321",
                Email = "maria.gonzalez@example.com",
                IsFavorite = true,
                FotoPath = @"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Default.jpg"
            });

            // Añadir más contactos de ejemplo aquí

            ActualizarInterfaz(contactos);
        }

        private void TestDatabaseConnection()
        {
            // Utiliza el hostname y la IP proporcionados
             string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

            ;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Connection to database successful!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to the database: " + ex.Message);
                }
            }
        }
    }
}
