using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SpeedWebAPI.Models;

namespace SpeedWebAPI.Controllers
{
    /*
        @GET("/api/v1/routespeedprovider/get?limit=100")
        @POST("/api/v1/routespeedprovider/push")
    */

    [ApiVersion("1")]
    [ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class RouteSpeedProviderController : ControllerBase
	{
        //GET: https://localhost:5001/api/RouteSpeedProvider?limit=100
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("Get")]
        public List<SpeedLimit> Get(int? limit)
        {
            if (limit != null)
            {
                return GetSpeedProviders();
            }
            return GetSpeedProviders();
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [Route("Push")]
        public SpeedLimit Push([FromBody] List<SpeedLimit> speedLimits)
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
