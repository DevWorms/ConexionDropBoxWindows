namespace Scanda.AppTray.Models
{
    public class Account
    {
        public int Success { get; set; }
        public string DBoxUser { get; set; }
        public string DBoxPassword { get; set; }
        public int StorageLimit { get; set; }
        public int UsedStorage { get; set; }
        public int FileTreatmen { get; set; }
        public int UploadFrecuency { get; set; }
        public int FileHistoricalNumber { get; set; }
    }
}
