using System;
using System.Collections.Generic;
using System.Text;

namespace Apose02.Models
{
    public class SpeedProviderUpLoadVm
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public long SegmentID { get; set; }
        public string Position { get; set; }
    }
}
