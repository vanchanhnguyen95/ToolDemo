
namespace SpeedWebAPI.ViewModels
{
    public class SpeedProviderUpLoad3PointVm
    {
        public long SegmentID { get; set; }
        public double Lat1 { get; set; }
        public double Lng1 { get; set; }
        public double Lat2 { get; set; }
        public double Lng2 { get; set; }
        public double Lat3 { get; set; }
        public double Lng3  { get; set; }
        public int? MinSpeed1 { get; set; } = 0;
        public int? MaxSpeed1 { get; set; } = 0;
        public int? MinSpeed2 { get; set; } = 0;
        public int? MaxSpeed2 { get; set; } = 0;
        public int? MinSpeed3 { get; set; } = 0;
        public int? MaxSpeed3 { get; set; } = 0;

        public string Position { get; set; }
    }
}
