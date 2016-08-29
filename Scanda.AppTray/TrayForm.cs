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
        static string REGEXP = "([A-Zz-z]{4}\\d{6})(---|\\w{3})?(\\d{14}).(\\w{3})";
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
        private void ConfigurationForm_Close(object sender, EventArgs e)
        {
            // abrimos de nuevo el json
            json = File.ReadAllText(configuration_path);
            config = JsonConvert.DeserializeObject<Config>(json);
            if (string.IsNullOrEmpty(config.id_customer))
            {
                syncNowToolStripMenuItem.Enabled = false;
                descargarToolStripMenuItem.Enabled = false;
            }
            else
            {
                syncNowToolStripMenuItem.Enabled = true;
                descargarToolStripMenuItem.Enabled = true;
                Start();
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
                if (!Directory.Exists(config.path))
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
                    notifyIconScanda.ShowBalloonTip(1000, "Sincronizando", "Se estan sicronizando los archivos a su dispositivo", ToolTipIcon.Info);
                    foreach (Control ctrl in form.controls)
                    {
                        string[] file = ctrl.Name.Split('_');
                        Status temp = new Status(base_url, notifyIconScanda, config.user, config.password);
                        // Pedimos donde descargar el archivo
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        fbd.Description = "Seleccione el folder donde desea almacenar su historico";
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            var selectedPath = fbd.SelectedPath;
                            var res = await ScandaConector.downloadFile(config.id_customer, file[0], file[1], file[2], temp, config.path);
                            if (!res)
                            {
                                notifyIconScanda.ShowBalloonTip(1000, "Alerta", string.Format("Error al sincronizar {0}", file[2]), ToolTipIcon.Error);
                            }
                            else
                            {
                                notifyIconScanda.ShowBalloonTip(1000, "DB Protector", string.Format("Finalizo descarga de {0}", file[2]), ToolTipIcon.Info);
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
                    syncNowToolStripMenuItem.Enabled = true;
                    configuracionToolStripMenuItem.Enabled = true;
                    descargarToolStripMenuItem.Enabled = true;
                }
                // notifyIconScanda.Icon = Properties.Resources.AppIcon;
            } catch(Exception ex)
            {
                Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");
            }
        }

        private void FormTray_Load(object sender, EventArgs e)
        {
            // ServiceBase service = new ServiceBase();
            // Scanda.Service.ScandaService.Run(service);
            
            if (DoesServiceExist("ServiceScanda", "."))
            {
                ServiceController sc = new ServiceController("ServiceScanda");
                
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
            {
                servicioToolStripMenuItem.Visible = false;
                exitToolStripMenuItem.Visible = false;
                if(string.IsNullOrEmpty(config.id_customer) && string.IsNullOrEmpty(config.path))
                {
                    syncNowToolStripMenuItem.Enabled = false;
                    descargarToolStripMenuItem.Enabled = false;
                } else
                {
                    syncNowToolStripMenuItem.Enabled = true;
                    descargarToolStripMenuItem.Enabled = true;
                }
            }
            Start();
        }
        
        private void Start()
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
        }

        private async void OnTimedEvent(object sender, EventArgs e)
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
                    Status temp2 = new Status(base_url, notifyIconScanda, config.user, config.password);
                    FileInfo info = new FileInfo(file);
                    var x = await ScandaConector.uploadFile(file, config.id_customer, temp2, config.extensions);
                    if (!x)
                    {
                        notifyIconScanda.ShowBalloonTip(1000, "Alerta", string.Format("Error al sincronizar {0}", info.Name), ToolTipIcon.Error);
                    }
                    else
                    {
                        notifyIconScanda.ShowBalloonTip(1000, "DBProtector", string.Format("Finalizo subida de {0}", info.Name), ToolTipIcon.Info);
                    }
                }
                // Realizamos el movimiento de los archivos que se suben a la carpeta historicos
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
                Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");
            }
        }

        bool DoesServiceExist(string serviceName, string machineName)
        {
            ServiceController[] services = ServiceController.GetServices(machineName);
            var service = services.FirstOrDefault(s => s.ServiceName == serviceName);
            return service != null;
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
            var x = 1 + 1;
            MessageBox.Show("Nuevo Archivo creado ->" + e.Name);
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
        private async void syncNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string base_url = ConfigurationManager.AppSettings["api_url"];
                syncNowToolStripMenuItem.Enabled = false;
                configuracionToolStripMenuItem.Enabled = false;
                descargarToolStripMenuItem.Enabled = false;
                syncNowToolStripMenuItem.Text = "Sincronizando...";
                #region Validacion de Directorios
                // Revisamos si existe el directorio de respaldos
                if (!Directory.Exists(config.path))
                {
                    Directory.CreateDirectory(config.path);
                }
                // Revisamos si existe el directorio de historicos, si esta en configuracion 3 significa que el archivo localmente se tiene que borrar, por tanto no es necesario crear una carpeta
                if (!Directory.Exists(config.hist_path) && config.type_storage != "3")
                {
                    Directory.CreateDirectory(config.hist_path);
                }
                
                #endregion
                // Obtenemos listado de archivos del directorio
                string[] fileEntries = Directory.GetFiles(config.path);
                notifyIconScanda.ShowBalloonTip(1000, "Sincronizando", "Se estan sicronizando los archivos a su dispositivo de la nube", ToolTipIcon.Info);
                foreach (string file in fileEntries)
                {
                    Status temp2 = new Status(base_url, notifyIconScanda, config.user, config.password);
                    FileInfo info = new FileInfo(file);
                    var x = await ScandaConector.uploadFile(file, config.id_customer, temp2, config.extensions);
                    if (!x)
                    {
                        notifyIconScanda.ShowBalloonTip(1000, "Alerta", string.Format("Error al sincronizar {0}", info.Name), ToolTipIcon.Error);
                    }
                    else
                    {
                        notifyIconScanda.ShowBalloonTip(1000, "DB Protector", string.Format("Finalizo subida de {0}", info.Name), ToolTipIcon.Info);
                        Logger.sendLog("archivo subido correctamente" + file);
                    }
                }
                // Realizamos el movimiento de los archivos que se suben a la carpeta historicos
                if (config.type_storage != "3")
                {
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
                            //cuando vale 1 y 2 se mueve a una carpeta el respaldo, cuanfdo vale 3 se borra localmente
                            if (config.type_storage == "1" || config.type_storage == "2")
                            {
                                // Se copia a Historicos
                                File.Copy(config.path + "\\" + file.Name, config.hist_path + "\\" + file.Name);
                            }
                            File.Delete(config.path + "\\" + file.Name);
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
                // Termino de hacer todos los respaldos
                syncNowToolStripMenuItem.Text = "Sincronizar ahora";
                syncNowToolStripMenuItem.Enabled = true;
                configuracionToolStripMenuItem.Enabled = true;
                descargarToolStripMenuItem.Enabled = true;
            } catch (Exception ex)
            {
                // Termino de hacer todos los respaldos
                syncNowToolStripMenuItem.Text = "Sincronizar ahora";
                syncNowToolStripMenuItem.Enabled = true;
                Logger.sendLog(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");
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
                    File.WriteAllText(configuration_path, JsonConvert.SerializeObject(config));
                }
            }
        }

        private void notifyIconScanda_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
