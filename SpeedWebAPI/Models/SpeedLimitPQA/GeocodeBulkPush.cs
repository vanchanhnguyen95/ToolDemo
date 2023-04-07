using System.Collections.Generic;

namespace SpeedWebAPI.Models.SpeedLimitPQA
{
    public class LocationPQA
    {
        public decimal lng { get; set; }
        public decimal lat { get; set; }
        public string vehicle_code { get; set; }

        public LocationPQA() { }
        public LocationPQA(LocationPQA orther)
        {
            lng = orther.lng;
            lat = orther.lat;
            vehicle_code = "300";
        }
        public LocationPQA(decimal lng, decimal lat, string vehicle_code = "300")
        {
            this.lng = lng;
            this.lat = lat;
            this.vehicle_code = vehicle_code;
        }
    }

    public class GeocodeBulkPush
    {
        public List<LocationPQA> locations { get; set; }
    }
}
