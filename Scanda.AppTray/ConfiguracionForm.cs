using MetroFramework.Forms;
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
using MetroFramework.Controls;
using System.Windows.Forms;
using System.Configuration;
using System.Net.Http.Headers;
using Scanda.AppTray.Models;

namespace Scanda.AppTray
{
    
    public partial class ConfiguracionForm : MetroForm
    {
        private LoginForm loginForm;
        private string selectedPath = "";
        private string json;
        private Config config;
        //private List<TimeIntervals> intervals;
        private string url;
        private bool flag;
        private string configuration_path;
        public ConfiguracionForm(bool isNuevaInstancia, string configPath)
        {
            InitializeComponent();
            // leemos el archivo de configuracion
            json = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(json);
            url = ConfigurationManager.AppSettings["api_url"];
            flag = isNuevaInstancia;
            configuration_path = configPath;
            //intervals = new List<TimeIntervals>()
            //{
            //    new TimeIntervals() {Name= "Horas", Value="horas" },
            //    new TimeIntervals() {Name= "Dias", Value="dias" }
            //};
            //mcmbTime.DataSource = intervals;
            //mcmbTime.DisplayMember = "Name";
            //mcmbTime.ValueMember = "Value";
            //mcmbTime.SelectedIndex = 0;


            metroTabPageAccount.Enabled = false;
            metroTabPageAccount.Visible = false;

            // txtRuta.Text = config.path;
            mtxt_folder.Text = config.path;

            if (!string.IsNullOrWhiteSpace(config.id_customer))
            {
                // Hay un usuario logueado
                btnLogin.Enabled = false;
                btnDesvincular.Enabled = true;

                // Guardamos
                if (!string.IsNullOrEmpty(config.time))
                {
                    mtxt_time.Text = config.time;
                    //mcmbTime.SelectedValue = config.time_type;
                }
            }
        }

        private async void ConfiguracionForm_Refresh(object sender, EventArgs e)
        {
            try
            {
                // volvemos a abrir el archivo json
                json = File.ReadAllText(configuration_path);
                config = JsonConvert.DeserializeObject<Config>(json);
                // Revisamos el archivo JSON
                if (!string.IsNullOrWhiteSpace(config.user.Trim()) && !string.IsNullOrWhiteSpace(config.password.Trim()) && !string.IsNullOrWhiteSpace(config.id_customer.Trim()))
                {
                    btnLogin.Enabled = false;
                    btnDesvincular.Enabled = true;
                    await sync_accountinfo();
                    await sync_extensions();
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
                    // txtRuta.Text = selectedPath;
                    mtxt_folder.Text = selectedPath;
                    config.path = selectedPath;
                    // Guardamos la ruta
                    File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
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
            loginForm = new LoginForm(false, configuration_path);
            loginForm.FormClosed += ConfiguracionForm_Refresh;
            loginForm.ShowDialog();
        }

        private void btnDesvincular_Click(object sender, EventArgs e)
        {
            config.user = "";
            config.password = "";
            config.id_customer = "";
            mtxt_user.Text = "";
            mtxt_totalspace.Text = "";
            mtxt_avalaiblespace.Text = "";
            mtxt_usespace.Text = "";
            // Guardamos
            File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
            // Habilitamos el Boton Add Acount
            btnLogin.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (mtxt_time.Text != "0")
            {
                config.time = mtxt_time.Text;
                config.time_type = "Horas";
                // config.time_type = intervals[mcmbTime.SelectedIndex].Value;
                // Guardamos
                File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
            }
            Close();
        }

        private void mcmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async Task sync_accountinfo()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(string.Format("Account_GET?User={0}&Password={0}", config.user, config.password));
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    Account r = JsonConvert.DeserializeObject<Account>(resp);
                    mtxt_user.Text = r.DBoxUser;
                    mtxt_totalspace.Text = r.StorageLimit.ToString();
                    mtxt_avalaiblespace.Text = (r.StorageLimit - r.UsedStorage).ToString();
                    mtxt_usespace.Text = r.UsedStorage.ToString();
                    mtxt_time.Text = r.UploadFrecuency.ToString();

                    config.time = r.UploadFrecuency.ToString();
                    config.time_type = "Horas";
                    config.type_storage = r.FileTreatmen.ToString();
                    config.file_historical = r.FileHistoricalNumber.ToString();
                    File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                }
            }
        }

        public async Task sync_extensions()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(string.Format("Extensions_GET?User={0}&Password={0}", config.user, config.password));
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    List<Ext> r = JsonConvert.DeserializeObject<List<Ext>>(resp);

                    config.extensions = r.Select(ent => ent.Extension).ToList();
                    //config.time_type = "Horas";
                    //config.type_storage = r.FileTreatmen.ToString();
                    //config.file_historical = r.FileHistoricalNumber.ToString();
                    File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                }
            }
        }

        private async void ConfiguracionForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(config.id_customer))
            {
                await sync_accountinfo();
                await sync_extensions();

                switch(int.Parse(config.type_storage))
                {
                    case 1:
                        mtxt_userfolder.Visible = false;
                        btnUserFolder.Visible = false;
                        break;
                    case 2:
                        break;
                    case 3:
                        mtxt_userfolder.Visible = false;
                        btnUserFolder.Visible = false;
                        break;
                }
            }
        }

        private void btnUserFolder_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = fbd.SelectedPath;
                    // txtRuta.Text = selectedPath;
                    mtxt_userfolder.Text = selectedPath;
                    config.user_path = selectedPath;
                    // Guardamos la ruta
                    File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                }
            }
            catch (Exception ex)
            {
                Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");
            }
        }
    }
}
