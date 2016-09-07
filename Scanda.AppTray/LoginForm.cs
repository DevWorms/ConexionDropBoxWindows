using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MetroFramework.Forms;
using Scanda.AppTray.Models;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

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

        bool DoesServiceExist(string serviceName, string machineName)
        {
            ServiceController[] services = ServiceController.GetServices(machineName);
            var service = services.FirstOrDefault(s => s.ServiceName == serviceName);
            return service != null;
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
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string responseUrl = string.Format("Login_GET?User={0}&Password={1}", username, password);
                    HttpResponseMessage response = await client.GetAsync(responseUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        LoginResponse r = JsonConvert.DeserializeObject<LoginResponse>(resp);
                        if (r.Success == 1)
                        {
                            config.user = username;
                            config.password = password;
                            config.id_customer = r.IdCustomer.ToString();
                            File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                            Close();

                            await sync_accountinfo();
                            await sync_extensions();
                            await sync_lastestUploads();

                            ServiceController sc = null;
                            if (DoesServiceExist("DBProtector Service", "."))
                            {
                                sc = new ServiceController("DBProtector Service");
                                if(!(sc.Status == ServiceControllerStatus.Stopped))
                                    sc.Stop();
                                sc.Start();
                            }
                        }
                        else
                        {
                            lblMessages.Text = "Contraseña/Usuario incorrectos";
                            throw new Exception("Contraseña/Usuario incorrectos");
                        }
                    } else
                    {
                        lblMessages.Text = "Contraseña/Usuario incorrectos";
                        throw new Exception("No se pudo iniciar sesión, revise su conexión de internet");
                    }
                }

            }
            catch(Exception ex)
            {
                lblMessages.Text = ex.Message;
                // Para los logs
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");*/
            }
        }

        private async Task sync_accountinfo()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(string.Format("Account_GET?User={0}&Password={1}", config.user, config.password));
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        Account r = JsonConvert.DeserializeObject<Account>(resp);
                        
                        config.time = r.UploadFrecuency.ToString();
                        config.time_type = "Horas";
                        config.type_storage = r.FileTreatmen.ToString();
                        config.cloud_historical = r.FileHistoricalNumberCloud.ToString();
                        config.file_historical = r.FileHistoricalNumber.ToString();
                        File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                    }
                }
            }
            catch (Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");*/
            }
        }

        private async Task sync_lastestUploads()
        {
            try
            {
                List<Historico> items = new List<Historico>() { };
                var response = await ScandaConector.getLastUploads(config.id_customer);
                if (response != null)
                {
                    foreach (string key in response.Keys)
                    {
                        string item = response[key];
                        var strs = item.Split(' ');
                        items.Add(new Historico() { RFC = key, Fecha = strs[0] + " " + strs[1] + " " + strs[2] });
                    }
                   
                }
            }
            catch (Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");*/
            }
        }

        public async Task sync_extensions()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(string.Format("Extensions_GET?User={0}&Password={1}", config.user, config.password));
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        List<Ext> r = JsonConvert.DeserializeObject<List<Ext>>(resp);

                        config.extensions = r.Select(ent => "." + ent.Extension.ToLower()).ToList();
                        //config.time_type = "Horas";
                        //config.type_storage = r.FileTreatmen.ToString();
                        //config.file_historical = r.FileHistoricalNumber.ToString();
                        File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                    }
                }
            }
            catch (Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");*/
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {


            limpirarVariables();
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

        private async void limpirarVariables()
        {
            try
            {
               
                config.user = "";
                config.password = "";
                config.id_customer = "";
                config.time = "0";
                config.time_type = "Horas";
                config.type_storage = "0";
                config.file_historical = "0";
                config.path = "";
                config.hist_path = "";
                // Guardamos
                File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                // Habilitamos el Boton Add Acount
                
                string appFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
                string settingsFolder = appFolder;
                string logFile = settingsFolder + "\\log.txt";

                File.WriteAllText(logFile, JsonConvert.SerializeObject(config));


            }
            catch (Exception ex) {
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");*/
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
            }
        }


        void LoginForm_Close(object sender, EventArgs e)
        {
            // var form = (LoginForm)sender;
            // form.Close();
            // Close();
            this.Hide();
            configuracionForm = new ConfiguracionForm(flag, configuration_path);
            // configuracionForm.Activate();
            configuracionForm.FormClosed += ConfiguracionForm_Close;
            configuracionForm.ShowDialog();
        }

        void ConfiguracionForm_Close(object sender, EventArgs e)
        {
            var form = (ConfiguracionForm)sender;
            form.Hide();
            frmTray = new FormTray(flag, configuration_path);
            // frmTray.Activate();
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
