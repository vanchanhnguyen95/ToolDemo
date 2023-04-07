using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedWebAPI.Models.SpeedLimitPQA;
using SpeedWebAPI.Services;
using SpeedWebAPI.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpeedWebAPI.Controllers
{
    [ApiVersion("1")]
    public class SpeedLimitPQAController : BaseController
    {
        private readonly ISpeedLimitPQAService _service;

        public SpeedLimitPQAController(ISpeedLimitPQAService service)
        {
            _service = service;
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [HttpPost("FromFileExcel")]
        public async Task<ImportResponse<List<SpeedLimitPQA>>> FromFileExcel(IFormFile formFile,string routeType, CancellationToken cancellationToken)
        {
            return await _service.ImportFromFileExcel(formFile, routeType, cancellationToken);
        }

        /// <summary>
        ///  Lấy danh sách các điểm cần để lấy tốc độ giới hạn
        /// </summary>
        /// <param name="limit">Giới hạn điểm cần lấy</param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("Points")]
        public async Task<IActionResult> Points(int? limit)
        {
            var data = await _service.GetSpeedProviders(limit);
            return Ok(data);
        }

        /// <summary>
        /// Cập nhật tốc độ giới hạn từ 1 danh sách các tọa độ
        /// </summary>
        /// <param name="speedLimitParams">Danh sách các điểm cần cập nhật tốc độ</param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [Route("SpeedFromDevice")]
        public async Task<IActionResult> SpeedFromDevice([FromBody] SpeedLimitParams speedLimitParams)
        {
            var data = await _service.UpdateSpeedLimitPush(speedLimitParams);
            return Ok(data);
        }
    }
}
