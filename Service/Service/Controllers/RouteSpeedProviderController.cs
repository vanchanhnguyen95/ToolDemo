using Microsoft.AspNetCore.Mvc;
using Service.Model;
using System.Collections.Generic;

namespace Service.Controllers
{
    //@GET("/api/v1/routespeedprovider/get?limit=100")
    //@POST("/api/v1/routespeedprovider/push")

    //[Route("api/[controller]")]
    //[ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class RouteSpeedProviderController : ControllerBase
    {

        //GET: api/RouteSpeedProvider/5
        [HttpGet("{limit}")]
        public List<SpeedLimit> Get(int? limit)
        {
            //return GetSpeedProviders().Find(e => e.Id == limit);
            if(limit != null)
            {
                return GetSpeedProviders();
            }
            return GetSpeedProviders();
        }

        public SpeedLimit Push([FromBody] SpeedLimit speedLimit)
        {
            // Logic to create new SpeedLimit
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
