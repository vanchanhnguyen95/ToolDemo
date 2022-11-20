using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static Sys.Common.Constants.ErrorCodes;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TempDisConfirmResultDetailController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITempDisConfirmResultDetailService _tempDisConfirmResultDetailService;
        public TempDisConfirmResultDetailController(ITempDisConfirmResultDetailService tempDisConfirmResultDetailService, IMapper mapper)
        {
            _tempDisConfirmResultDetailService = tempDisConfirmResultDetailService;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListTempDisConfirmResultDetail")]
        public async Task<IActionResult> GetListTempDisConfirmResultDetail([FromBody] EcoParameters parameters)
        {
            var disConfirmResults = await _tempDisConfirmResultDetailService.GetListTempDisConfirmResultDetailAsync();
            disConfirmResults = disConfirmResults.Where(await parameters.Predicate<TempDisConfirmResultDetail>());

            var result = await (parameters.HasOrderBy
                ? disConfirmResults.OrderBy(parameters.OrderBy)
                : disConfirmResults.OrderBy(x => x.CustomerCode))
                .ToPaginatedAsync(x =>
                _mapper.Map<TempDisConfirmResultDetailModel>(x),
                parameters.PageNumber, parameters.PageSize, parameters.IsDropdown);

            return Ok(BaseResultModel.Success(new TempDisConfirmResultDetailListModel(result)));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetListTempDisConfirmResultByDisplayCode/{DisplayCode}/{PeriodCode}")]
        public async Task<IActionResult> GetListTempDisConfirmResultByDisplayCode([FromRoute] string DisplayCode, string PeriodCode)
        {
            try
            {
                var data = await _tempDisConfirmResultDetailService.GetListTempDisConfirmResult(DisplayCode, PeriodCode);

                return Ok(BaseResultModel.Success(data));
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}