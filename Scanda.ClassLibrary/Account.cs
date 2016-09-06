namespace Scanda.ClassLibrary
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
        public int FileHistoricalNumberCloud { get; set; }
        public int PBYellowPercentage { get; set; }
        public int PBRedPercentage { get; set; }
        public string PBGreenColorCode { get; set; }
        public string PBYellowColorCode { get; set; }
        public string PBRedColorCode { get; set; }
    }
}
