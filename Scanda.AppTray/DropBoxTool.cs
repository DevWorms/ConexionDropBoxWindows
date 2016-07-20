using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.AppTray
{
    public class DropBoxTool
    {
        private string _AppKey = "";
        private string _AppSecret = "";
        private DropboxClient dbx;

        public DropBoxTool(string AppKey, string AppSecret) {
            this._AppKey = AppKey;
            this._AppSecret = AppSecret;
        }

        public void Connect()
        {
            using (this.dbx = new DropboxClient("YOUR ACCESS TOKEN"))
            {
                
                // var full = await dbx.Users.GetCurrentAccountAsync();
                // Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            }
        }
        public List<string> ListUserDirs()
        {
            return new List<string>() { };
        }
    }
}
