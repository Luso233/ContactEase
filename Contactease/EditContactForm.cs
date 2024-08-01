using System;
using Contactease;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


namespace ContactEase
{
    public partial class EditContactForm : Form
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsFavorite { get; set; }
        public string FotoPath { get; set; }
        private readonly string connectionString;
        private readonly Contact contact;

        public EditContactForm(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;
            connectionString = "Server=127.0.0.1; Port=3306; User ID=id22398096_luso; Password=Socima66; Database=contactease;";

            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;
            txtPhone.Text = contact.Phone;
            txtEmail.Text = contact.Email;
            chkIsFavorite.Checked = contact.IsFavorite;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Email = txtEmail.Text;
            contact.IsFavorite = chkIsFavorite.Checked;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE contacts SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @Email, IsFavorite = @IsFavorite WHERE ContactID = @ContactID", connection);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@Phone", contact.Phone);
                command.Parameters.AddWithValue("@Email", contact.Email);
                command.Parameters.AddWithValue("@IsFavorite", contact.IsFavorite);
                command.Parameters.AddWithValue("@ContactID", contact.ContactID);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Contacto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Cierra el formulario después de guardar
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM contacts WHERE ContactID = @ContactID", connection);
                command.Parameters.AddWithValue("@ContactID", contact.ContactID);
                command.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}
