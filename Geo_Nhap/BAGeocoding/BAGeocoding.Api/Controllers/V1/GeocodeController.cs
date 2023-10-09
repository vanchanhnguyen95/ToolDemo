using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models;
using BAGeocoding.Api.Services;
using BAGeocoding.Api.ViewModels;
using BAGeocoding.Entity.Public;
using Microsoft.AspNetCore.Mvc;
using Nest;
using NetTopologySuite.Operation.Distance;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BAGeocoding.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class GeocodeController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IGeoService _geoService;
        private readonly IRoadNameService _roadNameService;
        private readonly IRoadElasticService _roadElasticService;

        public GeocodeController(IGeoService geoService, IRegionService regionService, IRoadNameService roadNameService, IRoadElasticService roadElasticService)
        {
            _regionService = regionService;
            _roadNameService = roadNameService;
            _geoService = geoService;
            _roadElasticService = roadElasticService;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddressByGeo")]
        public async Task<Result<object>> AddressByGeo([FromBody] AddressByGeoVm? body)
        {
            return await _geoService.AddressByGeoAsyncV2(body?.lng, body?.lat); ;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GeoByAddress")]
        public async Task<Result<object>> GeoByAddress([FromBody] GeoByAddressVm? body)
        {
            return await _regionService.GeoByAddressAsync(body?.address, "vn");
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Search")]
        public async Task<IActionResult> Search([FromBody] AutoSugReqV2 body)
        {
            return Ok(await _roadElasticService.SearchV4(body.lat ?? 0, body.lng ?? 0, body?.distance ?? 300000.0, body?.size ?? 5, body.keys, body.shapeid));
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Add2Geo")]
        public async Task<IActionResult> Add2Geo([FromBody] Add2GeoReq? body)
        {
            return Ok(await _roadElasticService.Add2Geo(body?.keys, "vn"));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetDataSuggestion")]
        public async Task<IActionResult> GetDataSuggestion(double lat = 0, double lng = 0, string distance = "100km", int size = 5, string keyword = null)
        {
            return Ok(await _roadNameService.GetDataSuggestion(lat, lng, distance, size, keyword));
        }
    }
}
