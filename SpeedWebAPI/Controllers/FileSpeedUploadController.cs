using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedWebAPI.Services;
using System.Threading.Tasks;

namespace SpeedWebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class FileSpeedUploadController : ControllerBase
	{
        private readonly ISpeedUploadService _speedUploadService;

        public FileSpeedUploadController(ISpeedUploadService speedUploadService)
        {
            _speedUploadService = speedUploadService;
        }

        /// <summary>
        /// Upload File Speed Provider
        /// </summary>
        /// <param name="file">textfile có format .txt</param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [MapToApiVersion("1")]
        [Route("UploadFileSpeedProvider")]
        public async Task<IActionResult> UploadFileSpeedProvider(IFormFile file)
        {
            IFormFile postedFile = Request.Form.Files[0];
            var data = await _speedUploadService.UpdateListSpeedProvider(postedFile);
            return Ok(data);
        }
    }

}
