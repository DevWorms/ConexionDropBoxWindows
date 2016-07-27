using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Scanda.AppTray
{
    public class Logger
    {
        public static void writeErrors(string strMensajeError)
        {
            string appFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
            string settingsFolder = appFolder;
            string logFile = settingsFolder + "\\log.txt";
            TextWriter twError = new StreamWriter(logFile, true);
            twError.WriteLine(strMensajeError);
            twError.Close();
        }

        public static async void sendLog(string Message)
        {
            try
            {
                string appFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
                string settingsFolder = appFolder;
                string logFile = settingsFolder + "\\log.txt";
                string json = File.ReadAllText(logFile);
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
