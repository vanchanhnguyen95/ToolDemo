using Microsoft.AspNetCore.Mvc;
using SpeedWebAPI.Services;
using SpeedWebAPI.ViewModels;
using System.Threading.Tasks;

namespace SpeedWebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class RouteSpeedProviderController : ControllerBase
	{

        private readonly ISpeedLimitService _speedLimitService;
        private readonly ISpeedLimit3PointService _speedLimit3PointService;

        public RouteSpeedProviderController(ISpeedLimitService speedLimitService, ISpeedLimit3PointService speedLimit3PointService)
        {
            _speedLimitService = speedLimitService;
            _speedLimit3PointService = speedLimit3PointService;
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
            //var data = await _speedLimitService.GetSpeedProviders(limit);
            var data = await _speedLimit3PointService.GetSpeedProviders3Point(limit);
            return Ok(data);
        }

        /// <summary>
        /// Cập nhật tốc độ giới hạn từ 1 danh sách các tọa độ
        /// </summary>
        /// <param name="speedLimitParams">Danh sách các điểm cần cập nhật tốc độ</param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [Route("Push")]
        public async Task<IActionResult> Push([FromBody] SpeedLimitParams speedLimitParams)
        {
            //var data = await _speedLimitService.UpdateSpeedLimitPush(speedLimitParams);
            var data = await _speedLimit3PointService.UpdateSpeedLimitPush3Point(speedLimitParams);
            return Ok(data);
        }

        [HttpGet]
        [MapToApiVersion("1")]
        [Route("GetSpeedCurrent")]
        public async Task<IActionResult> GetSpeedCurrent(int? limit)
        {
            //var data = await _speedLimitService.GetSpeedCurrent(limit);
            var data = await _speedLimit3PointService.GetSpeedCurrent3Point(limit);
            return Ok(data);
        }
    }
}
