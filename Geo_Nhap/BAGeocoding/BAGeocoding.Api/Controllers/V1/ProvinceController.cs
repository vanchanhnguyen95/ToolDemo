using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models.PBD;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BAGeocoding.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ProvinceController : BaseController
    {
        private readonly IProvinceService _provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("BulkAsync")]
        public async Task<IActionResult> BulkAsync(string path = @"D:\Geo\PbdProvince.json")
        {

            if (string.IsNullOrEmpty(path)) path = @"D:\Geo\PbdProvince.json";
            var jsonData = System.IO.File.ReadAllText(path);
            var provinces = JsonConvert.DeserializeObject<List<Province>>(jsonData);

            return Ok(await _provinceService.BulkAsync(provinces ?? new List<Province>()));
        }

        
    }
}
