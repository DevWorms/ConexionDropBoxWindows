﻿using System;
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
using System.Reflection;
using Newtonsoft.Json;
using Scanda.AppTray.Models;

namespace Scanda.AppTray
{
    public partial class FormTray : Form
    {
        private RecuperarForm recoverForm;
        private ConfiguracionForm configuracionForm;
        private string selectedPath = "";
        private bool flag;
        private string configuration_path;
        // Configuraciones
        private string json;
        private Config config;
        // private ProcessInfo notePad;
        public FormTray(bool isNuevaInstancia, string configPath)
        {
            InitializeComponent();
            flag = isNuevaInstancia;
            configuration_path = configPath;
            json = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(json);
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
            
            //MessageBox.Show(exeFolder);
            this.Close();
        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            // notifyIconScanda.Icon = new System.Drawing.Icon(Application.StartupPath + @"\Resources\111.ico");
            configuracionForm = new ConfiguracionForm(flag, configuration_path);
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
        }

        async void RecuperarForm_Close(object sender, EventArgs e)
        {
            var form = (RecuperarForm)sender;
            Bitmap bmp = Properties.Resources.QuotaDownload;
            notifyIconScanda.Icon = Icon.FromHandle(bmp.GetHicon());
            notifyIconScanda.ShowBalloonTip(1000, "Sincronizando", "Se estan sicronizando los archivos a su dispositivo", ToolTipIcon.Info);
            foreach (Control ctrl in form.controls)
            {
                string[] file = ctrl.Name.Split('_');
                var res = await ScandaConector.downloadFile(config.id_customer, file[0], file[1], file[2], config.path);
                if (!res)
                {
                    notifyIconScanda.ShowBalloonTip(1000, "Alerta", string.Format("Error al sincronizar {0}", file[2]), ToolTipIcon.Error);
                } else
                {
                    notifyIconScanda.ShowBalloonTip(1000, "Información", string.Format("Termino de sincronizar {0}", file[2]), ToolTipIcon.Info);
                }
            }

            notifyIconScanda.Icon = Properties.Resources.AppIcon;
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
            ScandaConector.uploadFile(config.path + "\\spoderman.zip", config.id_customer, config.extensions);
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
