using AutoMapper;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis.PayReward;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Sys.Common.Constants;
using System.Collections.Generic;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;

namespace RDOS.TMK_DisplayAPI.Services.Dis.PayReward
{
	 public class PayRewardService : IPayRewardService
	 {
		  private readonly IMapper _mapper;
		  private readonly ILogger<PayRewardService> _logger;
		  private readonly IBaseRepository<DisPayReward> _repository;
		  private readonly IBaseRepository<DisPayRewardDetail> _repositoryDetail;
		  private readonly IBaseRepository<DisDefinitionStructure> _repositoryDisDefinitionStructures;
		  private readonly IBaseRepository<DisDefinitionProductTypeDetail> _repositoryDisDefinitionProductTypeDetail;
		  private readonly IBaseRepository<DisConfirmResultDetail> _repositoryDisConfirmResultDetail;
		  private readonly IBaseRepository<DisConfirmResult> _repositoryDisConfirmResult;
		  private readonly IBaseRepository<InventoryItem> _repositoryInventoryItem;
		  private readonly IBaseRepository<CustomerShipto> _serviceCustomerShipto;
		  private readonly IBaseRepository<CustomerInformation> _serviceCustomerInformation;
		  private readonly IBaseRepository<InventoryItem> _serviceInventoryItem;
		  private readonly IBaseRepository<ItemGroup> _serviceItemGroup;
		  private readonly IBaseRepository<ItemAttribute> _serviceItemAttribute;
		  private readonly IBaseRepository<Uom> _serviceUom;
		  private readonly IBaseRepository<DisWeightGetExtraRewardsDetail> _repositoryWeightGetExtraRewardsDetail;
		  private readonly IBaseRepository<TempDisOrderHeader> _serviceTempDisOrderHeader;
		  private readonly IBaseRepository<TempDisOrderDetail> _serviceTempDisOrderDetail;
		  private readonly IBaseRepository<DisDisplay> _serviceDisDisplay;

		  public PayRewardService(IMapper mapper,
										  IBaseRepository<DisPayReward> repository,
										  IBaseRepository<DisPayRewardDetail> repositoryDetail,
										  IBaseRepository<DisDefinitionStructure> repositoryDisDefinitionStructures,
										  IBaseRepository<DisDefinitionProductTypeDetail> repositoryDisDefinitionProductTypeDetail,
										  IBaseRepository<DisConfirmResultDetail> repositoryDisConfirmResultDetail,
										  IBaseRepository<DisConfirmResult> repositoryDisConfirmResult,
										  IBaseRepository<InventoryItem> repositoryInventoryItem,
										  IBaseRepository<CustomerShipto> serviceCustomerShipto,
										  IBaseRepository<CustomerInformation> serviceCustomerInformation,
										  IBaseRepository<InventoryItem> serviceInventoryItem,
										  IBaseRepository<ItemGroup> serviceItemGroup,
										  IBaseRepository<ItemAttribute> serviceItemAttribute,
										  IBaseRepository<Uom> serviceUom,
										  IBaseRepository<DisWeightGetExtraRewardsDetail> repositoryWeightGetExtraRewardsDetail,
										  IBaseRepository<TempDisOrderHeader> serviceTempDisOrderHeader,
										  IBaseRepository<TempDisOrderDetail> serviceTempDisOrderDetail,
										  IBaseRepository<DisDisplay> serviceDisDisplay,
										  ILogger<PayRewardService> logger)
		  {
				_mapper = mapper;
				_repository = repository;
				_repositoryDetail = repositoryDetail;
				_repositoryDisDefinitionStructures = repositoryDisDefinitionStructures;
				_repositoryDisDefinitionProductTypeDetail = repositoryDisDefinitionProductTypeDetail;
				_repositoryDisConfirmResultDetail = repositoryDisConfirmResultDetail;
				_repositoryDisConfirmResult = repositoryDisConfirmResult;
				_repositoryInventoryItem = repositoryInventoryItem;
				_serviceCustomerShipto = serviceCustomerShipto;
				_serviceCustomerInformation = serviceCustomerInformation;
				_serviceInventoryItem = serviceInventoryItem;
				_serviceItemGroup = serviceItemGroup;
				_serviceItemAttribute = serviceItemAttribute;
				_serviceUom = serviceUom;
				_repositoryWeightGetExtraRewardsDetail = repositoryWeightGetExtraRewardsDetail;
				_serviceTempDisOrderHeader = serviceTempDisOrderHeader;
				_serviceTempDisOrderDetail = serviceTempDisOrderDetail;
				_serviceDisDisplay = serviceDisDisplay;
				_logger = logger;
		  }

