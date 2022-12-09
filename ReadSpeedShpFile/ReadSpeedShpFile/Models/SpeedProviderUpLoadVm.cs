namespace ReadSpeedShpFile.Models
{
    public class SpeedProviderUpLoadVm
    {
        public double Lat { get; set; } // X
        public double Lng { get; set; } // Y
        public long SegmentID { get; set; }
        public int? ProviderType { get; set; } = 1;//1:Navital; 2:VietMap
        public string Position { get; set; }
    }
}
