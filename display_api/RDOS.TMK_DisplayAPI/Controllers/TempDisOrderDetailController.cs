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
    public class TempDisOrderDetailController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITempDisOrderDetailService _tempDisOrderDetailService;
        public TempDisOrderDetailController(ITempDisOrderDetailService tempDisOrderDetailService, IMapper mapper)
        {
            _tempDisOrderDetailService = tempDisOrderDetailService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Temp SettlementDetail Async
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetTempSettlementDetailAsync")]
        public IActionResult GetTempSettlementDetailAsync([FromBody] TempDisOrderHeaderParameters parameters)
        {
            try
            {
                var featureListTemp = _tempDisOrderDetailService.GetTempSettlementDetailAsync(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisSettlementDetailModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisSettlementDetailModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisSettlementDetailModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.DistributorCode).ThenBy(x => x.ProductName);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisSettlementDetailModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new DisSettlementDetailListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisSettlementDetailModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new DisSettlementDetailListModel { Items = result, MetaData = result.MetaData });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisSettlementError.ListSettlementFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}