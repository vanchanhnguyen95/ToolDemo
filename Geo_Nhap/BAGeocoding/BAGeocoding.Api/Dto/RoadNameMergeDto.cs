using BAGeocoding.Api.Models.PBD;

namespace BAGeocoding.Api.Dto;

public class RoadNameMergeDto
{
    public string? address { get; set; }
    public List<Coord>? coords { get; set; } = new List<Coord>();
    public string? kindname { get; set; }
    public string? name { get; set; }
    public string? searchstr { get; set; }
    public int? shapeid { get; set; }

    public RoadNameMergeDto() { }

    public RoadNameMergeDto(RoadNameMergeDto road)
    { 
    }

    public RoadNameMergeDto(RoadNameMerge roadNameMerge)
    {
        address = roadNameMerge.address;
        coords.Add(new Coord(roadNameMerge.Lat, roadNameMerge.Lng));
        kindname = roadNameMerge.kindname;
        name = roadNameMerge.name;
        shapeid = roadNameMerge.shapeid;
    }
}
