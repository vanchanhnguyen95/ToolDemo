using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SpeedWebAPI.Services;
using System.IO;
using System.Threading.Tasks;

namespace SpeedWebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class FileSpeedProvider3PointController : ControllerBase
	{
        private readonly ISpeedProviderFile3PointService _service;

        public FileSpeedProvider3PointController(ISpeedProviderFile3PointService service)
        {
            _service = service;
        }

        [HttpPost, DisableRequestSizeLimit]
        [MapToApiVersion("1")]
        [Route("UploadFile3Point")]
        public async Task<IActionResult> UploadFile3Point(IFormFile file)
        {
            IFormFile postedFile = Request.Form.Files[0];
            var result = await _service.UpdateListSpeedProvider3Point(postedFile);
            return Ok(result);
        }


        [HttpPost, DisableRequestSizeLimit]
        [MapToApiVersion("1")]
        [Route("GetFileListSpeed")]
        public async Task<IActionResult> GetFileListSpeedFromFileUpload(IFormFile file)
        {
            IFormFile postedFile = Request.Form.Files[0];

            // ... code for validation and get the file
            var result = await _service.GetFileListSpeedFromFileUpd3Point(postedFile);

            if (string.IsNullOrEmpty(result.FilePath))
                return Ok(result);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(result.FilePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(result.FilePath);
            return File(bytes, contentType, Path.GetFileName(result.FilePath));
        }
    }

}
