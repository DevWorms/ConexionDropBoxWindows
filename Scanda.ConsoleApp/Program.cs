using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //var x = ScandaConector.uploadFile("test.jpg", "TEST1");
            //var x = ScandaConector.uploadFile("testP.mkv", "TEST1");
            //x.Wait();

            //var y = ScandaConector.createFolder("TEST", "ERROR");
            //y.Wait();

            //Hay dos usuarios de prueba 1 y 2
            //var z = ScandaConector.listUserFolders("1");
            //z.Wait();
            //recorreListFolderResult(z.Result);
            //ScandaConector.cifrar("testP.mkv", "1");
            //ScandaConector.decifrar("testP.mkv.zip", "1");

            //ScandaConector.getFolders("1");
            //ScandaConector.uploadFile("test.jpg", "1");
            //ScandaConector.downloadFile("1","2016","07","test.jpg.zip", "Downloaded");

            ScandaConector.uploadFile("testP.mkv", "1");
            ScandaConector.downloadFile("1", "2016", "07", "testP.mkv.zip", "Downloaded");
        }

        static void recorreListFolderResult(ListFolderResult lf)
        {
            if (lf != null)
            {
                foreach (Metadata m in lf.Entries)
                {
                    if (m.IsFile)
                        Console.WriteLine(m.Name + "  " + m.AsFile.Size);
                    if (m.IsFolder)
                        Console.WriteLine(m.Name + "  " + m.AsFolder.PathLower);
                }
            }
            else
            {
                Console.WriteLine("Resultado Vacio");
            }
        }
    }
}
