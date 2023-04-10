using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWebAPI.Models.SpeedLimitBA
{
    public class DataJAddressByGeoResult
    {
        public bool accurate { get; set; }
        public int building { get; set; }
        public string commune { get; set; }
        public string district { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int maxspeed { get; set; }
        public int minspeed { get; set; }
        public string province { get; set; }
        public string road { get; set; }
    }

    public class JAddressByGeoBAResult
    {
        public List<DataJAddressByGeoResult> data { get; set; }
    }
}
