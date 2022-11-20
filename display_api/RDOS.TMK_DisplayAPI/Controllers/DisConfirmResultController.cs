using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis;
using Sys.Common.Constants;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DisConfirmResultController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDisConfirmResultService _resultService;
        public DisConfirmResultController(IDisConfirmResultService resultService, IMapper mapper)
        {
            _resultService = resultService;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListConfirmResult")]
        public async Task<IActionResult> GetListConfirmResult([FromBody] EcoParameters parameters)
        {
            var disConfirmResults = await _resultService.GetListConfirmResultAsync();
            disConfirmResults = disConfirmResults.Where(await parameters.Predicate<DisConfirmResult>());

            var result = await (parameters.HasOrderBy
                ? disConfirmResults.OrderBy(parameters.OrderBy)
                : disConfirmResults.OrderBy(x => x.Code))
                .ToPaginatedAsync(x =>
                _mapper.Map<DisConfirmResultDisplayModel>(x),
                parameters.PageNumber, parameters.PageSize, parameters.IsDropdown);

            return Ok(BaseResultModel.Success(new ConfirmResultListModel(result)));
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListConfirmResultView")]
        public async Task<IActionResult> GetListConfirmResultViewAsync([FromBody] EcoParameters parameters)
        {
            var disConfirmResults = await _resultService.GetListConfirmResultViewAsync();

            // check searching
            if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
            {
                var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisConfirmResultsModel).Assembly);
                var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisConfirmResultsModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                Func<DisConfirmResultsModel, bool> filterExpression = filterExpressionTemp.Result;

                var checkCondition = disConfirmResults.Where(filterExpression);
                disConfirmResults = checkCondition.AsQueryable();
            }

            // Check Orderby
            if (parameters.HasOrderBy)
            {
                disConfirmResults = disConfirmResults.OrderBy(parameters.OrderBy);
            }
            else
            {
                disConfirmResults = disConfirmResults.OrderBy(x => x.Code);
            }

            int totalCount = disConfirmResults.Count();
            int skip = parameters.Skip ?? 0;
            int top = parameters.Top ?? parameters.PageSize;
            var items = disConfirmResults.Skip(skip).Take(top).ToList();
            var result = new PagedList<DisConfirmResultsModel>(items, totalCount, (skip / top) + 1, top);

            return Ok(BaseResultModel.Success(new ListDisConfirmResultModel { Items = result, MetaData = result.MetaData }));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetConfirmResultByCode/{code}")]
        public async Task<IActionResult> GetConfirmResultByCode([FromRoute] string code)
        {   
            var disConfirmResul = await _resultService.GetConfirmResultByCodeAsync(code);
            return Ok(BaseResultModel.Success(disConfirmResul));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetConfirmResultByDisplayCode/{DisCode}")]
        public async Task<IActionResult> GetConfirmResultByDisplayCode([FromRoute] string DisCode)
        {
            var disConfirmResul = await _resultService.GetConfirmResultByDisplayCodeAsync(DisCode);
            var featureListTempPagged1 = PagedList<DisConfirmResultDisplayModel>.ToPagedList(disConfirmResul.ToList(), 0, disConfirmResul.Count);
            return Ok(BaseResultModel.Success(new ConfirmResultListModel(featureListTempPagged1)));
        }

        /// <summary>
        /// Get ConfirmResult By Display Code SaleCalendar
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetConfirmResultByDisplayCodeSaleCalendar")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetConfirmResultByDisplayCodeSaleCalendar(TempDisplayConfirmRequestModel parameters)
        {
            try
            {
                var result = await _resultService.GetConfirmResultByDisplayCodeSaleCalendar(parameters);
                return Ok(BaseResultModel.Success(result));
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }

        /// <summary>
        /// Delete Confirm Result
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteConfirmResult")]
        [MapToApiVersion("1.0")]
        public IActionResult DeleteConfirmResult(DisConfirmResultDisplayModel input)
        {
            try
            {
                string userlogin = string.Empty;
                // Check Exist Function Code
                var existItem = _resultService.GetConfirmResultByCode(input.Code);
                if (existItem == null)
                {
                    return Ok(BaseResultModel.Fail(ErrorCodes.DuplicateCode));
                }
                else
                {
                    input.Id = existItem.Id;
                    input.CreatedDate = existItem.CreatedDate;
                    input.CreatedBy = existItem.CreatedBy;
                    input.UpdatedDate = existItem.UpdatedDate;
                    input.UpdatedBy = existItem.UpdatedBy;
                    _resultService.DeleteConfirmResult(input, userlogin);
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = true
                    });
                }

            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }

        /// <summary>
        /// Create Update Confirm Display Confirm Result
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateUpdateConfirmResult")]
        [MapToApiVersion("1.0")]
        public IActionResult CreateUpdateConfirmResult(DisConfirmResultsModel input)
        {
            try
            {
                string userlogin = string.Empty;
                // Check Exist Function Code
                var existItem = _resultService.GetConfirmResultByCode(input.Code);
                if (existItem == null)
                {
                    _resultService.CreateDisConfirmResult(input, userlogin);
                    return Ok(BaseResultModel.Success("CreateSuccess"));
                }
                else
                {
                    input.Id = existItem.Id;
                    input.CreatedDate = existItem.CreatedDate;
                    input.CreatedBy = existItem.CreatedBy;
                    _resultService.UpdateDisConfirmResult(input, userlogin);
                    return Ok(BaseResultModel.Success("UpdateSuccess"));
                }
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}