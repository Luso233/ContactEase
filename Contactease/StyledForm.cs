using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ContactEase
{
    public class StyledForm : Form
    {
        protected Panel topPanel;
        protected Panel bottomPanel;

        public StyledForm()
        {
            InitializeStyle();
        }

        private void InitializeStyle()
        {
            this.BackColor = Color.FromArgb(15, 15, 15);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // Panel superior
            topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(29, 29, 29),
                Padding = new Padding(10)
            };

            PictureBox logoPictureBox = new PictureBox
            {
                Image = Image.FromFile(@"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Logo.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(50, 50),
                Location = new Point(10, 25)
            };

            topPanel.Controls.Add(logoPictureBox);

            // Panel inferior
            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(29, 29, 29),
                Padding = new Padding(10)
            };

            this.Controls.Add(topPanel);
            this.Controls.Add(bottomPanel);
        }

        

        private void StyledForm_Load(object sender, EventArgs e)
        {

        }
    }
}
