using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SpeedWebAPI.Services;
using System.IO;
using System.Threading.Tasks;

namespace SpeedWebAPI.Controllers
{
    [ApiVersion("1")]
	public class FileSpeedProviderController : BaseController
    {
        private readonly ISpeedProviderFileService _speedProviderService;

        public FileSpeedProviderController(ISpeedProviderFileService speedProviderService)
        {
            _speedProviderService = speedProviderService;
        }

        /// <summary>
        /// Upload File Speed Provider
        /// </summary>
        /// <param name="file">textfile có format .txt</param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [MapToApiVersion("1")]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            IFormFile postedFile = Request.Form.Files[0];
            var result = await _speedProviderService.UpdateListSpeedProvider(postedFile);
            return Ok(result);
        }

        /// <summary>
        /// Get File ListSpeed From File Upload
        /// </summary>
        /// <param name="postedFile">textfile có format .txt</param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [MapToApiVersion("1")]
        [Route("GetFileListSpeed")]
        public async Task<IActionResult> GetFileListSpeedFromFileUpload(IFormFile file)
        {
            IFormFile postedFile = Request.Form.Files[0];

            // ... code for validation and get the file
            var result = await _speedProviderService.GetFileListSpeedFromFileUpd(postedFile);

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
