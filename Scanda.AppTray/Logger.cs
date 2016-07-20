using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Scanda.AppTray
{
    public class Logger
    {
        public static void writeErrors(string strMensajeError)
        {
            TextWriter twError = new StreamWriter(@"c:\Scanda\log.txt", true);
            twError.WriteLine(strMensajeError);
            twError.Close();
        }

        public static async void sendLog(string Message)
        {
            try
            {
                string json = File.ReadAllText(@"C:\Scanda\configuration.json");
                Config config = JsonConvert.DeserializeObject<Config>(json);
                string url = ConfigurationManager.AppSettings["api_url"];

                using (var client = new HttpClient())
                {
                    var service_url = string.Format("Log_SET?Message={0}&MessageType=T&Code=1&AppVersion=4.1&User={1}&Password={2}", Message, config.user, config.password);
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(service_url);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        LoginResponse r = JsonConvert.DeserializeObject<LoginResponse>(resp);
                    }
                }
            }catch(Exception ex)
            {
                writeErrors(ex.Message
                    + "\n" + ex.Source
                    + "\n" + ex.InnerException
                    + "\n" + ex.StackTrace
                    + "\n");
            }
        }
    }
}
