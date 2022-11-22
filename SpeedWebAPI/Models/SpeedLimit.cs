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
        public double Lat { get; set; }
        [Key]
        [Column(Order = 2)]
        public double Lng { get; set; }
        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;
        public int? ProviderType { get; set; } = 1;
        public long? SegmentID { get; set; }
        public string Note { get; set; }
    }
}
