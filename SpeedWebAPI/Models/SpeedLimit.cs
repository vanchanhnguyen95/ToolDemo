using SpeedWebAPI.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.Models
{
    [Table("SpeedLimit")]
    public class SpeedLimit : DisAuditableEntity
    {
        //public int Id { get; set; }
        [Key]
        [Column(Order = 1)]
        public double Lat { get; set; }
        [Key]
        [Column(Order = 2)]
        public double Long { get; set; }
        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;
        public int? ProviderType { get; set; } = 1;
    }
}
