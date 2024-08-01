using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactEase;
using System.Windows.Forms;

namespace Contactease
{
    public partial class LandingForm  : StyledForm
    {
        public LandingForm()
        {
            InitializeComponent();
            // Configura el diseño similar a Form1
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void LandingForm_Load(object sender, EventArgs e)
        {

        }
    }

}
