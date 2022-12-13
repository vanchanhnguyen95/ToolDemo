
namespace SpeedWebAPI.ViewModels
{
    public class SpeedProviderDbVm
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public long SegmentID { get; set; }
        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;
        public string Note { get; set; }
        public string Position { get; set; }
    }
}
