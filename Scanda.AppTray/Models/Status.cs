using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scanda.AppTray.Models
{
    public class Status
    {
        public Upload upload;
        public Download download;
        private string base_url, username, password;
        private NotifyIcon icon;

        public Status(string base_url = "", NotifyIcon icon = null, string username = "", string password = "")
        {
            upload = new Upload();
            download = new Download();
            this.username = username;
            this.password = password;
            this.base_url = base_url;
            this.icon = icon;
        }

        public async Task updateStatusFile(Upload upload)
        {
            // notifyIconScanda.ShowBalloonTip(1000, "Scanda DB", string.Format("Finalizo descarga de {0}", file[2]), ToolTipIcon.Info);
            int unixTimestamp = (int)(DateTime.UtcNow.Subtract(DateTime.Now)).TotalSeconds;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.base_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string status = upload.status == 3 ? "Finalizado" : "En Progreso";
                HttpResponseMessage response = await client.GetAsync(string.Format("FileTransaction_SET?User={0}&Password={1}&StartDate={2}&ActualChunk={3}&TotalChunk={4}&Status={4}&FileName={5}", this.username, this.password, unixTimestamp, upload.chunk, upload.total, status, upload.file));
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task downloadStatusFile(Download download)
        {
            // No se implementa :P
            // this.icon.ShowBalloonTip(1000, "Scanda DB", string.Format("Finalizo descarga de {0}", file[2]), ToolTipIcon.Info);
            int unixTimestamp = (int)(DateTime.UtcNow.Subtract(DateTime.Now)).TotalSeconds;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.base_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(string.Format("FileTransaction_SET?User={0}&Password={1}&StartDate={2}&ActualChunk={3}&TotalChunk={4}&Status={4}&FileName={5}", this.username, this.password, unixTimestamp, download.));
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                }
            }
        }

    }
}
