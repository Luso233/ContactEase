using System.Drawing;
using System.Windows.Forms;

namespace ContactEase
{
    partial class DMForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelSuperior;
        private ListView listViewMensajes;
        private TextBox txtMensaje;
        private Button btnEnviar;
        private System.Windows.Forms.Panel messagePanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.listViewMensajes = new System.Windows.Forms.ListView();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.messagePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(2);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(665, 75);
            this.panelSuperior.TabIndex = 0;
            // 
            // listViewMensajes
            // 
            this.listViewMensajes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.listViewMensajes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMensajes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listViewMensajes.HideSelection = false;
            this.listViewMensajes.Location = new System.Drawing.Point(0, 75);
            this.listViewMensajes.Margin = new System.Windows.Forms.Padding(2);
            this.listViewMensajes.Name = "listViewMensajes";
            this.listViewMensajes.Size = new System.Drawing.Size(665, 462);
            this.listViewMensajes.TabIndex = 6;
            this.listViewMensajes.UseCompatibleStateImageBehavior = false;
            this.listViewMensajes.View = System.Windows.Forms.View.Details;
            // 
            // txtMensaje
            // 
            this.txtMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtMensaje.Location = new System.Drawing.Point(35, 451);
            this.txtMensaje.Margin = new System.Windows.Forms.Padding(2);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(401, 20);
            this.txtMensaje.TabIndex = 7;
            // 
            // btnEnviar
            // 
            this.btnEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnEnviar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnEnviar.Location = new System.Drawing.Point(501, 436);
            this.btnEnviar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 35);
            this.btnEnviar.TabIndex = 8;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // messagePanel
            // 
            this.messagePanel.AutoScroll = true;
            this.messagePanel.Location = new System.Drawing.Point(35, 124);
            this.messagePanel.Margin = new System.Windows.Forms.Padding(2);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Size = new System.Drawing.Size(557, 278);
            this.messagePanel.TabIndex = 0;
            // 
            // DMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(665, 537);
            this.Controls.Add(this.messagePanel);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.listViewMensajes);
            this.Controls.Add(this.panelSuperior);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DMForm";
            this.Text = "Mensajería";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}


