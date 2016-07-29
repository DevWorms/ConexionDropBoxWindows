using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.AppTray.Models
{
    public class Status
    {
        public Upload upload;
        public Download download;

        public Status()
        {
            upload = new Upload();
            download = new Download();
        }

    }
}
