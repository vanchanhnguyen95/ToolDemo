using BAGeocoding.Utility;
using Nest;
using System.ComponentModel;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BAGeocoding.Api.Models.PBD;

public class RoadNamePush
{
    [Number(Index = true)]
    public int RoadID { get; set; } = 0;

    [Number(Index = true)]
    public int ProvinceID { get; set; } = 0;

    [Text(Index = true, Fielddata = true)]
    public string? RoadName { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? NameExt { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? Address { get; set; } = string.Empty;

    [Number(Index = true)]
    public decimal Lng { get; set; } = 0;

    [Number(Index = true)]
    public decimal Lat { get; set; } = 0;

    public string? ProvinceName { get; set; } = string.Empty;
    public string? ProvinceNameAscii { get; set; } = string.Empty;

    public int TypeArea { get; set; } = 0;//0: Điểm hoặc đường thông thường, 1: ngõ, 2: ngách, 3: hẻm
}

[ElasticsearchType(IdProperty = nameof(Id)), Description("roadname-ext")]
public class RoadName : RoadNamePush
{
    [GeoPoint]
    public GeoLocation Location { get; set; }

    [Text(Index = true, Fielddata = true)]
    public string? AddressLower { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string Keywords { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsAscii { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsNoExt { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsAsciiNoExt { get; set; } = string.Empty;

    public int Priority { get; set; } = 99;

    public RoadName(RoadNamePush other)
    {
        RoadID = other.RoadID;
        ProvinceID = other.ProvinceID;
        RoadName = other.RoadName;
        NameExt = other.NameExt;
        Address = other.Address;
        AddressLower = other?.Address?.ToLower();
        Lng = other?.Lng ?? 0;
        Lat = other?.Lat ?? 0;
        KeywordsNoExt = other?.RoadName;

        if (other?.ProvinceID == 16)
        {
            Priority = 0;
        }
        else if (other?.ProvinceID == 50)
        {
            Priority = 1;
        }
        else if (other?.ProvinceID == 32)
        {
            Priority = 2;
        }
        else if (other?.ProvinceID == 20)
        {
            Priority = 3;
        }
        else if (other?.ProvinceID == 56)
        {
            Priority = 4;
        }

        //Location = other.Lat.ToString() + ", " + other.Lng.ToString();
        Location = new GeoLocation((double)Lat, (double)Lng);
        if (!string.IsNullOrEmpty(other?.NameExt))
        {
            Keywords = other?.RoadName?.ToString().ToLower() + " , " + other?.NameExt?.ToString().ToLower();
        }
        else
        {
            Keywords = other?.RoadName?.ToString().ToLower() ?? "";
            //Keywords = other.RoadName + " , ";
        }

        if (!string.IsNullOrEmpty(other?.Address))
        {
            Keywords += " , " + other?.Address?.ToString().ToLower();
            KeywordsNoExt += " , " + other?.Address?.ToString().ToLower();
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

public class RoadNameOut
{
    public string? RoadName { get; set; } = string.Empty;

    public string? NameExt { get; set; } = string.Empty;

    public string? Address { get; set; } = string.Empty;

    public decimal Lng { get; set; } = 0;
    public decimal Lat { get; set; } = 0;

    public RoadNameOut() { }

    public RoadNameOut(RoadName orther)
    {
        RoadName = orther.RoadName;
        NameExt = orther.NameExt;
        Address = orther.Address;
        Lng = orther.Lng;
        Lat = orther.Lat;
    }

    public RoadNameOut(RoadNameV2 orther)
    {
        RoadName = orther.RoadName;
        NameExt = orther.NameExt;
        Address = orther.Address;
        Lng = orther.Lng;
        Lat = orther.Lat;
    }
}

//[ElasticsearchType(IdProperty = nameof(Id)), Description("roadname")]
[ElasticsearchType(IdProperty = nameof(Id)), Description("roadname-extv2")]
//[ElasticsearchType(IdProperty = nameof(Id)), Description("roadname-ext")]
public class RoadNameV2 : RoadNamePush
{
    [GeoPoint]
    public GeoLocation Location { get; set; }

    [Text(Index = true, Fielddata = true)]
    public string? AddressLower { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string Keywords { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsAscii { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsNoExt { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsAsciiNoExt { get; set; } = string.Empty;

    public int? TypeArea { get; set; } = 0;//0: Khác Ngõ Ngách, Đường thông thường, 1: Ngõ, 2: Ngách: 

    public RoadNameV2(RoadNamePush other)
    {
        RoadID = other.RoadID;
        ProvinceID = other.ProvinceID;
        RoadName = other.RoadName;
        NameExt = other.NameExt;
        Address = other.Address;
        AddressLower = other?.Address?.ToLower();
        Lng = other?.Lng ?? 0;
        Lat = other?.Lat ?? 0;
        KeywordsNoExt = other?.RoadName;
        TypeArea = 0;

        string roadNo = LatinToAscii.Latin2Ascii(other?.RoadName?.ToLower().Trim()??"");

        if (roadNo.StartsWith("ngo"))
        {
            TypeArea = 1;
        }
        else if(roadNo.StartsWith("ngach"))
        {
            TypeArea = 2;
        }    

        Location = new GeoLocation((double)Lat, (double)Lng);
        if (!string.IsNullOrEmpty(other?.NameExt))
        {
            Keywords = other?.RoadName?.ToString().ToLower() + " , " + other?.NameExt?.ToString().ToLower();
        }
        else
        {
            Keywords = other?.RoadName?.ToString().ToLower() ?? "";
        }

        if (!string.IsNullOrEmpty(other?.Address))
        {
            Keywords += " , " + other?.Address?.ToString().ToLower();
            KeywordsNoExt += " , " + other?.Address?.ToString().ToLower();
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

public class DataVT
{
    public string? _index { get; set; }
    public string? _type { get; set; }
    public string? _id { get; set; }
    public double? _score { get; set; }
    public Source _source { get; set; }

    //public string Id { get; set; }
    //public string Name { get; set; }
    //public string FormattedAddress { get; set; }
    //public double Latitude { get; set; }
    //public double Longitude { get; set; }
    //public string LatLng { get; set; }
    //public int Provider { get; set; }

    //public RoadNameVT(string id, string name, string formattedAddress, double latitude, double longitude, string latLng, int provider)
    //{
    //    Id = id;
    //    Name = name;
    //    FormattedAddress = formattedAddress;
    //    Latitude = latitude;
    //    Longitude = longitude;
    //    LatLng = latLng;
    //    Provider = provider;
    //}
}

public class Source
{
    public string id { get; set; }
    public string name { get; set; }
    public string formattedAddress { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string latLng { get; set; }
    public int provider { get; set; }

    public int provinceid { get; set; } = 0;
}

[ElasticsearchType(IdProperty = nameof(Id)), Description("roadnamevt"),]
public class RoadNameVT : RoadNamePush
{
    [GeoPoint]
    public GeoLocation Location { get; set; }

    [Text(Index = true, Fielddata = true)]
    public string? AddressLower { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string Keywords { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsAscii { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsNoExt { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? KeywordsAsciiNoExt { get; set; } = string.Empty;

    public RoadNameVT(RoadNamePush other)
    {
        RoadID = other.RoadID;
        ProvinceID = other.ProvinceID;
        RoadName = other.RoadName;
        NameExt = other.NameExt;
        Address = other.Address;
        AddressLower = other?.Address?.ToLower();
        Lng = other?.Lng ?? 0;
        Lat = other?.Lat ?? 0;
        KeywordsNoExt = other?.RoadName;

        //Location = other.Lat.ToString() + ", " + other.Lng.ToString();
        Location = new GeoLocation((double)Lat, (double)Lng);
        if (!string.IsNullOrEmpty(other?.NameExt))
        {
            Keywords = other?.RoadName?.ToString().ToLower() + " , " + other?.NameExt?.ToString().ToLower();
        }
        else
        {
            Keywords = other?.RoadName?.ToString().ToLower() ?? "";
            //Keywords = other.RoadName + " , ";
        }

        if (!string.IsNullOrEmpty(other?.Address))
        {
            Keywords += " , " + other?.Address?.ToString().ToLower();
            KeywordsNoExt += " , " + other?.Address?.ToString().ToLower();
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

    public RoadNameVT(Source source)
    {
        //RoadID = source.RoadID;
        ProvinceID = source.provinceid;
        RoadName = source.name;
        NameExt = source.name;
        Address = source.name;
        AddressLower = source.name?.ToLower();
        Lng = (decimal)source.longitude;
        Lat = (decimal)source.latitude;
        KeywordsNoExt = source?.name;


        Location = new GeoLocation((double)Lat, (double)Lng);
        Keywords = source.name;

        KeywordsAscii = LatinToAscii.Latin2Ascii(Keywords);
        KeywordsAsciiNoExt = LatinToAscii.Latin2Ascii(KeywordsNoExt ?? "");
    }
}

