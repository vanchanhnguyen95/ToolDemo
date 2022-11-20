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
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using static Sys.Common.Constants.ErrorCodes;
using Sys.Common.Constants;
using RDOS.TMK_DisplayAPI.Models.Customer;
using System.Collections.Generic;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DisplayController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDisplayService _displayService;
        private readonly IDisBudgetService _disBudgetService;
        private readonly IDisDefinitionStructureService _disDefinitionStructureService;
        private readonly IDisCustomerShiptoService _disCustomerShiptoService;
        public DisplayController(
            IDisplayService displayService,
            IDisBudgetService disBudgetService,
            IDisDefinitionStructureService disDefinitionStructureService,
            IDisCustomerShiptoService disCustomerShiptoService,
            IMapper mapper)
        {
            _displayService = displayService;
            _disBudgetService = disBudgetService;
            _disDefinitionStructureService = disDefinitionStructureService;
            _disCustomerShiptoService = disCustomerShiptoService;
            _mapper = mapper;
        }

        #region Display List
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListDisplayGeneral")]
        public IActionResult GetListDisplayGeneral([FromBody] EcoParameters parameters)
        {
            try
            {
                var featureListTemp = _displayService.GetListDisplayGeneral();
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisplayGeneralModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisplayGeneralModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisplayGeneralModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.Code);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisplayGeneralModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(BaseResultModel.Success(new DisplayGeneralListModel { Items = featureListTempPagged1 }));
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisplayGeneralModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(BaseResultModel.Success(new DisplayGeneralListModel { Items = result, MetaData = result.MetaData }));
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetListDisplayAutoSearchModel")]
        public IActionResult GetListDisplayAutoSearchModel()
        {
            try
            {
                var lstDisplay = _displayService.GetListDisplayAutoSearchModel();
                return Ok(BaseResultModel.Success(lstDisplay));
            }
            catch (Exception ex)
            {
                return Ok(BaseResultModel.Fail(ex.InnerException?.Message ?? ex.Message));
            }
        }

        #endregion
        
        #region General
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListDisplay")]
        public async Task<IActionResult> GetListDisplay([FromBody] EcoParameters parameters)
        {
            try
            {
                var display = await _displayService.GetListDisplayAsync();
                // check searching
                if (parameters.HasQuerySearching)
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisplayPopupModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisplayPopupModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisplayPopupModel, bool> filterExpression = filterExpressionTemp.Result;

                    var checkCondition = display.Where(filterExpression);
                    display = checkCondition.AsQueryable();
                }

                // Check Orderby
                if (parameters.HasOrderBy)
                {
                    display = display.OrderBy(parameters.OrderBy);
                }
                else
                {
                    display = display.OrderBy(x => x.Code);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisplayPopupModel>.ToPagedList(display.ToList(), 0, display.Count());

                    return Ok(BaseResultModel.Success(new ListDisplayPopupModel(featureListTempPagged1)));
                }

                int totalCount = display.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = display.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisplayPopupModel>(items, totalCount, (skip / top) + 1, top);

                return Ok(BaseResultModel.Success(new ListDisplayPopupModel(result)));
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
        [MapToApiVersion("1.0")]
        [Route("GetListDisplayForReport")]
        public async Task<IActionResult> GetListDisplayForReport([FromBody] PopupDisplayEcoParameters parameters)
        {
            try
            {
                var featureListTemp = await _displayService.GetListDisplayForReportAsync(parameters.ListStatus);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisplayPopupModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisplayPopupModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisplayPopupModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderBy(x => x.Code);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisplayPopupModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListDisplayPopupModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisplayPopupModel>(items, totalCount, (skip / top) + 1, top);

                return Ok(new ListDisplayPopupModel(result));
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

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetDisplayByCode/{code}")]
        public async Task<IActionResult> GetDisplayByCode([FromRoute] string code)
        {
            var display = await _displayService.GetDisplayByCodeAsync(code);
            return Ok(BaseResultModel.Success(display));
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetFullDataDetail/{code}/{type}/{adjustmentsCount}")]
        public async Task<IActionResult> GetFullDataDetailAsync([FromRoute] string code, [FromRoute] int type = CommonData.DisplaySetting.TypeBudgetNow, [FromRoute] int adjustmentsCount = 0)
        {
            var display = await _displayService.FindByCodeAsync(code);
            if (display != null)
            {
                display.DisplayCustomerSettings = (await _displayService.GetListDisplayCustomerAttributeByDisplayCode(code));
                display.DisBudgets = _disBudgetService.GetListDisBudgets(code, type, adjustmentsCount);
                display.DisCustomerShiptos = _disCustomerShiptoService.GetListDisCustomerShiptoByDisplayCode(code);
                display.ListDefinitionStructure = (await _disDefinitionStructureService.GetAllDataDisDefinitionStructureListByDisplayCode(code));
            }
            return Ok(display);
        }

        [HttpGet]
        [Route("GetDetailDisplayByCode/{code}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetDetailPromotionByCode(string code)
        {
            try
            {
                var data = _displayService.GetDetailDisplayByCode(code);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(PromotionError.GetDetailPromotionByCodeFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("getbycode/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var display = await _displayService.FindByCodeAsync(code);
            if (display != null)
            {
                display.DisBudgets = _disBudgetService.GetListDisBudgets(code);
                display.DisplayCustomerSettings = (await _displayService.GetListDisplayCustomerAttributeByDisplayCode(code)).Where(x => x.IsApply).ToList();
            }

            return Ok(BaseResultModel.Success(display));
        }

        [HttpDelete]
        [Route("DeleteDisplay/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteDisplay([FromRoute] Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = false,
                        Message = IdIsRequired
                    });
                }

                var display = await _displayService.FindByIdAsync(id.Value);
                if (display == null)
                {
                    return Ok(new BaseResultModel
                    {
                        ObjectGuidId = id.Value,
                        IsSuccess = false,
                        Message = EntityNotFound
                    });
                }

                display.DeleteFlag = 1;
                var raesult = await _displayService.UpdateDisplayAsync(display);
                if (raesult == null)
                {
                    return Ok(new BaseResultModel
                    {
                        ObjectGuidId = id.Value,
                        IsSuccess = false,
                        Message = DeleteFailed
                    });
                }
                else
                {
                    return Ok(new BaseResultModel
                    {
                        ObjectGuidId = id.Value,
                        IsSuccess = true,
                        Message = DeleteSuccess
                    });
                }
            }
            catch
            {
                return Ok(new BaseResultModel
                {
                    ObjectGuidId = id.Value,
                    IsSuccess = false,
                    Message = DeleteFailed
                });
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SaveDisplay")]
        public async Task<IActionResult> SaveDisplay([FromBody] DisDisplay disDisplay)
        {
            try
            {
                // Check Exist Function Code
                var existItemCode = await _displayService.ExistsByCodeAsync(disDisplay.Code);
                if (existItemCode)
                {
                    await _displayService.UpdateDisplayAsync(disDisplay);
                }
                else
                {
                    await _displayService.CreateDisplayAsync(disDisplay);
                }

                return Ok(new BaseResultModel
                {
                    ObjectGuidId = disDisplay.Id,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.UpdateDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPut]
        [Route("ConfirmDisplay/{code}")]
        [MapToApiVersion("1.0")]
        public IActionResult ConfirmDisplay(string code)
        {
            try
            {
                var existItem = _displayService.GetDisplayByCodeAsync(code).Result;
                if (existItem != null)
                {
                    if (_displayService.ConfirmDisplay(code))
                    {
                        return Ok(BaseResultModel.Success("ConfirmDisplaySuccess"));
                    }
                    else
                    {
                        return Ok(BaseResultModel.Success("ConfirmDisplayFailed"));
                    }
                }
                else
                {
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = Convert.ToInt32(DisplayError.ConfirmDisplayFailed),
                        Message = "DisplayCodeIsNotExist"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        #endregion

        #region Scope
        [HttpPost]
        [Route("CreateDisplayScopes")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CreateDisplayScopes([FromBody] DisDisplayModel request)
        {
            try
            {
                var result = await _displayService.CreateDisplayScopes(request);
                if (result)
                {
                    return Ok(BaseResultModel.Success(ErrorCodes.CreateSuccess));
                }

                return Ok(BaseResultModel.Fail(ErrorCodes.CreateFailed));
            }
            catch
            {
                return Ok(BaseResultModel.Fail(ErrorCodes.CreateFailed));
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetListDisplayScopeByDisplayCode/{displayCode}")]
        public async Task<IActionResult> GetListDisplayScopeByDisplayCode([FromRoute] string displayCode)
        {
            try
            {
                var displayScopes = await _displayService.GetListDisplayScopeByDisplayCode(displayCode);
                return Ok(BaseResultModel.Success(displayScopes));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetListDisplayScopeByDisplayCodeFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        #endregion

        #region Applicable Object
        [HttpPost]
        [Route("CreateDisplayApplicableObject")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CreateDisplayApplicableObject([FromBody] DisDisplayModel request)
        {
            try
            {
                var result = await _displayService.CreateDisplayApplicableObject(request);
                if (result)
                {
                    return Ok(BaseResultModel.Success(ErrorCodes.CreateSuccess));
                }

                return Ok(BaseResultModel.Fail(ErrorCodes.CreateFailed));
            }
            catch
            {
                return Ok(BaseResultModel.Fail(ErrorCodes.CreateFailed));
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetListDisplayCustomerAttributeByDisplayCode/{displayCode}")]
        public async Task<IActionResult> GetListDisplayCustomerAttributeByDisplayCode([FromRoute] string displayCode)
        {
            try
            {
                var lstDisplayCustomerAttributes = await _displayService.GetListDisplayCustomerAttributeByDisplayCode(displayCode);
                return Ok(BaseResultModel.Success(lstDisplayCustomerAttributes));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetListDisplayCustomerAttributeByDisplayCodeFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        #endregion

        #region Display Budget
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayCode"></param>
        /// <param name="type"></param>
        /// <param name="adjustmentsCount"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetListDisBudgets/{displayCode}/{type}/{adjustmentsCount}")]
        public IActionResult GetListDisBudgets([FromRoute] string displayCode, [FromRoute] int type = 1, [FromRoute] int adjustmentsCount = 0)
        {
            try
            {
                return Ok(_disBudgetService.GetListDisBudgets(displayCode, type, adjustmentsCount));
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

        /// <summary>
        /// Get List Display Budget For Adjustment
        /// </summary>
        /// <param name="displayCode"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListDisBudgetForAdjustment/{displayCode}/{type}")]
        public IActionResult GetListDisBudgetForAdjustment([FromRoute] string displayCode, [FromRoute] int type, [FromBody] EcoParameters parameters)
        {
            try
            {
                var featureListTemp = _disBudgetService.GetListDisBudgetForAdjustment(displayCode, type);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(BudgetAdjustmentListModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<BudgetAdjustmentListModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<BudgetAdjustmentListModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    featureListTemp = featureListTemp.OrderByDescending(x => x.AdjustmentsCount);
                }

                // Check Dropdown
                if (parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<BudgetAdjustmentListModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListBudgetAdjustmentListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<BudgetAdjustmentListModel>(items, totalCount, (skip / top) + 1, top);

                return Ok(new ListBudgetAdjustmentListModel() {Items = result, MetaData= result.MetaData });
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

        /// <summary>
        /// Delete Display Budgets
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteDisBudgets")]
        [MapToApiVersion("1.0")]
        public IActionResult DeleteDisBudgets(DeleteDisBudgetsModel input)
        {
            try
            {
                var existItem = _displayService.GetDisplayByCodeAsync(input.DisplayCode).Result;
                if (existItem != null)
                {
                    return Ok(_disBudgetService.DeleteDisBudgets(input));
                }
                else
                {
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                        Message = "DisplayCodeIsNotExist"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Save Display Budget
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveDisBudget")]
        [MapToApiVersion("1.0")]
        public IActionResult SaveDisBudget(DisBudgetModel input)
        {
            try
            {
                var existItem = _displayService.GetDisplayByCodeAsync(input.DisplayCode).Result;
                if (existItem != null)
                {
                    return Ok(_disBudgetService.SaveDisBudget(input, input.UserName));
                }
                else
                {
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                        Message = "DisplayCodeIsNotExist"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Save Display Budgets
        /// </summary>
        /// <param name="lstInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveDisBudgets")]
        [MapToApiVersion("1.0")]
        public IActionResult SaveDisBudgets(List<DisBudgetModel> lstInput)
        {
            try
            {
                foreach (var item in lstInput)
                {
                    if (_displayService.GetDisplayByCodeAsync(item.DisplayCode).Result == null)
                    {
                        return Ok(new BaseResultModel
                        {
                            IsSuccess = false,
                            Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                            Message = "DisplayCodeIsNotExist"
                        });
                    }
                }

                return Ok(_disBudgetService.SaveDisBudgets(lstInput, lstInput[0]?.UserName));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Save DisBudgets For Adjustment
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveDisBudgetsForAdjustment")]
        [MapToApiVersion("1.0")]
        public IActionResult SaveDisBudgetsForAdjustment(DisBudgetForAdjustmentModel input)
        {
            try
            {
                if (_displayService.GetDisplayByCodeAsync(input.DisplayCode).Result == null)
                {
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                        Message = "DisplayCodeIsNotExist"
                    });
                }
                return Ok(_disBudgetService.SaveDisBudgetsForAdjustment(input, input.UserName));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        #endregion

        #region Dis DefinitionStructure
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetListDisDefinitionStructure/{code}")]
        public async Task<IActionResult> GetListDisDefinitionStructure([FromRoute] string code)
        {
            try
            {
                var definitionStructure = await _disDefinitionStructureService.GetDisDefinitionStructureListAsync(code);
                var result = definitionStructure.OrderBy(x => x.LevelCode).ToList();
                return Ok(BaseResultModel.Success(result));
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

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetDisDefinitionStructure/{code}/{levelcode}")]
        public async Task<IActionResult> GetListDisDefinitionStructure([FromRoute] string code, [FromRoute] string levelcode)
        {
            try
            {
                var result = await _disDefinitionStructureService.GetDisDefinitionStructureByCodeAsync(code, levelcode);
                return Ok(BaseResultModel.Success(result));
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

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("SaveDisDefinitionStructure")]
        public async Task<IActionResult> SaveDisDefinitionStructureAsync([FromBody] DisDefinitionStructureModel definitionStructure)
        {
            try
            {
                string message = "";
                string displayCode = definitionStructure.Structure.DisplayCode;
                string levelCode = definitionStructure.Structure.LevelCode;

                // Check Exist Function Code
                var existItemCode = await _disDefinitionStructureService.ExistsByCodeAsync(displayCode, levelCode);
                if (!existItemCode)
                {
                    var resultInsert = await _disDefinitionStructureService.CreateDisDefinitionStructureAsync(definitionStructure);
                    levelCode = resultInsert;
                    message = CreateSuccess;
                }
                else
                {
                    await _disDefinitionStructureService.UpdateDisDefinitionStructureAsync(definitionStructure);
                    message = UpdateSuccess;
                }
                var result = await _disDefinitionStructureService.GetDisDefinitionStructureByCodeAsync(displayCode, levelCode);

                return Ok(new BaseResultModel
                {
                    ObjectGuidId = result.Structure.Id,
                    IsSuccess = true,
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    //Code = Convert.ToInt32(DisplayError.UpdateDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetAllDataDisDefinitionStructureListByDisplayCode/{code}")]
        public async Task<IActionResult> GetAllDataDisDefinitionStructureListByDisplayCode([FromRoute] string code)
        {
            try
            {
                var result = await _disDefinitionStructureService.GetAllDataDisDefinitionStructureListByDisplayCode(code);
                return Ok(BaseResultModel.Success(result));
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
        #endregion Dis DefinitionStructure

        #region Display Customer Shipto
        /// <summary>
        /// Get List Display Customer Shipto
        /// </summary>
        /// <param name="displayCode"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetListDisCustomerShipto/{displayCode}")]
        public IActionResult GetListDisCustomerShipto([FromRoute] string displayCode)
        {
            try
            {
                return Ok(_disCustomerShiptoService.GetListDisCustomerShiptoByDisplayCode(displayCode));
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

        /// <summary>
        /// Delete Display Customer Shiptos
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteDisCustomerShiptos")]
        [MapToApiVersion("1.0")]
        public IActionResult DeleteDisCustomerShiptos(DeleteDisCustomerShiptosModel input)
        {
            try
            {
                var existItem = _displayService.GetDisplayByCodeAsync(input.DisplayCode).Result;
                if (existItem != null)
                {
                    return Ok(_disCustomerShiptoService.DeleteDisCustomerShiptos(input));
                }
                else
                {
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                        Message = "DisplayCodeIsNotExist"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Delete All Display Customer Shipto By DisplayCode
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteAllDisCustomerShiptoByDisplayCode/{displayCode}")]
        [MapToApiVersion("1.0")]
        public IActionResult DeleteAllDisCustomerShiptoByDisplayCode([FromRoute] string displayCode)
        {
            try
            {
                var existItem = _displayService.GetDisplayByCodeAsync(displayCode).Result;
                if (existItem != null)
                {
                    return Ok(_disCustomerShiptoService.DeleteAllDisCustomerShiptoByDisplayCode(displayCode));
                }
                else
                {
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                        Message = "DisplayCodeIsNotExist"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Save Display Customer Shipto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveDisCustomerShipto")]
        [MapToApiVersion("1.0")]
        public IActionResult SaveDisCustomerShipto(DisCustomerShiptoModel input)
        {
            try
            {
                var existItem = _displayService.GetDisplayByCodeAsync(input.DisplayCode).Result;
                if (existItem != null)
                {
                    return Ok(_disCustomerShiptoService.SaveDisCustomerShipto(input, input.UserName));
                }
                else
                {
                    return Ok(new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                        Message = "DisplayCodeIsNotExist"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetDisplayFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListCustomerShiptoByScope")]
        public IActionResult GetListCustomerShiptoByScope([FromBody] CustomerShiptoByScopeSearchModel parameters)
        {
            try
            {
                var featureListTemp = _disCustomerShiptoService.GetListCustomerShiptoByScope(parameters);
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisScopeCustomerShiptoModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisScopeCustomerShiptoModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisScopeCustomerShiptoModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    var featureListTempPagged1 = PagedList<DisScopeCustomerShiptoModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListDisScopeCustomerShiptoModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisScopeCustomerShiptoModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new ListDisScopeCustomerShiptoModel { Items = result, MetaData = result.MetaData });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.GetListCustomerShiptoByScopeFailed),
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
        #endregion

        #region Display Report Header

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetDisplayReportHeader/{code}")]
        public async Task<IActionResult> GetDisplayReportHeaderByCode([FromRoute] string code)
        {
            try
            {
                var result = await _displayService.GetDisplayReportHeaderByCode(code);
                return Ok(BaseResultModel.Success(result));
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

        #endregion Display Report Header

    }
}
