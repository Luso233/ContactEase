using System;
using System.Windows.Forms;
using Contactease;
using ContactEase;


namespace ContactEase
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form()); 
        }
    }
}
