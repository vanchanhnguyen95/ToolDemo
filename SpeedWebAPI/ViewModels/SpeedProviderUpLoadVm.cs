
namespace SpeedWebAPI.ViewModels
{
    public class SpeedProviderUpLoadVm
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public long SegmentID { get; set; }
        public int? ProviderType { get; set; } = 1;//1:Navital; 2:VietMap
        public string Position { get; set; }
    }
}
