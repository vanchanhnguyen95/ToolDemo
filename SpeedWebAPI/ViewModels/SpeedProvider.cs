using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.ViewModels
{
    public class SpeedProvider
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int? ProviderType { get; set; } = 1;
        public long? SegmentID { get; set; }
    }
}
