using MetroFramework.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net.Http.Headers;
using Scanda.AppTray.Models;
using System.Reflection;

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

            metroTabPageAccount.Enabled = false;
            metroTabPageAccount.Visible = false;

            // txtRuta.Text = config.path;
            mtxt_folder.Text = config.path;
            mtxt_userfolder.Text = config.hist_path;
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
                var form = (LoginForm)sender;
                form.Dispose();
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
                    await sync_lastestUploads();
                    btnElegir.Enabled = true;
                    btnUserFolder.Enabled = true;
                }
                this.Show();
            }
            catch(Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                //Logger.sendLog(ex.Message
                //    + "\n" + ex.Source
                //    + "\n" + ex.InnerException
                //    + "\n" + ex.StackTrace
                //    + "\n");
            }
        }

        private async void btnElegir_Click(object sender, EventArgs e)
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
                    // Historicos
                    if (config.type_storage == "1") { 
                        string historicosFolder = selectedPath + "\\historicos";
                        config.hist_path = historicosFolder;
                        mtxt_userfolder.Text = historicosFolder;
                    }
                  
                    // Guardamos la ruta
                    File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                }
            } catch(Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                //Logger.sendLog(ex.Message
                //    + "\n" + ex.Source
                //    + "\n" + ex.InnerException
                //    + "\n" + ex.StackTrace
                //    + "\n");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm = new LoginForm(false, configuration_path);
            loginForm.FormClosed += ConfiguracionForm_Refresh;
            loginForm.ShowDialog();
        }

        private void btnDesvincular_Click(object sender, EventArgs e)
        {
            limpirarVariables();
        }

        private async void limpirarVariables()
        {
            try
            {
                mtxt_user.Text = "";
                mtxt_totalspace.Text = "";
                // mtxt_avalaiblespace.Text = "";
                metroPB_CloudSpace.Value = 0;
                metroPB_CloudSpace.Style = MetroFramework.MetroColorStyle.Green;
                metroPB_CloudSpace.Refresh();
                mtxt_time.Text = "0 Horas";
                mtxt_localHist.Text = "0";
                mtxt_cloudHist.Text = "0";
                mtxt_userfolder.Text = "";
                mtxt_folder.Text = "";
                gpbHistorycal.Visible = true;
                config.user = "";
                config.password = "";
                config.id_customer = "";
                config.time = "0";
                config.time_type = "Horas";
                config.type_storage = "0";
                config.file_historical = "0";
                config.cloud_historical = "0";
                config.path = "";
                config.hist_path = "";
                // Guardamos
                File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                // Habilitamos el Boton Add Acount
                btnLogin.Enabled = true;
                btnElegir.Enabled = false;
                btnUserFolder.Enabled = false;
                dataGridViewHistoricos.DataSource = new List<Historico>() { };
            }
            catch (Exception ex)
            {
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");*/
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            this.Dispose();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (mtxt_time.Text != "0")
            {
                // config.time = mtxt_time.Text;
                // config.time_type = "Horas";
                // config.time_type = intervals[mcmbTime.SelectedIndex].Value;
                // Guardamos
                // File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
            }

            if (!string.IsNullOrEmpty(config.id_customer))
            {

                if (string.IsNullOrEmpty(config.path))
                {
                    MessageBox.Show("No se ha configurado la ruta de respaldos");
                }
                if (config.type_storage == "2")// carpeta externa
                {
                    MessageBox.Show("No se ha configurado la ruta de historicos");
                }
            }
            this.Hide();
            this.Close();
            this.Dispose();

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
                        mtxt_user.Text = config.user; // r.DBoxUser;
                        mtxt_totalspace.Text =  r.UsedStorage.ToString() + " MB" + " de "+ r.StorageLimit + " MB usados";
                        // mtxt_avalaiblespace.Text = (r.StorageLimit - r.UsedStorage).ToString();

                        metroPB_CloudSpace.Value = ((r.UsedStorage * 100) / r.StorageLimit);
                        mtxt_time.Text = r.UploadFrecuency.ToString() + " Horas";
                        mtxt_localHist.Text = "Hasta "+ r.FileHistoricalNumber.ToString()+ (r.FileHistoricalNumber.ToString() == "1" ? " archivo":" archivos");
                        mtxt_cloudHist.Text = "Hasta "+r.FileHistoricalNumberCloud.ToString()+(r.FileHistoricalNumberCloud.ToString() == "1" ? " archivo" : " archivos");
                        if (metroPB_CloudSpace.Value < r.PBYellowPercentage)
                        {
                            metroPB_CloudSpace.Style = MetroFramework.MetroColorStyle.Green;
                        }
                        else if (metroPB_CloudSpace.Value >= r.PBYellowPercentage && metroPB_CloudSpace.Value < r.PBRedPercentage)
                        {
                            metroPB_CloudSpace.Style = MetroFramework.MetroColorStyle.Yellow;
                        }
                        else if (metroPB_CloudSpace.Value >= r.PBRedPercentage)
                        {
                            metroPB_CloudSpace.Style = MetroFramework.MetroColorStyle.Red;
                            await Logger.sendLog("Cuenta llegando al limite de almacenamiento", "W");
                        }

                        config.time = r.UploadFrecuency.ToString();
                        config.time_type = "Horas";
                        config.type_storage = r.FileTreatmen.ToString();
                        config.cloud_historical = r.FileHistoricalNumberCloud.ToString();
                        config.file_historical = r.FileHistoricalNumber.ToString();
                        File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));

                        switch (int.Parse(config.type_storage))
                        {
                            case 1:
                                // mtxt_userfolder.Visible = false;
                                // btnUserFolder.Visible = false;
                                gpbHistorycal.Visible = true;
                                mlbl_localHist.Text = "Historicos local";
                                mtxt_localHist.Visible = true;
                                break;
                            case 2:
                                gpbHistorycal.Visible = true;
                                mlbl_localHist.Text = "Historicos local";
                                mtxt_localHist.Visible = true;
                                break;
                            case 3:

                                gpbHistorycal.Visible = false;
                                mlbl_localHist.Text = "Este perfil no almacena respaldos localmente";
                                mtxt_localHist.Visible = false;
                                // mtxt_userfolder.Visible = false;
                                // btnUserFolder.Visible = false;
                                break;
                        }
                    }
                }
            } catch(Exception ex) {
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
                    dataGridViewHistoricos.DataSource = items;
                    dataGridViewHistoricos.Columns[0].Width = 140;
                    dataGridViewHistoricos.Columns[1].Width = 170;
                }
            }
            catch (Exception ex)
            {
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
            } catch (Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                //Logger.sendLog(ex.Message
                //    + "\n" + ex.Source
                //    + "\n" + ex.InnerException
                //    + "\n" + ex.StackTrace
                //    + "\n");
            }
        }

        private async void ConfiguracionForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(config.id_customer))
                {
                    await sync_accountinfo();
                    await sync_extensions();
                    await sync_lastestUploads();

                   
                    btnElegir.Enabled = true;
                    btnUserFolder.Enabled = true;
                    
                }
                else
                {
                    btnElegir.Enabled = false;
                    btnUserFolder.Enabled = false;
                }
            } catch (Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                //Logger.sendLog(ex.Message
                //    + "\n" + ex.Source
                //    + "\n" + ex.InnerException
                //    + "\n" + ex.StackTrace
                //    + "\n");
            }
        }

        private async void btnUserFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (config.type_storage == "2")
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = fbd.SelectedPath;
                        // txtRuta.Text = selectedPath;
                        mtxt_userfolder.Text = selectedPath;
                        config.hist_path = selectedPath;
                        // Guardamos la ruta
                        File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                    }
                }
            }
            catch (Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                //Logger.sendLog(ex.Message
                //    + "\n" + ex.Source
                //    + "\n" + ex.InnerException
                //    + "\n" + ex.StackTrace
                //    + "\n");
            }
        }

        private void dataGridViewHistoricos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewHistoricos.SelectedCells.Count > 0)
                dataGridViewHistoricos.ClearSelection();
        }

        private void metroPB_CloudSpace_Click(object sender, EventArgs e)
        {

        }

        private void gpbHistorycal_Enter(object sender, EventArgs e)
        {

        }

        private void metroTabPageAccount_Click(object sender, EventArgs e)
        {

        }
    }
}
