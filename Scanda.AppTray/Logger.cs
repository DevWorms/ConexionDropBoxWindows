using Newtonsoft.Json;
using Scanda.AppTray.Models;
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
                Message = Message.Replace("<", "[").Replace(">", "]");
                string appFolder = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
                string settingsFolder = appFolder;
                string logFile = settingsFolder + "\\Settings\\configuration.json";
                string json = File.ReadAllText(logFile);
                Config config = JsonConvert.DeserializeObject<Config>(json);
                string url = ConfigurationManager.AppSettings["api_url"];

                using (var client = new HttpClient())
                {
                    var service_url = string.Format("Log_SET?Message={0}&MessageType=T&Code=1&AppVersion=4.1&IdCustomer={1}", Message, config.id_customer);
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
