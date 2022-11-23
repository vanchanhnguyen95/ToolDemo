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

        public RouteSpeedProviderController(ISpeedLimitService speedLimitService)
        {
            _speedLimitService = speedLimitService;
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
            var data = await _speedLimitService.GetSpeedProviders(limit);
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
            var data = await _speedLimitService.UpdateSpeedLimitPush(speedLimitParams);
            return Ok(data);
        }

        /// <summary>
        /// Cập nhật tốc độ giới hạn
        /// </summary>
        /// <param name="speedLimit">Đối tượng cần cập nhật tọa độ</param>
        /// <returns></returns>
        //[HttpPost]
        //[MapToApiVersion("1")]
        //[Route("Save")]
        //public async Task<IActionResult> Save([FromBody] SpeedLimitPush speedLimit)
        //{
        //    var data = await _speedLimitService.Save(speedLimit);
        //    return Ok(data);
        //}
    }
}
