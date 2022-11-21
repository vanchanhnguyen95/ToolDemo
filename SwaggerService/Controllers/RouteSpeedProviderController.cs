using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SwaggerService.Models;

namespace SwaggerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteSpeedProviderController : ControllerBase
    {
        //GET: https://localhost:44352/api/RouteSpeedProvider?limit=100
        [HttpGet(Name = "RouteSpeedProvider/Get")]
        public List<SpeedLimit> Get(int? limit)
        {
            if (limit != null)
            {
                return GetSpeedProviders();
            }
            return GetSpeedProviders();
        }

        [HttpPost(Name = "Push")]
        public SpeedLimit Push([FromBody] SpeedLimit speedLimit)
        {
            return new SpeedLimit();
        }

        private List<SpeedLimit> GetSpeedProviders()
        {
            return new List<SpeedLimit>()
            {
                new SpeedLimit()
                {
                    Id = 1,
                    Long = 1,
                    Lat = 1,
                    MinSpeed = 0,
                    MaxSpeed = 50,
                    ProviderType = 1
                },
                new SpeedLimit()
                {
                    Id = 2,
                    Long = 1,
                    Lat = 1,
                    MinSpeed = 0,
                    MaxSpeed = 50,
                    ProviderType = 1
                },
                new SpeedLimit()
                {
                    Id = 3,
                    Long = 1,
                    Lat = 1,
                    MinSpeed = 0,
                    MaxSpeed = 50,
                    ProviderType = 1
                },
                new SpeedLimit()
                {
                    Id = 4,
                    Long = 1,
                    Lat = 1,
                    MinSpeed = 0,
                    MaxSpeed = 50,
                    ProviderType = 1
                },
            };
        }

    }
}
