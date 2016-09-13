using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.ServiceProcess;
using Newtonsoft.Json;
using Scanda.AppTray.Models;
using System.Net.Http.Headers;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Scanda.AppTray
{
    public partial class FormTray : Form
    {
        private RecuperarForm recoverForm;
        private ConfiguracionForm configuracionForm;
        private string selectedPath = "";
        private bool flag;
        private string configuration_path;
        //static string REGEXP = "([A-Zz-z]{4}\\d{6})(---|\\w{3})?(\\d{14}).(\\w{3})";
        //static string REGEXP = "[A-Za-z]{3,4}[0-9]{6}[A-Za-z0-9]{3}";
        static string ARCHIVO = "([A-Za-z]{3,4}[0-9]{6}[A-Za-z0-9]{3})([0-9]{14})";


        // Configuraciones
        private string json;
        private Config config;
        // private ProcessInfo notePad;
        /// <summary>
        /// Constructor de la aplicacion principal
        /// </summary>
        /// <param name="isNuevaInstancia">Indica si es una instancia nueva, o ya esta instalada</param>
        /// <param name="configPath">Indica el path de configuracion de la aplicacion</param>
        public FormTray(bool isNuevaInstancia, string configPath)
        {
            InitializeComponent();
            flag = isNuevaInstancia;
            configuration_path = configPath;
            json = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(json);
        }

        private void FormTray_Move(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIconScanda.ShowBalloonTip(1000, "Aviso importante", "DBProtector se está ejecutando", ToolTipIcon.Info);
            }
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                selectedPath = fbd.SelectedPath;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //MessageBox.Show(exeFolder);
            this.Close();
        }

        public static void RestartService(string serviceName, int timeoutMilliseconds)
            {
            ServiceController service = new ServiceController(serviceName);
            try
                {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
            catch
            {
                // ...
                }
            }

        private void ConfigurationForm_Close(object sender, EventArgs e)
        {
       
            // abrimos de nuevo el json
            json = File.ReadAllText(configuration_path);
            config = JsonConvert.DeserializeObject<Config>(json);
            if (string.IsNullOrEmpty(config.id_customer))
            {
                syncNowToolStripMenuItem.Enabled = false;
                descargarToolStripMenuItem.Enabled = false;
                
                StopService("DBProtector Service", 60*1000);
            
            }
            else if (string.IsNullOrEmpty(config.path))
            {
                syncNowToolStripMenuItem.Enabled = false;
                descargarToolStripMenuItem.Enabled = true;
                StopService("DBProtector Service", 60 * 1000);
            }
            else
            {
                syncNowToolStripMenuItem.Enabled = true;
                descargarToolStripMenuItem.Enabled = true;
                StartService("DBProtector Service", 60 * 1000);
                }
            }
        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            // notifyIconScanda.Icon = new System.Drawing.Icon(Application.StartupPath + @"\Resources\111.ico");
            configuracionForm = new ConfiguracionForm(flag, configuration_path);
            configuracionForm.FormClosed += ConfigurationForm_Close;
            configuracionForm.ShowDialog();
            // Bitmap bmp = Properties.Resources.QuotaDownload;
            // notifyIconScanda.Icon = Icon.FromHandle(bmp.GetHicon());
        }

        private void descargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            List<Control> selectedDownload = new List<Control>() { };
            recoverForm = new RecuperarForm(flag, configuration_path);
            recoverForm.FormClosed += RecuperarForm_Close;
            recoverForm.ShowDialog();
            
            /* while (!x.IsCompleted)
            {
                Console.WriteLine(temp2.upload.file);
                Console.WriteLine(temp2.upload.chunk + " MB Subidos");
                Console.WriteLine("de : " + temp2.upload.total + " MB Totales");
                System.Threading.Thread.Sleep(10000); // 10 segundos
            } */
        }

        async void RecuperarForm_Close(object sender, EventArgs e)
        {
            try
            {
                var form = (RecuperarForm)sender;
                string base_url = ConfigurationManager.AppSettings["api_url"];
                //Bitmap bmp = Properties.Resources.QuotaDownload;
                //notifyIconScanda.Icon = Icon.FromHandle(bmp.GetHicon());
                #region Validacion de Directorios
                // Revisamos si existe el directorio de respaldos
                if (!string.IsNullOrEmpty(config.path) && !Directory.Exists(config.path))
                {
                    Directory.CreateDirectory(config.path);
                }
                if (!string.IsNullOrEmpty(config.hist_path))
                {
                    // Revisamos si existe el directorio de historicos
                    if (!Directory.Exists(config.hist_path))
                    {
                        Directory.CreateDirectory(config.hist_path);
                    }
                }
                
                #endregion
                if (form.controls.Count > 0)
                {
                    syncNowToolStripMenuItem.Enabled = false;
                    configuracionToolStripMenuItem.Enabled = false;
                    descargarToolStripMenuItem.Enabled = false;
                    
                    foreach (Control ctrl in form.controls)
                    {
                        string[] file = ctrl.Name.Split('_');
                        Status temp = new Status(base_url, notifyIconScanda, descargarToolStripMenuItem, config.user, config.password);
                        // Pedimos donde descargar el archivo
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        fbd.Description = "Seleccione el folder donde desea almacenar su historico";
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            var selectedPath = fbd.SelectedPath;
                            notifyIconScanda.ShowBalloonTip(1000, "Sincronizando", "Se estan sicronizando los archivos a su dispositivo", ToolTipIcon.Info);
                            var res = await ScandaConector.downloadFile(config.id_customer, file[0], file[1], file[2], temp, config.path);
                            if (!res)
                            {
                                notifyIconScanda.ShowBalloonTip(1000, "Alerta", string.Format("Error al sincronizar {0}", file[2]), ToolTipIcon.Error);
                                await Logger.sendLog(string.Format("Error al sincronizar {0}", file[2]), "E");
                            }
                            else
                            {
                                notifyIconScanda.ShowBalloonTip(1000, "DB Protector", string.Format("Finalizo descarga de {0}", file[2]), ToolTipIcon.Info);
                                await Logger.sendLog(string.Format("Finalizo descarga de {0}", file[2]), "T");
                                switch (int.Parse(config.type_storage))
                                {
                                    case 1:
                                        if (File.Exists(config.path + "\\" + file[2]))
                                        {
                                            // Se copia a respaldados
                                            File.Move(config.path + "\\" + file[2], selectedPath + "\\" + file[2]);
                                        }
                                        break;
                                    case 2:
                                        if (File.Exists(config.path + "\\" + file[2]))
                                        {
                                            // Se copia a la carpeta seleccionada
                                            File.Move(config.path + "\\" + file[2], selectedPath + "\\" + file[2]);
                                            // File.Move(config.path + "\\" + file[2], config.user_path + "\\" + file[2]);
                                        }
                                        break;
                                    case 3:
                                        if (File.Exists(config.path + "\\" + file[2]))
                                        {
                                            // Se copia a la carpeta seleccionada
                                            File.Move(config.path + "\\" + file[2], selectedPath + "\\" + file[2]);
                                            // File.Delete(config.path + "\\" + file[2]);
                                        }
                                        break;
                                }
                            }
                            //switch (int.Parse(config.type_storage))
                            //{
                            //    case 1:
                            //        // Se copia a respaldados
                            //        if (File.Exists(config.path + "\\" + file[2]))
                            //        {
                            //            File.Move(config.path + "\\" + file[2], config.user_path + "\\" + file[2]);
                            //        }
                            //        break;
                            //    case 2:
                            //        if (File.Exists(config.path + "\\" + file[2]))
                            //        {
                            //            File.Move(config.path + "\\" + file[2], config.user_path + "\\" + file[2]);
                            //        }
                            //        break;
                            //    case 3:
                            //        if (File.Exists(config.path + "\\" + file[2]))
                            //        {
                            //            File.Delete(config.path + "\\" + file[2]);
                            //        }
                            //        break;
                            //}

                        }
                    }

                    if(string.IsNullOrEmpty(config.path))
                        syncNowToolStripMenuItem.Enabled = false;
                    else
                        syncNowToolStripMenuItem.Enabled = true;
                    configuracionToolStripMenuItem.Enabled = true;
                    descargarToolStripMenuItem.Enabled = true;
                }
                // notifyIconScanda.Icon = Properties.Resources.AppIcon;
            } catch(Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");*/
            }
        }

        private void FormTray_Load(object sender, EventArgs e)
        {
            // ServiceBase service = new ServiceBase();
            // Scanda.Service.ScandaService.Run(service);
            
            /*if (DoesServiceExist("DBProtector Services", ".")) // le coloque una S de mas XD
            {
                ServiceController sc = new ServiceController("DBProtector Service"); //con esto controlamos el servicio :o
                
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        startToolStripMenuItem.Checked = true;
                        pauseToolStripMenuItem.Checked = false;
                        stopToolStripMenuItem.Checked = false;
                        break;
                    case ServiceControllerStatus.Stopped:
                        startToolStripMenuItem.Checked = false;
                        pauseToolStripMenuItem.Checked = false;
                        stopToolStripMenuItem.Checked = true;
                        break;
                    case ServiceControllerStatus.Paused:
                        startToolStripMenuItem.Checked = false;
                        pauseToolStripMenuItem.Checked = true;
                        stopToolStripMenuItem.Checked = false;
                        break;
                    case ServiceControllerStatus.StopPending:
                        servicioToolStripMenuItem.Enabled = true;
                        break;
                    case ServiceControllerStatus.StartPending:
                        servicioToolStripMenuItem.Enabled = true;
                        break;
                    default:
                        servicioToolStripMenuItem.Enabled = false;
                        break;
                }
            }else
            {*/

                servicioToolStripMenuItem.Visible = false;
                exitToolStripMenuItem.Visible = false;
                if (string.IsNullOrEmpty(config.id_customer))

                {
                    syncNowToolStripMenuItem.Enabled = false;
                    descargarToolStripMenuItem.Enabled = false;
                }
                else if (string.IsNullOrEmpty(config.path))
                {
                    syncNowToolStripMenuItem.Enabled = false;
                    descargarToolStripMenuItem.Enabled = true;
                }
                else { 
                    syncNowToolStripMenuItem.Enabled = true;
                    descargarToolStripMenuItem.Enabled = true;
                }
            //}
            // Start();
        }
        
      /*  private void Start()
        {
            int xTime = int.Parse(config.time);
            // Es en milisegundos 48 * 60 * 1000
            // convertimos a segundos nuestras horas
            if (xTime != 0)
            {
                int timestamp = xTime * 3600 * 1000; // horas * 60 * 1000
                timerUpload = new System.Windows.Forms.Timer();
                timerUpload.Tick += OnTimedEvent;
                timerUpload.Interval = timestamp;
                timerUpload.Start();
            }
        }^*/

     /*   private async void OnTimedEvent(object sender, EventArgs e)
        {
            try
            {
                syncNowToolStripMenuItem.Enabled = false;
                configuracionToolStripMenuItem.Enabled = false;
                descargarToolStripMenuItem.Enabled = false;
                string base_url = ConfigurationManager.AppSettings["api_url"];
                syncNowToolStripMenuItem.Text = "Sincronizando...";
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
                // Obtenemos listado de archivos del directorio
                string[] fileEntries = Directory.GetFiles(config.path);
                // Comienza a subir los archivos
                //Bitmap bmp = Properties.Resources.QuotaNearing;
                //notifyIconScanda.Icon = Icon.FromHandle(bmp.GetHicon());
                notifyIconScanda.ShowBalloonTip(1000, "Sincronizando", "Se estan sicronizando los archivos a su dispositivo de la nube", ToolTipIcon.Info);
                foreach (string file in fileEntries)
                {
                    Status temp2 = new Status(base_url, notifyIconScanda, syncNowToolStripMenuItem, config.user, config.password);
                    FileInfo info = new FileInfo(file);
                    var x = await ScandaConector.uploadFile(file, config.id_customer, temp2, config.extensions);
                    if (!x)
                    {
                        notifyIconScanda.ShowBalloonTip(1000, "Alerta", string.Format("Error al sincronizar {0}", info.Name), ToolTipIcon.Error);
                        await Logger.sendLog(string.Format("!Error al sincronizar {0}!", info.Name), "E");
                    }
                    else
                    {
                        notifyIconScanda.ShowBalloonTip(1000, "DBProtector", string.Format("Finalizo subida de {0}", info.Name), ToolTipIcon.Info);
                        await Logger.sendLog(string.Format("Archivo subido correctamente: {0}", info.Name), "T");
                    }
                }
                // Realizamos la limpieza en Cloud
                await ScandaConector.deleteHistory(config.id_customer, int.Parse(config.cloud_historical));

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
                            /* if (histFileEntries.Count() == 0)
                        {
                            canTransfer = true;
                        }
                             else if (fileEntries.Count <= histFileEntries.Count() || fileEntries.Count < int.Parse(config.file_historical))
                             //else if (fileEntries.Length <= histFileEntries.Count() || fileEntries.Length < int.Parse(config.file_historical))
                        {
                            canTransfer = true;
                        }
                        else
                        {
                            FileInfo item = histFileEntries.FirstOrDefault();
                            if (item != null)
                                histFileEntries.Remove(item);
                             }
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
                    if (isValidFileName(file.Name) )
                    {
                            // Se borra el archivo localmente porque la configurcion es 3
                            File.Delete(config.path + "\\" + file.Name);
                        }
                    }
                }
                #endregion
                await sync_updateAccount();
                // Termino de hacer todos los respaldos
                syncNowToolStripMenuItem.Text = "Sincronizar ahora";
                syncNowToolStripMenuItem.Enabled = true;
                configuracionToolStripMenuItem.Enabled = true;
                descargarToolStripMenuItem.Enabled = true;
                // Termino de hacer todos los respaldos
                // notifyIconScanda.Icon = Properties.Resources.AppIcon;
            } catch(Exception ex)
            {
                // Fallo al realizar los respaldos
                syncNowToolStripMenuItem.Text = "Sincronizar ahora";
                syncNowToolStripMenuItem.Enabled = true;
                configuracionToolStripMenuItem.Enabled = true;
                descargarToolStripMenuItem.Enabled = true;
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
            }
        }

/*
        bool DoesServiceExist(string serviceName, string machineName)
        {
            bool existe = false;
            try
            {
            ServiceController[] services = ServiceController.GetServices(machineName);
            var service = services.FirstOrDefault(s => s.ServiceName == serviceName);
                existe = service != null;
            }
            catch { }

            return existe;
             
         }
         */
        public void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                // ...
            }
        }



        public static void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch
            {
                // ...
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScandaServiceController.Start();
            startToolStripMenuItem.Checked = true;
            pauseToolStripMenuItem.Checked = false;
            stopToolStripMenuItem.Checked = false;
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScandaServiceController.Stop();
            startToolStripMenuItem.Checked = false;
            pauseToolStripMenuItem.Checked = false;
            stopToolStripMenuItem.Checked = true;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScandaServiceController.Pause();
            startToolStripMenuItem.Checked = false;
            pauseToolStripMenuItem.Checked = true;
            stopToolStripMenuItem.Checked = false;
        }

        private void watch()
        {
            fileSystemWatcherScanda = new FileSystemWatcher();
            fileSystemWatcherScanda.Path = config.path;
            fileSystemWatcherScanda.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcherScanda.Filter = ".";
            fileSystemWatcherScanda.Changed += new FileSystemEventHandler(OnChanged);
            fileSystemWatcherScanda.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // var x = 1 + 1;
            MessageBox.Show("Nuevo Archivo creado ->" + e.Name);
        }
        private static bool isValidFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return false;
            try
            {
                
                    bool valido = Regex.IsMatch(fileName, ARCHIVO);

                return valido;
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

        private async void syncNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                json = File.ReadAllText(configuration_path);
                config = JsonConvert.DeserializeObject<Config>(json);
                string base_url = string.Empty;
                if (!string.IsNullOrEmpty(config.path))
                {
                    base_url = ConfigurationManager.AppSettings["api_url"];
                    syncNowToolStripMenuItem.Enabled = false;
                    configuracionToolStripMenuItem.Enabled = false;
                    descargarToolStripMenuItem.Enabled = false;
                    syncNowToolStripMenuItem.Text = "Sincronizando...";
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
                        notifyIconScanda.ShowBalloonTip(1000, "Sincronizando", "Se estan sicronizando los archivos a su dispositivo de la nube", ToolTipIcon.Info);
                        foreach (string file in fileEntries)
                        {
                            Status temp2 = new Status(base_url, notifyIconScanda, syncNowToolStripMenuItem, config.user, config.password);
                            FileInfo info = new FileInfo(file);
                            var x = await ScandaConector.uploadFile(file, config.id_customer, temp2, config.extensions);
                            if (!x)
                            {
                                notifyIconScanda.ShowBalloonTip(1000, "Alerta", string.Format("Error al sincronizar {0}", info.Name), ToolTipIcon.Error);
                                await Logger.sendLog(string.Format("Error al sincronizar {0}", info.Name), "E");
                            }
                            else
                            {

                                notifyIconScanda.ShowBalloonTip(1000, "DB Protector", string.Format("Finalizo subida de {0}", info.Name), ToolTipIcon.Info);
                                await Logger.sendLog(string.Format("Archivo subido correctamente: {0}", info.Name), "T");
                            }
                        }
                    }
                    else
                    {
                        notifyIconScanda.ShowBalloonTip(1000, "Sincronizando", "No hay respaldos pendientes por sincronizar", ToolTipIcon.Warning);

                    }


                    // Realizamos la limpieza en Cloud
                    await ScandaConector.deleteHistory(config.id_customer, int.Parse(config.cloud_historical));
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
                // Termino de hacer todos los respaldos
                syncNowToolStripMenuItem.Text = "Sincronizar ahora";
                syncNowToolStripMenuItem.Enabled = true;
                configuracionToolStripMenuItem.Enabled = true;
                descargarToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                // Termino de hacer todos los respaldos
                syncNowToolStripMenuItem.Text = "Sincronizar ahora";
                syncNowToolStripMenuItem.Enabled = true;
                configuracionToolStripMenuItem.Enabled = true;
                descargarToolStripMenuItem.Enabled = true;
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n", "E");*/
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
            }catch(Exception ex) {
                await Logger.sendLog(string.Format("{0} | {1} | {2}", ex.Source, ex.Message, ex.InnerException), "E");
                /*Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
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

        private void notifyIconScanda_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
