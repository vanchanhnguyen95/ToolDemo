using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Extensions;
using System;
using System.Linq;
using static Sys.Common.Constants.ErrorCodes;
using System.Linq.Dynamic.Core;
using RDOS.TMK_DisplayAPI.Services.Common;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class ExternalController : BaseController
    {
        private readonly IExternalService _externalService;
        public ExternalController(IExternalService externalService)
        {
            _externalService = externalService;
        }

        /// <summary>
        /// Get Calendar By Type By Date
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCalendarByTypeByDate/{type}/{date}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetCalendarByTypeByDate(string type, string date)
        {
            try
            {
                DateTime RequestDate = Convert.ToDateTime(date);
                var data = _externalService.GetCalendarByTypeByDate(type, RequestDate);
                return Ok(BaseResultModel.Success(data));
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }

        /// <summary>
        /// Get Calendar By Display Code
        /// </summary>
        /// <param name="DisplayCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCalendarByTypeByDisplayCode/{DisplayCode}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetCalendarByTypeByDisplayCode(string DisplayCode)
        {
            try
            {
                var data = _externalService.GetCalendarByTypeByDisplayCode(DisplayCode);
                return Ok(BaseResultModel.Success(data));
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
