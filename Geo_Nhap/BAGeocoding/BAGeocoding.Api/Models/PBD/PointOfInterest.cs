using Nest;

namespace BAGeocoding.Api.Models.PBD;

public class PointOfInterest
{
    [Number(Index = true)]
    public int? PoiID { get; set; }
    public int? ProvinceID { get; set; }
    //public int? KindID { get; set; }
    [Text(Index = true, Fielddata = true)]
    public string? Name { get; set; }
    //public string? House { get; set; }
    [Text(Index = true, Fielddata = true)]
    public string? Road { get; set; }
    //[Text(Index = true, Fielddata = true)]
    //public string? Address { get; set; }
    //public string? Tel { get; set; }
    //public string? Anchor { get; set; }
    [Text(Index = true, Fielddata = true)]
    public string? Info { get; set; }
    //public string? Note { get; set; }
    //public string? ShortKey { get; set; }
    public decimal? Lng { get; set; }
    public decimal? Lat { get; set; }

    public string? ProvinceName { get; set; } = string.Empty;

    public int? TypeArea { get; set; } = 0;//0: Điểm hoặc đường thông thường, 1: ngõ, 2: ngách, 3: hẻm
}
