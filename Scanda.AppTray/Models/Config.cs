using System.Collections.Generic;

namespace Scanda.AppTray.Models
{
    public class Config
    {
        public string path { get; set; }
        public string hist_path { get; set; }
        public string time_type { get; set; }
        public string time { get; set; }
        public string id_customer { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string type_storage { get; set; }
        public string file_historical { get; set; }
        public string cloud_historical { get; set; }
        public List<string> extensions { get; set; }

    }
}
