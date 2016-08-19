using Dropbox.Api;
using Dropbox.Api.Files;
using Ionic.Zip;
using Newtonsoft.Json;
using Scanda.AppTray.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scanda.AppTray
{
    public class ScandaConector
    {
        static DropboxClient client;
        static DropboxClientConfig clientConf;
        //static string APITOKEN = "f-taP7WG2wAAAAAAAAAAEPgxbzHQ7EDctvivjSJCqLwCA0tcsgyuRT7H9vnxqwVK";
        static string APITOKEN = "DnYsuEHH3ssAAAAAAAAYodsCelGBXj22nko-HeIh5ENG5OFjSpmelu6R-_Obw0jM";

        static int B_TO_MB = 1024 * 1024;
        static int CHUNK_SIZE = 5 * B_TO_MB;

        static string STATUSFILE = "status.json";
        //static string BACKEDFOLDER = "Backed";

        static WriteMode OVERWRITE = WriteMode.Overwrite.Instance;

        static string REGEXP = "([A-Zz-z]{4}\\d{6})(---|\\w{3})?(\\d{14}).(\\w{3})";
        static string RFCregexp = "([A-Zz-z]{4}\\d{6}(---|\\w{3})?)";

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
        public static async Task deleteHistory(string userID, int cant)
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
                        }
                        catch (BadInputException ex)
                        {
                            Console.WriteLine("Error de Token");
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }



        public static async Task<bool> uploadFile(string archivo, string usrId, Status status, List<string> extensions = null, double remmainingSpace = -1)
        {
            status.upload.file = archivo;
            status.upload.status = 0;
            try
            {
                FileInfo info = new FileInfo(archivo);
                //validamos extensiones
                if (!isValidExt(info.Extension, extensions))
                    return false; // No es una extension valida


                double size = info.Length / B_TO_MB;

                //Validamos el tamanio
                if (!isValidSize(size, remmainingSpace))
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
                string zip = cifrar(archivo, usrId);
                string ruta = usrId + "/" + year + "/" + month;
                status.upload.status = 1;
                var res = await uploadZipFile(zip, ruta, status);

                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }
        public static async Task<bool> downloadFile(string usrId, string year, string month, string fileN, Status status, string destino)
        {
            try
            {
                status.download.file = fileN;
                status.download.status = 0;

                fileN = fileN + ".zip";

                string pathRemoto = usrId + "/" + year + "/" + month + "/" + fileN;
                string zip = await downloadZipFile(pathRemoto, destino);
                //extraemos el archivo
                string archivo = decifrar(zip, usrId);
                //delete zip
                File.Delete(zip);
                //Moverlo a la ruta
                File.Move(archivo, destino + "/" + archivo);

                status.download.status = 1;
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
            try
            {
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);


                FileInfo info = new FileInfo(origen);

                status.upload.total = ((info.Length * 1.0) / B_TO_MB) + ""; //Lo convierte a MB y a string

                string extension = info.Extension;
                float size = info.Length / (B_TO_MB * 1.0f);
                long nChunks = info.Length / CHUNK_SIZE;
                FileStream stream = new FileStream(origen, FileMode.Open);
                string nombre = info.Name;

                if (nChunks == 0)
                {
                    var subidaS = await client.Files.UploadAsync("/" + folder + "/" + nombre, OVERWRITE, false, body: stream);
                    //subidaS.Wait();
                    //Console.WriteLine(subidaS.Result.AsFile.Size);
                    //stream.Close();
                    await status.updateStatusFile(status.upload);
                }
                else
                {
                    byte[] buffer = new byte[CHUNK_SIZE];
                    string sessionId = null;



                    for (var idx = 0; idx <= nChunks; idx++)
                    {
                        status.upload.status = 2;
                        var byteRead = stream.Read(buffer, 0, CHUNK_SIZE);

                        status.upload.chunk = (idx * CHUNK_SIZE * 1.0) / B_TO_MB + "";

                        using (var memSream = new MemoryStream(buffer, 0, byteRead))
                        {
                            if (idx == 0)
                            {
                                //var result = client.Files.UploadSessionStartAsync(body: memSream);
                                //result.Wait();
                                var result = await client.Files.UploadSessionStartAsync(body: memSream);
                                sessionId = result.SessionId;
                            }
                            else
                            {
                                var cursor = new UploadSessionCursor(sessionId, (ulong)(CHUNK_SIZE * idx));

                                if (idx == nChunks)
                                {
                                    //var x = client.Files.UploadSessionFinishAsync(cursor, new CommitInfo("/" + folder + "/" + nombre), memSream);
                                    //x.Wait();
                                    var x = await client.Files.UploadSessionFinishAsync(cursor, new CommitInfo("/" + folder + "/" + nombre), memSream);
                                    status.upload.status = 3;
                                    await status.updateStatusFile(status.upload);
                                }
                                else
                                {
                                    //var x =  client.Files.UploadSessionAppendAsync(cursor, memSream);
                                    //x.Wait();
                                    //var x = await client.Files.UploadSessionAppendAsync(cursor, memSream);
                                    await client.Files.UploadSessionAppendV2Async(new UploadSessionAppendArg(cursor), memSream);
                                    await status.updateStatusFile(status.upload);
                                    // x.Wait();
                                    //await client.Files.UploadSessionAppendV2Async(new UploadSessionAppendArg(cursor), memSream);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Se acabo la memoria");
                return false;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No existe el archivo");
                return false;
            }
            catch (AggregateException ex) //Excepciones al vuelo
            {

                Console.WriteLine("Tarea Cancelada");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        private static async Task<string> downloadZipFile(string path, string folderName)
        {
            try
            {
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);
                path = "/" + path;
                var x = await client.Files.DownloadAsync(path);

                FileMetadata metadata = x.Response;
                FileStream archivo = File.Create(metadata.Name);

                var y = await x.GetContentAsStreamAsync();

                Stream stream = y;
                stream.CopyTo(archivo);

                archivo.Close();
                return metadata.Name;
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Se acabo la memoria");
                return null;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No existe el archivo");
                return null;
            }
            catch (AggregateException ex) //Excepciones al vuelo
            {

                Console.WriteLine("Tarea Cancelada");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        private static string cifrar(string origen, string usrId)
        {
            FileInfo info = new FileInfo(origen);
            using (ZipFile zip = new ZipFile())
            {
                //Crifrar Password
                zip.Password = SHA256string(usrId);

                zip.AddFile(origen, ".");
                zip.Save(info.Name + ".zip");
            }

            return info.Name + ".zip";
        }
        private static string decifrar(string origen, string usrId)
        {
            try
            {
                FileInfo info = new FileInfo(origen);
                using (ZipFile zip = ZipFile.Read(origen))
                {
                    string pass = SHA256string(usrId);

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
        private static bool isValidSize(double tam, double res)
        {
            if (res == -1) return true;//Amacenamiento ilimitado
            return tam <= res;
        }
        private static bool isValidFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return false;
            try
            {
                return Regex.IsMatch(fileName, REGEXP);
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
            return extensions.Contains(ext);
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
