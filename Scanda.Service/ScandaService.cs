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
        private string app_settingsPath = @"C:\DBProtector\Settings\";
        #endregion
        public ScandaService(string[] args)
        {
            InitializeComponent();
            // Setup Base API URL
            this.base_url = ConfigurationManager.AppSettings["api_url"];
            // When user pass extra args
            string eventSourceName = "DBProtector Service";
            string logName = "DBProtector Service";
            #region Extra params
            if (args.Count() > 0)
            {
                eventSourceName = args[0];
            }
            if (args.Count() > 1)
            {
                logName = args[1];
            }
            #endregion
            // Logger Setup
            evnLogger = new EventLog();
            if (!EventLog.SourceExists(eventSourceName))
            {
                EventLog.CreateEventSource(eventSourceName, logName);
            }
            evnLogger.Source = eventSourceName;
            evnLogger.Log = logName;

            // Read configuration file
            string json = File.ReadAllText(string.Format("{0}{1}", this.app_settingsPath,this.configuration_file));
            this.config = JsonConvert.DeserializeObject<Config>(json);

            dbProtector = new DBProtector(this.base_url, this.app_settingsPath, this.configuration_file, evnLogger);
        }

        private async void TimerHandler(object sender, EventArgs e)
        {
            await this.dbProtector.StartUpload();
        }

        protected override void OnStart(string[] args)
        {
            evnLogger.WriteEntry("Configuring onStart", EventLogEntryType.Information);
            int xTime = int.Parse(config.time);
            if (xTime != 0)
            {
                int timestamp = xTime * 3600 * 1000; // horas * 60 * 1000
                timerUpload = new Timer();
                timerUpload.Tick += this.TimerHandler;
                timerUpload.Interval = timestamp;
                timerUpload.Start();
            }
            MessageBox.Show("Service Start papu");
        }

        protected override void OnStop()
        {
            evnLogger.WriteEntry("Configuring onStop", EventLogEntryType.Information);
            if (int.Parse(config.time) != 0)
            {
                timerUpload.Stop();
            }
        }

        protected override void OnPause()
        {
            evnLogger.WriteEntry("Configuring onPause", EventLogEntryType.Information);
        }
    }
}