		  public Task<DisPayRewardModel> CreateOrUpdate(RequestPayRewardModel request)
		  {
				string userlogin = string.Empty;
				_logger.LogInformation("info: {request}", request);
				var payReward = _mapper.Map<DisPayReward>(request.PayReward);
				var details = _mapper.Map<List<DisPayRewardDetail>>(request.ListPayRewardDetail);
				if (request.PayReward.Id != Guid.Empty)
				{
					 // Update Pay Reward
					 payReward.UpdatedBy = userlogin;
					 payReward.UpdatedDate = DateTime.Now;
					 _repository.Update(payReward);

					 // Update Pay Reward Detail
					 details.ForEach(x =>
					 {
						  x.UpdatedBy = userlogin;
						  x.UpdatedDate = DateTime.Now;
					 });
					 _repositoryDetail.UpdateRange(details);
				}
				else
				{
					 // Create
					 payReward.CreatedBy = userlogin;
					 payReward.CreatedDate = DateTime.Now;
					 payReward.DeleteFlag = 0;
					 _repository.Insert(payReward);

					 // Detail
					 details.ForEach(x =>
					 {
						  x.CreatedBy = userlogin;
						  x.CreatedDate = DateTime.Now;
						  x.DeleteFlag = 0;
					 });
					 _repositoryDetail.InsertRange(details);
				}

				return Task.FromResult(_mapper.Map<DisPayRewardModel>(payReward));
		  }

		  public BaseResultModel SoftDelete(string PayRewardCode)
		  {
				// Get and delete Pay Reward
				var payReward = _repository.GetAllQueryable(
					 x => x.Code == PayRewardCode && x.DeleteFlag == 0)?.AsNoTracking()?.FirstOrDefault();
				if (payReward != null)
				{
					 payReward.DeleteFlag = 1;
					 _repository.Update(payReward);
				}

				// Get and delete Pay reward Detail
				var details = _repositoryDetail.GetAllQueryable(
					 x => x.DisPayRewardCode == PayRewardCode && x.DeleteFlag == 0).AsNoTracking().ToList();

				if (details != null && details.Any())
				{
					 details.ForEach(x =>
					 {
						  x.DeleteFlag = 1;
					 });
					 _repositoryDetail.UpdateRange(details);
				}

				return new BaseResultModel
				{
					 IsSuccess = true,
					 Code = 200,
					 Message = "DeleteSuccess"
				};
		  }

		  public BaseResultModel Delete(string PayRewardCode)
		  {
				try
				{
					 // Get and delete Pay Reward
					 var _payReward = _repository.GetAllQueryable(x => x.Code == PayRewardCode).AsNoTracking().ToList();
					 if (_payReward != null && _payReward.Any())
					 {
						  _repository.Delete(_payReward.FirstOrDefault().Id);
					 }

					 // Get and delete Pay reward Detail
					 var _payRewardDetail = _repositoryDetail.GetAllQueryable(x => x.DisPayRewardCode == PayRewardCode).AsNoTracking().ToList();

					 if (_payRewardDetail != null && _payRewardDetail.Any())
					 {
						  _repositoryDetail.DeleteRange(_payRewardDetail);
					 }

					 return new BaseResultModel
					 {
						  IsSuccess = true,
						  Code = 200,
						  Message = "DeleteSuccess"
					 };
				}
				catch (Exception ex)
				{
					 return new BaseResultModel
					 {
						  IsSuccess = false,
						  Code = 500,
						  Message = ex.InnerException?.Message ?? ex.Message
					 };
				}
		  }

