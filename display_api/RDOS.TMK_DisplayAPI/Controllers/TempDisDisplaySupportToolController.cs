using RDOS.TMK_DisplayAPI.Services.Dis;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using static Sys.Common.Constants.ErrorCodes;
using Sys.Common.Constants;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TempDisDisplaySupportToolController : BaseController
    {
        private readonly ITempDisDisplaySupportToolService _tempDisDisplaySupportToolService;

        public TempDisDisplaySupportToolController(ITempDisDisplaySupportToolService tempDisDisplaySupportToolService)
        {
            this._tempDisDisplaySupportToolService = tempDisDisplaySupportToolService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetListDisDisplaySupportTool")]
        public async Task<IActionResult> GetDisDisplaySupportToolList()
        {
            try
            {
                var display = await _tempDisDisplaySupportToolService.GetDisDisplaySupportToolListAsync();
                return Ok(BaseResultModel.Success(display));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    //Code = Convert.ToInt32(DisplayError.ListDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

    }
}
