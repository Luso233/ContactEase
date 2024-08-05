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
        private readonly int UserID;
        private readonly List<Contact> contactos;
        private readonly string connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

        public Form1(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;

            InitializeNavigationBar();
            contactos = new List<Contact>();
            CargarContactosDesdeBaseDeDatos();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarContactosDesdeBaseDeDatos();
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
                            writer.WriteLine("VERSION:3.0");
                            writer.WriteLine($"N:{contact.LastName};{contact.FirstName}");
                            writer.WriteLine($"TEL:{contact.Phone}");
                            writer.WriteLine($"EMAIL:{contact.Email}");
                            writer.WriteLine("END:VCARD");
                        }
                    }
                }

                MessageBox.Show("Contactos exportados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar contactos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ImportContacts(openFileDialog.FileName, format);
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
                        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null,
                            MissingFieldFound = null
                        };

                        using (var csvReader = new CsvReader(reader, csvConfig))
                        {
                            var contacts = csvReader.GetRecords<Contact>().ToList();
                            foreach (var contact in contacts)
                            {
                                AddContactToList(contact);
                                GuardarContactoEnBaseDeDatos(contact);
                            }
                        }
                    }
                    else if (format == "vcf")
                    {
                        string line;
                        Contact contact = null;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("BEGIN:VCARD"))
                            {
                                contact = new Contact();
                            }
                            else if (line.StartsWith("N:") && contact != null)
                            {
                                var parts = line.Substring(2).Split(';');
                                contact.LastName = parts[0];
                                contact.FirstName = parts[1];
                            }
                            else if (line.StartsWith("TEL:") && contact != null)
                            {
                                contact.Phone = line.Substring(4);
                            }
                            else if (line.StartsWith("EMAIL:") && contact != null)
                            {
                                contact.Email = line.Substring(6);
                            }
                            else if (line.StartsWith("END:VCARD") && contact != null)
                            {
                                AddContactToList(contact);
                                GuardarContactoEnBaseDeDatos(contact);
                                contact = null;
                            }
                        }
                    }
                }

                MessageBox.Show("Contactos importados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al importar contactos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddContactToList(Contact contact)
        {
            contactos.Add(contact);
            int columnIndex = contactos.Count % 3;
            if (columnIndex == 0) columnIndex = 3;
            else if (columnIndex == 1) columnIndex = 1;
            else columnIndex = 2;

            var contactControl = new ContactControl(contact);
            tableLayoutPanel.Controls.Add(contactControl, columnIndex - 1, contactos.Count / 3);
        }

        private void CargarContactosDesdeBaseDeDatos()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "SELECT * FROM contacts WHERE UserID = @UserID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var contact = new Contact
                                {
                                    ContactID = reader.GetInt32("ContactID"),
                                    FirstName = reader.GetString("FirstName"),
                                    LastName = reader.GetString("LastName"),
                                    Phone = reader.GetString("Phone"),
                                    Email = reader.GetString("Email"),
                                    IsFavorite = reader.GetBoolean("IsFavorite"),
                                    FotoPath = reader.IsDBNull(reader.GetOrdinal("FotoPath")) ? null : reader.GetString("FotoPath")
                                };

                                AddContactToList(contact);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar contactos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarContactoEnBaseDeDatos(Contact contact)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO contacts (FirstName, LastName, Phone, Email, IsFavorite, FotoPath, UserID) VALUES (@FirstName, @LastName, @Phone, @Email, @IsFavorite, @FotoPath, @UserID)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                        command.Parameters.AddWithValue("@LastName", contact.LastName);
                        command.Parameters.AddWithValue("@Phone", contact.Phone);
                        command.Parameters.AddWithValue("@Email", contact.Email);
                        command.Parameters.AddWithValue("@IsFavorite", contact.IsFavorite);
                        command.Parameters.AddWithValue("@FotoPath", contact.FotoPath);
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar contacto en la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


