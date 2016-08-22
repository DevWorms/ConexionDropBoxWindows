using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scanda.AppTray
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Obtenemos el Folder donde se aloja nuestra aplicacion
            string appFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
            string settingsFolder = appFolder + "\\Settings";
            string baseFolder = @"C:\Backups";
            string historicosFolder = baseFolder + "\\historicos";
            string respaldadosFolder = baseFolder + "\\respaldados";
            string confFile = settingsFolder + "\\configuration.json";
            // Revisamos si existe el directorio de Settings
            // string startPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            //       + @"\YourPublisher\YourSuite\YourProduct";
            using (Mutex mutex = new Mutex(false, @"Global\" + "8a811882-a3ad-402e-b2f0-d8963a938d18"))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Ya hay una instancia de DB Protector ejecutandose");
                    return;
                }

                GC.Collect();
                if (!Directory.Exists(settingsFolder))
                {
                    // No existe lo creamos el Directorio
                    Directory.CreateDirectory(settingsFolder);
                    Directory.CreateDirectory(baseFolder);
                    Directory.CreateDirectory(respaldadosFolder);
                    Directory.CreateDirectory(historicosFolder);

                    if (!File.Exists(confFile))
                    {
                        // Si no existe lo creamos
                        // Creamos el archivo de configuracion
                        JObject configSettings = new JObject(
                            new JProperty("path", baseFolder),
                            new JProperty("user_path", respaldadosFolder),
                            new JProperty("hist_path", historicosFolder),
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
                        File.WriteAllText(confFile, configSettings.ToString());
                    }
                    Application.Run(new LoginForm(true, confFile));
                }
                else
                {
                    Application.Run(new FormTray(false, confFile));
                }
            }
        }

        
    }
}
