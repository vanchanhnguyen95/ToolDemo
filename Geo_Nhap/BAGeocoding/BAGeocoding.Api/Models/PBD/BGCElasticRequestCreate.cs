using BAGeocoding.Entity.Enum;
using BAGeocoding.Entity.RestfulApi.ElasticSearch;
using BAGeocoding.Utility;
using Nest;
using System.ComponentModel;

namespace BAGeocoding.Api.Models.PBD;

[ElasticsearchType(IdProperty = nameof(Id)), Description("roadelastic")]
public class BGCElasticRequestCreate
{
    //public byte? typeid { get; set; }
    public byte? typeid { get; set; }
    public int? indexid { get; set; }
    public byte? shapeid { get; set; }
    public string? kindname { get; set; }
    public string? name { get; set; }
    public string? address { get; set; }
    public string? shortkey { get; set; }
    //public byte? provinceid { get; set; }
    public int? provinceid { get; set; }
    //public byte? priority { get; set; }
    public int? priority { get; set; }
    //public List<BGCElasticPointBase> coords { get; set; }

    public decimal? Lng { get; set; }
    public decimal? Lat { get; set; }

    [GeoPoint]
    public GeoLocation Location { get; set; }

    public string? ProvinceName { get; set; } = string.Empty;

    public string? nameAscii { get; set; }
    public int TypeArea { get; set; } = 0;//0: Điểm hoặc đường thông thường, 1: ngõ, 2: ngách, 3: hẻm

    [Text(Index = true, Fielddata = true)]
    public string Keywords { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsAscii { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsNoExt { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsAsciiNoExt { get; set; } = string.Empty;

    public BGCElasticRequestCreate()
    {
        //coords = new List<BGCElasticPointBase>();
    }

    public BGCElasticRequestCreate(BGCPointOfInterest other)
    {
        typeid = (byte)EnumMapObjectType.PointOfInterest;
        indexid = other.PoiID;
        shapeid = (byte)EnumMapObjectShape.Point;
        //kindname = other.KindInfo.Name;
        kindname = other.KindName;
        //name = string.Format("{0} {1}", other.KindInfo.Name, other.Name);
        name = string.Format("{0} {1}", other.KindName, other.Name);
        address = other.Info;
        shortkey = other.ShortKey;
        provinceid = other.ProvinceID;
        priority = (byte)EnumElasticSearchPriority.Normal;
        //coords = new List<BGCElasticPointBase>();
        //coords.Add(new BGCElasticPointBase(other.Coord));

        Lng = other.Lng;
        Lat = other.Lat;

        Location = new GeoLocation((double)Lat, (double)Lng);

        KeywordsNoExt = other?.Name?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim() ?? "";

        ProvinceName = other?.ProvinceName ?? "";
        //ProvinceID = other?.ProvinceID ?? 0;

        nameAscii = LatinToAscii.Latin2Ascii(other?.Name ?? "");
        TypeArea = other?.TypeArea ?? 0;

        Keywords = other?.Name?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim() + " , "
            + other?.ProvinceName?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();
        KeywordsNoExt = other?.Name?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();

        KeywordsAscii = LatinToAscii.Latin2Ascii(Keywords);
        KeywordsAsciiNoExt = LatinToAscii.Latin2Ascii(KeywordsNoExt ?? "");

    }

    public BGCElasticRequestCreate(BGCElasticRequestCreate other)
    {
        typeid = other.typeid;
        indexid = other.indexid;
        shapeid = other.shapeid;
        kindname = other.kindname;
        name = other.name;
        address = other.address;
        shortkey = other.shortkey;
        provinceid = other.provinceid;
        priority = other.priority;
        //coords = new List<BGCElasticPointBase>();
        //for (int i = 0; i < other.coords.Count; i++)
        //    coords.Add(new BGCElasticPointBase(other.coords[i]));

        Lng = other.Lng;
        Lat = other.Lat;
        Location = new GeoLocation((double)other?.Lat, (double)other.Lng);
    }

    public BGCElasticRequestCreate(BGCRoadName other)
    {
        typeid = (byte)EnumMapObjectType.RoadSegment;
        indexid = other.RoadID;
        shapeid = (byte)EnumMapObjectShape.Polyline;
        kindname = "Đường";
        if (other.NameExt.Length == 0)
        {
            name = other.RoadName;
            address = string.Format("{0}, {1}", other.RoadName, other.Address);
        }
        else
        {
            name = string.Format("{0}, {1}", other.RoadName, other.NameExt);
            address = string.Format("{0}, {1}, {2}", other.RoadName, other.NameExt, other.Address);
        }
        shortkey = string.Empty;
        provinceid = other.ProvinceID;
        priority = (byte)EnumElasticSearchPriority.Normal;
        //coords = new List<BGCElasticPointBase>();
        //coords.Add(new BGCElasticPointBase(other.Coord));

        Lng = other.Lng;
        Lat = other.Lat;

        Location = new GeoLocation((double)other?.Lat, (double)other.Lng);

        ProvinceName = other.ProvinceName;
        //ProvinceID = other.ProvinceID;
        nameAscii = LatinToAscii.Latin2Ascii(other?.RoadName ?? "");
        TypeArea = other?.TypeArea ?? 0;

        Location = new GeoLocation((double)Lat, (double)Lng);

        KeywordsNoExt = other?.RoadName?.ToLower() ?? "";

        if (!string.IsNullOrEmpty(other?.NameExt))
        {
            Keywords = other?.RoadName?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim()
                + " , " + other?.NameExt?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();
        }
        else
        {
            Keywords = other?.RoadName?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim() ?? "";
        }

        if (!string.IsNullOrEmpty(other?.Address))
        {
            Keywords += " , " + other?.Address?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();
            KeywordsNoExt += " , " + other?.Address?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();
        }
        else
        {
            Keywords += " , ";
        }
        //else
        //{
        //    Keywords += other?.Address ?? "";
        //}

        KeywordsAscii = LatinToAscii.Latin2Ascii(Keywords);
        KeywordsAsciiNoExt = LatinToAscii.Latin2Ascii(KeywordsNoExt ?? "");
    }
}
