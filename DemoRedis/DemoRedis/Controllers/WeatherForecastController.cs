using DemoRedis.Attributes;
using DemoRedis.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IResponseCacheService _responseCacheService;

        public class AddressByGeoVm
        {
            public string? lat { get; set; }
            public string? lng { get; set; }
        }

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IResponseCacheService responseCacheService)
        {
            _logger = logger;
            _responseCacheService = responseCacheService;
        }

        [HttpGet]
        [Route("getall")]
        [Cache(1000)]
        public async Task<IActionResult> GetAsync(string keyword, int pageIndex, int pageSize)
        {
            var result = new List<WeatherForecast>();
            await Task.Run(() =>
            {
                result = new List<WeatherForecast>()
                {
                    new WeatherForecast() { Name = "Nguyen Van Chanh 001"},
                    new WeatherForecast() { Name = "Nguyen Van Chanh 002"},
                    new WeatherForecast() { Name = "Nguyen Van Chanh 003"},
                    new WeatherForecast() { Name = "Nguyen Van Chanh 004"},
                    new WeatherForecast() { Name = "Nguyen Van Chanh 005"}
                };
               
            });
            return Ok(result);

        }

        [HttpPost]
        [Route("post")]
        [Cache(1000)]
        public async Task<IActionResult> PostAsync([FromBody] AddressByGeoVm addressByGeoVm)
        {
            var result = new List<AddressByGeoVm>();
            await Task.Run(() =>
            {
                result = new List<AddressByGeoVm>()
                {
                    new AddressByGeoVm() { lat = "1" , lng = "1"},
                    new AddressByGeoVm() { lat = "2" , lng = "2"},
                    new AddressByGeoVm() { lat = "3" , lng = "3"},
                   
                };

            });
            return Ok(addressByGeoVm);
        }

        [HttpGet]
        [Route("create")]
        [Cache(1)]
        public async Task<IActionResult> CreateAsync()
        {
            //await _responseCacheService.RemoveCacheResponseAsync(HttpContext.Request.Path);
            await _responseCacheService.RemoveCacheResponseAsync("/WeatherForecast/");
            var result = new List<AddressByGeoVm>();
            return Ok(result);
        }
    }
}