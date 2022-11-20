using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Settlement;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static Sys.Common.Constants.ErrorCodes;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DisSettlementController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDisSettlementService _settlementService;
        public DisSettlementController(IDisSettlementService settlementService, IMapper mapper)
        {
            _settlementService = settlementService;
            _mapper = mapper;
        }

        /// <summary>
        /// GetListSettlementGeneral
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListSettlementGeneral")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListSettlementGeneralAsync()
        {
            try
            {
                var data = _settlementService.GetListSettlementGeneralAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(TpSettlementError.ListSettlementFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListSettlementAsync")]
        public async Task<IActionResult> GetListSettlementAsync([FromBody] EcoParameters parameters)
        {
            var settlements = await _settlementService.GetListSettlementAsync();

            // check searching
            if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
            {
                var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisSettlementModel).Assembly);
                var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisSettlementModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                Func<DisSettlementModel, bool> filterExpression = filterExpressionTemp.Result;

                var checkCondition = settlements.Where(filterExpression);
                settlements = checkCondition.AsQueryable();
            }

            // Check Orderby
            if (parameters.HasOrderBy)
            {
                settlements = settlements.OrderBy(parameters.OrderBy);
            }
            else
            {
                settlements = settlements.OrderBy(x => x.Code);
            }

            int totalCount = settlements.Count();
            int skip = parameters.Skip ?? 0;
            int top = parameters.Top ?? parameters.PageSize;
            var items = settlements.Skip(skip).Take(top).ToList();
            var result = new PagedList<DisSettlementModel>(items, totalCount, (skip / top) + 1, top);

            return Ok(BaseResultModel.Success(new ListDisSettlementModel(result)));
        }

        /// <summary>
        /// Get List Pay Reward By Display Code
        /// </summary>
        /// <param name="displayCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListPayRewardByDisplayCode/{displayCode}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListPayRewardByDisplayCode(string displayCode)
        {
            try
            {
                var featureListTemp = _settlementService.GetListPayRewardByDisplayCode(displayCode);
                var featureListTempPagged1 = PagedList<DisPayRewardDisplayModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());
                return Ok(new ListPayRewardDisplayModel { Items = featureListTempPagged1 });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisSettlementError.ListSalePeriodFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Get Settlement By Code Async
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSettlementByCodeAsync/{code}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetSettlementByCodeAsync([FromRoute] string code)
        {
            try
            {
                var data = await _settlementService.GetSettlementByCodeAsync(code);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisSettlementError.SuggestionCodeFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Create Or Update Settlement
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateOrUpdateSettlement")]
        [MapToApiVersion("1.0")]
        public IActionResult CreateOrUpdateSettlement(DisSettlementModel input)
        {
            try
            {
                string userlogin = string.Empty;
                // Check Exist Function Code
                var existItem = _settlementService.GetSettlementByCode(input.Code);
                if (existItem == null)
                {
                    _settlementService.CreateSettlement(input, userlogin);
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = true
                    });
                }
                else
                {
                    input.Id = existItem.Id;
                    input.CreatedDate = existItem.CreatedDate;
                    input.CreatedBy = existItem.CreatedBy;
                    _settlementService.UpdateSettlement(input, userlogin);
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = true
                    });
                }

            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisSettlementError.UpdateSettlementFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Delete Settlement
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSettlement")]
        [MapToApiVersion("1.0")]
        public IActionResult DeleteSettlement(DisSettlementModel input)
        {
            try
            {
                string userlogin = string.Empty;
                // Check Exist Function Code
                var existItem = _settlementService.GetSettlementByCode(input.Code);
                if (existItem == null)
                {
                    return Ok(BaseResultModel.Fail(ErrorCodes.DeleteFailed));
                }
                else
                {
                    input.Id = existItem.Id;
                    input.CreatedDate = existItem.CreatedDate;
                    input.CreatedBy = existItem.CreatedBy;
                    input.UpdatedDate = existItem.UpdatedDate;
                    input.UpdatedBy = existItem.UpdatedBy;
                    _settlementService.DeleteSettlement(input, userlogin);
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = true
                    });
                }

            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisSettlementError.DeleteSettlementFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        #region DisSettlementConfirmByDistributor

        /// <summary>
        /// Get List Settlement
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListSettlementConfirm")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListSettlementConfirm(EcoParameters parameters)
        {
            try
            {
                var featureListTemp = _settlementService.GetListSettlementConfirm();
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisSettlementConfirmModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisSettlementConfirmModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisSettlementConfirmModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.SettlementCode);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisSettlementConfirmModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new DisSettlementConfirmListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisSettlementConfirmModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new DisSettlementConfirmListModel { Items = result, MetaData = result.MetaData });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisSettlementError.GetListSettlementConfirmByDistributorFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Get List Detail Settlement Confirm By Distributor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="distributorCode"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListDetailSettlementConfirmByDistributor/{code}/{distributorCode}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListDetailSettlementConfirmByDistributor(string code, string distributorCode, EcoParameters parameters)
        {
            try
            {
                var featureListTemp = _settlementService.GetListDetailSettlementConfirmByDistributor(code, distributorCode);
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
                    featureListTemp = featureListTemp.OrderBy(x => x.OrdNbr);
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
                    Code = Convert.ToInt32(DisSettlementError.GetListSettlementConfirmByDistributorFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Get List Settlement Confirm By Distributor
        /// </summary>
        /// <param name="distributorCode"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListSettlementConfirmByDistributor/{distributorCode}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListSettlementConfirmByDistributor(string distributorCode, EcoParameters parameters)
        {
            try
            {
                var featureListTemp = _settlementService.GetListSettlementConfirmByDistributor(distributorCode);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisSettlementConfirmModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisSettlementConfirmModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisSettlementConfirmModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.SettlementCode);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisSettlementConfirmModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new DisSettlementConfirmListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisSettlementConfirmModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new DisSettlementConfirmListModel { Items = result, MetaData = result.MetaData });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisSettlementError.GetListSettlementConfirmByDistributorFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Confirm Settlement By Distributor
        /// </summary>
        /// <param name="distributorCode"></param>
        /// <param name="lstInput"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ConfirmSettlementByDistributor/{distributorCode}")]
        [MapToApiVersion("1.0")]
        public IActionResult ConfirmSettlementByDistributor(string distributorCode, List<string> lstInput)
        {
            try
            {
                return Ok(_settlementService.ConfirmSettlementByDistributor(distributorCode, lstInput));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisSettlementError.UpdateSettlementFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        #endregion
    }
}