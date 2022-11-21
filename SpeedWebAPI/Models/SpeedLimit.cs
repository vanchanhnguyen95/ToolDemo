using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.Models
{
    [Table("SpeedLimit")]
    public class SpeedLimit
    {
        [Key]
        public int Id { get; set; }
        public double Lat { get; set; } = 0;
        public double Long { get; set; } = 0;
        public int MinSpeed { get; set; } = 0;
        public int MaxSpeed { get; set; } = 0;
        public int ProviderType { get; set; } = 1;
    }
}
