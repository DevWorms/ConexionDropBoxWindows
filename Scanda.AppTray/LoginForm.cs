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
        private Config config;
        private string url;
        public LoginForm()
        {
            InitializeComponent();
            json = File.ReadAllText(@"C:\Scanda\configuration.json");
            config = JsonConvert.DeserializeObject<Config>(json);
            url = ConfigurationManager.AppSettings["api_url"];
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos los datos de los inputs
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                // Validamos que no esten vacios los campos
                if (string.IsNullOrWhiteSpace(username))
                {
                    lblMessages.Text = "El campo usuario es obligatorio";
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
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
                        File.WriteAllText(@"C:\Scanda\configuration.json", JsonConvert.SerializeObject(config));
                        Close();
                        // Product product = await response.Content.ReadAsAsync > Product > ();
                        // Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
                    }
                }

            }
            catch(Exception ex)
            {
                // Para los logs
            }
            // Revisamos si existe el directorio para almacenar nuestros archivos.

            //if (!Directory.Exists("C:\\Scanda") )
            //{
            //    // Creamos el Directorio
            //    Directory.CreateDirectory("C:\\Scanda");
            //}
            //// Revisamos si existe el archivo credentials.txt
            //if (!File.Exists("C:\\Scanda\\credentials.txt"))
            //{
            //    // Si no existe lo creamos
            //    File.Create("C:\\Scanda\\credentials.txt", 2048, FileOptions.Asynchronous);
            //}
            //// abrimos el archivo
            //File.WriteAllText("C:\\Scanda\\credentials.txt", string.Format("{0}|{1}", username, password));
            //// Servicio de Chemas
            //this.Close();
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
