using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWebAPI.Models.SpeedLimitPQA
{
    public class SpeedLimitPQAImport
    {
        public int Id { get; set; }
        public decimal Lng { get; set; }
        public decimal Lat { get; set; }
        public int SpeedGPS { get; set; }
        public int SpeedDetect { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }

        //public string IdString { get; set; }
        //public string LngString { get; set; }
        //public string LatString { get; set; }
        //public string SpeedGPSString { get; set; }
    }
}
