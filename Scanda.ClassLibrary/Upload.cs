using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.ClassLibrary
{
    public class Upload
    {
        //Status int
        public int status;
        //file name
        public string file;
        //total Chunk
        public string total;
        //Actual Chunk
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
