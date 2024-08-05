using System;
using System.Drawing;
using System.Windows.Forms;

namespace ContactEase
{
    public partial class ContactControl : UserControl
    {
        private readonly Contact contact;

        public ContactControl(Contact contact)
        {
            this.contact = contact;
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ContactControl
            // 
            this.Name = "ContactControl";
            this.Size = new System.Drawing.Size(200, 60);
            this.ResumeLayout(false);
        }

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            this.Padding = new Padding(10);
            this.Margin = new Padding(5);

            var picFoto = new PictureBox
            {
                Size = new Size(40, 40),
                Dock = DockStyle.Left,
                Image = string.IsNullOrEmpty(contact.FotoPath) ? null : Image.FromFile(contact.FotoPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(0, 0, 10, 0)
            };

            var lblName = new Label
            {
                Text = $"{contact.FirstName} {contact.LastName}",
                AutoSize = true,
                Dock = DockStyle.Top
            };

            var lblPhone = new Label
            {
                Text = contact.Phone,
                AutoSize = true,
                Dock = DockStyle.Top
            };

            var lblEmail = new Label
            {
                Text = contact.Email,
                AutoSize = true,
                Dock = DockStyle.Top
            };

            var infoPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 0, 0, 0)
            };

            infoPanel.Controls.Add(lblEmail);
            infoPanel.Controls.Add(lblPhone);
            infoPanel.Controls.Add(lblName);

            this.Controls.Add(infoPanel);
            this.Controls.Add(picFoto);
        }
    }
}
