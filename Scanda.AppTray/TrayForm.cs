using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.ServiceProcess;

namespace Scanda.AppTray
{
    public partial class FormTray : Form
    {
        private RecuperarForm recoverForm;
        private ConfiguracionForm configuracionForm;
        private string selectedPath = "";
        // private ProcessInfo notePad;
        public FormTray()
        {
            InitializeComponent();
            if (!Directory.Exists("C:\\Scanda"))
            {
                // Creamos el Directorio
                Directory.CreateDirectory("C:\\Scanda");
            }
            if (!File.Exists(@"C:\Scanda\configuration.json"))
            {
                // Si no existe lo creamos
                // Creamos el archivo de configuracion
                JObject configSettings = new JObject(
                    new JProperty("path", ""),
                    new JProperty("time_type", ""),
                    new JProperty("time", ""),
                    new JProperty("id_customer", ""),
                    new JProperty("user", ""),
                    new JProperty("password", ""),
                    new JProperty("token", "")
                );
                // escribimos el archivo
                File.WriteAllText(@"C:\Scanda\configuration.json", configSettings.ToString());
            }
            
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

        private void FormTray_Move(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIconScanda.ShowBalloonTip(1000, "Important notice", "Scanda Service is Running", ToolTipIcon.Info);
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
            this.Close();
        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            // notifyIconScanda.Icon = new System.Drawing.Icon(Application.StartupPath + @"\Resources\111.ico");
            configuracionForm = new ConfiguracionForm();
            configuracionForm.ShowDialog();
            // recoverForm = new RecuperarForm();
            // recoverForm.ShowDialog();
        }

        private void descargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            List<Control> selectedDownload = new List<Control>() { };
            recoverForm = new RecuperarForm();
            recoverForm.FormClosed += RecuperarForm_Close;
            recoverForm.ShowDialog();
        }

        void RecuperarForm_Close(object sender, EventArgs e)
        {
            var form = (RecuperarForm)sender;
            foreach(Control ctrl in form.controls)
            {
                
            }
            notifyIconScanda.ShowBalloonTip(1000, "Sincronizando", "Se estan sicronizando los archivos a su dispositivo", ToolTipIcon.Info);
            // combinar co codigo de chemas
            //using (var httpClient = new HttpClient())
            //{
            //    using (var request = new HttpRequestMessage(HttpMethod.Get, "ftp://speedtest:speedtest@ftp.otenet.gr/test10Mb.db"))
            //    {
            //        using (
            //            Stream contentStream = await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(),
            //            stream = new FileStream(@"C:\Scanda\test.db", FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 100, true))
            //        {
            //            await contentStream.CopyToAsync(stream);
            //        }
            //    }
            //}
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
    }
}
