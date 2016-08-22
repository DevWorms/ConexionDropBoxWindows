using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.ClassLibrary
{
    public class SyncFile
    {
        public bool canTransferToHistorical(List<FileInfo> fileEntries, int limit)
        {
            bool canTransfer = false;
            while (!canTransfer)
            {
                if (fileEntries.Count() < limit)
                {
                    canTransfer = true;
                }
                else
                {
                    FileInfo item = fileEntries.FirstOrDefault();
                    if (item != null)
                        fileEntries.Remove(item);
                }
            }
            return true;
        }
    }
}
