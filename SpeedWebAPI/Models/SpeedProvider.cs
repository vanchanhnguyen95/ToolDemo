using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.Models
{
    [Table("SpeedProvider")]
    public class SpeedProvider
    {
        [Key]
        public int Id { get; set; }
        public double Lat { get; set; } = 0;
        public double Long { get; set; } = 0;
    }
}
