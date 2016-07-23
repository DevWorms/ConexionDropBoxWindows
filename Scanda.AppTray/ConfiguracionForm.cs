﻿using MetroFramework.Forms;
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

namespace Scanda.AppTray
{
    
    public partial class ConfiguracionForm : MetroForm
    {
        private LoginForm loginForm;
        private string selectedPath = "";
        private string json;
        private Config config;
        private List<TimeIntervals> intervals;
        private string url;
        public ConfiguracionForm()
        {
            InitializeComponent();
            // leemos el archivo de configuracion
            json = File.ReadAllText(@"C:\Scanda\configuration.json");
            config = JsonConvert.DeserializeObject<Config>(json);
            url = ConfigurationManager.AppSettings["api_url"];
            intervals = new List<TimeIntervals>()
            {
                new TimeIntervals() {Name= "Horas", Value="horas" },
                new TimeIntervals() {Name= "Dias", Value="dias" }
            };
            mcmbTime.DataSource = intervals;
            mcmbTime.DisplayMember = "Name";
            mcmbTime.ValueMember = "Value";
            mcmbTime.SelectedIndex = 0;


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
                mtxt_time.Text = config.time;
                mcmbTime.SelectedValue = config.time_type;
            }
        }

        private async void ConfiguracionForm_Refresh(object sender, EventArgs e)
        {
            try
            {
                // volvemos a abrir el archivo json
                json = File.ReadAllText(@"C:\Scanda\configuration.json");
                config = JsonConvert.DeserializeObject<Config>(json);
                // Revisamos el archivo JSON
                if (!string.IsNullOrWhiteSpace(config.user.Trim()) && !string.IsNullOrWhiteSpace(config.password.Trim()) && !string.IsNullOrWhiteSpace(config.id_customer.Trim()))
                {
                    btnLogin.Enabled = false;
                    btnDesvincular.Enabled = true;
                    await sync_accountinfo();
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
            mtxt_user.Text = "";
            mtxt_totalspace.Text = "";
            mtxt_avalaiblespace.Text = "";
            mtxt_usespace.Text = "";
            // Guardamos
            File.WriteAllText(@"C:\Scanda\configuration.json", JsonConvert.SerializeObject(config));
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
                config.time_type = intervals[mcmbTime.SelectedIndex].Value;
                // Guardamos
                File.WriteAllText(@"C:\Scanda\configuration.json", JsonConvert.SerializeObject(config));
            }
            Close();
        }

        private void mcmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mtxt_time.Text != "0")
            {
                config.time = mtxt_time.Text;
                config.time_type = intervals[mcmbTime.SelectedIndex].Value;
                // Guardamos
                File.WriteAllText(@"C:\Scanda\configuration.json", JsonConvert.SerializeObject(config));
            }
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
                }
            }
        }

        private void ConfiguracionForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(config.id_customer))
            {
                sync_accountinfo();
            }
        }
    }

    public struct Account
    {
        public int Success;
        public string DBoxUser;
        public string DBoxPassword;
        public int StorageLimit;
        public int UsedStorage;
    }
}