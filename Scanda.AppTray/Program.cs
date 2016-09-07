using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Scanda.AppTray
{
    static class Program
    {
        //Startup registry key and value
        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static readonly string StartupValue = "DB Protector";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Obtenemos el Folder donde se aloja nuestra aplicacion
            // string appFolder = new FileInfo(Application.ExecutablePath.ToString()).Directory.FullName;
            string appFolder = @"C:\DBProtector";
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
                // Creating Application Folder
                if (!Directory.Exists(appFolder))
                {
                    DirectoryInfo di = Directory.CreateDirectory(appFolder);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
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
                        SetStartup();
                    }
                    Application.Run(new LoginForm(true, confFile));
                }
                else
                {
                    Application.Run(new FormTray(false, confFile));
                }
            }
        }

        private static void SetStartup()
        {
            // Set the application to run at startup
            RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
            key.SetValue(StartupValue, Application.ExecutablePath.ToString());
        }
    }
}
