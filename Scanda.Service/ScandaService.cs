using Scanda.ClassLibrary;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Timers;

namespace Scanda.Service
{
    public partial class ScandaService : ServiceBase
    {
        #region Private Vars
        /// <summary>
        /// Service Base URL 
        /// </summary>
        private string base_url;
        /// <summary>
        /// Configuration object
        /// </summary>
        private DBProtector dbProtector;
        /// <summary>
        /// Config Object
        /// </summary>
        private Config config;
        /// <summary>
        /// Configuration File
        /// </summary>
        private string configuration_file;
        /// <summary>
        /// Application Directory
        /// </summary>
        /// 
        private System.Timers.Timer aTimer;
        /// <summary>
        /// relog
        /// </summary>
        /// 

        private string app_settingsPath = @"C:\DBProtector\Settings\";
        #endregion
        public ScandaService(string[] args)
        {
            InitializeComponent();

            try
            {
                // // Setup Base API URL
                this.base_url = ConfigurationManager.AppSettings["api_url"];
                // When user pass extra args

                this.configuration_file = "C:\\DBProtector\\Settings\\configuration.json";

                if (!File.Exists(configuration_file))
                {
                    // Si no existe lo creamos
                    // Creamos el archivo de configuracion
                    JObject configSettings = new JObject(
                        new JProperty("path", ""),
                        new JProperty("user_path", ""),
                        new JProperty("hist_path", ""),
                        new JProperty("time_type", "Horas"),
                        new JProperty("time", "0"),
                        new JProperty("id_customer", ""),
                        new JProperty("user", ""),
                        new JProperty("password", ""),
                        new JProperty("token", ""),
                        new JProperty("type_storage", ""),
                        new JProperty("file_historical", ""),
                        new JProperty("cloud_historical", ""),
                        new JProperty("extensions", "")
                    );
                    // escribimos el archivo
                    File.WriteAllText(configuration_file, configSettings.ToString());

                }

                // Read configuration file
                string json = File.ReadAllText(this.configuration_file);
                this.config = JsonConvert.DeserializeObject<Config>(json);

                dbProtector = new DBProtector(this.base_url, this.app_settingsPath, this.configuration_file);
            }
            catch(Exception ex)
            {

            }
        }

        private async void TimerHandler(object sender, ElapsedEventArgs e)
        {
            await this.dbProtector.StartUpload();
        }
        

        protected override void OnStart(string[] args)
        {

            if (config != null && !string.IsNullOrEmpty(config.time))
            {
                try
                {
                    int xTime = int.Parse(config.time);
                    if (xTime != 0)
                    {
                        int timestamp = 1000;//xTime * 3600 * 1000; // horas * 60 * 1000
                                             // Create a timer with a ten second interval.
                        aTimer = new System.Timers.Timer(timestamp);

                        // Hook up the Elapsed event for the timer.
                        aTimer.Elapsed += new ElapsedEventHandler(TimerHandler);

                        // Set the Interval to 2 seconds (2000 milliseconds).
                        aTimer.Interval = timestamp;
                        aTimer.Enabled = true;

                    }
                }
                catch(Exception ex)
                {

                }
            }
           
        }

        protected override void OnStop()
        {
            if (config != null && !string.IsNullOrEmpty(config.time))
            {
                try
                {
                    if (int.Parse(config.time) != 0)
                    {
                        aTimer.Enabled = false;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected override void OnPause()
        {
            aTimer.Enabled = false;
        }
    }
}
