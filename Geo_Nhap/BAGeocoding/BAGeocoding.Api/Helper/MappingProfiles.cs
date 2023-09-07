using AutoMapper;
using BAGeocoding.Api.Dto;
using BAGeocoding.Api.Models.PBD;
using BAGeocoding.Entity.MapObj;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<BAGProvinceSearchDto, BAGProvince>();
        CreateMap<BAGProvince, BAGProvinceSearchDto>();

        CreateMap<BAGDistrictSearchDto, BAGDistrict>();
        CreateMap<BAGDistrict, BAGDistrictSearchDto>();

        CreateMap<RoadNameMergeDto, RoadNameMerge>();
        CreateMap<RoadNameMerge, RoadNameMergeDto>();
    }
}
