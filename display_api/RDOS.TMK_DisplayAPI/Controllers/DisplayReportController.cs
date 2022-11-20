using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Paging;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using RDOS.TMK_DisplayAPI.Services.Dis.Report;
using static Sys.Common.Constants.ErrorCodes;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using System.Threading.Tasks;
using RDOS.TMK_DisplayAPI.Services.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using Sys.Common.Constants;
using Microsoft.EntityFrameworkCore;
using RDOS.TMK_DisplayAPI.Services.Dis.PayReward;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class DisplayReportController : Controller
    {
        #region Property
        private readonly IDisplayService _displayService;
        private readonly IDisConfirmResultDetailService _confirmResultDetailService;
        private readonly IDisplayProgressReportService _displayProgressReportService;
        private readonly IDisplayDetailReportService _displayDetailReportService;
        private readonly IDisplayListCustomerReportService _displayListCustomerReportService;
        private readonly IDisplayPeriodTrackingReportService _displayPeriodTrackingReportService;
        private readonly IDisplaySyntheticReportSettlementService _displaySyntheticReportSettlementService;
        private readonly IPayRewardService _payRewardService;
        #endregion

        #region Constructor
        public DisplayReportController(
              IDisplayProgressReportService displayProgressReportService,
              IDisplayDetailReportService displayDetailReportService,
              IDisplayListCustomerReportService displayListCustomerReportService,
              IDisplayPeriodTrackingReportService displayPeriodTrackingReportService,
              IDisConfirmResultDetailService confirmResultDetailService,
              IDisplayService displayService,
              IDisplaySyntheticReportSettlementService displaySyntheticReportSettlementService,
              IPayRewardService payRewardService)
        {
            _displayProgressReportService = displayProgressReportService;
            _displayDetailReportService = displayDetailReportService;
            _displayListCustomerReportService = displayListCustomerReportService;
            _displayPeriodTrackingReportService = displayPeriodTrackingReportService;
            _confirmResultDetailService = confirmResultDetailService;
            _displayService = displayService;
            _displaySyntheticReportSettlementService = displaySyntheticReportSettlementService;
            _payRewardService = payRewardService;
        }
        #endregion

        #region Display Progress Report
        [HttpPost]
        [Route("DisplayProgressReport")]
        [MapToApiVersion("1.0")]
        public IActionResult DisplayProgressReport(DisplayReportEcoParameters parameters)
        {
            try
            {
                var featureListTemp = _displayProgressReportService.GetDisplayDetailForDisplayProgressReport(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisplayProgressReportListModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisplayProgressReportListModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisplayProgressReportListModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.Code).ThenBy(x => x.Code).ThenBy(x => x.ShortName).ThenBy(x => x.DisplayLevel);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisplayProgressReportListModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListDisplayProgressReportListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisplayProgressReportListModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new ListDisplayProgressReportListModel { Items = result, MetaData = result.MetaData });
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
        #endregion

        #region DisplayDetailReport
        [HttpPost]
        [Route("DisplayDetailReport")]
        [MapToApiVersion("1.0")]
        public IActionResult DisplayDetailReport(DisplayReportEcoParameters parameters)
        {
            try
            {
                var featureListTemp = _displayDetailReportService.GetDisplayDetailReport(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisplayDetailReportListModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisplayDetailReportListModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisplayDetailReportListModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.DisplayCodeLevel).ThenBy(x => x.DisplayCodeLevel);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisplayDetailReportListModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListDisplayDetailReportListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisplayDetailReportListModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new ListDisplayDetailReportListModel { Items = result, MetaData = result.MetaData });
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
        #endregion

        #region DisplayDetailReport
        [HttpPost]
        [Route("DisplayListCustomerReport")]
        [MapToApiVersion("1.0")]
        public IActionResult DisplayListCustomerReport(DisplayReportEcoParameters parameters)
        {
            try
            {
                var featureListTemp = _displayListCustomerReportService.GetDisplayDetailReport(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisplayListCustomerListModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisplayListCustomerListModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisplayListCustomerListModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.DisplayCodeLevel).ThenBy(x => x.DisplayCodeLevel);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisplayListCustomerListModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListDisplayListCustomerListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisplayListCustomerListModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new ListDisplayListCustomerListModel { Items = result, MetaData = result.MetaData });
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

        [HttpPost]
        [Route("ListCustomerConfirmedReport")]
        [MapToApiVersion("1.0")]
        public IActionResult ListCustomerConfirmedReport(DisplayReportEcoParameters parameters)
        {
            try
            {
                var featureListTemp = _displayListCustomerReportService.GetListCustomerConfirmReport(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(ListCustomerConfirmModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<ListCustomerConfirmModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<ListCustomerConfirmModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.Customer).ThenBy(x => x.CustomerShipto);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<ListCustomerConfirmModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListListCustomerConfirmModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<ListCustomerConfirmModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new ListListCustomerConfirmModel { Items = result, MetaData = result.MetaData });
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
        #endregion

        #region DisplayPeriodTrackingReport
        [HttpPost]
        [Route("DisplayPeriodTrackingReport")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DisplayPeriodTrackingReport(DisplayReportEcoParameters parameters)
        {
            try
            {
                var payRewardDetail = await _payRewardService.GetListDisplayPeriodTrackingReportAsync(parameters.DisplayCode);

                // Check Orderby
                if (parameters.OrderBy != null && parameters.OrderBy.Trim() != string.Empty && parameters.OrderBy.Trim() != "NA_EMPTY")
                {
                    payRewardDetail = payRewardDetail.OrderBy(parameters.OrderBy);
                }
                else
                {
                    payRewardDetail = payRewardDetail.OrderBy(x => x.DisplayCode).ThenBy(x => x.Code);
                }
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisplayPeriodTrackingReportListModel>.ToPagedList(payRewardDetail.ToList(), 0, payRewardDetail.Count());

                    return Ok(BaseResultModel.Success(new ListDisplayPeriodTrackingReportListModel { Items = featureListTempPagged1 }));
                }

                int totalCount = payRewardDetail.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = payRewardDetail.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisplayPeriodTrackingReportListModel>(items, totalCount, (skip / top) + 1, top);

                return Ok(BaseResultModel.Success(new ListDisplayPeriodTrackingReportListModel(result)));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPost]
        [Route("FollowRewardProgressQuantityCustomer")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetListQuantityCustomerAsync(DisplayReportEcoParameters parameters)
        {
            try
            {
                var payRewardDetail = await _payRewardService.GetListQuantityCustomerAsync(parameters.DisplayCode);
                return Ok(BaseResultModel.Success(new ListDisFollowRewardProgressQuantityCustomerModel { Items = payRewardDetail.ToList() }));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,

                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        #endregion DisplayPeriodTrackingReport

        #region DisConfirmReport
        [HttpGet]
        [Route("GetConfirmResultDetailReportGrouped")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetConfirmResultDetailReportGrouped([FromQuery] ConfirmResultDetailReportRequest rq)
        {

				var display = await _displayService.FindByCodeAsync(rq.DisplayCode);
				if (display == null || display.Status != CommonData.DisplaySetting.Implementation 
					 && display.Status != CommonData.DisplaySetting.Closed)
				{
					 return Ok(BaseResultModel.Fail("DisplayNotFound"));
				}

            var details = _confirmResultDetailService.GetConfirmResultDetailGrouped(display, rq.ConfirmResultCode);

            var result = new ConfirmResultDetailReport
            {
                Display = display,
                Total = await details.CountAsync(),
                ConfirmResultDetails = await details.ToPaginatedAsync(rq.PageNumber, rq.PageSize)
            };

            return Ok(BaseResultModel.Success(result));
        }

        [HttpGet]
        [Route("GetConfirmResultDetailsReport")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetConfirmResultDetailsReport(string confirmResultCode, string levelCode, bool passed, bool isIndependent)
        {
            var details = await _confirmResultDetailService.GetConfirmResultDetailsReportAsync(confirmResultCode, levelCode, passed, isIndependent);
            return Ok(BaseResultModel.Success(details));
        }
        #endregion

		  #region PayReward
		  [HttpGet]
		  [MapToApiVersion("1.0")]
		  [Route("GetPayRewardReportHeader")]
		  public async Task<IActionResult> GetPayRewardReportHeader(string confirmResultCode)
		  {
				var result = await _payRewardService.GetPayRewardReportHeaderAsync(confirmResultCode);
				return Ok(BaseResultModel.Success(result));
		  }

		  [HttpGet]
		  [MapToApiVersion("1.0")]
		  [Route("GetPayRewardLevelGrouped")]
		  public async Task<IActionResult> GetPayRewardLevelGrouped(string confirmResultCode)
		  {
				var result = await _payRewardService.GetPayRewardLevelGroupedAsync(confirmResultCode);
				return Ok(BaseResultModel.Success(result));
		  }

		  [HttpGet]
		  [MapToApiVersion("1.0")]
		  [Route("GetPayRewardReport")]
		  public async Task<IActionResult> GetPayRewardReport(string levelCode, string confirmResultCode)
		  {
				var result = await _payRewardService.GetPayRewardReportByLevelAsync(levelCode, confirmResultCode);
				return Ok(BaseResultModel.Success(result));
		  }
		  #endregion PayReward

		  #region DisplaySyntheticReportSettlement
		  [HttpPost]
		  [Route("DisplaySyntheticReportSettlement")]
		  [MapToApiVersion("1.0")]
		  public IActionResult DisplaySyntheticReportSettlement(DisplayReportEcoParameters parameters)
		  {
				try
				{
					 var featureListTemp = _displaySyntheticReportSettlementService.GetDisplayDetailReport(parameters);
					 // check searching
					 if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
					 {
						  var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisplaySyntheticReportSettlementListModel).Assembly);
						  var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisplaySyntheticReportSettlementListModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
						  Func<DisplaySyntheticReportSettlementListModel, bool> filterExpression = filterExpressionTemp.Result;

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
						  //featureListTemp = featureListTemp.OrderBy(x => x.DisplayCodeLevel).ThenBy(x => x.DisplayCodeLevel);
					 }

					 // Check Dropdown
					 if (parameters.IsDropdown)
					 {
						  var featureListTempPagged1 = PagedList<DisplaySyntheticReportSettlementListModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

						  return Ok(new ListDisplaySyntheticReportSettlementListModel { Items = featureListTempPagged1 });
					 }

					 int totalCount = featureListTemp.Count();
					 int skip = parameters.Skip ?? 0;
					 int top = parameters.Top ?? parameters.PageSize;
					 var items = featureListTemp.Skip(skip).Take(top).ToList();
					 var result = new PagedList<DisplaySyntheticReportSettlementListModel>(items, totalCount, (skip / top) + 1, top);
					 return Ok(new ListDisplaySyntheticReportSettlementListModel { Items = result, MetaData = result.MetaData });
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
		  [HttpPost]
		  [Route("GetListDistributorPopupReportSettlement/{settlementCode}")]
		  [MapToApiVersion("1.0")]
		  public IActionResult GetListDistributorPopupReportSettlement(string settlementCode, EcoParameters parameters)
		  {
				try
				{
					 var featureListTemp = _displaySyntheticReportSettlementService.GetListDistributorPopupReportSettlement(settlementCode);
					 // check searching
					 if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
					 {
						  var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DistributorPopupReportSettlementListModel).Assembly);
						  var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DistributorPopupReportSettlementListModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
						  Func<DistributorPopupReportSettlementListModel, bool> filterExpression = filterExpressionTemp.Result;

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
						  featureListTemp = featureListTemp.OrderBy(x => x.DistributorCode);
					 }

					 // Check Dropdown
					 if (parameters.IsDropdown)
					 {
						  var featureListTempPagged1 = PagedList<DistributorPopupReportSettlementListModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

						  return Ok(new ListDistributorPopupReportSettlementListModel { Items = featureListTempPagged1 });
					 }

					 int totalCount = featureListTemp.Count();
					 int skip = parameters.Skip ?? 0;
					 int top = parameters.Top ?? parameters.PageSize;
					 var items = featureListTemp.Skip(skip).Take(top).ToList();
					 var result = new PagedList<DistributorPopupReportSettlementListModel>(items, totalCount, (skip / top) + 1, top);
					 return Ok(new ListDistributorPopupReportSettlementListModel { Items = result, MetaData = result.MetaData });
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
		  #endregion
	 }
}
