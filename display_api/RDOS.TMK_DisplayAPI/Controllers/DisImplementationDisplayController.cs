using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Sys.Common.Extensions;
using System;
using System.Linq;
using static Sys.Common.Constants.ErrorCodes;
using System.Linq.Dynamic.Core;
using Sys.Common.Models;
using System.Collections.Generic;
using RDOS.TMK_DisplayAPI.Services.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class DisImplementationDisplayController : BaseController
    {
        private readonly IDisImplementationDisplayService _implementationDisplayService;
        public DisImplementationDisplayController(IDisImplementationDisplayService service)
        {
            _implementationDisplayService = service;
        }

        /// <summary>
        /// Get List Display
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListDisplay")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListDisplay(EcoParameters parameters)
        {
            try
            {
                var featureListTemp = _implementationDisplayService.GetListDisplay();
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisDisplayModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisDisplayModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisDisplayModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    var featureListTempPagged1 = PagedList<DisDisplayModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListDisDisplayModelListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisDisplayModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new ListDisDisplayModelListModel { Items = result, MetaData = result.MetaData });
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
        /// Get List Display Code
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListDisplayCode")]
        [MapToApiVersion("1.0")]
        public IActionResult GetListDisplayCode(EcoParameters parameters)
        {
            try
            {
                var featureListTemp = _implementationDisplayService.GetListDisplayCode();
                // check searching
                if (parameters.Filter != null && parameters.Filter.Trim() != string.Empty && parameters.Filter.Trim() != "NA_EMPTY")
                {
                    var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisDisplayModel).Assembly);
                    var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisDisplayModel, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
                    Func<DisDisplayModel, bool> filterExpression = filterExpressionTemp.Result;

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
                    var featureListTempPagged1 = PagedList<DisDisplayModel>.ToPagedList(featureListTemp.ToList(), 0, featureListTemp.Count());

                    return Ok(new ListDisDisplayModelListModel { Items = featureListTempPagged1 });
                }

                int totalCount = featureListTemp.Count();
                int skip = parameters.Skip ?? 0;
                int top = parameters.Top ?? parameters.PageSize;
                var items = featureListTemp.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisDisplayModel>(items, totalCount, (skip / top) + 1, top);
                return Ok(new ListDisDisplayModelListModel { Items = result, MetaData = result.MetaData });
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
        /// Get Display By Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDisplayByCode/{code}")]
        [MapToApiVersion("1.0")]
        public IActionResult GetDisplayByCode(string code)
        {
            try
            {
                var data = _implementationDisplayService.GetDisplayByCode(code);
                return Ok(data);
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
        /// Update Data Display
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateDataDisplay")]
        [MapToApiVersion("1.0")]
        public IActionResult UpdateDataDisplay(DisDisplayUpdModel input)
        {
            try
            {
                // Check Exist Function Code
                var existItemCode = _implementationDisplayService.GetDisplayByCode(input.Code);
                if (existItemCode != null && _implementationDisplayService.UpdateDataDisplay(input))
                {
                    return Ok(new BaseResultModel
                    {
                        ObjectGuidId = existItemCode.Id,
                        IsSuccess = true
                    });
                }

                return Ok(new BaseResultModel
                {
                    IsSuccess = false,
                    Code = Convert.ToInt32(DisplayError.UpdateDisplayFailedCodeExist),
                    Message = "CodeNotExist" //"Function code not exist, please use code exist."
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

    }
}
