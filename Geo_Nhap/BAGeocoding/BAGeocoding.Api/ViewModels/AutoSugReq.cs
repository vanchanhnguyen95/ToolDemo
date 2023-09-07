namespace BAGeocoding.Api.ViewModels
{
    //double lat = 0, double lng = 0, string distance = "300km", int size = 5, string keyword = null, int shapeid = 0
    public class AutoSugReq
    {
        public double? lat { get; set; } = 0;
        public double? lng { get; set; }  = 0;
        public string? distance { get; set; }  = "300km";
        public int? size { get; set; }  = 5;
        public string keys { get; set; }
        public int shapeid { get; set; } = 0;
        public AutoSugReq()
        {
            lat = 0;
            lng = 0;
            distance = "300km";
            size = 10;
            shapeid = 0;
        }
    }

    public class AutoSugReqV2
    {
        public double? lat { get; set; } = 0;
        public double? lng { get; set; } = 0;
        public double distance { get; set; } = 300000.0;
        public int? size { get; set; } = 10;
        public string keys { get; set; }
        public int shapeid { get; set; } = 0;
        public AutoSugReqV2()
        {
            lat = 0;
            lng = 0;
            distance = 300000.0;
            size = 10;
            shapeid = 0;
        }
    }

    public class Add2GeoReq
    {
        public string keys { get; set; }
    }
}
