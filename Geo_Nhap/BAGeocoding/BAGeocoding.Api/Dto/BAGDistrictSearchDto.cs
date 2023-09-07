using Nest;
using System.ComponentModel;

namespace BAGeocoding.Api.Dto;


[ElasticsearchType(IdProperty = nameof(Id)), Description("districtsearchv2")]
public class BAGDistrictSearchDto
{
    [Number(Index = true)]
    public short DistrictID { get; set; }
    [Text(Index = true, Fielddata = true)]
    public short ProvinceID { get; set; }
    [Text(Index = true, Fielddata = true)]
    public string? VName { get; set; }
    [Text(Index = true, Fielddata = true)]
    public string? EName { get; set; }
}
