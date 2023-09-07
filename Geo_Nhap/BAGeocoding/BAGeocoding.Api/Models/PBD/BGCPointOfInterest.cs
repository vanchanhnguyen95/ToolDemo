namespace BAGeocoding.Api.Models.PBD;

public class BGCPointOfInterest
{
    public int PoiID { get; set; }
    //public byte ProvinceID { get; set; }
    public int ProvinceID { get; set; }
    //public BGCKindInfo KindInfo { get; set; }
    public string? KindName { get; set; }
    public string? Name { get; set; }
    public short? House { get; set; }
    public string? Road { get; set; }
    public string? Address { get; set; }
    public string? Tel { get; set; }
    public string? Anchor { get; set; }
    public string? Info { get; set; }
    public string? Node { get; set; }
    public string? ShortKey { get; set; }
    //public BGCLngLat Coord { get; set; }

    public decimal? Lng { get; set; }
    public decimal? Lat { get; set; }

    public int TypeArea { get; set; } = 0;//0: Điểm hoặc đường thông thường, 1: ngõ, 2: ngách, 3: hẻm
    public string? ProvinceName { get; set; } = string.Empty;

    public BGCPointOfInterest()
    {
        //KindInfo = new BGCKindInfo();
        //Coord = new BGCLngLat();
    }

    public BGCPointOfInterest(BGCPointOfInterest other)
    {
        PoiID = other.PoiID;
        ProvinceID = other.ProvinceID;
        //KindInfo = new BGCKindInfo(other.KindInfo);
        KindName = other.Name;
        Name = other.Name;
        House = other.House;
        Road = other.Road;
        Address = other.Address;
        Tel = other.Tel;
        Anchor = other.Anchor;
        Info = other.Info;
        Node = other.Node;
        ShortKey = other.ShortKey;
        //Coord = new BGCLngLat(other.Coord);

        Lng = other.Lng;
        Lat = other.Lat;
    }

}
