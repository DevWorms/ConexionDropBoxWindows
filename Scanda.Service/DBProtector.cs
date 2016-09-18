using Newtonsoft.Json;
using Scanda.ClassLibrary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Scanda.Service
{
    public class DBProtector
    {
        static string ARCHIVO = "([A-Za-z]{3,4}[0-9]{6}[A-Za-z0-9]{3})([0-9]{14})";
        private string base_url;
        private string appDir;
        private Config config;
        private string configuration_path;
        private string json;

        public DBProtector(string base_url, string appDir, string config_file = "configuration.json")
        {
            this.base_url = base_url;
            this.appDir = appDir;
            this.configuration_path = config_file;
            
            this.Init();
        }
        /// <summary>
        /// Read & Instance the config object
        /// </summary>
        private void Init()
        {
            // Read configuration file
            string json = File.ReadAllText(this.configuration_path);
            this.config = JsonConvert.DeserializeObject<Config>(json);
        }
        /// <summary>
        /// Check if file is valid
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool isValidFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return false;
            try
            {
                return Regex.IsMatch(fileName, ARCHIVO);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static bool isValidExt(string ext, List<string> extensions)
        {
            if (extensions == null)
                return true;

            bool valido = false;

            foreach (string extension in extensions)
            {
                if (ext.ToLower().Contains(extension.ToLower()))
                {
                    valido = true;
                    break;
                }
            }

            return valido;
        }

        public async Task StartUpload()
        {
           
            if (!string.IsNullOrEmpty(config.id_customer) && !string.IsNullOrEmpty(config.path)  )
            {
                if (!((config.type_storage != "3") && string.IsNullOrEmpty(config.hist_path)))
                { //esta validacion es para evitar que se traten de subir respaldos sin una ruta de respaldos, 3 indica que no se deben de considerar respaldos en ninguna carpeta local
                    try
                    {
                        json = File.ReadAllText(configuration_path);
                        config = JsonConvert.DeserializeObject<Config>(json);
                        string base_url = string.Empty;
                        if (!string.IsNullOrEmpty(config.path))
                        {
                            base_url = ConfigurationManager.AppSettings["api_url"];
                        }

                        #region Validacion de Directorios
                        // Revisamos si existe el directorio de respaldos
                        if (!string.IsNullOrEmpty(config.path) && !Directory.Exists(config.path))
                        {
                            Directory.CreateDirectory(config.path);
                        }
                        // Revisamos si existe el directorio de historicos, si esta en configuracion 3 significa que el archivo localmente se tiene que borrar, por tanto no es necesario crear una carpeta
                        if (!string.IsNullOrEmpty(config.hist_path) && !Directory.Exists(config.hist_path) && config.type_storage != "3")
                        {
                            Directory.CreateDirectory(config.hist_path);
                        }

                        #endregion
                        // Obtenemos listado de archivos del directorio
                        if (!string.IsNullOrEmpty(config.path))
                        {
                            //string[] fileEntries = Directory.GetFiles(config.path);
                            List<string> fileEntries = Directory.GetFiles(config.path).Where(ent => isValidFileName(ent) && isValidExt(ent, config.extensions)).ToList();
                            //if (fileEntries != null && fileEntries.Length>0)
                            if (fileEntries != null && fileEntries.Count > 0)
                            {
                                foreach (string file in fileEntries)
                                {
                                    Status temp2 = new Status(base_url, null, null, config.user, config.password);

                                    FileInfo info = new FileInfo(file);
                                    var x = await ScandaConector.uploadFile(file, config.id_customer, temp2, config.extensions);
                                    if (!x)
                                    {
                                        await Logger.sendLog(string.Format("{0} | {1} | {2}", "Scanda.Service.DBProtector.StartUpload ", "Error al sincronizar: " + info.Name, "servicio de windows"), "E");

                                    }
                                    else
                                    {
                                        await Logger.sendLog(string.Format("{0} | {1} | {2}", "Scanda.Service.DBProtector.StartUpload ", "Archivo subido correctamente: ", info.Name), "T");
                                    }
                                }
                            }
                            else
                            {
                                await Logger.sendLog(string.Format("{0} | {1} | {2}", "Scanda.Service.DBProtector.StartUpload ", "No hay respaldos pendientes por sincronizar",""), "W");
                            }


                            // Realizamos la limpieza en Cloud
                            await ScandaConector.deleteHistory(config.id_customer, int.Parse(config.cloud_historical),config);
                            #region Realizamos el movimiento de los archivos que se suben a la carpeta historicos
                            if (!string.IsNullOrEmpty(config.type_storage) && config.type_storage != "3")
                            {
                                // Comenzamos a mover los archivos 
                                List<FileInfo> fileEntries2 = new DirectoryInfo(config.path).GetFiles().Where(ent => isValidFileName(ent.Name) && isValidExt(ent.Name, config.extensions)).OrderBy(f => f.LastWriteTime).ToList();
                                foreach (FileInfo file in fileEntries2)
                                {
                                    if (isValidFileName(file.Name))
                                    {
                                        //cuando vale 1 y 2 se mueve a una carpeta el respaldo, cuanfdo vale 3 se borra localmente
                                        if (config.type_storage == "1" || config.type_storage == "2")
                                        {
                                            // Se copia a Historicos
                                            if (File.Exists(config.hist_path + "\\" + file.Name))
                                                File.Delete(config.hist_path + "\\" + file.Name);
                                            File.Copy(config.path + "\\" + file.Name, config.hist_path + "\\" + file.Name);
                                        }
                                        File.Delete(config.path + "\\" + file.Name);
                                    }
                                }

                                List<FileInfo> histFileEntries = new DirectoryInfo(config.hist_path).GetFiles().OrderBy(f => f.LastWriteTime).ToList();
                                // verificamos el limite

                                //Borramos en la nube
                                //ScandaConector.deleteHistory(config.id_customer, config.file_historical);
                                //Borramos local
                                bool canTransfer = false;
                                while (!canTransfer)
                                {
                                    if (histFileEntries.Count() <= int.Parse(config.file_historical))
                                    {

                                        canTransfer = true;
                                    }
                                    else
                                    {
                                        FileInfo item = histFileEntries.FirstOrDefault();
                                        if (item != null)
                                            File.Delete(config.hist_path + "\\" + item.Name);
                                        histFileEntries.Remove(item);

                                    }
                                }

                            }
                            else if (config.type_storage == "3")
                            {
                                // Comenzamos a mover los archivos 
                                List<FileInfo> fileEntries2 = new DirectoryInfo(config.path).GetFiles().OrderBy(f => f.LastWriteTime).ToList();
                                foreach (FileInfo file in fileEntries2)
                                {
                                    if (isValidFileName(file.Name))
                                    {
                                        // Se borra el archivo localmente porque la configurcion es 3
                                        File.Delete(config.path + "\\" + file.Name);
                                    }
                                }
                            }

                            #endregion
                            await sync_updateAccount();
                        }


                        //Se borran los archivos zip de la carpeta dbprotector

                        List<string> eliminables = Directory.GetFiles("C:\\DBProtector\\").Where(ent => { return ent.EndsWith(".zip"); }).ToList();

                        if (eliminables != null)
                        {
                            foreach (string file in eliminables)
                            {

                                File.Delete(file); //Se borra el zip creado
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // Termino de hacer todos los respaldos
                        await Logger.sendLog(string.Format("{0} | {1} | {2}", "Scanda.Service.DBProtector.StartUpload", ex.Message, ex.StackTrace), "E");
                        Console.WriteLine(ex);
                        /*Logger.sendLog(ex.Message
                        + "\n" + ex.Source
                        + "\n" + ex.StackTrace
                        + "\n" + ex.StackTrace
                        + "\n", "E");*/
                    }
                }
            }
        }

        private async Task sync_updateAccount()
        {
            try
            {
                // Obtenemos los datos de dropbox
                var x = await ScandaConector.getUsedSpace(config.id_customer);
                string url = ConfigurationManager.AppSettings["api_url"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(string.Format("CustomerStorage_SET?UsedStorage={2}&User={0}&Password={1}", config.user, config.password, x));
                    if (response.IsSuccessStatusCode)
                    {
                        /*var resp = await response.Content.ReadAsStringAsync();
                        Account r = JsonConvert.DeserializeObject<Account>(resp);
                        config.time = r.UploadFrecuency.ToString();
                        config.time_type = "Horas";
                        config.type_storage = r.FileTreatmen.ToString();
                        config.file_historical = r.FileHistoricalNumber.ToString();
                        File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));*/
                    }
                }
            }
            catch (Exception ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}","Scanda.Service.DBProtector.sync_updateAccount", ex.Message, ex.StackTrace), "E");
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.StackTrace
                    + "\n" + ex.StackTrace
                    + "\n", "E");*/
            }
        }
        private async Task sync_accountinfo()
        {
            string url = ConfigurationManager.AppSettings["api_url"];
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
                    config.time = r.UploadFrecuency.ToString();
                    config.time_type = "Horas";
                    config.type_storage = r.FileTreatmen.ToString();
                    config.file_historical = r.FileHistoricalNumber.ToString();
                    config.cloud_historical = r.FileHistoricalNumberCloud.ToString();
                    File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                }
            }
        }

    }


}
