using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactEase
{
    public partial class DMForm : Form
    {
        private Panel panelSuperior;
        private TextBox searchBar;
        private ComboBox filtroComboBox;
        private Button btnNuevoMensaje;
        private Button btnExportar;
        private Button btnImportar;
        private ListView listViewMensajes;
        private TextBox txtMensaje;
        private Button btnEnviar;

        public DMForm()
        {
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Configuración del Formulario
            this.Text = "Mensajería";
            this.Size = new Size(800, 600);
            this.BackColor = Color.FromArgb(27, 26, 26);
            this.Font = new Font("Segoe UI", 12);
            this.ForeColor = Color.FromArgb(225, 225, 225);

            // Panel Superior
            panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(51, 51, 51)
            };

            // Barra de búsqueda
            searchBar = new TextBox
            {
                Width = 400,
                Height = 30,
                Location = new Point(10, 15),
                BackColor = Color.FromArgb(51, 51, 51),
                ForeColor = Color.FromArgb(255, 255, 255),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Menú de filtros
            filtroComboBox = new ComboBox
            {
                Location = new Point(420, 15),
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(51, 51, 51),
                ForeColor = Color.FromArgb(255, 255, 255),
            };
            filtroComboBox.Items.AddRange(new[] { "Todos", "No leídos" });

            // Botones de acción
            btnNuevoMensaje = CreateButton("Nuevo", new Point(550, 15));
            btnExportar = CreateButton("Exportar", new Point(630, 15));
            btnImportar = CreateButton("Importar", new Point(710, 15));

            panelSuperior.Controls.Add(searchBar);
            panelSuperior.Controls.Add(filtroComboBox);
            panelSuperior.Controls.Add(btnNuevoMensaje);
            panelSuperior.Controls.Add(btnExportar);
            panelSuperior.Controls.Add(btnImportar);
            this.Controls.Add(panelSuperior);

            // Lista de mensajes
            listViewMensajes = new ListView
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 60),
                BackColor = Color.FromArgb(27, 26, 26),
                ForeColor = Color.FromArgb(255, 255, 255),
                View = View.Details
            };
            listViewMensajes.Columns.Add("Mensajes", -2, HorizontalAlignment.Left);
            this.Controls.Add(listViewMensajes);

            // Panel Inferior
            Panel panelInferior = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(51, 51, 51)
            };

            // Campo de texto para mensaje
            txtMensaje = new TextBox
            {
                Width = 600,
                Height = 30,
                Location = new Point(10, 15),
                BackColor = Color.FromArgb(51, 51, 51),
                ForeColor = Color.FromArgb(255, 255, 255),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Botón de enviar
            btnEnviar = CreateButton("Enviar", new Point(620, 15));

            panelInferior.Controls.Add(txtMensaje);
            panelInferior.Controls.Add(btnEnviar);
            this.Controls.Add(panelInferior);
        }

        private Button CreateButton(string text, Point location)
        {
            return new Button
            {
                Text = text,
                Size = new Size(80, 30),
                Location = location,
                BackColor = Color.FromArgb(51, 51, 51),
                ForeColor = Color.FromArgb(255, 255, 255),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderColor = Color.FromArgb(255, 76, 76), BorderSize = 1 }
            };
        }

    }
}