		  public Task<IQueryable<DisPayReward>> GetListPayReward()
		  {
				var result = _repository.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking().AsQueryable();
				return Task.FromResult(result);
		  }

		  public List<DisPayRewardDetailModel> GetListPayRewardDetail(RequestPayRewardDetailModel request)
		  {
				_logger.LogInformation("info: {request}", request);
				var lstDetail = new List<DisPayRewardDetailModel>();

				#region DATA
				// Data Name
				var listCustomerShipto = (from customershipto in _serviceCustomerShipto.GetAllQueryable().AsNoTracking()
												  join customer in _serviceCustomerInformation.GetAllQueryable().AsNoTracking()
												  on customershipto.CustomerInfomationId equals customer.Id into emptyCustomershipto
												  from customer in emptyCustomershipto.DefaultIfEmpty()
												  select new CustomerShiptoModel()
												  {
														CustomerCode = customer.CustomerCode,
														ShiptoCode = customershipto.ShiptoCode,
														Address = customershipto.Address,
														FullName = customer.FullName
												  }).AsNoTracking();
				DateTime now = DateTime.Now;
				var lstSkus = _serviceInventoryItem.GetAllQueryable(x => x.DelFlg == 0).AsNoTracking();
				var lstItemGroup = _serviceItemGroup.GetAllQueryable().AsNoTracking();
				var lstItemHierarchy = _serviceItemAttribute.GetAllQueryable(
					 x => x.DeleteFlag == 0 && x.EffectiveDate <= now &&
					 (!x.ValidUntilDate.HasValue || x.ValidUntilDate.Value >= now)).AsNoTracking();
				var uoms = _serviceUom.GetAllQueryable(
					 x => x.DeleteFlag == 0 && x.EffectiveDateFrom <= now &&
					 (!x.ValidUntil.HasValue || x.ValidUntil.Value >= now)).AsNoTracking();
				#endregion DATA

				#region existed
				// Pay reward already exist
				var _payRewardExist = _repositoryDetail.GetAllQueryable(x => x.DisPayRewardCode == request.DisPayRewardCode && x.DeleteFlag == 0).AsNoTracking();
				if (_payRewardExist != null && _payRewardExist.Any())
				{
					 foreach (var item in _payRewardExist.ToList())
					 {
						  var tmp = new DisPayRewardDetailModel()
						  {
								Id = item.Id,
								CustomerCode = item.CustomerCode,
								CustomerShiptoCode = item.CustomerShiptoCode,
								ProductCode = item.ProductCode,
								PackingCode = item.PackingCode,
								Quantity = item.Quantity,
								Amount = item.Amount,
								DisPayRewardCode = item.DisPayRewardCode,
								DisplayLevelCode = item.DisplayLevelCode,
								RewardType = item.RewardType,
								ProductType = item.ProductType,
								ItemHierarchyLevel = item.ItemHierarchyLevel,

								PayRewardType = -1,
								CustomerShiptoAddress = listCustomerShipto?.FirstOrDefault(x => x.CustomerCode == item.CustomerCode && x.ShiptoCode == item.CustomerShiptoCode)?.Address,
								ProductDescription = GetProductDecription(item.ProductType, item.ItemHierarchyLevel, item.ProductCode, lstSkus, lstItemGroup, lstItemHierarchy),
								PackingDescription = uoms?.FirstOrDefault(x => x.UomId == item.PackingCode)?.Description,
						  };
						  lstDetail.Add(tmp);
					 }
					 return lstDetail;
				}
				#endregion existed

				#region new
				// Check Status
				var check = _repositoryDisConfirmResult.GetAllQueryable(x => x.Code == request.ConfirmResultDisplayCode && x.DeleteFlag == 0).AsNoTracking();
				if (check.Any() && check.FirstOrDefault().Status != "02")
				{
					 // Đang định nghĩa
					 return lstDetail;
				}

				// DisDisplay
				var display = _serviceDisDisplay.GetAllQueryable(x => x.Code == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking().FirstOrDefault();
				// Danh sách điểm bán
				var listSale = _repositoryDisConfirmResultDetail.GetAllQueryable(x => x.DisConfirmResultCode == request.ConfirmResultDisplayCode && x.DeleteFlag == 0).AsNoTracking();
				// Level
				var levels = _repositoryDisDefinitionStructures.GetAllQueryable(x => x.DisplayCode == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking();
				var products = _repositoryDisDefinitionProductTypeDetail.GetAllQueryable(x => x.DisplayCode == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking();
				var weightProducts = _repositoryWeightGetExtraRewardsDetail.GetAllQueryable(x => x.DisplayCode == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking();

				// get Order
				// B0
				var disOrderHeaders = _serviceTempDisOrderHeader.GetAllQueryable(
					 x => request.StartDate <= x.OrdDate && x.OrdDate <= request.EndDate
					 && x.Status == "01" && string.IsNullOrEmpty(x.RecallOrderCode)).AsNoTracking();
				var disOrderDetails = _serviceTempDisOrderDetail.GetAllQueryable(x => x.TMKType == "02").AsNoTracking();

				// Detail
				foreach (var tmplstSale in listSale.ToList())
				{
					 // check tmplstSale đạt
					 bool isPassReWard = false;
					 bool isPassSales = false;
					 if (display.IndependentDisplay.HasValue && display.IndependentDisplay.Value)
					 {
						  isPassReWard = tmplstSale.DisplayImageResult;
						  isPassSales = tmplstSale.DisplaySalesResult;
					 }
					 else
					 {
						  isPassReWard = isPassSales = tmplstSale.AssessmentPeriodResult;
					 }

					 var tmp = new DisPayRewardDetailModel()
					 {
						  DisPayRewardCode = request.DisPayRewardCode,
						  CustomerCode = tmplstSale.CustomerCode,
						  CustomerShiptoCode = tmplstSale.CustomerShiptoCode,
						  CustomerShiptoAddress = listCustomerShipto?.FirstOrDefault(
								x => x.CustomerCode == tmplstSale.CustomerCode && x.ShiptoCode == tmplstSale.CustomerShiptoCode)?.Address,
					 };

					 var level = levels.FirstOrDefault(x => x.LevelCode == tmplstSale.DisplayLevelCode);
					 if (level != null)
					 {
						  tmp.DisplayLevelCode = level.LevelCode;

						  // reward
						  if (isPassReWard)
						  {
								if (level.IsRewardDonate)
								{
									 var tmpRewardDonate = tmp.CloneData();
									 tmpRewardDonate.RewardType = CommonData.PayRewardTypeByStructure.DisplayReward;

									 if (level.IsRewardFixMoney)
									 {
										  tmpRewardDonate.PayRewardType = CommonData.PayRewardType.DONATE;
										  tmpRewardDonate.Amount = level.RewardAmountOfDonation;
									 }
									 else
									 {
										  tmpRewardDonate.PayRewardType = CommonData.PayRewardType.DONATEPERCENT;
										  // calculate - by dis product
										  var productsDisplay = products.Where(x => x.LevelCode == level.LevelCode
												&& x.ProductType == CommonData.DisplaySetting.ProductForDisplay).Select(x => x.ProductCode).Distinct();
										  // B1
										  var disOrderHeaderSale = disOrderHeaders.Where(x => x.CustomerId == tmplstSale.CustomerCode).Select(x => x.OrdNbr).Distinct();
										  // B2
										  var disOrderDetailSale = disOrderDetails.Where(x => disOrderHeaderSale.Contains(x.OrdNbr) && productsDisplay.Contains(x.InventoryId));
										  // B3 - fix to Shipped_Line_Extend_Amt
										  decimal sum = disOrderDetailSale?.Select(x => x.ShippedLineExtendAmt)?.Sum() ?? 0;
										  tmpRewardDonate.Amount = sum * (decimal)(level.RewardPercentageOfAmount / 100);
									 }
									 lstDetail.Add(tmpRewardDonate);
								}
								if (level.IsRewardProduct)
								{
									 var tmpRewardProduct = tmp.CloneData();
									 tmpRewardProduct.RewardType = CommonData.PayRewardTypeByStructure.DisplayReward;
									 tmpRewardProduct.PayRewardType = CommonData.PayRewardType.PRODUCT;
									 tmpRewardProduct.ProductType = level.RewardProductType;
									 tmpRewardProduct.ItemHierarchyLevel = level.ItemHierarchyLevelReward;

									 var productsReward = products.Where(x => x.LevelCode == level.LevelCode && x.ProductType == CommonData.DisplaySetting.ProductForReward);
									 if (productsReward != null && productsReward.Any())
									 {
										  foreach (var p in productsReward.ToList())
										  {
												var tmpProductDetail = tmpRewardProduct.CloneData();
												tmpProductDetail.ProductCode = p.ProductCode;
												tmpProductDetail.PackingCode = p.Packing;
												tmpProductDetail.Quantity = p.Quantity;
												tmpProductDetail.ProductDescription = GetProductDecription(tmpProductDetail.ProductType,
													 tmpProductDetail.ItemHierarchyLevel, p.ProductCode, lstSkus, lstItemGroup, lstItemHierarchy);
												tmpProductDetail.PackingDescription = uoms?.FirstOrDefault(x => x.UomId == p.Packing)?.Description;

												lstDetail.Add(tmpProductDetail);
										  }
									 }
								}
						  }

						  // isbonus & isPassSales
						  //// salesoutput
						  //// weight
						  ////// botay
						  if (level.IsCheckSalesOutput.HasValue && level.IsCheckSalesOutput.Value && isPassSales)
						  {
								if (level.IsSalesOutputDonate)
								{
									 var tmpSalesDonate = tmp.CloneData();
									 tmpSalesDonate.RewardType = CommonData.PayRewardTypeByStructure.SalesOutputReward;

									 if (level.IsRewardFixMoney)
									 {
										  tmpSalesDonate.PayRewardType = CommonData.PayRewardType.DONATE;
										  tmpSalesDonate.Amount = level.SalesOutputAmountOfDonation;
									 }
									 else
									 {
										  tmpSalesDonate.PayRewardType = CommonData.PayRewardType.DONATEPERCENT;
										  // calculate - by review product
										  var productsReview = products.Where(x => x.LevelCode == level.LevelCode
												&& x.ProductType == CommonData.DisplaySetting.ProductForRewardReview).Select(x => x.ProductCode).Distinct();
										  // B1
										  var disOrderHeaderSale = disOrderHeaders.Where(x => x.CustomerId == tmplstSale.CustomerCode).Select(x => x.OrdNbr).Distinct();
										  // B2
										  var disOrderDetailSale = disOrderDetails.Where(x => disOrderHeaderSale.Contains(x.OrdNbr) && productsReview.Contains(x.InventoryId));
										  // B3 - fix to Shipped_Line_Extend_Amt
										  decimal sum = disOrderDetailSale?.Select(x => x.ShippedLineExtendAmt)?.Sum() ?? 0;
										  tmpSalesDonate.Amount = sum * (decimal)(level.SalesOutputPercentageOfAmount / 100);
									 }
									 lstDetail.Add(tmpSalesDonate);
								}
								if (level.IsSalesOutputProduct)
								{
									 var tmpSalesProduct = tmp.CloneData();
									 tmpSalesProduct.RewardType = CommonData.PayRewardTypeByStructure.SalesOutputReward;
									 tmpSalesProduct.PayRewardType = CommonData.PayRewardType.PRODUCT;
									 tmpSalesProduct.ProductType = level.SalesOutputProductType;
									 tmpSalesProduct.ItemHierarchyLevel = level.ItemHierarchyLevelSalesOut;

									 var productsSales = products.Where(x => x.LevelCode == level.LevelCode && x.ProductType == CommonData.DisplaySetting.ProductForSalesOut);
									 if (productsSales != null && productsSales.Any())
									 {
										  foreach (var p in productsSales.ToList())
										  {
												var tmpProductDetail = tmpSalesProduct.CloneData();
												tmpProductDetail.ProductCode = p.ProductCode;
												tmpProductDetail.PackingCode = p.Packing;
												tmpProductDetail.Quantity = p.Quantity;
												tmpProductDetail.ProductDescription = GetProductDecription(tmpProductDetail.ProductType,
													 tmpProductDetail.ItemHierarchyLevel, p.ProductCode, lstSkus, lstItemGroup, lstItemHierarchy);
												tmpProductDetail.PackingDescription = uoms?.FirstOrDefault(x => x.UomId == p.Packing)?.Description;

												lstDetail.Add(tmpProductDetail);
										  }
									 }
								}

								if (level.IsUseWeights)
								{
									 // TODO check weight reward - 3 case xét thưởng - luồng chưa ổn nhé
									 bool IsWeightPass = false;
									 if (IsWeightPass)
									 {
										  if (level.IsBonusDonate.HasValue && level.IsBonusDonate.Value && level.IsBonusFixMoney.HasValue)
										  {
												var tmpWeightDonate = tmp.CloneData();
												tmpWeightDonate.RewardType = CommonData.PayRewardTypeByStructure.WeightReward;

												if (level.IsBonusFixMoney.Value)
												{
													 tmpWeightDonate.PayRewardType = CommonData.PayRewardType.DONATE;
													 // TODO calculate - by weight product gift
													 tmpWeightDonate.Amount = 0;
												}
												else
												{
													 tmpWeightDonate.PayRewardType = CommonData.PayRewardType.DONATEPERCENT;
													 // TODO calculate - by weight product
													 tmpWeightDonate.Amount = 0;
												}
												lstDetail.Add(tmpWeightDonate);
										  }
										  if (level.IsBonusGiftProduct.HasValue && level.IsBonusGiftProduct.Value)
										  {
												// TODO type product ?
												var tmpWeightProduct = tmp.CloneData();
												tmpWeightProduct.RewardType = CommonData.PayRewardTypeByStructure.WeightReward;
												tmpWeightProduct.PayRewardType = CommonData.PayRewardType.PRODUCT;
												tmpWeightProduct.ProductType = level.ProductTypeForDisplay;
												tmpWeightProduct.ItemHierarchyLevel = level.ItemHierarchyLevelDisplay;

												//lstDetail.Add(tmpWeightProduct);
										  }
									 }
								}
						  }
					 }
				}
				#endregion new

				return lstDetail;
		  }
		  private string GetProductDecription(string productType, string itemHierarchyLevel, string productCode,
				IQueryable<InventoryItem> lstSkus, IQueryable<ItemGroup> lstItemGroup, IQueryable<ItemAttribute> lstItemHierarchy)
		  {
				return productType switch
				{
					 CommonData.DisplaySetting.SKU => lstSkus?.FirstOrDefault(x => x.InventoryItemId == productCode)?.Description,
					 CommonData.DisplaySetting.ItemGroup => lstItemGroup?.FirstOrDefault(x => x.Code == productCode)?.Description,
					 CommonData.DisplaySetting.ItemHierarchyValue => lstItemHierarchy?.FirstOrDefault(x => x.ItemAttributeCode == productCode && x.ItemAttributeMaster == itemHierarchyLevel)?.Description,
					 _ => string.Empty
				};
		  }

		  public Task<DisPayRewardModel> GetPayRewardByCode(string code)
		  {
				var result = _repository.GetAllQueryable(x => x.Code == code && x.DeleteFlag == 0)?.AsNoTracking()?.FirstOrDefault();

				if (result != null)
				{
					 return Task.FromResult(_mapper.Map<DisPayRewardModel>(result));
				}

				return Task.FromResult(new DisPayRewardModel());
		  }

		  public async Task<DisPayRewardModel> FindByCodeAndDisplayCodeAsync(string code, string displayCode)
		  => _mapper.Map<DisPayRewardModel>(await _repository.GetAllQueryable(
				  x => x.Code == code && x.DisplayCode == displayCode && x.DeleteFlag == 0).FirstOrDefaultAsync());

		  public async Task<IQueryable<PayRewardReportResponse>> GetPayRewardReportHeaderAsync(string comfirmResultCode)
		  {
				var confirmResult = await _repositoryDisConfirmResult.GetAllQueryable(
					 x => x.DeleteFlag == 0 && x.Code == comfirmResultCode
				)
				.AsNoTracking()
				.FirstOrDefaultAsync();

				if (confirmResult == null) return null;

				var orderCodes = await _serviceTempDisOrderHeader.GetAllQueryable(
					 x => x.DiscountCode == confirmResult.DisplayCode && x.PeriodCode == confirmResult.SalesCalendarCode
				).Select(x => x.OrdNbr).ToListAsync();

				var result = _serviceTempDisOrderDetail.GetAllQueryable(x => orderCodes.Contains(x.OrdNbr))
					 .GroupBy(x => new
					 {
						  Packing = x.UomName,
						  ProductName = x.InventoryName,
					 })
					 .Select(x => new PayRewardReportResponse
					 {
						  Packing = x.Key.Packing,
						  ProductName = x.Key.ProductName,
						  Quantity = x.Sum(x => x.ShippedQty).Value,
						  Amount = x.Sum(x => x.UnitPrice)
					 });

				return result;
		  }

		  public async Task<IQueryable<PayRewardReportLevelGrouped>> GetPayRewardLevelGroupedAsync(string comfirmResultCode)
		  {
				var confirmResult = await _repositoryDisConfirmResult.GetAllQueryable(
					 x => x.DeleteFlag == 0 && x.Code == comfirmResultCode
				)
				.AsNoTracking()
				.FirstOrDefaultAsync();

				if (confirmResult == null) return null;

				var result = _serviceTempDisOrderHeader.GetAllQueryable(
					 x => x.DiscountCode == confirmResult.DisplayCode && x.PeriodCode == confirmResult.SalesCalendarCode
				)
				.Join(
					 _serviceTempDisOrderDetail.GetAllQueryable(),
					 a => a.OrdNbr,
					 b => b.OrdNbr,
					 (a, b) => new
					 {
						  LevelCode = b.DisplayLevel,
						  LevelName = b.DisplayLevelName,
					 }
				).GroupBy(x => new
				{
					 x.LevelCode,
					 x.LevelName
				})
				.Select(x => new PayRewardReportLevelGrouped
				{
					 TotalCustomer = x.Count(),
					 LevelCode = x.Key.LevelCode,
					 LevelName = x.Key.LevelName,
				});

				return result;
		  }

		  public async Task<IQueryable<PayRewardReportResponse>> GetPayRewardReportByLevelAsync(string levelCode, string comfirmResultCode)
		  {
				var confirmResult = await _repositoryDisConfirmResult.GetAllQueryable(
					 x => x.DeleteFlag == 0 && x.Code == comfirmResultCode
				)
				.AsNoTracking()
				.FirstOrDefaultAsync();

				if (confirmResult == null) return null;

				var result = _serviceTempDisOrderHeader.GetAllQueryable(
					 x => x.DiscountCode == confirmResult.DisplayCode && x.PeriodCode == confirmResult.SalesCalendarCode
				)
				.Join(
					 _serviceTempDisOrderDetail.GetAllQueryable(),
					 a => a.OrdNbr,
					 b => b.OrdNbr,
					 (a, b) => new PayRewardReportResponse
					 {
						  Packing = b.UomName,
						  Amount = b.UnitPrice,
						  CustomerShipTo = a.ShiptoId,
						  Customer = a.CustomerId,
						  CustomerShipToName = a.ShiptoName,
						  ProductName = b.InventoryName,
						  Quantity = b.ShippedQty.Value,
						  LevelCode = b.DisplayLevel,
						  LevelName = b.DisplayLevelName
					 }
				)
				.DefaultIfEmpty()
				.Where(x => x.LevelCode == levelCode)
				.AsNoTracking();

				return result;
		  }
		
		public async Task<IQueryable<DisplayPeriodTrackingReportListModel>> GetListDisplayPeriodTrackingReportAsync(string DisplayCode)
		{
			#region Data Name
			DateTime now = DateTime.Now;
			var lstSkus = _serviceInventoryItem.GetAllQueryable(x => x.DelFlg == 0).AsNoTracking();
			var lstItemGroup = _serviceItemGroup.GetAllQueryable().AsNoTracking();
			var lstItemHierarchy = _serviceItemAttribute.GetAllQueryable(
				 x => x.DeleteFlag == 0 && x.EffectiveDate <= now &&
				 (!x.ValidUntilDate.HasValue || x.ValidUntilDate.Value >= now)).AsNoTracking();
			var uoms = _serviceUom.GetAllQueryable(
				 x => x.DeleteFlag == 0 && x.EffectiveDateFrom <= now &&
				 (!x.ValidUntil.HasValue || x.ValidUntil.Value >= now)).AsNoTracking();
			#endregion Data Name

			var dataDisPayReward = await _repository.GetAllQueryable(x => x.DisplayCode == DisplayCode).AsNoTracking().ToListAsync();
            var dataDisPayRewardDetail = await _repositoryDetail.GetAllQueryable().AsNoTracking().ToListAsync();
			var result1 = dataDisPayReward.Join(dataDisPayRewardDetail, a => a.Code, b => b.DisPayRewardCode,
				(a, b) => new
				{
					a.Code,
					a.Name,
					a.StartDate,
					a.EndDate,
					b.ProductType,
					b.ItemHierarchyLevel,
					b.ProductCode,
					b.PackingCode,
					b.Quantity,
					b.Amount,
				}).GroupBy(x => new {x.Code, x.StartDate, x.EndDate, x.ProductCode, x.PackingCode }, (grpKey, group) => new DisplayPeriodTrackingReportListModel
				{
                    DisplayCode = DisplayCode,
                    Code = grpKey.Code,
                    Name = group.First().Name,
                    StartDate = grpKey.StartDate,
                    EndDate = grpKey.EndDate,
                    ProductCode = grpKey.ProductCode,
                    Packing = grpKey.PackingCode,
					Quantity = string.IsNullOrEmpty(grpKey.ProductCode) ? null : group.Where(p => p.Quantity.HasValue)?.Select(p => p.Quantity)?.Sum(),
					Amount = string.IsNullOrEmpty(grpKey.ProductCode) ? group.Where(p => p.Amount.HasValue)?.Select(p => p.Amount)?.Sum() : null,

					ProductDescription = string.IsNullOrEmpty(grpKey.ProductCode) ? null : GetProductDecription(group.First().ProductType, group.First().ItemHierarchyLevel, grpKey.ProductCode, lstSkus, lstItemGroup, lstItemHierarchy),
                    PackingDescription = string.IsNullOrEmpty(grpKey.ProductCode) ? null : uoms?.FirstOrDefault(x => x.UomId == grpKey.PackingCode)?.Description,
                }).AsQueryable();

			return await Task.FromResult(result1);
		}
		public async Task<IQueryable<DisFollowRewardProgressQuantityCustomerModel>> GetListQuantityCustomerAsync(string DisplayCode)
		{
			var dataDisPayReward = await _repository.GetAllQueryable(x => x.DisplayCode == DisplayCode).AsNoTracking().ToListAsync();
			var dataDisPayRewardDetail = await _repositoryDetail.GetAllQueryable().AsNoTracking().ToListAsync();
			var result1 = dataDisPayReward.Join(dataDisPayRewardDetail, a => a.Code, b => b.DisPayRewardCode,
				(a, b) => new
				{
					a.Code,
					a.Name,
					b.CustomerCode,
				}).Distinct().GroupBy(x => new { x.Code, x.CustomerCode }, (key, group) => new DisFollowRewardProgressQuantityCustomerModel
				{
					DisplayCode = DisplayCode,
					Code = key.Code,
					Name = group.First().Name,
					CustomerCode = group.First().CustomerCode,
				}).OrderBy(x => x.Name).AsQueryable();

			return await Task.FromResult(result1);
		}
	}
}
