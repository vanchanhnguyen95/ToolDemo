namespace SpeedWebAPI.ViewModels
{
    public class SpeedLimitPush
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;
        public int? ProviderType { get; set; } = 1;
        public bool? PointError { get; set; } = false;
        public long? SegmentID { get; set; }
    }
}
