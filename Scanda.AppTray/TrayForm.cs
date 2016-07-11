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
using Newtonsoft.Json.Linq;

namespace Scanda.AppTray
{
    public partial class FormTray : Form
    {
        private RecuperarForm recoverForm;
        private ConfiguracionForm configuracionForm;
        private LoginForm loginForm;
        private string selectedPath = "";
        public FormTray()
        {
            InitializeComponent();
            if (!Directory.Exists("C:\\Scanda"))
            {
                // Creamos el Directorio
                Directory.CreateDirectory("C:\\Scanda");
            }
            if (!File.Exists(@"C:\Scanda\configuration.json"))
            {
                // Si no existe lo creamos
                // File.Create(@"C:\Scanda\configuration.json", 2048, FileOptions.Asynchronous);

                // Creamos el archivo de configuracion
                JObject configSettings = new JObject(
                    new JProperty("path", ""),
                    new JProperty("time_type", ""),
                    new JProperty("time", ""),
                    new JProperty("id_customer", ""),
                    new JProperty("user", ""),
                    new JProperty("password", ""),
                    new JProperty("token", "")
                );
                // escribimos el archivo
                File.WriteAllText(@"C:\Scanda\configuration.json", configSettings.ToString());
            }
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

        private void FormTray_Move(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIconScanda.ShowBalloonTip(1000, "Important notice", "Scanda Service is Running", ToolTipIcon.Info);
            }
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                selectedPath = fbd.SelectedPath;
            }
        }

        private void autenticarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            loginForm = new LoginForm();
            loginForm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            // notifyIconScanda.Icon = new System.Drawing.Icon(Application.StartupPath + @"\Resources\111.ico");
            configuracionForm = new ConfiguracionForm();
            configuracionForm.ShowDialog();
            // recoverForm = new RecuperarForm();
            // recoverForm.ShowDialog();
        }
    }
}
