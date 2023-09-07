using Nest;

namespace BAGeocoding.Api.Models.PBD;

public class VTPost
{

    [Text(Index = true, Fielddata = true)]
    public string? _index { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? _type { get; set; } = string.Empty;

    [Text(Index = true, Fielddata = true)]
    public string? _id { get; set; } = string.Empty;

    public float _score { get; set; }

    public SourceObject? _source { get; set; }

}

public class SourceObject
{
    public string? id { get; set; }
    public string? name { get; set; }
    public string? formattedAddress { get; set; }
    public double? latitude { get; set; }
    public double? longitude { get; set; }
    public string? latLng { get; set; }
    public int provider { get; set; }
}
