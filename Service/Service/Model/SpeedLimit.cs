using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Model
{
    public class SpeedLimit
    {
        public int Id { get; set; }
        public double Lat { get; set; } = 0;
        public double Long { get; set; } = 0;
        public int MinSpeed { get; set; } = 0;
        public int MaxSpeed { get; set; } = 0;
        public int ProviderType { get; set; } = 1;
    }
}
