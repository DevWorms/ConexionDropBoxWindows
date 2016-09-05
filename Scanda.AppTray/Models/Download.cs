using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.AppTray.Models
{
    public class Download
    {
        //Status en entero
        public int  status;
        //total Chunk
        public string total;
        //Actual Chunk
        public string chunk;
        //File Name
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
