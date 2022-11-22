using Microsoft.AspNetCore.Mvc;
using SpeedWebAPI.Services;
using SpeedWebAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        private readonly ISpeedLimitService _service;

        public RouteSpeedProviderController(ISpeedLimitService service)
        {
            _service = service;
        }

        /// <summary>
        ///  Lấy danh sách các điểm cần để lấy tốc độ giới hạn
        /// </summary>
        /// <param name="limit">Giới hạn điểm cần lấy</param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("Get")]
        public async Task<IActionResult> Get(int? limit)
        {
            var data = await _service.GetSpeedProviders(limit);
            return Ok(data);
        }

        /// <summary>
        /// Cập nhật tốc độ giới hạn từ 1 danh sách các tọa độ
        /// </summary>
        /// <param name="speedLimits">Danh sách các điểm cần cập nhật tốc độ</param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [Route("Push")]
        public async Task<IActionResult> Push([FromBody] List<SpeedProviderVm> speedLimits)
        {
            var data = await _service.UpdateListSpeedProvider(speedLimits);
            return Ok(data);
        }

        /// <summary>
        /// Cập nhật tốc độ giới hạn
        /// </summary>
        /// <param name="speedLimit">Đối tượng cần cập nhật tọa độ</param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] SpeedProviderVm speedLimit)
        {
            var data = await _service.Save(speedLimit);
            return Ok(data);
        }
    }
}
