using DemoRedis.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DemoRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "getall")]
        [Cache(1000)]
        public async Task<IActionResult> GetAsync(string keyword, int pageIndex, int pageSize)
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();

            var result = new List<WeatherForecast>()
            {
                new WeatherForecast() { Name = "Nguyen Van Chanh 001"},
                new WeatherForecast() { Name = "Nguyen Van Chanh 002"},
                new WeatherForecast() { Name = "Nguyen Van Chanh 003"},
                new WeatherForecast() { Name = "Nguyen Van Chanh 004"},
                new WeatherForecast() { Name = "Nguyen Van Chanh 005"}
            };
            return Ok(result);
        }
    }
}