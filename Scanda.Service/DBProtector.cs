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
        private string config_file;
        private Config config;
        
        public DBProtector(string base_url, string appDir, string config_file = "configuration.json")
        {
            this.base_url = base_url;
            this.appDir = appDir;
            this.config_file = config_file;
            
            this.Init();
        }
        /// <summary>
        /// Read & Instance the config object
        /// </summary>
        private void Init()
        {
            // Read configuration file
            string json = File.ReadAllText(this.config_file);
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

        public async Task StartUpload()
        {
            await Logger.sendLog(string.Format("servicio ejecutandose {0}", DateTime.Now), "W");
            if (config!= null && !string.IsNullOrEmpty(config.path) && !string.IsNullOrEmpty(config.id_customer))
            {
                #region Validacion de Directorios
                // Revisamos si existe el directorio de respaldos
                if (!Directory.Exists(config.path))
                {
                    Directory.CreateDirectory(config.path);
                }
                // Revisamos si existe el directorio de historicos
                if (!Directory.Exists(config.hist_path))
                {
                    Directory.CreateDirectory(config.hist_path);
                }
                #endregion

                #region Subida de archivos
                // Obtenemos listado de archivos del directorio

                string[] fileEntries = Directory.GetFiles(config.path);
                foreach (string file in fileEntries)
                {
                    Status temp2 = new Status(base_url, null, null, config.user, config.password);
                    FileInfo info = new FileInfo(file);
                    var x = await ScandaConector.uploadFile(file, config.id_customer, temp2, config.extensions);
                    if (!x)
                    {
                        await Logger.sendLog(string.Format("Error al sincronizar {0}", info.Name), "T");
                    }
                    else
                    {
                        await Logger.sendLog(string.Format("Archivo subido correctamente: {0}", info.Name), "T");
                    }
                }
                #endregion
                // Realizamos la limpieza en Cloud
                await ScandaConector.deleteHistory(config.id_customer, int.Parse(config.cloud_historical));

         

                #region Realizamos el movimiento de los archivos que se suben a la carpeta historicos
                List<FileInfo> histFileEntries = new DirectoryInfo(config.hist_path).GetFiles().OrderBy(f => f.LastWriteTime).ToList();
                // verificamos el limite
                bool canTransfer = false;
                while (!canTransfer)
                {
                    if (histFileEntries.Count() < int.Parse(config.file_historical))
                    {
                        if (histFileEntries.Count() == 0)
                        {
                            canTransfer = true;
                        }
                        else if (fileEntries.Length <= histFileEntries.Count() || fileEntries.Length < int.Parse(config.file_historical))
                        {
                            canTransfer = true;
                        }
                        else
                        {
                            FileInfo item = histFileEntries.FirstOrDefault();
                            if (item != null)
                                histFileEntries.Remove(item);
                        }
                    }
                    else
                    {
                        FileInfo item = histFileEntries.FirstOrDefault();
                        if (item != null)
                            File.Delete(config.hist_path + "\\" + item.Name);
                        histFileEntries.Remove(item);
                    }
                }

                // Comenzamos a mover los archivos 
                List<FileInfo> fileEntries2 = new DirectoryInfo(config.path).GetFiles().OrderBy(f => f.LastWriteTime).ToList();
                foreach (FileInfo file in fileEntries2)
                {
                    if (isValidFileName(file.Name))
                    {
                        //cuando el filetreatment es 3 se borra localmente, en el caso 1 o 2 se mueve a una carpeta de respaldos 
                        if (config.type_storage == "1" || config.type_storage == "2")
                        {
                            // Se copia a Historicos
                            File.Copy(config.path + "\\" + file.Name, config.hist_path + "\\" + file.Name);
                        }
                        // Se copia a Respaldados
                        File.Delete(config.path + "\\" + file.Name);
                    }
                }
                #endregion

                await SyncUpdateAccount();
            }
        }

        private async Task SyncUpdateAccount()
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
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
            }
        }
    }
}
