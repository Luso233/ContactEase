using System.Drawing;
using System.Windows.Forms;

namespace ContactEase
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Declaración de miembros de la clase
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ComboBox comboBoxOrden;
        private System.Windows.Forms.TextBox searchBar;
        private Panel bottomPanel;
        private Label lblContactos;
        private Label lblMensajes;
        private PictureBox profilePictureBox;

        // Método InitializeComponent original
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxOrden = new System.Windows.Forms.ComboBox();
            this.searchBar = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // Initialize tableLayoutPanel
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 58);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(776, 380);
            this.tableLayoutPanel.TabIndex = 0;

            // Initialize comboBoxOrden
            this.comboBoxOrden.FormattingEnabled = true;
            this.comboBoxOrden.Items.AddRange(new object[] {
            "Nombre",
            "Apellido"});
            this.comboBoxOrden.Location = new System.Drawing.Point(12, 12);
            this.comboBoxOrden.Name = "comboBoxOrden";
            this.comboBoxOrden.Size = new System.Drawing.Size(121, 24);
            this.comboBoxOrden.TabIndex = 1;

            // Initialize searchBar
            this.searchBar.Location = new System.Drawing.Point(139, 12);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(100, 22);
            this.searchBar.TabIndex = 2;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.comboBoxOrden);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

            // Initialize NavigationBar
            InitializeNavigationBar();
        }

        private void InitializeNavigationBar()
        {
            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(29, 29, 29),
                Padding = new Padding(10)
            };

            lblContactos = new Label
            {
                Text = "Contactos",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            lblMensajes = new Label
            {
                Text = "Mensajes",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(this.Width / 2 - 30, 20)
            };

            profilePictureBox = new PictureBox
            {
                Image = Image.FromFile(@"C:\Users\luisf\source\repos\Contactease\Contactease\Carpeta\Favoriteicon.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(30, 30),
                Location = new Point(this.Width - 50, 20)
            };

            bottomPanel.Controls.Add(lblContactos);
            bottomPanel.Controls.Add(lblMensajes);
            bottomPanel.Controls.Add(profilePictureBox);
            this.Controls.Add(bottomPanel);
        }
    }
}
