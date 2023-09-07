using Nest;

namespace BAGeocoding.Api.Models.PBD;

public class BGCRoadName
{
    public int RoadID { get; set; }
    public byte ProvinceID { get; set; }
    public string RoadName { get; set; }
    public string NameExt { get; set; }
    public string Address { get; set; }
    //public BGCLngLat Coord { get; set; }

    [Number(Index = true)]
    public decimal Lng { get; set; } = 0;

    [Number(Index = true)]
    public decimal Lat { get; set; } = 0;

    public int TypeArea { get; set; } = 0;//0: Điểm hoặc đường thông thường, 1: ngõ, 2: ngách, 3: hẻm
    public string? ProvinceName { get; set; } = string.Empty;

    public BGCRoadName()
    {
        //Coord = new BGCLngLat();
    }

    public BGCRoadName(BGCRoadName other)
    {
        RoadID = other.RoadID;
        ProvinceID = other.ProvinceID;
        RoadName = other.RoadName;
        NameExt = other.NameExt;
        Address = other.Address;
        //Coord = new BGCLngLat(other.Coord);

        Lng = other.Lng;
        Lat = other.Lat;
    }

}
