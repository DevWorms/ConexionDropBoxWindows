using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Scanda.AppTray
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Obtenemos los datos de los inputs
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Revisamos si existe el directorio para almacenar nuestros archivos.

            if (!Directory.Exists("C:\\Scanda") )
            {
                // Creamos el Directorio
                Directory.CreateDirectory("C:\\Scanda");
            }
            // Revisamos si existe el archivo credentials.txt
            if (!File.Exists("C:\\Scanda\\credentials.txt"))
            {
                // Si no existe lo creamos
                File.Create("C:\\Scanda\\credentials.txt", 2048, FileOptions.Asynchronous);
            }
            // abrimos el archivo
            File.WriteAllText("C:\\Scanda\\credentials.txt", string.Format("{0}|{1}", username, password));
            // Servicio de Chemas
            this.Close();
        }
    }
}
