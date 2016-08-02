using System;
using System.Collections.Generic;
using System.Linq;
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

        public void updateStatusFile(Upload upload)
        {
            // notifyIconScanda.ShowBalloonTip(1000, "Scanda DB", string.Format("Finalizo descarga de {0}", file[2]), ToolTipIcon.Info);
        }

        public void downloadStatusFile(Download download)
        {
            // this.icon.ShowBalloonTip(1000, "Scanda DB", string.Format("Finalizo descarga de {0}", file[2]), ToolTipIcon.Info);
        }

    }
}
