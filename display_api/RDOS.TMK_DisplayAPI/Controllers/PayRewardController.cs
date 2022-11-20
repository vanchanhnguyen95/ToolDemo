using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis.PayReward;
using System;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using System.Linq;
using static Sys.Common.Constants.ErrorCodes;
using Microsoft.AspNetCore.Mvc;
using RDOS.TMK_DisplayAPI.Models.Dis.PayReward;
using AutoMapper;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[AllowAnonymous]
	[ApiVersion("1.0")]
	public class PayRewardController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly IPayRewardService _payRewardService;
		public PayRewardController(IMapper mapper, IPayRewardService payRewardService)
		{
			_mapper = mapper;
			_payRewardService = payRewardService;
		}

		[HttpPost]
		[MapToApiVersion("1.0")]
		[Route("InsertOrUpdatePayReward")]
		public async Task<IActionResult> CreateOrUpdatePayReward(RequestPayRewardModel request)
		{
			try
			{
				var featureListTemp = await _payRewardService.CreateOrUpdate(request);

				return Ok(BaseResultModel.Success(featureListTemp));
			}
			catch (Exception ex)
			{
				return Ok(new BaseResultModel
				{
					IsSuccess = false,
					Code = Convert.ToInt32(DisPayRewardError.UpdatePayRewardFailedCodeExist),
					Message = ex.InnerException?.Message ?? ex.Message
				});
			}
		}

		[HttpPost]
		[MapToApiVersion("1.0")]
		[Route("GetPayRewardByCode/{code}")]
		public async Task<IActionResult> GetPayRewardByCode(string code)
		{
			try
			{
				var payRewardModel = await _payRewardService.GetPayRewardByCode(code);

				return Ok(BaseResultModel.Success(payRewardModel));
			}
			catch (Exception ex)
			{
				return Ok(new BaseResultModel
				{
					IsSuccess = false,
					Code = Convert.ToInt32(DisPayRewardError.UpdatePayRewardFailedCodeExist),
					Message = ex.InnerException?.Message ?? ex.Message
				});
			}
		}

		[HttpGet]
		[MapToApiVersion("1.0")]
		[Route("getByCodeAndDisplayCode")]
		public async Task<IActionResult> GetByCodeAndDisplayCodeAsync([FromQuery] string code, [FromQuery] string displayCode)
		{
			var result = await _payRewardService.FindByCodeAndDisplayCodeAsync(code, displayCode);
			return Ok(BaseResultModel.Success(result));
		}

		[HttpDelete]
		[Route("Delete/{code}")]
		[MapToApiVersion("1.0")]
		public IActionResult DeleteDisBudgets(string code)
		{
			try
			{
				var existItem = _payRewardService.GetPayRewardByCode(code).Result;
				if (!string.IsNullOrEmpty(existItem.Code))
				{
					return Ok(_payRewardService.Delete(code));
				}
				else
				{
					return Ok(new BaseResultModel
					{
						IsSuccess = false,
						Code = Convert.ToInt32(DisPayRewardError.ListPayRewardFailed),
						Message = "PayRewardCodeIsNotExist"
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
		[Route("SoftDelete/{code}")]
		[MapToApiVersion("1.0")]
		public IActionResult SoftDeletePayReward(string code)
		{
			try
			{
				var existItem = _payRewardService.GetPayRewardByCode(code).Result;
				if (!string.IsNullOrEmpty(existItem.Code))
				{
					return Ok(_payRewardService.SoftDelete(code));
				}
				else
				{
					return Ok(new BaseResultModel
					{
						IsSuccess = false,
						Code = Convert.ToInt32(DisPayRewardError.ListPayRewardFailed),
						Message = "PayRewardCodeIsNotExist"
					});
				}
			}
			catch (Exception ex)
			{
				return Ok(new BaseResultModel
				{
					IsSuccess = false,
					Code = Convert.ToInt32(DisPayRewardError.ListPayRewardFailed),
					Message = ex.InnerException?.Message ?? ex.Message
				});
			}
		}

		[HttpPost]
		[MapToApiVersion("1.0")]
		[Route("GetListPayReward")]
		public async Task<IActionResult> GetListPayReward([FromBody] EcoParameters parameters)
		{
			try
			{
				var display = await _payRewardService.GetListPayReward();
				// check searching
				if (parameters.HasQuerySearching)
				{
					var optionsAssembly = ScriptOptions.Default.AddReferences(typeof(DisPayReward).Assembly);
					var filterExpressionTemp = CSharpScript.EvaluateAsync<Func<DisPayReward, bool>>(($"s=> {parameters.Filter}"), optionsAssembly);
					Func<DisPayReward, bool> filterExpression = filterExpressionTemp.Result;

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
					var result1 = PagedList<DisPayReward>.ToPagedList(display.ToList(), 0, display.Count());
					var resultMap1 = new ListPayRewardModel
					{
						Items = _mapper.Map<List<DisPayRewardModel>>(result1),
						MetaData = result1.MetaData,
					};
					return Ok(BaseResultModel.Success(resultMap1));
				}

				int totalCount = display.Count();
				int skip = parameters.Skip ?? 0;
				int top = parameters.Top ?? parameters.PageSize;
				var items = display.Skip(skip).Take(top).ToList();
				var result = new PagedList<DisPayReward>(items, totalCount, (skip / top) + 1, top);

				var resultMap = new ListPayRewardModel
				{
					Items = _mapper.Map<List<DisPayRewardModel>>(result),
					MetaData = result.MetaData,
				};
				return Ok(BaseResultModel.Success(resultMap));
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
        [Route("GetListPayRewardDetail")]
        public IActionResult GetListPayRewardDetail([FromBody] RequestPayRewardDetailModel request)
        {
            try
            {
                var dataAll = _payRewardService.GetListPayRewardDetail(request);
				if (request.parameters.IsDropdown)
                {
                    var featureListTempPagged1 = PagedList<DisPayRewardDetailModel>.ToPagedList(dataAll, 0, dataAll.Count);
					var result1 = new ResponsePayRewardDetailModel(featureListTempPagged1);
                    return Ok(BaseResultModel.Success(result1));
				}

				// Groupby
				var data = dataAll.GroupBy(x => new { x.CustomerCode, x.CustomerShiptoCode, x.ProductCode }, (grpKey, groups) => new DisPayRewardDetailModel
				{
					CustomerCode = grpKey.CustomerCode,
					CustomerShiptoCode = grpKey.CustomerShiptoCode,
					ProductCode = grpKey.ProductCode,
					PackingCode = groups.First().PackingCode,
					Quantity = string.IsNullOrEmpty(grpKey.ProductCode) ? null : groups.Where(p => p.Quantity.HasValue)?.Select(p => p.Quantity)?.Sum(),
					Amount = string.IsNullOrEmpty(grpKey.ProductCode) ? groups.Where(p => p.Amount.HasValue)?.Select(p => p.Amount)?.Sum() : null,
					DisPayRewardCode = groups.First().DisPayRewardCode,
					DisplayLevelCode = groups.First().DisplayLevelCode,
					ProductType = groups.First().ProductType,
					ItemHierarchyLevel = groups.First().ItemHierarchyLevel,

					CustomerShiptoAddress = groups.First().CustomerShiptoAddress,
					ProductDescription = groups.First().ProductDescription,
					PackingDescription = groups.First().PackingDescription,
				}).OrderBy(p => p.CustomerCode).ThenBy(p => p.CustomerShiptoCode).ToList();

				int totalCount = data.Count;
                int skip = request.parameters.Skip ?? 0;
                int top = request.parameters.Top ?? request.parameters.PageSize;
                var items = data.Skip(skip).Take(top).ToList();
                var result = new PagedList<DisPayRewardDetailModel>(items, totalCount, (skip / top) + 1, top);
                // Total
                var result2 = new ResponsePayRewardDetailModel(result)
                {
                    TotalCustomerPayReward = data.Select(x => x.CustomerCode).Distinct().Count(),
                    TotalAmountPayReward = data.Where(x => x.Amount.HasValue).Select(x => x.Amount.Value).Sum()
                };
                var lstProductCode2 = data.Select(x => x.ProductCode).Distinct().ToList();
                foreach (var itemTt in lstProductCode2)
                {
                    if (!string.IsNullOrEmpty(itemTt))
                    {
                        var tmp = new TotalProductPayRewardModel()
                        {
                            ProductName = data.FirstOrDefault(x => x.ProductCode == itemTt).ProductDescription,
                            Quantity = data.Where(x => x.ProductCode == itemTt && x.Quantity.HasValue).Select(x => x.Quantity.Value).Sum(),
                            Packing = data.FirstOrDefault(x => x.ProductCode == itemTt).PackingDescription,
                        };
                        result2.ListSumProductPayReward.Add(tmp);
                    }
                }
                return Ok(BaseResultModel.Success(result2));
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
    }
}
