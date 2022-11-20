using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis;
using RDOS.TMK_DisplayAPI.Services.TempDis;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class TempDisplayController : Controller
    {
        private readonly ITempDisCusShiptoSaleOrQuantityService _tempDisCusShiptoSaleOrQuantityService;
        private readonly ITempDisCusShiptoNotHaveService _tempDisCusShiptoNotHaveService;
        private readonly ITempDisPosmForCusShiptoService _tempDisPosmForCusShiptoService;
        private readonly ITempDisOrderDetailService _tempDisOrderService;

        public TempDisplayController(
            ITempDisCusShiptoSaleOrQuantityService tempDisCusShiptoSaleOrQuantityService,
            ITempDisCusShiptoNotHaveService tempDisCusShiptoNotHaveService,
            ITempDisPosmForCusShiptoService tempDisPosmForCusShiptoService,
            ITempDisOrderDetailService tempDisOrderService
            )
        {
            _tempDisCusShiptoSaleOrQuantityService = tempDisCusShiptoSaleOrQuantityService;
            _tempDisCusShiptoNotHaveService = tempDisCusShiptoNotHaveService;
            _tempDisPosmForCusShiptoService = tempDisPosmForCusShiptoService;
            _tempDisOrderService = tempDisOrderService;
        }

        /// <summary>
        /// GetListTempDisCusShiptoSaleOrQuantity
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListTempDisCusShiptoSaleOrQuantity")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListTempDisCusShiptoSaleOrQuantity(TempDisCustomerShiptoSaleOrQuantitySeachModel parameters)
        {
            try
            {
                var featureListTemp = _tempDisCusShiptoSaleOrQuantityService.GetListTempDisCusShiptoSaleOrQuantity(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(TempDisCustomerShiptoSaleOrQuantityModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<TempDisCustomerShiptoSaleOrQuantityModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<TempDisCustomerShiptoSaleOrQuantityModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.CustomerCode);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<TempDisCustomerShiptoSaleOrQuantityModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new TempDisCustomerShiptoSaleOrQuantityListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<TempDisCustomerShiptoSaleOrQuantityModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new TempDisCustomerShiptoSaleOrQuantityListModel { Items = result, MetaData = result.MetaData });
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }

        /// <summary>
        /// GetListTempDisCusShiptoNotHave
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListTempDisCusShiptoNotHave")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListTempDisCusShiptoNotHave(TempDisCusShiptoNotHaveSearchModel parameters)
        {
            try
            {
                var featureListTemp = _tempDisCusShiptoNotHaveService.GetListTempDisCusShiptoNotHave(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(TempDisCustomerShiptoNotHaveModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<TempDisCustomerShiptoNotHaveModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<TempDisCustomerShiptoNotHaveModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.CustomerCode);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<TempDisCustomerShiptoNotHaveModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new TempDisCustomerShiptoNotHaveListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<TempDisCustomerShiptoNotHaveModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new TempDisCustomerShiptoNotHaveListModel { Items = result, MetaData = result.MetaData });
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }

        /// <summary>
        /// GetListTempDisPosmForCustomerShipto
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListTempDisPosmForCustomerShipto")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListTempDisPosmForCustomerShipto(TempDisPosmForCusShiptoSearchModel parameters)
        {
            try
            {
                var featureListTemp = _tempDisPosmForCusShiptoService.GetListTempDisPosmForCustomerShipto(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(TempDisPosmForCustomerShiptoModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<TempDisPosmForCustomerShiptoModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<TempDisPosmForCustomerShiptoModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.CustomerCode);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<TempDisPosmForCustomerShiptoModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new TempDisPosmForCustomerShiptoListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<TempDisPosmForCustomerShiptoModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new TempDisPosmForCustomerShiptoListModel { Items = result, MetaData = result.MetaData });
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }

        [HttpPost]
        [Route("GetDataSaleOrOutputByDisplayByPeriodCode")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetDataSaleOrOutputByDisplayByPeriodCode(TempDisplaySaleOrOutputRequestModel parameters)
        {
            try
            {
                var result = await _tempDisOrderService.GetDataSaleOrOutputByDisplayByPeriodCodeAsync(parameters);
                return Ok(BaseResultModel.Success(result));
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
