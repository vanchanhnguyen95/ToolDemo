
namespace SpeedWebAPI.ViewModels
{
    public class SpeedProviderDbVm
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public long SegmentID { get; set; }
        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;
        public string Note { get; set; }
    }
}
