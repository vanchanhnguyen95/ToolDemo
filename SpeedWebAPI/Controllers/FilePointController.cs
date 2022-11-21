using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SpeedWebAPI.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace SpeedWebAPI.Controllers
{
    /*
        @GET("/api/v1/routespeedprovider/get?limit=100")
        @POST("/api/v1/routespeedprovider/push")
    */

    [ApiVersion("1")]
    [ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class FilePointController : ControllerBase
	{
        [System.Obsolete]
        private IHostingEnvironment environment;

        [System.Obsolete]
        public FilePointController(IHostingEnvironment _environment)
        {
            environment = _environment;
        }

        // The HTTP post request. The Body size limit is disabled 
        [HttpPost, DisableRequestSizeLimit]
        [MapToApiVersion("1")]
        [Route("UploadFilePoint")]
        public IActionResult UploadFilePoint(IFormFile file)
        {
            //Create the Directory.
            string pathFile = Path.Combine(environment.WebRootPath, "Uploads\\");
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }

            //Fetch the File.
            IFormFile postedFile = Request.Form.Files[0];

            //Fetch the File Name.
            //string fileName = Request.Form["fileName"] +Path.GetExtension(postedFile.FileName);
            string fileName = postedFile.FileName;

            //Save the File.
            using (FileStream stream = new FileStream(Path.Combine(pathFile, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            //Send OK Response to Client.
            return Ok(fileName);
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
            System.IO.StreamWriter streamWriter;
            if (System.IO.File.Exists(link))
            {
                System.IO.File.SetAttributes(link, System.IO.FileAttributes.Normal);
                streamWriter = new System.IO.StreamWriter(link);
            }
            else
            {
                streamWriter = new System.IO.StreamWriter(link);
                System.IO.File.SetAttributes(link, System.IO.FileAttributes.Normal);
            }

            for (int i = 0; i < arrString.Length - 1; i++)
            {
                streamWriter.WriteLine(arrString[i]);
            }

            streamWriter.Write(arrString[arrString.Length - 1]);
            streamWriter.Close();
            System.IO.File.SetAttributes(link, System.IO.FileAttributes.ReadOnly);

        }
    }

}
