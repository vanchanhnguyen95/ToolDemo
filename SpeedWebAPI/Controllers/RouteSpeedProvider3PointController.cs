using Microsoft.AspNetCore.Mvc;
using SpeedWebAPI.Services;
using SpeedWebAPI.ViewModels;
using System.Threading.Tasks;

namespace SpeedWebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class RouteSpeedProvider3PointController : ControllerBase
	{

        private readonly ISpeedLimit3PointService _service;

        public RouteSpeedProvider3PointController(ISpeedLimit3PointService service)
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
            var data = await _service.GetSpeedProviders3Point(limit);
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
            var data = await _service.UpdateSpeedLimitPush3Point(speedLimitParams);
            return Ok(data);
        }
    }
}
