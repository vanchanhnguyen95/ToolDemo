using System.Collections.Generic;

namespace SpeedWebAPI.Models.SpeedLimitPQA
{
    public class InfoMationPQA
    {
        public int index { get; set; }
        public string road_name { get; set; }
        public int speed_max { get; set; }
        public string commune { get; set; }
        public string district { get; set; }
        public string province { get; set; }
    }

    public class GeocodeBulkResponse
    {
        public string status { get; set; }
        public List<InfoMationPQA> info { get; set; }
    }
}
