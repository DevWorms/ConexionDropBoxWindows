using Dropbox.Api;
using Dropbox.Api.Files;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.ConsoleApp
{
    public class ScandaConector
    {
        static DropboxClient client;
        static DropboxClientConfig clientConf;
        //static string APITOKEN = "";
        static string APITOKEN = "";

        static int B_TO_MB = 1024 * 1024;
        static int CHUNK_SIZE = 1 * B_TO_MB;

        static string BACKEDFOLDER = "Backed";

        static WriteMode OVERWRITE = WriteMode.Overwrite.Instance;


        private static async Task<ListFolderResult> listFiles(string path)
        {
            ListFolderResult list = null;
            try
            {
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);
                list = await client.Files.ListFolderAsync("/" + path);
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


        public static List<string> getFiles(string usrID, string year, string month)
        {
            return getFolders(usrID + "/" + year + "/" + month);
        }
        public static List<string> getMonths(string usrID, string year)
        {
            return getFolders(usrID + "/" + year);
        }
        public static List<string> getYears(string usrID)
        {
            return getFolders(usrID);
        }
        public static List<string> getFolders(string path)
        {
            try
            {
                List<string> lista = new List<string>();
                var x = listFiles(path);
                x.Wait();
                ListFolderResult res = x.Result;
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
        public static List<string> getFiles(string path)
        {
            try
            {
                List<string> lista = new List<string>();
                var x = listFiles(path);
                x.Wait();
                ListFolderResult res = x.Result;
                foreach (Metadata m in res.Entries)
                {
                    if (m.IsFile)
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
        public static bool uploadFile(string archivo, string usrId)
        {
            try
            {
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
                uploadZipFile(zip, ruta);
                //Eliminar el archivo Zip
                File.Delete(zip);
                //Mover el archivo a backuped
                if (!Directory.Exists(BACKEDFOLDER))
                    Directory.CreateDirectory(BACKEDFOLDER);
                FileInfo info = new FileInfo(archivo);
                info.MoveTo(BACKEDFOLDER + "/" + info.Name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool downloadFile(string usrId, string year, string month, string fileN, string destino)
        {
            try
            {
                string pathRemoto = usrId + "/" + year + "/" + month + "/" + fileN;
                string zip = downloadZipFile(pathRemoto, destino);
                //extraemos el archivo
                string archivo = decifrar(zip, usrId);
                //delete zip
                File.Delete(zip);
                //Moverlo a la ruta
                File.Move(archivo, destino + "/" + archivo);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }



        private static void uploadZipFile(string origen, string folder)
        {
            try
            {
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);

                FileInfo info = new FileInfo(origen);
                string extension = info.Extension;
                float size = info.Length / (B_TO_MB * 1.0f);
                long nChunks = info.Length / CHUNK_SIZE;
                FileStream stream = new FileStream(origen, FileMode.Open);
                string nombre = info.Name;

                if (nChunks == 0)
                {
                    var subidaS = client.Files.UploadAsync("/" + folder + "/" + nombre, OVERWRITE, false, body: stream);
                    subidaS.Wait();
                    Console.WriteLine(subidaS.Result.AsFile.Size);
                    stream.Close();
                }
                else
                {
                    byte[] buffer = new byte[CHUNK_SIZE];
                    string sessionId = null;

                    for (var idx = 0; idx <= nChunks; idx++)
                    {
                        var byteRead = stream.Read(buffer, 0, CHUNK_SIZE);

                        using (var memSream = new MemoryStream(buffer, 0, byteRead))
                        {
                            if (idx == 0)
                            {
                                var result = client.Files.UploadSessionStartAsync(body: memSream);
                                result.Wait();
                                sessionId = result.Result.SessionId;
                            }
                            else
                            {
                                var cursor = new UploadSessionCursor(sessionId, (ulong)(CHUNK_SIZE * idx));

                                if (idx == nChunks)
                                {
                                    var x = client.Files.UploadSessionFinishAsync(cursor, new CommitInfo("/" + folder + "/" + nombre), memSream);
                                    x.Wait();
                                }
                                else
                                {
                                    var x = client.Files.UploadSessionAppendAsync(cursor, memSream);
                                    x.Wait();
                                }
                            }
                        }
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Se acabo la memoria");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No existe el archivo");
            }
            catch (AggregateException ex) //Excepciones al vuelo
            {

                Console.WriteLine("Tarea Cancelada");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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

        private static string downloadZipFile(string path, string folderName)
        {
            try
            {
                clientConf = new DropboxClientConfig("ScandaV1");
                client = new DropboxClient(APITOKEN);
                path = "/" + path;
                var x = client.Files.DownloadAsync(path);
                x.Wait();

                FileMetadata metadata = x.Result.Response;
                FileStream archivo = File.Create(metadata.Name);

                var y = x.Result.GetContentAsStreamAsync();
                y.Wait();

                Stream stream = y.Result;
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
            ZipFile zip = new ZipFile();
            //Crifrar Password
            //SHA256 sha2 = SHA256.Create(usrId);
            zip.Password = SHA256string(usrId);
            zip.AddFile(info.Name);
            zip.Save(info.Name + ".zip"); SHA256 mySHA256 = SHA256Managed.Create();
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
            Console.WriteLine(hashString);
            return hashString;
        }
    }
}
