using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedWebAPI.Models;
using SpeedWebAPI.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SpeedWebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class FilePointController : ControllerBase
	{

        private readonly ISpeedUploadService _speedUploadService;

        [System.Obsolete]
        public FilePointController(IHostingEnvironment environment, ISpeedUploadService speedUploadService)
        {
            _speedUploadService = speedUploadService;
        }

        // The HTTP post request. The Body size limit is disabled 
        [HttpPost, DisableRequestSizeLimit]
        [MapToApiVersion("1")]
        [Route("UploadFilePoint")]
        public IActionResult UploadFilePoint(IFormFile file)
        {
            IFormFile postedFile = Request.Form.Files[0];

            string linkfileUpload =  _speedUploadService.GetLinkFileUpLoad(postedFile);
            //Send OK Response to Client.
            return Ok(linkfileUpload);
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [Route("Push")]
        public SpeedLimit UpdateFileToServer([FromBody] List<SpeedLimit> speedLimits)
        {
            string textFile = "";

            if (System.IO.File.Exists(textFile))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(textFile))
                {
                    // Write Cach 2
                    string[] arrstr = file.ReadToEnd().Split("\t");

                    //WriteFile(@"E:\Chanh\Data\Data2.txt", arrstr);
                }
            }

            return new SpeedLimit();
        }


        /// <summary>
        /// Ghi file
        /// </summary>
        /// <param name="link"></param>
        /// <param name="arrString"></param>
        private static void WriteFile(string link, string[] arrString)
        {
            StreamWriter streamWriter;
            if (System.IO.File.Exists(link))
            {
                System.IO.File.SetAttributes(link, FileAttributes.Normal);
                streamWriter = new StreamWriter(link);
            }
            else
            {
                streamWriter = new StreamWriter(link);
                System.IO.File.SetAttributes(link, FileAttributes.Normal);
            }

            for (int i = 0; i < arrString.Length - 1; i++)
            {
                streamWriter.WriteLine(arrString[i]);
            }

            streamWriter.Write(arrString[arrString.Length - 1]);
            streamWriter.Close();
            System.IO.File.SetAttributes(link, FileAttributes.ReadOnly);

        }

    }

}
