using System;
using System.Windows.Forms;

namespace ContactEase
{
    public partial class AddContactForm : Form
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string FotoPath { get; private set; }
        public bool IsFavorite { get; private set; }

        public AddContactForm()
        {
            InitializeComponent();
            ApplyCustomDesign();
        }

        private void ApplyCustomDesign()
        {
            this.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.ForeColor = System.Drawing.Color.White;
            foreach (Control control in this.Controls)
            {
                if (control is Label)
                {
                    control.ForeColor = System.Drawing.Color.White;
                }
                if (control is TextBox)
                {
                    control.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
                    control.ForeColor = System.Drawing.Color.White;
                }
                if (control is Button)
                {
                    control.BackColor = System.Drawing.Color.FromArgb(63, 63, 70);
                    control.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            FirstName = txtFirstName.Text;
            LastName = txtLastName.Text;
            Phone = txtPhone.Text;
            Email = txtEmail.Text;
            IsFavorite = chkIsFavorite.Checked;
            FotoPath = txtFotoPath.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
