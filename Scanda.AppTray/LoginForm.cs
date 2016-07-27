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
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MetroFramework.Forms;
using MetroFramework.Controls;

namespace Scanda.AppTray
{
    public struct LoginResponse
    {
        public int Success;
        public int Status;
        public int IdCustomer;
    }
    public partial class LoginForm : MetroForm
    {
        private string json;
        private string url;
        private bool flag;
        private string configuration_path;
        private ConfiguracionForm configuracionForm;
        private FormTray frmTray;
        private Config config;
        public LoginForm(bool isNuevaInstancia, string configPath)
        {
            InitializeComponent();
            json = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(json);
            url = ConfigurationManager.AppSettings["api_url"];
            flag = isNuevaInstancia;
            configuration_path = configPath;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos los datos de los inputs
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Validamos que no esten vacios los campos
                if (string.IsNullOrEmpty(username) || username == "alguien@example.com")
                {
                    lblMessages.Text = "El campo usuario es obligatorio";
                    return;
                }

                if (string.IsNullOrEmpty(password) || password == "contraseña")
                {
                    lblMessages.Text = "El campo password es obligatorio";
                    return;
                }

                using (var client = new HttpClient())
                {
                    // TODO - Send HTTP requests
                    // var query = HttpUtility.ParseQueryString(string.Empty);
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(string.Format("Login_GET?User={0}&Password={0}", username, password));
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        LoginResponse r = JsonConvert.DeserializeObject<LoginResponse>(resp);
                        config.user = username;
                        config.password = password;
                        config.id_customer = r.IdCustomer.ToString();
                        File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                        Close();
                        configuracionForm = new ConfiguracionForm(flag, configuration_path);
                        configuracionForm.FormClosed += ConfiguracionForm_Close;
                        configuracionForm.ShowDialog();
                    } else
                    {
                        lblMessages.Text = "Contraseña/Usuario incorrectos";
                    }
                }

            }
            catch(Exception ex)
            {
                // Para los logs
                Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "alguien@example.com";
            txtUsername.ForeColor = Color.Gray;
            txtUsername.GotFocus += RemoveText;
            txtUsername.LostFocus += AddText;
            txtPassword.Text = "contraseña";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.GotFocus += RemoveText;
            txtPassword.LostFocus += AddText;

            if (flag)
            {
                // abrimos la ventana de configuraciones 
                this.FormClosed += LoginForm_Close;
            }
        }

        void LoginForm_Close(object sender, EventArgs e)
        {
            // var form = (LoginForm)sender;
            // form.Close();
            // Close();
        }

        void ConfiguracionForm_Close(object sender, EventArgs e)
        {
            var form = (ConfiguracionForm)sender;
            form.Close();
            frmTray = new FormTray(flag, configuration_path);
            frmTray.ShowDialog();
        }

        void RemoveText(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            txt.ForeColor = Color.Black;
            txt.Text = "";
        }

        void AddText(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.ForeColor = Color.Gray;
                if (txt.Name == "txtUsername")
                {
                    txt.Text = "alguien@example.com";
                }
                if (txt.Name == "txtPassword")
                {
                    txt.Text = "contraseña";
                }
            }
                
        }
    }
}
