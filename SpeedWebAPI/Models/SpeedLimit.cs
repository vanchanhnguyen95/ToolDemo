using SpeedWebAPI.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.Models
{
    [Table("SpeedLimit")]
    public class SpeedLimit : DisAuditableEntity
    {
        [Key]
        [Column(Order = 1)]
        public double Lat { get; set; }// Y
        [Key]
        [Column(Order = 2)]
        public double Lng { get; set; } // X
        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;
        [MaxLength(1)]
        public int? ProviderType { get; set; } = 1;//1:Navital; 2:VietMap
        public bool? PointError { get; set; } = false;//True: Tọa độ cung cấp bị lỗi, False: Tọa độ cung cấp ko bị lỗi
        public long? SegmentID { get; set; }
        [MaxLength(1)]
        public string Position { get; set; }
    }
}
