using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
	 public class DisConfirmResultDetailService : IDisConfirmResultDetailService
	 {
		  private readonly IMapper _mapper;
		  private readonly ILogger<DisConfirmResultDetailService> _logger;
		  private readonly IBaseRepository<DisConfirmResult> _result;
		  private readonly IBaseRepository<DisConfirmResultDetail> _resultDetail;
		  private readonly IBaseRepository<CustomerShipto> _serviceCustomerShipto;
		  private readonly IBaseRepository<CustomerInformation> _serviceCustomerInformation;
		  private readonly IBaseRepository<DisDefinitionStructure> _definitionStructure;
		  private readonly IBaseRepository<DisDefinitionProductTypeDetail> _productTypes;
		  private readonly IBaseRepository<InventoryItem> _inventoryItems;
		  private readonly IBaseRepository<ItemGroup> _itemGroups;
		  private readonly IBaseRepository<ItemAttribute> _itemAttribute;
		  private readonly IBaseRepository<Uom> _uom;

		  public DisConfirmResultDetailService(IMapper mapper, IBaseRepository<DisConfirmResult> result,
				IBaseRepository<DisConfirmResultDetail> resultDetail,
				IBaseRepository<CustomerShipto> serviceCustomerShipto,
				IBaseRepository<CustomerInformation> serviceCustomerInformation,
				IBaseRepository<DisDefinitionStructure> definitionStructure,
				ILogger<DisConfirmResultDetailService> logger, IBaseRepository<DisDefinitionProductTypeDetail> productTypes, IBaseRepository<InventoryItem> inventoryItems, IBaseRepository<Uom> uom, IBaseRepository<ItemGroup> itemGroups, IBaseRepository<ItemAttribute> itemAttribute)
		  {
				_mapper = mapper;
				_result = result;
				_resultDetail = resultDetail;
				_serviceCustomerShipto = serviceCustomerShipto;
				_serviceCustomerInformation = serviceCustomerInformation;
				_definitionStructure = definitionStructure;
				_logger = logger;
				_productTypes = productTypes;
				_inventoryItems = inventoryItems;
				_uom = uom;
				_itemGroups = itemGroups;
				_itemAttribute = itemAttribute;
		  }

		  public Task<bool> CreateAsync(DisConfirmResultDetailDisplayRequest request)
		  {
				_logger.LogInformation("info: {request}", request);
				try
				{
					 //Fix later when has authorize
					 var entity = _mapper.Map<DisConfirmResultDetail>(request)
												.InitInsert("");

					 var result = _resultDetail.Insert(entity);

					 return Task.FromResult(result != null);
				}
				catch (ArgumentException ex)
				{
					 _logger.LogError(ex, "Was an error occrred while create Dis ConfirmResult Detail");
					 throw new ArgumentException(ex.Message);
				}
		  }

		  public Task<bool> DeleteAsync(Guid id)
		  {
				try
				{
					 return Task.FromResult(_resultDetail.Delete(id) != null);
				}
				catch (ArgumentException ex)
				{
					 _logger.LogError(ex, "Was an error occurred while delete Dis ConfirmResult Detail");
					 throw new ArgumentException(ex.Message);
				}
		  }

		  public async Task<bool> ExistsByCodeAsync(string code, Guid? id = null)
			 => await _resultDetail.GetAllQueryable(x => (id == null || x.Id != id) && x.DisConfirmResultCode == code).AnyAsync();

		  public async Task<DisConfirmResultDetail> FindByIdAsync(Guid id)
			 => await _resultDetail.GetAllQueryable(x => x.Id == id).FirstOrDefaultAsync();

		  public IQueryable<DisConfirmResultDetailDisplayModel> GetListConfirmResultDetail(TempDisOrderHeaderParameters request)
		  {
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
												  }).AsNoTracking().AsQueryable();

				var listresult = (from rs in _result.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
										join rsdetail in _resultDetail.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
										on rs.Code equals rsdetail.DisConfirmResultCode into emptyDetail
										from rsdetail in emptyDetail.DefaultIfEmpty()
										join customershipto in listCustomerShipto on
															new { customer_code = rsdetail.CustomerCode, customer_shipto_code = rsdetail.CustomerShiptoCode } equals
															new { customer_code = customershipto.CustomerCode, customer_shipto_code = customershipto.ShiptoCode }
															into temptyCustomerShipto
										from customershipto in temptyCustomerShipto.DefaultIfEmpty()

										select new DisConfirmResultDetailDisplayModel
										{
											 Id = rsdetail.Id,
											 DisConfirmResultCode = rsdetail.DisConfirmResultCode,
											 CustomerCode = rsdetail.CustomerCode,
											 ShiptoCode = rsdetail.CustomerShiptoCode,
											 CustomerShiptoName = (customershipto != null) ? customershipto.Address : string.Empty,
											 DisplayLevelCode = rsdetail.DisplayLevelCode,
											 DisplayLevelName = string.Empty,
											 NumberMustRating = rsdetail.NumberMustRating,
											 NumberHasEvaluate = rsdetail.NumberHasEvaluate,
											 NumberPassed = rsdetail.NumberPassed,
											 RevenuesRegistered = 0,
											 RevenuesPass = 0,
											 //DisplayImageResult = rsdetail.DisplayImageResult,
											 //DisplaySalesResult = rsdetail.DisplaySalesResult,
											 //AssessmentPeriodResult = rsdetail.AssessmentPeriodResult,
											 DisplayCode = rsdetail.DisplayCode,
											 CustomerShiptoCode = rsdetail.CustomerShiptoCode,
											 SalesPass = rsdetail.SalesPass,
											 OutputPass = rsdetail.OutputPass
										}).AsQueryable();
				return listresult;
		  }

		  public IQueryable<DisConfirmResultDetailDisplayModel> GetListConfirmResultDetailAsync()
		  {
				var listConfirmResultDetail = _resultDetail.GetAllQueryable().AsNoTracking()
					 .ProjectTo<DisConfirmResultDetailDisplayModel>(_mapper.ConfigurationProvider);
				return listConfirmResultDetail;
		  }

		  public IQueryable<DisConfirmResultDetailDisplayModel> GetListConfirmResultDetailByResultCodeAsync(string code)
		  {
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
												  }).AsNoTracking().AsQueryable();

				var listresult = (from rs in _result.GetAllQueryable(x => x.DeleteFlag == 0 && x.Code == code).AsNoTracking()
										join rsdetail in _resultDetail.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisConfirmResultCode == code)
										.AsNoTracking()
										on rs.Code equals rsdetail.DisConfirmResultCode into emptyDetail
										from rsdetail in emptyDetail.DefaultIfEmpty()
										join customershipto in listCustomerShipto on
										  new { customer_code = rsdetail.CustomerCode, customer_shipto_code = rsdetail.CustomerShiptoCode }
										  equals
										  new { customer_code = customershipto.CustomerCode, customer_shipto_code = customershipto.ShiptoCode }
										  into temptyCustomerShipto
										from customershipto in temptyCustomerShipto.DefaultIfEmpty()
										join defin in _definitionStructure.GetAllQueryable().AsNoTracking() on
										new { display_code = rsdetail.DisplayCode, display_level_code = rsdetail.DisplayLevelCode }
										equals
										new { display_code = defin.DisplayCode, display_level_code = defin.LevelCode }
										 into temptyDefin
										from defin in temptyDefin.DefaultIfEmpty()

										select new DisConfirmResultDetailDisplayModel
										{
											 Id = rsdetail.Id,
											 DisConfirmResultCode = rsdetail.DisConfirmResultCode,
											 CustomerCode = rsdetail.CustomerCode,
											 ShiptoCode = rsdetail.CustomerShiptoCode,
											 CustomerShiptoName = (customershipto != null) ? customershipto.Address : string.Empty,
											 DisplayLevelCode = rsdetail.DisplayLevelCode,
											 DisplayLevelName = defin.LevelName,
											 NumberMustRating = rsdetail.NumberMustRating,
											 NumberHasEvaluate = rsdetail.NumberHasEvaluate,
											 NumberPassed = rsdetail.NumberPassed,
											 RevenuesRegistered = 0,
											 RevenuesPass = 0,
											 //DisplayImageResult = rsdetail.DisplayImageResult,
											 //DisplaySalesResult = rsdetail.DisplaySalesResult,
											 //AssessmentPeriodResult = rsdetail.AssessmentPeriodResult,
											 DisplayCode = rsdetail.DisplayCode,
											 CustomerShiptoCode = rsdetail.CustomerShiptoCode,
											 SalesPass = rsdetail.SalesPass,
											 OutputPass = rsdetail.OutputPass
										}).AsNoTracking().AsQueryable();
				return listresult;
		  }

		  public Task<DisConfirmResultDetailDisplayModel> UpdateAsync(DisConfirmResultDetail request)
		  {
				_logger.LogInformation("info: {request}", request);
				try
				{
					 var result = _resultDetail.Update(request.InitUpdate(""));
					 return Task.FromResult(_mapper.Map<DisConfirmResultDetailDisplayModel>(result));
				}
				catch (ArgumentException ex)
				{
					 _logger.LogError(ex, "Was an error occrred while update Dis ConfirmResult Detail");
					 throw new ArgumentException(ex.Message);
				}
		  }

		  public IQueryable<DisConfirmResultDetailGrouped> GetConfirmResultDetailGrouped(DisDisplayModel display, string confirmResultCode)
		  {
				var definitionStructures = _definitionStructure.GetAllQueryable
					 (
						  x => x.DeleteFlag == 0 && x.DisplayCode == display.Code
					 ).AsNoTracking();

				var isIndependent = display.IndependentDisplay.HasValue && display.IndependentDisplay.Value;
				var result = _resultDetail.GetAllQueryable
					 (
						  x => x.DeleteFlag == 0
						  && x.DisConfirmResultCode == confirmResultCode
						  && x.DisplayCode == display.Code
					 )
					 .Select(x => new DisConfirmResultDetailResponse
					 {
						  Id = x.Id,
						  IsPassed = isIndependent ? (x.AssessmentPeriodResult || x.DisplayImageResult) : x.DisplaySalesResult,
						  DisplayLevelCode = x.DisplayLevelCode,
						  AssessmentPeriodResult = x.AssessmentPeriodResult,
						  AssessmentPeriodResultDes = x.AssessmentPeriodResultDes,
						  CreatedBy = x.CreatedBy,
						  CreatedDate = x.CreatedDate,
						  CustomerCode = x.CustomerCode,
						  CustomerShiptoCode = x.CustomerShiptoCode,
						  DeleteFlag = x.DeleteFlag,
						  DisConfirmResultCode = x.DisConfirmResultCode,
						  DisplayCode = x.DisplayCode,
						  DisplayImageResult = x.DisplayImageResult,
						  DisplayImageResultDes = x.DisplaySalesResultDes,
						  DisplaySalesResultDes = x.DisplaySalesResultDes,
						  DisplaySalesResult = x.DisplaySalesResult,
						  NumberHasEvaluate = x.NumberHasEvaluate,
						  NumberMustRating = x.NumberMustRating,
						  NumberPassed = x.NumberPassed,
						  OutputPass = x.OutputPass,
						  SalesPass = x.SalesPass,
						  UpdatedBy = x.UpdatedBy,
						  UpdatedDate = x.UpdatedDate
					 })
					 .GroupBy(x => new
					 {
						  x.DisplayLevelCode,
						  x.IsPassed
					 })
					 .Select(x => new
					 {
						  x.Key.IsPassed,
						  x.Key.DisplayLevelCode,
						  TotalPass = x.Count(i => i.IsPassed),
						  TotalNoPass = x.Count(i => !i.IsPassed),
					 })
					 .Join(definitionStructures, a => a.DisplayLevelCode, b => b.LevelCode,
					 (a, b) => new DisConfirmResultDetailGrouped
					 {
						  LevelName = b.LevelName,
						  LevelCode = a.DisplayLevelCode,
						  TotalPass = a.TotalPass,
						  TotalNoPass = a.TotalNoPass
					 })
					 .AsNoTracking();

				return result;
		  }

		  public async Task<List<ConfirmResultDetailJoinReport>> GetConfirmResultDetailsReportAsync(string confirmResultCode, string levelCode, bool passed, bool isIndependent)
		  {
				var itemGroups = _itemGroups.GetAllQueryable();
				var uoms = _uom.GetAllQueryable(x => x.DeleteFlag == 0);
				var customerShipTos = _serviceCustomerShipto.GetAllQueryable();
				var productTypes = _productTypes.GetAllQueryable(x => x.DeleteFlag == 0);
				var inventoryItems = _inventoryItems.GetAllQueryable(x => x.DelFlg == 0);
				var itemAttributes = _itemAttribute.GetAllQueryable(x => x.DeleteFlag == 0);
				var definitionStructures = _definitionStructure.GetAllQueryable(x => x.DeleteFlag == 0);
				var customerInformations = _serviceCustomerInformation.GetAllQueryable();

				var confirmResultDetails = _resultDetail.GetAllQueryable
					 (
						  x => x.DeleteFlag == 0
						  && x.DisConfirmResultCode == confirmResultCode
						  && x.DisplayLevelCode == levelCode
						  && (passed ? (isIndependent ? (x.AssessmentPeriodResult || x.DisplayImageResult) : x.DisplaySalesResult)
						  : (isIndependent ? (!x.AssessmentPeriodResult && !x.DisplayImageResult) : !x.DisplaySalesResult))
					 );

				var cusomterShips = customerShipTos.Join(customerInformations,
					 a => a.CustomerInfomationId, b => b.Id, (a, b) => new
					 {
						  b.CustomerCode,
						  b.FullName,
						  a.ShiptoCode,
						  a.Address
					 });

				var confirmResultDetailJoinings = confirmResultDetails.Join(definitionStructures,
					 a => new
					 {
						  a.DisplayCode,
						  LevelCode = a.DisplayLevelCode
					 },
					 b => new
					 {
						  b.DisplayCode,
						  b.LevelCode,
					 },
					 (a, b) => new ConfirmResultDetailJoiningResponse
					 {
						  CustomerCode = a.CustomerCode,
						  CustomerShiptoCode = a.CustomerShiptoCode,
						  DisplayCode = a.DisplayCode,
						  DisConfirmResultCode = a.DisConfirmResultCode,
						  DisplayLevelCode = a.DisplayLevelCode,
						  IsRewardDonate = b.IsRewardDonate,
						  IsRewardProduct = b.IsRewardProduct,
						  RewardProductType = b.RewardProductType,
						  SalesOutputProductType = b.SalesOutputProductType,
						  RewardAmountOfDonation = b.RewardAmountOfDonation,
						  SalesOutputPercentageOfAmount = b.SalesOutputPercentageOfAmount,
						  LevelName = b.LevelName,
						  LevelCode = b.LevelCode,
					 })
					 .Join(cusomterShips, a => new
					 {
						  a.CustomerCode,
						  ShiptoCode = a.CustomerShiptoCode
					 },
					 b => new
					 {
						  b.CustomerCode,
						  b.ShiptoCode
					 },
					 (a, b) => new ConfirmResultDetailJoiningResponse
					 {
						  CustomerCode = a.CustomerCode,
						  CustomerShiptoCode = a.CustomerShiptoCode,
						  DisplayCode = a.DisplayCode,
						  DisConfirmResultCode = a.DisConfirmResultCode,
						  DisplayLevelCode = a.DisplayLevelCode,
						  IsRewardDonate = a.IsRewardDonate,
						  IsRewardProduct = a.IsRewardProduct,
						  RewardProductType = a.RewardProductType,
						  SalesOutputProductType = a.SalesOutputProductType,
						  RewardAmountOfDonation = a.RewardAmountOfDonation,
						  SalesOutputPercentageOfAmount = a.SalesOutputPercentageOfAmount,
						  Address = b.Address,
						  LevelName = a.LevelName,
						  LevelCode = a.LevelCode

					 }).AsNoTracking();

				if (!passed)
				{
					 return await confirmResultDetailJoinings.Select
						  (
								x => _mapper.Map<ConfirmResultDetailJoinReport>(x)
						  ).ToListAsync();
				}

				var listConfirmResultDetail = await confirmResultDetailJoinings.ToListAsync();
				List<ConfirmResultDetailJoinReport> result = new();
				foreach (var item in listConfirmResultDetail)
				{
					 if (item.IsRewardDonate)
					 {
						  result.Add(new()
						  {
								Customer = item.CustomerCode,
								CustomerShipToCode = item.CustomerShiptoCode,
								Amount = item.RewardAmountOfDonation,
								CustomerShipToName = item.Address,
								LevelName = item.LevelName,
								LevelCode = item.LevelCode
						  });
					 }

					 if (item.IsRewardProduct)
					 {
						  var productRewardDescriptions = await GetProductDescriptionAsync(item.RewardProductType, item);
						  if (productRewardDescriptions?.Count > 0)
						  {
								result.AddRange(productRewardDescriptions);
						  }
					 }
				}

				return result;

				async Task<List<ConfirmResultDetailJoinReport>> GetProductDescriptionAsync(string productType, ConfirmResultDetailJoiningResponse item)
				{
					 productTypes = productTypes.Where
						  (
								x => x.DisplayCode == item.DisplayCode
								&& x.LevelCode == item.DisplayLevelCode
								&& x.ProductType == CommonData.DisplaySetting.ProductForReward
						  );

					 return productType switch
					 {
						  CommonData.DisplaySetting.SKU
						  => await productTypes.Join(inventoryItems, a => a.ProductCode, b => b.InventoryItemId, (a, b) =>
														  new
														  {
																Product = a,
																InventoryItem = b
														  })
														  .DefaultIfEmpty()
														  .Join(uoms, a => a.Product.Packing, b => b.UomId
														  , ((a, b) => new
														  {
																Packing = b.Description,
																a.Product.DisplayCode,
																a.Product.ProductCode,
																a.Product.Quantity,
																ProductName = a.InventoryItem.Description,

														  }))
														  .DefaultIfEmpty()
														  .Select(x => new ConfirmResultDetailJoinReport
														  {
																Customer = item.CustomerCode,
																CustomerShipToName = item.Address,
																CustomerShipToCode = item.CustomerShiptoCode,
																Packing = x.Packing,
																ProductName = x.ProductName,
																Quantity = x.Quantity,
																LevelCode = item.LevelCode,
																LevelName = item.LevelName
														  }).ToListAsync(),
						  CommonData.DisplaySetting.ItemGroup 
						  => await productTypes.Join(itemGroups, a => a.ProductCode, b => b.Code, (a, b) =>
								new
								{
									 Product = a,
									 ItemGroup = b
								})
								.DefaultIfEmpty()
								.Join(uoms, a => a.Product.Packing, b => b.UomId
								, ((a, b) => new ConfirmResultDetailJoinReport
								{
									 Customer = item.CustomerCode,
									 Packing = b.Description,
									 CustomerShipToName = item.Address,
									 ProductName = a.ItemGroup.Description,
									 Quantity = a.Product.Quantity,
									 CustomerShipToCode = item.CustomerShiptoCode,
									 LevelCode = item.LevelCode,
									 LevelName = item.LevelName
								})).DefaultIfEmpty().ToListAsync(),
						  CommonData.DisplaySetting.ItemHierarchyValue 
						  => await productTypes.Join(itemAttributes, a => a.ProductCode, b => b.ItemAttributeCode, (a, b) =>
								new
								{
									 Product = a,
									 ItemAttribute = b
								})
								.DefaultIfEmpty()
								.Join(uoms, a => a.Product.Packing, b => b.UomId,
								((a, b) => new ConfirmResultDetailJoinReport
								{
									 Customer = item.CustomerCode,
									 CustomerShipToCode = item.CustomerShiptoCode,
									 CustomerShipToName = item.Address,
									 Packing = b.Description,
									 Quantity = a.Product.Quantity,
									 ProductName = a.ItemAttribute.Description,
									 LevelCode = item.LevelCode,
									 LevelName = item.LevelName
								})).DefaultIfEmpty().ToListAsync(),
						  _ => new(),
					 };
				}
		  }

	 }
}