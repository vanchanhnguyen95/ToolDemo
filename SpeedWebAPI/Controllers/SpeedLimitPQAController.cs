using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedWebAPI.Models.SpeedLimitPQA;
using SpeedWebAPI.Services;
using SpeedWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<ImportResponse<List<SpeedLimitPQA>>> FromFileExcel(IFormFile formFile,int providerType, CancellationToken cancellationToken)
        {
            return await _service.ImportFromFileExcel(formFile, providerType, cancellationToken);
        }

        /// <summary>
        /// Cập nhật vận tốc từ API SOAP Bình Anh
        /// </summary>
        /// <param name="url"></param>
        /// <param name="providerType"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("GetSpeedFromAPISoapBA")]
        public async Task<IActionResult> GetSpeedGetSpeedFromAPISoapBAFromBA( int providerType = 2000)
        {
            var data = await _service.GetSpeedFromAPISoapBA(providerType);
            return Ok(data);
        }

        /// <summary>
        /// Cập nhật vận tốc từ Geocodebulk
        /// </summary>
        /// <param name="url"></param>
        /// <param name="providerType"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("GetSpeedFromGeocodebulk")]
        public async Task<IActionResult> GetSpeedFromGeocodebulk(string url = "http://103.47.194.15:11580/geocodebulk", int providerType = 2000)
        {
            var data = await _service.GetSpeedFromGeocodebulk(url, providerType);
            return Ok(data);
        }

        // HttpGet
        [HttpGet("ExportFromExcel")]
        public async Task<IActionResult> ExportFromExcel(int providerType, CancellationToken cancellationToken)
        {
            // query data from database
            MemoryStream stream = await _service.ExportFromExcel(providerType);

            string fileNameEx = @"Tổng hợp";
            if (providerType == 2000)
            {
                fileNameEx = @"HaTinh";
            }
            else if(providerType == 1000)
            {
                fileNameEx = @"NgheAn";
            }    
               
            string excelName = $"{fileNameEx}-TongHop-{DateTime.Now.ToString("yyyyMMddHH")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
