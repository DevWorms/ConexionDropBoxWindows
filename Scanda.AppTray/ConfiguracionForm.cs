using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scanda.AppTray
{
    public partial class ConfiguracionForm : Form
    {
        private LoginForm loginForm;
        private string selectedPath = "";
        private string json;
        private Config config;
        public ConfiguracionForm()
        {
            InitializeComponent();
            // leemos el archivo de configuracion
            json = File.ReadAllText(@"C:\Scanda\configuration.json");
            config = JsonConvert.DeserializeObject<Config>(json);

            txtRuta.Text = config.path;

            if (!string.IsNullOrWhiteSpace(config.id_customer))
            {
                // Hay un usuario logueado
                btnLogin.Enabled = false;
                btnDesvincular.Enabled = true;
            }
        }

        private void ConfiguracionForm_Refresh(object sender, EventArgs e)
        {
            try
            {
                // Revisamos el archivo JSON
                if (!string.IsNullOrWhiteSpace(config.user.Trim()) && !string.IsNullOrWhiteSpace(config.password.Trim()) && !string.IsNullOrWhiteSpace(config.id_customer.Trim()))
                {
                    btnLogin.Enabled = false;
                    btnDesvincular.Enabled = true;
                }
            }catch(Exception ex)
            {
                Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");
            }
        }

        private void btnElegir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = fbd.SelectedPath;
                    txtRuta.Text = selectedPath;
                    config.path = selectedPath;
                    // Guardamos la ruta
                    File.WriteAllText(@"C:\Scanda\configuration.json", JsonConvert.SerializeObject(config));
                }
            }catch(Exception ex)
            {
                Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            loginForm = new LoginForm();
            loginForm.FormClosed += ConfiguracionForm_Refresh;
            loginForm.ShowDialog();
        }

        private void btnDesvincular_Click(object sender, EventArgs e)
        {
            config.user = "";
            config.password = "";
            config.id_customer = "";
            // Guardamos
            File.WriteAllText(@"C:\Scanda\configuration.json", JsonConvert.SerializeObject(config));
            // Habilitamos el Boton Add Acount
            btnLogin.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
