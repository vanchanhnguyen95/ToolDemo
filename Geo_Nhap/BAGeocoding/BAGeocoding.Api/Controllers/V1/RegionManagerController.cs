using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models;
using BAGeocoding.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BAGeocoding.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class RegionManagerController : BaseController
    {
        private readonly IRegionService _service;

        public RegionManagerController(IRegionService geoService)
        {
            _service = geoService;
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("BulkAsyncProvince")]
         public async Task<string> BulkAsyncProvince()
        {
            return await _service.BulkAsyncProvince();
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("BulkAsyncDistrict")]
        public async Task<string> BulkAsyncDistrict()
        {
            return await _service.BulkAsyncDistrict();
        }

        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //[Route("GetKeySearch")]
        //public async Task<string> GetKeySearch(string keyword)
        //{
        //    return await _service.GetKeySearch(keyword);
        //}

        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //[Route("GetProvince")]
        //public async Task<Result<object>> GetProvince(string keyword)
        //{
        //    return await _service.GetProvince(keyword);
        //}

        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //[Route("GetBAGSearchKey")]
        //public async Task<Result<object>> GetBAGSearchKey(string keyword)
        //{
        //    return await _service?.GetBAGSearchKey(keyword);
        //}

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GeoByAddress")]
        public async Task<Result<object>> GeoByAddress([FromBody] GeoByAddressVm? body)
        {
            return await _service.GeoByAddressAsync(body?.address, "vn");
        }


    }
}
