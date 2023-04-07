using SpeedWebAPI.Models;
using SpeedWebAPI.Models.SpeedLimitPQA;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.ViewModels
{
    public class SpeedProvider
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int? ProviderType { get; set; } = 1;
        public long? SegmentID { get; set; }
        public int? Direction { get; set; } = 0;// Hướng 0: chiều xuôi, 1: chiều ngược lại
        //public string Position { get; set; }//S:Start, E:End: M-1, M-2: Middle-1,2,BS: ngược lại start, BE; ngược lại End, tương tự M cũng phải chạy ngược
        //[NotMapped]
        //public decimal Sort { get; set; } // Sort theo tổng của (5 chữ số sau dấu phẩy của lat + 5 chữ số sau dấu phẩy của long)

        public SpeedProvider () { }

        public SpeedProvider(SpeedLimitPQA orther) {
            Lat = orther.Lat;
            Lng = orther.Lng;
            ProviderType = orther.ProviderType;
            SegmentID = orther.SegmentID;
            Direction = orther.Direction;
        }
    }
}
