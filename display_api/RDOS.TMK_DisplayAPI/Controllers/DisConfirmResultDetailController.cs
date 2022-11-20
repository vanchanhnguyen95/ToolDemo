using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using static Sys.Common.Constants.ErrorCodes;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DisConfirmResultDetailController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDisConfirmResultDetailService _disConfirmResultDetailService;
        public DisConfirmResultDetailController(IDisConfirmResultDetailService disConfirmResultDetailService, IMapper mapper)
        {
            _disConfirmResultDetailService = disConfirmResultDetailService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List Result Detail By Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListResultDetailByCodeAsync/{code}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListResultDetailByCodeAsync(string code, EcoParameters parameters)
        {
            try
            {
                var featureListTemp = _disConfirmResultDetailService.GetListConfirmResultDetailByResultCodeAsync(code);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisConfirmResultDetailDisplayModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisConfirmResultDetailDisplayModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisConfirmResultDetailDisplayModel, bool> filterExpression = filterExpressionTemp.Result;

                    var checkCondition = featureListTemp.Where(filterExpression);
                    featureListTemp = checkCondition.AsQueryable();
                }

                // Check Orderby
                if (parameters.OrderBy != null && parameters.OrderBy.Trim() != string.Empty && parameters.OrderBy.Trim() != "NA_EMPTY")
                {
                    featureListTemp = featureListTemp.OrderBy(parameters.OrderBy);
                }
                else
                {
                    featureListTemp = featureListTemp.OrderBy(x => x.DisConfirmResultCode);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisConfirmResultDetailDisplayModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ConfirmResultDetailListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisConfirmResultDetailDisplayModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new ConfirmResultDetailListModel { Items = result, MetaData = result.MetaData });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.ListDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

    }
}