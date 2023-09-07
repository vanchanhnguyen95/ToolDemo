using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models;
using BAGeocoding.Api.Services;
using BAGeocoding.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BAGeocoding.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class GeocodeController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IGeoService _geoService;
        private readonly IRoadNameService _roadNameService;
        public GeocodeController(IGeoService geoService, IRegionService regionService, IRoadNameService roadNameService)
        {
            _regionService = regionService;
            _roadNameService = roadNameService;
            _geoService = geoService;
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        [Route("AddressByGeo")]
        public async Task<Result<object>> AddressByGeo([FromBody] AddressByGeoVm? body)
        {
            return await _geoService.AddressByGeoAsyncV2(body?.lng, body?.lat); ;
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        [Route("GeoByAddress")]
        public async Task<Result<object>> GeoByAddress([FromBody] GeoByAddressVm? body)
        {
            return await _regionService.GeoByAddressAsync(body?.address, "vn");
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("GetDataSuggestion")]
        public async Task<IActionResult> GetDataSuggestion(double lat = 0, double lng = 0, string distance = "100km", int size = 5, string keyword = null)
        {
            return Ok(await _roadNameService.GetDataSuggestion(lat, lng, distance, size, keyword));
        }
    }
}
