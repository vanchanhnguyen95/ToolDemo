using BAGeocoding.Utility;
using Nest;
using System.ComponentModel;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BAGeocoding.Api.Models.PBD;

[ElasticsearchType(IdProperty = nameof(Id)), Description("roadmerge")]
public class RoadNameMerge
{
    public string? address { get; set; }
    //public List<Coord>? coords { get; set; } = new List<Coord>();
    public string? kindname { get; set; }
    public string? name { get; set; }
    public string? searchstr { get; set; }
    public int? shapeid { get; set; }

    [Number(Index = true)]
    public decimal Lng { get; set; } = 0;

    [Number(Index = true)]
    public decimal Lat { get; set; } = 0;

    [GeoPoint]
    public GeoLocation Location { get; set; }

    public int? ProvinceID { get; set; }
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

    public RoadNameMerge(RoadNameMerge other)
    {
        address = other.address;
        //coords = other.coords;
        kindname = other.kindname;
        name = other.name;
        searchstr = other.searchstr;
        shapeid = other.shapeid;
        Location = other.Location;
        ProvinceID = other.ProvinceID;
        nameAscii = other.nameAscii;
        TypeArea = other.TypeArea;
    }

    public RoadNameMerge(RoadNamePush roadName)
    {
        if (!string.IsNullOrEmpty(roadName.RoadName))
            address = roadName.RoadName;
        if (!string.IsNullOrEmpty(roadName.ProvinceName))
            address += ", " + roadName.ProvinceName;

        //coords.Add(new Coord(roadName?.Lat??0, roadName?.Lng??0));
        kindname = string.Empty;

        name = roadName?.RoadName??"";
       
        searchstr = string.Empty;
        shapeid = 2;// Đường

        Lng = roadName?.Lng??0;
        Lat = roadName?.Lat??0;

        Location = new GeoLocation((double)roadName?.Lat, (double)roadName.Lng);

        ProvinceName = roadName.ProvinceName;
        ProvinceID = roadName.ProvinceID;
        nameAscii = LatinToAscii.Latin2Ascii(roadName?.RoadName ?? "");
        TypeArea = roadName?.TypeArea??0;

        Location = new GeoLocation((double)Lat, (double)Lng);

        KeywordsNoExt = roadName?.RoadName?.ToLower() ?? "";

        if (!string.IsNullOrEmpty(roadName?.NameExt))
        {
            Keywords = roadName?.RoadName?.ToLower().Replace("tp.","").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim()
                + " , " + roadName?.NameExt?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();
        }
        else
        {
            Keywords = roadName?.RoadName?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim() ?? "";
        }

        if (!string.IsNullOrEmpty(roadName?.Address))
        {
            Keywords += " , " + roadName?.Address?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();
            KeywordsNoExt += " , " + roadName?.Address?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();
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

    public RoadNameMerge(PointOfInterest point)
    {
        if (!string.IsNullOrEmpty(point.Info))
            address = point.Info;

        kindname = string.Empty;

        name = point?.Name ?? "";
       
        searchstr = string.Empty;
        shapeid = 1;// Điểm

        Lng = point?.Lng ?? 0;
        Lat = point?.Lat ?? 0;

        Location = new GeoLocation((double)point?.Lat, (double)point.Lng);

        KeywordsNoExt = point?.Name?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim() ?? "";

        ProvinceName = point?.ProvinceName??"";
        ProvinceID = point?.ProvinceID??0;

        nameAscii = LatinToAscii.Latin2Ascii(point?.Name ?? "");
        TypeArea = point?.TypeArea??0;

        Keywords = point?.Name?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim() + " , "
            + point?.ProvinceName?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();
        KeywordsNoExt = point?.Name?.ToLower().Replace("tp.", "").Replace("h.", "").Replace("q.", "").Replace("tx.", "").Trim();

        KeywordsAscii = LatinToAscii.Latin2Ascii(Keywords);
        KeywordsAsciiNoExt = LatinToAscii.Latin2Ascii(KeywordsNoExt ?? "");

        //Keywords = point?.Name?.ToLower() ?? "";
        //KeywordsNoExt = point?.Name?.ToLower() ?? "";

        //KeywordsAscii = LatinToAscii.Latin2Ascii(Keywords);
        //KeywordsAsciiNoExt = LatinToAscii.Latin2Ascii(KeywordsNoExt ?? "");
    }
}

public class Coord
{
    public decimal lat { get; set; }
    public decimal lng { get; set; }

    public Coord(decimal lattitude, decimal longtitude)
    {
        lat = lattitude;
        lng = longtitude;
    }
}