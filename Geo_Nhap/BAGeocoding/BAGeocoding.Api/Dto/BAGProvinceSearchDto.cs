using Nest;
using System.ComponentModel;

namespace BAGeocoding.Api.Dto;

[ElasticsearchType(IdProperty = nameof(Id)), Description("provincesearchv2")]
public class BAGProvinceSearchDto
{
    [Number(Index = true)]
    public short ProvinceID { get; set; }
    [Text(Index = true, Fielddata = true)]
    public string? VName { get; set; }
    [Text(Index = true, Fielddata = true)]
    public string? EName { get; set; }
}
