using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.ViewModels
{
    public class SpeedProvider
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int? ProviderType { get; set; } = 1;
        public long? SegmentID { get; set; }

        [NotMapped]
        public int Sort { get; set; } // Sort theo tổng của (5 chữ số sau dấu phẩy của lat + 5 chữ số sau dấu phẩy của long)
    }
}
