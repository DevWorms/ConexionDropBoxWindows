using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.AppTray.Models
{
    public class Upload
    {
        public int status;
        public string file;
        public string total;
        public string chunk;

        public Upload()
        {
            status = -1;
            file = "";
            total = "";
            chunk = "";
        }
    }
}
