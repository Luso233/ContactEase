using System;
using System.Drawing;
using System.Windows.Forms;

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
            this.WindowState = FormWindowState.Maximized;
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
                Image = Image.FromFile(@"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Favoriteicon.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(50, 50),
                Location = new Point(10, 25)
            };

            

            // Panel inferior
            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(29, 29, 29),
                Padding = new Padding(10)
            };

      
        }

        private void AddButton(string text, Point location, EventHandler clickEventHandler)
        {
            Button button = new Button
            {
                Text = text,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 45),
                FlatStyle = FlatStyle.Flat,
                Location = location
            };
            button.FlatAppearance.BorderColor = Color.FromArgb(192, 0, 0);
            button.Click += clickEventHandler;
            EstilizarBoton(button);

           
        }

        private void EstilizarBoton(Button button)
        {
            button.FlatAppearance.BorderColor = Color.FromArgb(192, 0, 0);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            button.Padding = new Padding(5);
        }
    }
}
