using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.Service
{
    public partial class ScandaService : ServiceBase
    {
        public ScandaService(string[] args)
        {
            InitializeComponent();
            // En caso de pasarle argumentos al instalarse
            string eventSourceName = "ScandaService";
            string logName = "ScandaService";
            if (args.Count() > 0)
            {
                eventSourceName = args[0];
            }
            if (args.Count() > 1)
            {
                logName = args[1];
            }

            // Configuramos el logger
            evnLogger = new EventLog();
            if (!EventLog.SourceExists(eventSourceName))
            {
                EventLog.CreateEventSource(eventSourceName, logName);
            }
            evnLogger.Source = eventSourceName;
            evnLogger.Log = logName;
        }

        protected override void OnStart(string[] args)
        {
            evnLogger.WriteEntry("Configuring onStart", EventLogEntryType.Information);
            MessageBox.Show("Service Start papu");
        }

        protected override void OnStop()
        {
            evnLogger.WriteEntry("Configuring onStop", EventLogEntryType.Information);
        }

        protected override void OnPause()
        {
            evnLogger.WriteEntry("Configuring onPause", EventLogEntryType.Information);
        }
    }
}
