using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            string respaldadosFolder = appFolder + "\\respaldados";
            string confFile = settingsFolder + "\\configuration.json";
            // Revisamos si existe el directorio de Settings
            string startPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            //       + @"\YourPublisher\YourSuite\YourProduct";

            if (!Directory.Exists(settingsFolder))
            {
                // No existe lo creamos el Directorio
                Directory.CreateDirectory(settingsFolder);
                Directory.CreateDirectory(respaldadosFolder);

                if (!File.Exists(confFile))
                {
                    // Si no existe lo creamos
                    // Creamos el archivo de configuracion
                    JObject configSettings = new JObject(
                        new JProperty("path", ""),
                        new JProperty("user_path", respaldadosFolder),
                        new JProperty("time_type", ""),
                        new JProperty("time", ""),
                        new JProperty("id_customer", ""),
                        new JProperty("user", ""),
                        new JProperty("password", ""),
                        new JProperty("token", ""),
                        new JProperty("type_storage", ""),
                        new JProperty("file_historical", ""),
                        new JProperty("extensions", "")
                    );
                    // escribimos el archivo
                    File.WriteAllText(confFile, configSettings.ToString());
                }
                Application.Run(new LoginForm(true, confFile));
            } else
            {
                Application.Run(new FormTray(false, confFile));
            }
            
        }

        
    }
}
