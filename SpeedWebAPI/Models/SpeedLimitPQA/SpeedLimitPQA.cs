using SpeedWebAPI.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.Models.SpeedLimitPQA
{
    [Table("SpeedLimitPQA")]
    public class SpeedLimitPQA : DisAuditableEntity
    {
        

        [Key]
        [Column(Order = 1, TypeName = "decimal(18,10)")]
        public decimal Lat { get; set; }// Y
        [Key]
        [Column(Order = 2, TypeName = "decimal(18,10)")]
        public decimal Lng { get; set; } // X
        //[Key]
        //[Column(Order = 3)]
        //[MaxLength(1)]
        public int? ProviderType { get; set; } = 1;//1:Navital; 2:VietMap; 1000:Nghe An, 2000 Ha Tinh
        [Key]
        [Column(Order = 4)]
        [MaxLength(50)]
        public string Position { get; set; }//S:Start, E:End: M-1, M-2: Middle-1,2,BS: ngược lại start, BE; ngược lại End, tương tự M cũng phải chạy ngược

        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;

        public bool? PointError { get; set; } = false;//True: Tọa độ cung cấp bị lỗi, False: Tọa độ cung cấp ko bị lỗi
        public long? SegmentID { get; set; }

        public bool? IsUpdateSpeed { get; set; } = false;//True: đang cập nhật vận tốc giới hạn
        public int? Direction { get; set; } = 0;// Hướng 0: chiều xuôi, 1: chiều ngược lại

        public int STT { get; set; }
        public int SpeedGPS { get; set; }
        public int SpeedDetect { get; set; }
        public int SpeedPQA { get; set; }
        public string Address { set; get; }
        public string FileName { set; get; }
        //public string RouteType { set; get; }// Loại tuyến đường đễ phân biệt khi import : 1: Nghệ An, 2: Hà Tĩnh
        public bool IsUpdSpeedPQA { get; set; } = false;

    }
}
