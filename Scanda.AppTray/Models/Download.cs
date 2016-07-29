using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.AppTray.Models
{
    public class Download
    {
        public int status;
        public string file;
        public string path;

        public Download()
        {
            status = -1;
            file = "";
            path = "";
        }
    }
}
