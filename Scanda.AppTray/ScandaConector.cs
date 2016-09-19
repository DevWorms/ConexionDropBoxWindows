using Dropbox.Api;
using Dropbox.Api.Files;
using Ionic.Zip;
using Scanda.AppTray.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json;

namespace Scanda.AppTray
{
    public class ScandaConector
    {
        static DropboxClient client;
        static DropboxClientConfig clientConf;
        //static string APITOKEN = "f-taP7WG2wAAAAAAAAAATT2oK8oc3cov6Bfk5dQlxjFubRX7cWBoJS5PyErF8HmQ";
        static string APITOKEN = "f-taP7WG2wAAAAAAAAAATT2oK8oc3cov6Bfk5dQlxjFubRX7cWBoJS5PyErF8HmQ";

        static int B_TO_MB = 1024 * 1024;
        static int CHUNK_SIZE = 5 * B_TO_MB;

        static string STATUSFILE = "status.json";
        //static string BACKEDFOLDER = "Backed";

        static WriteMode OVERWRITE = WriteMode.Overwrite.Instance;

        //static string REGEXP = "([A-Zz-z]{4}\\d{6})(---|\\w{3})?(\\d{14}).(\\w{3})";
        static string ARCHIVO = "([A-Za-z]{3,4}[0-9]{6}[A-Za-z0-9]{3})([0-9]{14})";
        //static string RFCregexp = "([A-Zz-z]{4}\\d{6}(---|\\w{3})?)";
        static string RFCregexp = "([A-Za-z]{3,4}[0-9]{6}[A-Za-z0-9]{3})";

