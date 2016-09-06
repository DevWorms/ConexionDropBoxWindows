using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scanda.ClassLibrary
{
    public class Status
    {
        public Upload upload;
        public Download download;
        private string base_url, username, password;
        private NotifyIcon icon;
        private ToolStripMenuItem menuItem;

        public Status(string base_url = "", NotifyIcon icon = null, ToolStripMenuItem menuItem = null, string username = "", string password = "")
        {
            upload = new Upload();
            download = new Download();
            this.username = username;
            this.password = password;
            this.base_url = base_url;
            this.icon = icon;
            this.menuItem = menuItem;
        }

        public async Task uploadStatusFile(Upload upload)
        {
            this.menuItem.Text = string.Format("Sincronizando {0} de {1}", upload.chunk, upload.total);
            // notifyIconScanda.ShowBalloonTip(1000, "Scanda DB", string.Format("Finalizo descarga de {0}", file[2]), ToolTipIcon.Info);
            string unixTimestamp = DateTime.Now.ToString("yyyyMMddHHmmss");//(int)(DateTime.UtcNow.Subtract(DateTime.Now)).TotalSeconds;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.base_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string requestUrl = "";
                string status = upload.status == 3 ? "Finalizado" : "EnProgreso";
                switch(upload.status)
                {
                    case 1:
                        requestUrl = string.Format("FileTransaction_SET?User={0}&Password={1}&StartDate={2}&ActualChunk={3}&TotalChunk={4}&Status={5}&FileName={6}&TransactionType=1", this.username, this.password, unixTimestamp, upload.chunk, upload.total, status, upload.file);
                        break;
                    case 2:
                        requestUrl = string.Format("FileTransaction_UPDATE?User={0}&Password={1}&ActualChunk={2}&Status={3}&FileName={4}&TransactionType=1", this.username, this.password, upload.chunk, status, upload.file);
                        break;
                    case 3:
                        requestUrl = string.Format("FileTransaction_UPDATE?User={0}&Password={1}&ActualChunk={2}&Status={3}&FileName={4}&TransactionType=1", this.username, this.password, upload.chunk, status, upload.file);
                        break;
                }
                HttpResponseMessage response = await client.GetAsync(requestUrl);
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
            //int unixTimestamp = (int)(DateTime.UtcNow.Subtract(DateTime.Now)).TotalSeconds;
            string unixTimestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.base_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string status = upload.status == 3 ? "Finalizado" : "EnProgreso";
                string requestUrl = "";

                switch(download.status)
                {
                    case 1:
                        requestUrl = string.Format("FileTransaction_SET?User={0}&Password={1}&StartDate={2}&ActualChunk={3}&TotalChunk={4}&Status={5}&FileName={6}&TransactionType=2", this.username, this.password, unixTimestamp, 2, 2, status, download.file);
                        break;
                    case 3:
                        requestUrl = string.Format("FileTransaction_UPDATE?User={0}&Password={1}&ActualChunk={2}&Status={3}&FileName={4}&TransactionType=2", this.username, this.password,download.chunk, status, download.file);
                        break;
                }

                HttpResponseMessage response = await client.GetAsync(requestUrl); if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                }
            }
        }

    }
}