        public static async Task<List<string>> getFiles(string usrID, string year, string month)
        {
            return await getFiles(usrID + "/" + year + "/" + month);
        }
        public static async Task<List<string>> getMonths(string usrID, string year)
        {
            return await getFolders(usrID + "/" + year);
        }
        public static async Task<List<string>> getYears(string usrID)
        {
            return await getFolders(usrID);
        }
        public static async Task<List<string>> getFolders(string path, bool recursive = false)
        {
            try
            {
                List<string> lista = new List<string>();
                var x = await listFiles(path, recursive);
                ListFolderResult res = x;

                foreach (Metadata m in res.Entries)
                {
                    if (m.IsFolder)
                    {
                        lista.Add(m.Name);
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                return new List<string>(); //Lista vacia
            }
        }
        public static async Task<List<string>> getFiles(string path, bool recursive = false)
        {
            try
            {
                List<string> lista = new List<string>();
                List<FileMetadata> preLista = new List<FileMetadata>();
                var x = await listFiles(path, recursive);
                ListFolderResult res = x;
                foreach (Metadata m in res.Entries)
                {
                    if (m.IsFile)
                    {
                        preLista.Add(m.AsFile);
                        //lista.Add(m.Name);
                    }
                }

                //ordenamos
                preLista.Sort((a, b) => a.ServerModified.CompareTo(b.ServerModified) * -1); // El menos uno invierte el orden natural

                foreach (FileMetadata fm in preLista)
                {
                    int pos = fm.Name.LastIndexOf(".");
                    string nom = fm.Name.Substring(0, pos);
                    lista.Add(nom);
                    //lista.Add(fm.Name);
                }

                return lista;
            }
            catch (Exception)
            {
                return new List<string>(); //Lista vacia
            }

        }

        public static async Task<Dictionary<string, string>> getLastUploads(string userID)
        {
            try
            {
                Regex exp = new Regex(RFCregexp);

                Dictionary<string, Metadata> metadatos = new Dictionary<string, Metadata>();

                Dictionary<string, string> ret = new Dictionary<string, string>();

                ListFolderResult res = await listFiles(userID, true);

                foreach (Metadata m in res.Entries)
                {
                    if (m.IsFile && exp.IsMatch(m.AsFile.Name))
                    {
                        //Obtenemos el RFC
                        string[] arr = exp.Split(m.AsFile.Name);

                        if (!metadatos.ContainsKey(arr[1]))//No existe
                        {
                            metadatos.Add(arr[1], m);
                        }
                        else
                        {
                            Metadata pre = metadatos[arr[1]];
                            //La fecha de M es mas reciente
                            if (pre.AsFile.ServerModified.CompareTo(m.AsFile.ServerModified) < 0)
                            {
                                metadatos.Remove(arr[1]);
                                metadatos.Add(arr[1], m);
                            }
                        }
                    }
                }

                foreach (string client in metadatos.Keys)
                {
                    FileMetadata fileData = metadatos[client].AsFile;
                    ret.Add(client, fileData.ServerModified.ToString() + " " + fileData.Name);
                }

                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //si cantidad -1 no hay limite
        public static async Task deleteHistory(string userID, int cant, Config config)
        {
            if (cant == -1)
            {
                return;
            }
            else
            {
                //obtengo todos los archivos 
                ListFolderResult res = await listFiles(userID, true);
                List<Metadata> todo = res.Entries as List<Metadata>;
                List<FileMetadata> archivos = todo.Where(x => x.IsFile).Select(y => y.AsFile).ToList();
                archivos.Sort((a, b) => a.ServerModified.CompareTo(b.ServerModified)); //Ordenamos , mas viejos primero

                if (archivos.Count < cant)//Aun tiene lugar en el historico
                {
                    return;
                }
                else
                {
                    int dif = archivos.Count - cant; //Cuantos debo de eliminar
                    foreach (FileMetadata fm in archivos.Take(dif))
                    {
                        //Necesitamos crear un cliente
                        try
                        {
                            
                            await client.Files.DeleteAsync(fm.PathDisplay);
                            var espacio_archivo_borrar=(long) fm.Size;
                            var espacio_usado = await ScandaConector.getUsedSpace(config.id_customer)* B_TO_MB;
                            var espacio_nuevo = Math.Abs(espacio_archivo_borrar - espacio_usado);
                            double espacio = (double)espacio_nuevo / (double)1024 / (double)1024;

                            int espacio_usado_Final = (int)Math.Ceiling(espacio);
                            string url = ConfigurationManager.AppSettings["api_url"];
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(url);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                string llamada = string.Format("CustomerStorage_SET?UsedStorage={2}&User={0}&Password={1}", config.user, config.password, espacio_usado_Final);
                                HttpResponseMessage response = await client.GetAsync(llamada);
                                if (response.IsSuccessStatusCode)
                                {

                                }


                            }
                        }
                        catch (BadInputException ex)
                        {
                            await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.deleteHistory "), "E");
                            Console.WriteLine("Error de Token");
                            Console.WriteLine(ex.Message);
                        }
                        catch(Exception ex){
                            await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.deleteHistory "), "E");
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }



        public static async Task<bool> uploadFile(string archivo, string usrId, Status status, List<string> extensions = null, Config config = null, string config_path = null)
        {
            
            status.upload.file = archivo;
            status.upload.status = 1;
            try
            {
                FileInfo info = new FileInfo(archivo);
                //validamos extensiones
                if (!isValidExt(info.Extension, extensions))
                    return false; // No es una extension valida


                double size = (double) info.Length / (double)B_TO_MB;

                //Validamos el tamanio
                bool esvalido = await isValidSize(Math.Ceiling(size), config);
                if (! esvalido )
                    return false;


                string name = info.Name;
                status.upload.file = info.Name;
                //NOmbre del archivo
                if (!isValidFileName(name))
                    return false;


                //Generamos la ruta
                DateTime date = DateTime.Today;
                string year;
                string month;

                year = date.Year + "";
                if (date.Month < 10)
                    month = "0" + date.Month;
                else
                    month = "" + date.Month;

                //Zipeamos l archivo
                await Logger.sendLog(string.Format("{0} | {1} | {2}", archivo, "Cifrando archivo...", "Scanda.AppTray.ScandaConector.uploadFile"), "T");
                string zip = cifrar(archivo, usrId);
                await Logger.sendLog(string.Format("{0} | {1} | {2}", archivo, "Cifrando completo...", "Scanda.AppTray.ScandaConector.uploadFile"), "T");
                string ruta = usrId + "/" + year + "/" + month;
                status.upload.status = 1;
                await Logger.sendLog(string.Format("{0} | {1} | {2}", archivo, "Comenzando subida...", "Scanda.AppTray.ScandaConector.uploadFile"), "T");
                var res = await uploadZipFile(zip, ruta, status);
                await Logger.sendLog(string.Format("{0} | {1} | {2}", archivo, "subida terminada...", "Scanda.AppTray.ScandaConector.uploadFile"), "T");
                await sync_accountinfo(config, config_path);


                // Realizamos la limpieza en Cloud
                await Logger.sendLog(string.Format("{0} | {1} | {2}", "", "Comienza limpieza en la nube", "Scanda.AppTray.FormTray.syncNowToolStripMenuItem_Click"), "T");

                await ScandaConector.deleteHistory(config.id_customer, int.Parse(config.cloud_historical), config);
                await Logger.sendLog(string.Format("{0} | {1} | {2}", "", "Termina limpieza en la nube", "Scanda.AppTray.Scanda.AppTray.ScandaConector.uploadFile"), "T");

                await Logger.sendLog(string.Format("{0} | {1} | {2}", "Eliminando archivos temporales", "Comienza limpieza de archivos temporales local", "Scanda.AppTray.Scanda.AppTray.ScandaConector.uploadFile"), "T");

                #region Realizamos el movimiento de los archivos que se suben a la carpeta historicos
                if (!string.IsNullOrEmpty(config.type_storage) && config.type_storage != "3")
                {
                    // Comenzamos a mover los archivos 
                  //  List<FileInfo> fileEntries2 = new DirectoryInfo(config.path).GetFiles().Where(ent => isValidFileName(ent.Name) && isValidExt(ent.Name, config.extensions)).OrderBy(f => f.LastWriteTime).ToList();
                   // foreach (FileInfo file in fileEntries2)
                    //{
                        if (isValidFileName(info.Name))
                        {
                            //cuando vale 1 y 2 se mueve a una carpeta el respaldo, cuanfdo vale 3 se borra localmente
                            if (config.type_storage == "1" || config.type_storage == "2")
                            {
                                // Se copia a Historicos
                                if (File.Exists(config.hist_path + "\\" + info.Name))
                                    File.Delete(config.hist_path + "\\" + info.Name);
                                File.Copy(config.path + "\\" + info.Name, config.hist_path + "\\" + info.Name);
                            }
                            File.Delete(config.path + "\\" + info.Name);
                        }
                    //}

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
                  //  List<FileInfo> fileEntries2 = new DirectoryInfo(config.path).GetFiles().OrderBy(f => f.LastWriteTime).ToList();
                 //   foreach (FileInfo file in fileEntries2)
                   // {
                        if (isValidFileName(info.Name))
                        {
                            // Se borra el archivo localmente porque la configurcion es 3
                            File.Delete(config.path + "\\" + info.Name);
                        }
                    //}
                }

                #endregion
                await Logger.sendLog(string.Format("{0} | {1} | {2}", "", "Termina limpieza de archivos temporales local", "Scanda.AppTray.ScandaConector.syncNowToolStripMenuItem_Click"), "T");

                await sync_accountinfo(config, config_path);
                await sync_updateAccount(config, config_path);


                return true;

            }
            catch (Exception ex)
            {
                await Logger.sendLog(string.Format("{0} | Error al sincronizar {1} | {2}", "Scanda.AppTray.ScandaConector.uploadFile", ex.Message, ex.StackTrace), "E");
                return false;

            }
            finally
            {

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
        }

        private static async Task sync_updateAccount(Config config, string config_path)
        {
            try
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", "", "Comienza actualizando informacion del usuario...", "Scanda.AppTray.ScandaConector.syncUpdateAccount"), "T");

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

                    }
                }
                await Logger.sendLog(string.Format("{0} | {1} | {2}", "", "informacion del usuario actualizada", "Scanda.AppTray.ScandaConector.syncUpdateAccount"), "T");

            }
            catch (Exception ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scadna.AppTray.ScandaConector.sync_updateAccount"), "E");
            }
        }

        private static async Task sync_accountinfo(Config config, string config_path)
        {
            try
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", "", "Comienza actualizando informacion del usuario...", "Scanda.AppTray.ScandaConector.syncUpdateAccount"), "T");

                string url = ConfigurationManager.AppSettings["api_url"];
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
                        config.time = r.UploadFrecuency.ToString();
                        config.time_type = "Horas";
                        config.type_storage = r.FileTreatmen.ToString();
                        config.file_historical = r.FileHistoricalNumber.ToString();
                        config.cloud_historical = r.FileHistoricalNumberCloud.ToString();
                        File.WriteAllText(config_path, JsonConvert.SerializeObject(config));
                    }
                }
            }
            catch (Exception ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scadna.AppTray.ScandaConector.sync_accountinfo"), "E");
            }
        }


        public static async Task<bool> downloadFile(string usrId, string year, string month, string fileN, Status status, string destino)
        {
            try
            {
                status.download.file = fileN;

                fileN = fileN + ".zip";

                string pathRemoto = usrId + "/" + year + "/" + month + "/" + fileN;
                status.download.status = 1;
                
                status.download.path = destino + "/" + fileN;
                await status.downloadStatusFile(status.download);
                //Bajo el zip a la carpeta del programa
                string zip = await downloadZipFile(pathRemoto, "C:\\DBProtector");
                //string zip = await downloadZipFile(pathRemoto, destino);
                //extraemos el archivo
                string archivo = decifrar(zip, usrId);
                //delete zip
                File.Delete(zip);
                //Moverlo a la ruta
                if (File.Exists(destino + "/" + archivo))
                    File.Delete(destino + "/" + archivo);
                File.Move(archivo, destino + "/" + archivo);

                FileInfo info = new FileInfo(destino + "/" + archivo);

                status.download.chunk = info.Length+"";
                status.download.status = 3;
                status.download.path = destino + "/" + archivo;
                await status.downloadStatusFile(status.download);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }



        private static async Task<ListFolderResult> listFiles(string path, bool recursive)
        {
            ListFolderResult list = null;
            try
            {
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);
                list = await client.Files.ListFolderAsync("/" + path, recursive);
            }
            catch (BadInputException ex)
            {
                Console.WriteLine("Error de Token");
                Console.WriteLine(ex.Message);
            }
            catch (ApiException<ListFolderError> ex)
            {
                //ApiException<ListFolderError>
                ListFolderError err = ex.ErrorResponse;
                if (err.IsPath)
                {
                    LookupError lerr = err.AsPath.Value;
                    if (lerr.IsMalformedPath)
                    {
                        Console.WriteLine("Ruta Mal Formateada");
                    }
                    if (lerr.IsNotFile)
                    {
                        Console.WriteLine("No es un archivo");
                    }
                    if (lerr.IsNotFolder)
                    {
                        Console.WriteLine("No es un Folder");
                    }
                    if (lerr.IsNotFound)
                    {
                        Console.WriteLine("Ruta no Hallada");
                    }
                    if (lerr.IsRestrictedContent)
                    {
                        Console.WriteLine("No tiene permisos");
                    }
                }
                else
                {
                    Console.WriteLine("Error No Indentificado");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Fallo Desconocido");
            }


            return list;
        }
        private static async Task<bool> uploadZipFile(string origen, string folder, Status status)
        {
            FileStream stream = null;
            try
            {
                
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);
                FileInfo info = new FileInfo(origen);

                status.upload.total = ((info.Length* 1))/ (B_TO_MB * 1.0f) + ""; //Esta en bytes para convertirlos en megas /1024d)/1024d

                string extension = info.Extension;
                float size = info.Length / (B_TO_MB * 1.0f);
                long nChunks = info.Length / CHUNK_SIZE;
                stream = new FileStream(origen, FileMode.Open);
                {
                    string nombre = info.Name;

                    if (nChunks == 0)
                    {
                        status.upload.chunk = "0";
                        await status.uploadStatusFile(status.upload);
                        var subidaS = await client.Files.UploadAsync("/" + folder + "/" + nombre, OVERWRITE, false, body: stream);
                        //subidaS.Wait();
                        //Console.WriteLine(subidaS.Result.AsFile.Size);
                        //stream.Close();

                        status.upload.chunk = status.upload.total;
                        status.upload.status = 3;
                        await status.uploadStatusFile(status.upload);
                    }
                    else
                    {
                        byte[] buffer = new byte[CHUNK_SIZE];
                        string sessionId = null;


                        for (var idx = 0; idx <= nChunks; idx++)
                        {
                            status.upload.status = 2;
                            var byteRead = stream.Read(buffer, 0, CHUNK_SIZE);

                            status.upload.chunk = (idx * (CHUNK_SIZE/1024d)/1024d) + "";
                            //status.upload.chunk = idx.ToString();
                            //status.upload.total = nChunks.ToString();
                            using (var memSream = new MemoryStream(buffer, 0, byteRead))
                            {
                                if (idx == 0)
                                {
                                    status.upload.status = 1; 
                                    //var result = client.Files.UploadSessionStartAsync(body: memSream);
                                    //result.Wait();
                                    var result = await client.Files.UploadSessionStartAsync(body: memSream);
                                    sessionId = result.SessionId;
                                    await status.uploadStatusFile(status.upload);

                                }
                                else
                                {
                                    ulong trans = (ulong)CHUNK_SIZE * (ulong)idx;
                                    
                                    var cursor = new UploadSessionCursor(sessionId, trans);
                                    await status.uploadStatusFile(status.upload);
                                    if (idx == nChunks)
                                    {
                                        //var x = client.Files.UploadSessionFinishAsync(cursor, new CommitInfo("/" + folder + "/" + nombre), memSream);
                                        //x.Wait();
                                        var x = await client.Files.UploadSessionFinishAsync(cursor, new CommitInfo("/" + folder + "/" + nombre), memSream);
                                        status.upload.status = 3;
                                        await status.uploadStatusFile(status.upload);
                                    }
                                    else
                                    {
                                        //var x =  client.Files.UploadSessionAppendAsync(cursor, memSream);
                                        //x.Wait();
                                        //var x = await client.Files.UploadSessionAppendAsync(cursor, memSream);
                                        await client.Files.UploadSessionAppendV2Async(new UploadSessionAppendArg(cursor), memSream);
                                        await status.uploadStatusFile(status.upload);
                                        // x.Wait();
                                        //await client.Files.UploadSessionAppendV2Async(new UploadSessionAppendArg(cursor), memSream);
                                    }
                                }
                            }
                        }
                    }
                }


                return true;
            }
            catch (OutOfMemoryException ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.uploadZipFile"), "E");
                Console.WriteLine("Se acabo la memoria");
                return false;
            }
            catch (FileNotFoundException ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.uploadZipFile"), "E");
                Console.WriteLine("No existe el archivo");
                return false;
            }
            catch (AggregateException ex) //Excepciones al vuelo
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.uploadZipFile"), "E");
                Console.WriteLine("Tarea Cancelada");
                return false;
            }
            catch (Exception ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.uploadZipFile"), "E");
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                if(stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
        private static async Task<string> downloadZipFile(string path, string folderName)
        {
            FileStream archivo = null;
            try
            {
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);
                path = "/" + path;
                var x = await client.Files.DownloadAsync(path);

                FileMetadata metadata = x.Response;
                archivo = File.Create(metadata.Name);

                var y = await x.GetContentAsStreamAsync();

                Stream stream = y;
                stream.CopyTo(archivo);

                archivo.Close();
                return metadata.Name;
            }
            catch (OutOfMemoryException ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.downloadZipFile"), "E");
                Console.WriteLine("Se acabo la memoria");
                return null;
            }
            catch (FileNotFoundException ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.downloadZipFile"), "E");
                Console.WriteLine("No existe el archivo");
                return null;
            }
            catch (AggregateException ex) //Excepciones al vuelo
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scanda.AppTray.ScandaConector.downloadZipFile"), "E");
                Console.WriteLine("Tarea Cancelada");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            finally
            {
                if (archivo != null)
                {
                    archivo.Close();
                    archivo.Dispose();
                }
            }
        }
        public static async Task<int> getUsedSpace(string userID, int s = 1)
        {
            //obtengo todos los archivos 
            ListFolderResult res = await listFiles(userID, true);
            List<Metadata> todo = res.Entries as List<Metadata>;
            List<FileMetadata> archivos = todo.Where(x => x.IsFile).Select(y => y.AsFile).ToList();

            long tam = (long)archivos.Select((x) => { return x.Size; }).Aggregate((x, y) => { return x + y; });

            return (int)Math.Ceiling((double) tam / (double) B_TO_MB);
        }
        private static string cifrar(string origen, string usrId)
        {
            FileInfo info = new FileInfo(origen);
            using (ZipFile zip = new ZipFile())
            {
                //Crifrar Password
                zip.Password = SHA256string(usrId);

                zip.UseZip64WhenSaving = Zip64Option.Always;
                zip.AddFile(origen, ".");
                zip.Name = info.Name + ".zip";
                if (File.Exists("C:\\DBProtector\\" + info.Name + ".zip"))
                    File.Delete("C:\\DBProtector\\" + info.Name + ".zip");

                zip.Save("C:\\DBProtector\\"+ info.Name + ".zip");
                
            }

            return "C:\\DBProtector\\" + info.Name + ".zip";
        }
        private static string decifrar(string origen, string usrId)
        {
            try
            {
                FileInfo info = new FileInfo(origen);
                using (ZipFile zip = ZipFile.Read(origen))
                {
                    string pass = SHA256string(usrId);
                    string file = origen.Replace(".zip","");
                    if (File.Exists(file)) {
                        File.Delete(file);
                    }
                    ZipEntry entry = zip.First();
                    entry.ExtractWithPassword(info.DirectoryName, pass);

                    return entry.FileName;
                }
            }
            catch (ZipException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        private static string SHA256string(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            //Console.WriteLine(hashString);
            return hashString;
        }
        private static async Task<bool> isValidSize(double tam, Config config)
        {
            bool tamvalido = true;
            try
            {
                
                string url = ConfigurationManager.AppSettings["api_url"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string llamada = string.Format("Account_GET?User={0}&Password={1}", config.user, config.password);
                    HttpResponseMessage response = await client.GetAsync(llamada);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        Account r = JsonConvert.DeserializeObject<Account>(resp);


                        if (r.StorageLimit == -1)
                        {
                            tamvalido = true;//Amacenamiento ilimitado
                        }
                        else
                        {
                            int res = r.StorageLimit - r.UsedStorage;
                            if(res > 0)
                            {
                                tamvalido = true;
                            }
                            else
                            {
                                await Logger.sendLog(string.Format("{0} | {1} | {2}"," Sin espacio", "Se agoto el espacio, este cliente cuenta con " + r.StorageLimit + " MB disponibles en la nube", "Scadna.AppTray.ScandaConector.isValidSize"), "E");
                                tamvalido = false;
                            }
                        }
                        
                    }
                }
                return tamvalido;

            }
            catch (Exception ex)
            {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, "Scadna.AppTray.ScandaConector.isValidSize"), "E");
                return false;
            }
            
        }
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
                if(ext.ToLower().Contains(extension.ToLower()))
                {
                    valido = true;
                    break;
                }
            }
            
            return valido;
        }
        private static async Task<FolderMetadata> createFolder(string path, string folderName)
        {
            FolderMetadata folder = null;
            try
            {
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);
                folder = await client.Files.CreateFolderAsync("/" + path + "/" + folderName);
            }
            catch (ApiException<CreateFolderError> ex)
            {
                CreateFolderError err = ex.ErrorResponse;

                if (err.AsPath.Value.IsConflict)
                {
                    Console.WriteLine("Nombre Conflictivo");
                }
                if (err.AsPath.Value.IsInsufficientSpace)
                {
                    Console.WriteLine("No hay Espacio");
                }
                if (err.AsPath.Value.IsNoWritePermission)
                {
                    Console.WriteLine("No hay PErmisos de Escritura");
                }
                if (err.AsPath.Value.IsMalformedPath)
                {
                    Console.WriteLine("Ruta Invalida");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Fallo Desconocido");
            }

            return folder;
        }
    }
}
