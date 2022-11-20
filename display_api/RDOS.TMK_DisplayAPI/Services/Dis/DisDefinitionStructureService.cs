using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
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
    public class DisDefinitionStructureService : IDisDefinitionStructureService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DisDefinitionStructure> _logger;
        private readonly IBaseRepository<DisDefinitionStructure> _repository;
        private readonly IBaseRepository<DisDefinitionProductTypeDetail> _repositoryProductTypeDetail;
        private readonly IBaseRepository<DisDefinitionCriteriaEvaluate> _repositoryCriteriaEvaluate;
        private readonly IBaseRepository<DisWeightGetExtraRewardsDetail> _repositoryWeightGetExtraRewardsDetail;
        private readonly IBaseRepository<DisDefinitionGuideImage> _repositoryDisplayGuideImage;
        private readonly IBaseRepository<InventoryItem> _serviceInventoryItem;
        private readonly IBaseRepository<ItemGroup> _serviceItemGroup;
        private readonly IBaseRepository<ItemAttribute> _serviceItemAttribute;
        private readonly IBaseRepository<DisCriteriaEvaluatePictureDisplay> _serviceCriteria;
        private readonly IBaseRepository<Uom> _serviceUom;

        public DisDefinitionStructureService(IMapper mapper,
            ILogger<DisDefinitionStructure> logger,
            IBaseRepository<DisDefinitionStructure> repository,
            IBaseRepository<DisDefinitionProductTypeDetail> repositoryProductTypeDetail,
            IBaseRepository<DisDefinitionCriteriaEvaluate> repositoryCriteriaEvaluate,
            IBaseRepository<DisWeightGetExtraRewardsDetail> repositoryWeightGetExtraRewardsDetail,
            IBaseRepository<DisDefinitionGuideImage> repositoryDisplayGuideImage,
            IBaseRepository<InventoryItem> serviceInventoryItem,
            IBaseRepository<ItemGroup> serviceItemGroup,
            IBaseRepository<ItemAttribute> serviceItemAttribute,
            IBaseRepository<DisCriteriaEvaluatePictureDisplay> repositoryCriteria,
            IBaseRepository<Uom> repositoryUom)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _repositoryProductTypeDetail = repositoryProductTypeDetail;
            _repositoryCriteriaEvaluate = repositoryCriteriaEvaluate;
            _repositoryWeightGetExtraRewardsDetail = repositoryWeightGetExtraRewardsDetail;
            _repositoryDisplayGuideImage = repositoryDisplayGuideImage;
            _serviceInventoryItem = serviceInventoryItem;
            _serviceItemGroup = serviceItemGroup;
            _serviceItemAttribute = serviceItemAttribute;
            _serviceCriteria = repositoryCriteria;
            _serviceUom = repositoryUom;
        }

        public async Task<bool> ExistsByCodeAsync(string displayCode, string levelCode, Guid? id = null)
            => await _repository.GetAllQueryable(x => (id == null || x.Id != id) &&
            x.DisplayCode == displayCode && x.LevelCode == levelCode).AnyAsync();

        public async Task<DisDefinitionStructure> FindByIdAsync(Guid id)
            => await _repository.GetAllQueryable(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<DisDefinitionStructure>> GetDisDefinitionStructureListAsync(string displayCode)
            => await _repository.GetAllQueryable(x => x.DisplayCode == displayCode).OrderBy(x => x.LevelCode).ToListAsync();

        public async Task<DisDefinitionStructureModel> GetDisDefinitionStructureByCodeAsync(string displayCode, string levelCode)
        {
            // Get data
            var definitionStructure = await _repository.GetAllQueryable(x
                => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0).AsNoTracking().FirstOrDefaultAsync();

            var productDisplay = await _repositoryProductTypeDetail.GetAllQueryable(x
                => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0
                && x.ProductType == CommonData.DisplaySetting.ProductForDisplay).AsNoTracking().ToListAsync();
            var productReward = await _repositoryProductTypeDetail.GetAllQueryable(x
                => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0
                && x.ProductType == CommonData.DisplaySetting.ProductForReward).AsNoTracking().ToListAsync();
            var productSaleOut = await _repositoryProductTypeDetail.GetAllQueryable(x
                => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0
                && x.ProductType == CommonData.DisplaySetting.ProductForSalesOut).AsNoTracking().ToListAsync();
            var productReview = await _repositoryProductTypeDetail.GetAllQueryable(x
                => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0
                && x.ProductType == CommonData.DisplaySetting.ProductForRewardReview).AsNoTracking().ToListAsync();

            var criteriaEvaluate = await _repositoryCriteriaEvaluate.GetAllQueryable(x
                => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0).AsNoTracking().ToListAsync();
            var weightGetExtraRewardsDetail = await _repositoryWeightGetExtraRewardsDetail.GetAllQueryable(x
                => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0).AsNoTracking().ToListAsync();
            var displayGuideImage = await _repositoryDisplayGuideImage.GetAllQueryable(x
                => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0).AsNoTracking().OrderBy(x => x.Code).ToListAsync();

            // Get Uom, Criteria
            var listCriteria = await _serviceCriteria.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking().ToListAsync();

            DateTime now = DateTime.Now;
            var lstSkus = await _serviceInventoryItem.GetAllQueryable(x => x.DelFlg == 0).AsNoTracking().ToListAsync();
            var lstItemGroup = await _serviceItemGroup.GetAllQueryable().AsNoTracking().ToListAsync();
            var lstItemHierarchy = await _serviceItemAttribute.GetAllQueryable(x => x.DeleteFlag == 0
            && x.EffectiveDate <= now && (!x.ValidUntilDate.HasValue || x.ValidUntilDate.Value >= now)).AsNoTracking().ToListAsync();
            var uoms = _serviceUom.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking().ToList();

            // Get Description
            var productDisplayDescription = GetProductDecription(definitionStructure.ProductTypeForDisplay, productDisplay, lstSkus, lstItemGroup, lstItemHierarchy, uoms);
            var productRewardDescription = GetProductDecription(definitionStructure.RewardProductType, productReward, lstSkus, lstItemGroup, lstItemHierarchy, uoms);
            var productSaleOutDescription = GetProductDecription(definitionStructure.SalesOutputProductType, productSaleOut, lstSkus, lstItemGroup, lstItemHierarchy, uoms);
            var productReviewDescription = GetProductDecription(definitionStructure.ProductTypeForDisplay, productReview, lstSkus, lstItemGroup, lstItemHierarchy, uoms);

            var criteriaEvaluateDescription = (from item in criteriaEvaluate
                                               join c in listCriteria on item.CriteriaCode equals c.Code into emptyCriteria
                                               from c in emptyCriteria.DefaultIfEmpty()
                                               select new DisDefinitionCriteriaEvaluateModel()
                                               {
                                                   Id = item.Id,
                                                   DisplayCode = item.DisplayCode,
                                                   LevelCode = item.LevelCode,
                                                   CriteriaCode = item.CriteriaCode,
                                                   CriteriaDescription = (c != null) ? c.CriteriaDescription : string.Empty,
                                                   Result = (c != null) ? c.Result : string.Empty,
                                               }).ToList();

            var productWeigtDescription = GetProductWeightDecription(definitionStructure.ProductTypeForDisplay,
                weightGetExtraRewardsDetail, lstSkus, lstItemGroup, lstItemHierarchy, uoms, definitionStructure.ItemHierarchyLevelDisplay);

            // Map data
            DisDefinitionStructureModel definitionLevel = new();
            definitionLevel.Structure = _mapper.Map<DisDefinitionStructureDataModel>(definitionStructure);
            definitionLevel.ProductDisplay = productDisplayDescription;
            definitionLevel.ProductReward = productRewardDescription;
            definitionLevel.ProductSaleOut = productSaleOutDescription;
            definitionLevel.ProductRewardReview = productReviewDescription;
            definitionLevel.CriteriaEvaluates = criteriaEvaluateDescription;
            definitionLevel.WeightGetExtraRewards = productWeigtDescription;
            definitionLevel.GuideImages = _mapper.Map<List<DisDefinitionGuideImageModel>>(displayGuideImage);
            return definitionLevel;
        }
        private List<DisDefinitionProductTypeDetailModel> GetProductDecription(string productType, List<DisDefinitionProductTypeDetail> product,
            List<InventoryItem> lstSkus, List<ItemGroup> lstItemGroup, List<ItemAttribute> lstItemHierarchy, List<Uom> uoms)
        {
            DateTime now = DateTime.Now;
            var lstUom = uoms.Where(x => x.EffectiveDateFrom <= now && (!x.ValidUntil.HasValue || x.ValidUntil.Value >= now)).ToList();
            return productType switch
            {
                CommonData.DisplaySetting.SKU => (from p in product
                                                  join sku in lstSkus on p.ProductCode equals sku.InventoryItemId into emptySku
                                                  from sku in emptySku.DefaultIfEmpty()
                                                  join uom in lstUom on p.Packing equals uom.UomId into emptyUom
                                                  from uom in emptyUom.DefaultIfEmpty()
                                                  select new DisDefinitionProductTypeDetailModel()
                                                  {
                                                      Id = p.Id,
                                                      DisplayCode = p.DisplayCode,
                                                      LevelCode = p.LevelCode,
                                                      ProductType = p.ProductType,
                                                      ItemHierarchyLevel = p.ItemHierarchyLevel,
                                                      ProductCode = p.ProductCode,
                                                      ProductDescription = (sku != null) ? sku.Description : string.Empty,
                                                      Packing = p.Packing,
                                                      PackingDescription = (uom != null) ? uom.Description : string.Empty,
                                                      Quantity = p.Quantity,
                                                      ListUom = GetListUomByInventoryItemCode(p.ProductCode, lstSkus, uoms)
                                                  }).ToList(),
                CommonData.DisplaySetting.ItemGroup => (from p in product
                                                        join ig in lstItemGroup on p.ProductCode equals ig.Code into emptyIg
                                                        from ig in emptyIg.DefaultIfEmpty()
                                                        join uom in lstUom on p.Packing equals uom.UomId into emptyUom
                                                        from uom in emptyUom.DefaultIfEmpty()
                                                        select new DisDefinitionProductTypeDetailModel()
                                                        {
                                                            Id = p.Id,
                                                            DisplayCode = p.DisplayCode,
                                                            LevelCode = p.LevelCode,
                                                            ProductType = p.ProductType,
                                                            ItemHierarchyLevel = p.ItemHierarchyLevel,
                                                            ProductCode = p.ProductCode,
                                                            ProductDescription = (ig != null) ? ig.Description : string.Empty,
                                                            Packing = p.Packing,
                                                            PackingDescription = (uom != null) ? uom.Description : string.Empty,
                                                            Quantity = p.Quantity,
                                                            ListUom = GetListUomByItemGroupCode(p.ProductCode, lstSkus, uoms)
                                                        }).ToList(),
                CommonData.DisplaySetting.ItemHierarchyValue => (from p in product
                                                                 join ih in lstItemHierarchy on p.ProductCode equals ih.ItemAttributeCode into emptyIg
                                                                 from ih in emptyIg.DefaultIfEmpty()
                                                                 join uom in lstUom on p.Packing equals uom.UomId into emptyUom
                                                                 from uom in emptyUom.DefaultIfEmpty()
                                                                 where p.ItemHierarchyLevel.Equals(ih.ItemAttributeMaster)
                                                                 select new DisDefinitionProductTypeDetailModel()
                                                                 {
                                                                     Id = p.Id,
                                                                     DisplayCode = p.DisplayCode,
                                                                     LevelCode = p.LevelCode,
                                                                     ProductType = p.ProductType,
                                                                     ItemHierarchyLevel = p.ItemHierarchyLevel,
                                                                     ProductCode = p.ProductCode,
                                                                     ProductDescription = (ih != null) ? ih.Description : string.Empty,
                                                                     Packing = p.Packing,
                                                                     PackingDescription = (uom != null) ? uom.Description : string.Empty,
                                                                     Quantity = p.Quantity,
                                                                     ListUom = GetListUomByItemHierarchyValue(p.ProductCode, p.ItemHierarchyLevel, lstItemHierarchy, lstSkus, uoms)
                                                                 }).ToList(),
                _ => new()
            };
        }
        private List<DisWeightGetExtraRewardsDetailModel> GetProductWeightDecription(string productType, List<DisWeightGetExtraRewardsDetail> product,
            List<InventoryItem> lstSkus, List<ItemGroup> lstItemGroup, List<ItemAttribute> lstItemHierarchy, List<Uom> uoms, string ItemhierarchyLevel = "")
        {
            DateTime now = DateTime.Now;
            var lstUom = uoms.Where(x => x.EffectiveDateFrom <= now && (!x.ValidUntil.HasValue || x.ValidUntil.Value >= now)).ToList();
            return productType switch
            {
                CommonData.DisplaySetting.SKU => (from p in product
                                                  join sku in lstSkus on p.ProductCode equals sku.InventoryItemId into emptySku
                                                  from sku in emptySku.DefaultIfEmpty()
                                                  join uom in lstUom on p.Packing equals uom.UomId into emptyUom
                                                  from uom in emptyUom.DefaultIfEmpty()
                                                  select new DisWeightGetExtraRewardsDetailModel()
                                                  {
                                                      Id = p.Id,
                                                      DisplayCode = p.DisplayCode,
                                                      LevelCode = p.LevelCode,
                                                      ProductCode = p.ProductCode,
                                                      ProductDescription = (sku != null) ? sku.Description : string.Empty,
                                                      Packing = p.Packing,
                                                      PackingDescription = (uom != null) ? uom.Description : string.Empty,
                                                      NumberOfGift = p.NumberOfGift,
                                                      AmountOfGift = p.AmountOfGift,
                                                      AmountOfDonation = p.AmountOfDonation,
                                                      PercentageOfAmount = p.PercentageOfAmount,
                                                      PercentageToBeAchieved = p.PercentageToBeAchieved,
                                                      SalesToBeAchieved = p.SalesToBeAchieved,
                                                      OutputToBeAchieved = p.OutputToBeAchieved,
                                                      Status = p.Status,
                                                      ListUom = GetListUomByInventoryItemCode(p.ProductCode, lstSkus, uoms)
                                                  }).ToList(),
                CommonData.DisplaySetting.ItemGroup => (from p in product
                                                        join ig in lstItemGroup on p.ProductCode equals ig.Code into emptyIg
                                                        from ig in emptyIg.DefaultIfEmpty()
                                                        join uom in lstUom on p.Packing equals uom.UomId into emptyUom
                                                        from uom in emptyUom.DefaultIfEmpty()
                                                        select new DisWeightGetExtraRewardsDetailModel()
                                                        {
                                                            Id = p.Id,
                                                            DisplayCode = p.DisplayCode,
                                                            LevelCode = p.LevelCode,
                                                            ProductCode = p.ProductCode,
                                                            ProductDescription = (ig != null) ? ig.Description : string.Empty,
                                                            Packing = p.Packing,
                                                            PackingDescription = (uom != null) ? uom.Description : string.Empty,
                                                            NumberOfGift = p.NumberOfGift,
                                                            AmountOfGift = p.AmountOfGift,
                                                            AmountOfDonation = p.AmountOfDonation,
                                                            PercentageOfAmount = p.PercentageOfAmount,
                                                            PercentageToBeAchieved = p.PercentageToBeAchieved,
                                                            SalesToBeAchieved = p.SalesToBeAchieved,
                                                            OutputToBeAchieved = p.OutputToBeAchieved,
                                                            Status = p.Status,
                                                            ListUom = GetListUomByItemGroupCode(p.ProductCode, lstSkus, uoms)
                                                        }).ToList(),
                CommonData.DisplaySetting.ItemHierarchyValue => (from p in product
                                                                 join ih in lstItemHierarchy on p.ProductCode equals ih.ItemAttributeCode into emptyIg
                                                                 from ih in emptyIg.DefaultIfEmpty()
                                                                 join uom in lstUom on p.Packing equals uom.UomId into emptyUom
                                                                 from uom in emptyUom.DefaultIfEmpty()
                                                                 select new DisWeightGetExtraRewardsDetailModel()
                                                                 {
                                                                     Id = p.Id,
                                                                     DisplayCode = p.DisplayCode,
                                                                     LevelCode = p.LevelCode,
                                                                     ProductCode = p.ProductCode,
                                                                     ProductDescription = (ih != null) ? ih.Description : string.Empty,
                                                                     Packing = p.Packing,
                                                                     PackingDescription = (uom != null) ? uom.Description : string.Empty,
                                                                     NumberOfGift = p.NumberOfGift,
                                                                     AmountOfGift = p.AmountOfGift,
                                                                     AmountOfDonation = p.AmountOfDonation,
                                                                     PercentageOfAmount = p.PercentageOfAmount,
                                                                     PercentageToBeAchieved = p.PercentageToBeAchieved,
                                                                     SalesToBeAchieved = p.SalesToBeAchieved,
                                                                     OutputToBeAchieved = p.OutputToBeAchieved,
                                                                     Status = p.Status,
                                                                     ListUom = GetListUomByItemHierarchyValue(p.ProductCode, ItemhierarchyLevel, lstItemHierarchy, lstSkus, uoms)
                                                                 }).ToList(),
                _ => new()
            };
        }
        private List<UomsModel> GetListUomByInventoryItemCode(string inventoryItemCode, List<InventoryItem> inventoryItems, List<Uom> uoms)
        {
            List<UomsModel> uomsModels = new List<UomsModel>();

            var inventoryItemWithUom = (from x in inventoryItems.Where(x => x.InventoryItemId.ToLower().Equals(inventoryItemCode.ToLower()))
                                        join uom1 in uoms on x.BaseUnit equals uom1.Id into defaultUom1
                                        from uom1 in defaultUom1.DefaultIfEmpty()
                                        join uom2 in uoms on x.SalesUnit equals uom2.Id into defaultUom2
                                        from uom2 in defaultUom2.DefaultIfEmpty()
                                        join uom3 in uoms on x.PurchaseUnit equals uom3.Id into defaultUom3
                                        from uom3 in defaultUom3.DefaultIfEmpty()
                                        select new InventoryItemUomModel()
                                        {
                                            Id = x.Id,
                                            Code = x.InventoryItemId,
                                            UomBaseId = (uom1 != null) ? uom1.Id : Guid.Empty,
                                            UomBaseCode = (uom1 != null) ? uom1.UomId : string.Empty,
                                            UomBaseDescription = (uom1 != null) ? uom1.Description : string.Empty,
                                            UomSalesId = (uom2 != null) ? uom2.Id : Guid.Empty,
                                            UomSalesCode = (uom2 != null) ? uom2.UomId : string.Empty,
                                            UomSalesDescription = (uom2 != null) ? uom2.Description : string.Empty,
                                            UomPurchaseId = (uom3 != null) ? uom3.Id : Guid.Empty,
                                            UomPurchaseCode = (uom3 != null) ? uom3.UomId : string.Empty,
                                            UomPurchaseDescription = (uom3 != null) ? uom3.Description : string.Empty,
                                        }).FirstOrDefault();

            if (inventoryItemWithUom != null)
            {
                if (!string.IsNullOrEmpty(inventoryItemWithUom.UomBaseCode))
                {
                    if (!uomsModels.Exists(x => x.UomId.ToLower().Equals(inventoryItemWithUom.UomBaseCode.ToLower())))
                    {
                        uomsModels.Add(new UomsModel() { Id = inventoryItemWithUom.UomBaseId, UomId = inventoryItemWithUom.UomBaseCode, Description = inventoryItemWithUom.UomBaseDescription });
                    }
                }
                if (!string.IsNullOrEmpty(inventoryItemWithUom.UomSalesCode))
                {
                    if (!uomsModels.Exists(x => x.UomId.ToLower().Equals(inventoryItemWithUom.UomSalesCode.ToLower())))
                    {
                        uomsModels.Add(new UomsModel() { Id = inventoryItemWithUom.UomSalesId, UomId = inventoryItemWithUom.UomSalesCode, Description = inventoryItemWithUom.UomSalesDescription });
                    }
                }
                if (!string.IsNullOrEmpty(inventoryItemWithUom.UomPurchaseCode))
                {
                    if (!uomsModels.Exists(x => x.UomId.ToLower().Equals(inventoryItemWithUom.UomPurchaseCode.ToLower())))
                    {
                        uomsModels.Add(new UomsModel() { Id = inventoryItemWithUom.UomPurchaseId, UomId = inventoryItemWithUom.UomPurchaseCode, Description = inventoryItemWithUom.UomPurchaseDescription });
                    }
                }
            }
            return uomsModels;
        }
        private List<UomsModel> GetListUomByItemGroupCode(string itemGroupCode, List<InventoryItem> inventoryItems, List<Uom> uoms)
        {
            List<UomsModel> uomsModels = new List<UomsModel>();

            var tempInventory = inventoryItems.Where(x => x.GroupId.ToLower().Equals(itemGroupCode.ToLower())).ToList();
            var listAllUom = (from u in uoms
                              select new UomsModel()
                              {
                                  Id = u.Id,
                                  UomId = u.UomId,
                                  Description = u.Description
                              }).ToList();

            if (tempInventory != null && tempInventory.Count > 3)
            {
                tempInventory = tempInventory.Take(3).ToList();
            }

            // If have 1 inventory => Uoms
            if (tempInventory != null && tempInventory.Count == 1)
            {
                return GetListUomByInventoryItemCode(tempInventory.FirstOrDefault().InventoryItemId, inventoryItems, uoms);
            }

            // If have 2 or 3 inventory. 
            if (tempInventory != null && tempInventory.Count > 1)
            {
                var numberInventory = tempInventory.Count;
                var temp = new List<UomsModel>();
                foreach (var item in tempInventory)
                {
                    var tempUomsModels = GetListUomByInventoryItemCode(item.InventoryItemId, inventoryItems, uoms);
                    if (tempUomsModels != null && tempUomsModels.Count > 0)
                    {
                        tempUomsModels = tempUomsModels.Distinct().ToList();
                        temp.AddRange(tempUomsModels);
                    }
                }
                var groupUom = from u in temp
                               group u by u.UomId into grp
                               where grp.Count() > (numberInventory - 1)
                               select grp;

                foreach (var item in groupUom)
                {
                    var uom = listAllUom.FirstOrDefault(x => x.UomId.ToLower().Equals(item.Key.ToLower()));
                    if (uom != null)
                    {
                        uomsModels.Add(uom);
                    }
                }

                return uomsModels;
            }

            return uomsModels;
        }
        private List<UomsModel> GetListUomByItemHierarchyValue(string itemHierarchyValue, string itemAttributeId,
            List<ItemAttribute> lstItemHierarchy, List<InventoryItem> inventoryItems, List<Uom> uoms)
        {
            DateTime now = DateTime.Now;
            List<UomsModel> uomsModels = new List<UomsModel>();
            var itemHierarchy = lstItemHierarchy.Where(x => x.ItemAttributeMaster.ToLower().Equals(itemAttributeId.ToLower()) && x.ItemAttributeCode.ToLower().Equals(itemHierarchyValue.ToLower())).FirstOrDefault();
            if (itemHierarchy == null)
            {
                return uomsModels;
            }

            var tempInventory = new List<InventoryItem>();
            switch (itemAttributeId)
            {
                case CommonData.ItemSetting.IT01:
                    tempInventory = inventoryItems.Where(x => x.Attribute1.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT02:
                    tempInventory = inventoryItems.Where(x => x.Attribute2.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT03:
                    tempInventory = inventoryItems.Where(x => x.Attribute3.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT04:
                    tempInventory = inventoryItems.Where(x => x.Attribute4.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT05:
                    tempInventory = inventoryItems.Where(x => x.Attribute5.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT06:
                    tempInventory = inventoryItems.Where(x => x.Attribute6.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT07:
                    tempInventory = inventoryItems.Where(x => x.Attribute7.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT08:
                    tempInventory = inventoryItems.Where(x =>  x.Attribute8.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT09:
                    tempInventory = inventoryItems.Where(x => x.Attribute9.Equals(itemHierarchy.Id)).ToList();
                    break;
                case CommonData.ItemSetting.IT10:
                    tempInventory = inventoryItems.Where(x => x.Attribute10.Equals(itemHierarchy.Id)).ToList();
                    break;
                default:
                    break;
            }

            var listAllUom = (from u in uoms
                              select new UomsModel()
                              {
                                  Id = u.Id,
                                  UomId = u.UomId,
                                  Description = u.Description
                              }).ToList();

            if (tempInventory != null && tempInventory.Count > 3)
            {
                tempInventory = tempInventory.Take(3).ToList();
            }

            // If have 1 inventory => Uoms
            if (tempInventory != null && tempInventory.Count == 1)
            {
                return GetListUomByInventoryItemCode(tempInventory.FirstOrDefault().InventoryItemId, inventoryItems, uoms);
            }

            // If have 2 or 3 inventory. 
            if (tempInventory != null && tempInventory.Count > 1)
            {
                var numberInventory = tempInventory.Count;
                var temp = new List<UomsModel>();
                foreach (var item in tempInventory)
                {
                    var tempUomsModels = GetListUomByInventoryItemCode(item.InventoryItemId, inventoryItems, uoms);
                    if (tempUomsModels != null && tempUomsModels.Count > 0)
                    {
                        tempUomsModels = tempUomsModels.Distinct().ToList();
                        temp.AddRange(tempUomsModels);
                    }
                }
                var groupUom = from u in temp
                               group u by u.UomId into grp
                               where grp.Count() > (numberInventory - 1)
                               select grp;

                foreach (var item in groupUom)
                {
                    var uom = listAllUom.FirstOrDefault(x => x.UomId.ToLower().Equals(item.Key.ToLower()));
                    if (uom != null)
                    {
                        uomsModels.Add(uom);
                    }
                }

                return uomsModels;
            }

            return uomsModels;
        }

        public async Task<string> CreateDisDefinitionStructureAsync(DisDefinitionStructureModel request)
        {
            string userlogin = string.Empty;
            _logger.LogInformation("info: {request}", request);
            try
            {
                // Defined level
                var listAll = _repository.GetAllQueryable(x => x.DisplayCode == request.Structure.DisplayCode);
                var maxLevelCode = (listAll == null || !listAll.Any()) ? 0 : listAll.MaxAsync(x => Convert.ToInt64(x.LevelCode)).Result;
                maxLevelCode++;
                var insertItem = _mapper.Map<DisDefinitionStructure>(request.Structure);
                insertItem.LevelCode = maxLevelCode.ToString();
                insertItem.CreatedDate = DateTime.Now;
                insertItem.CreatedBy = userlogin;
                var result = _repository.Insert(insertItem);
                insertItem = result ?? throw new ArgumentException("CreateFailed");
                string displayCode = insertItem.DisplayCode;
                string levelCode = insertItem.LevelCode;

                // Products For Display
                // Reward Display
                // Sale Output
                // Reward Review
                List<DisDefinitionProductTypeDetail> disDefinitionProduct = new();
                disDefinitionProduct.AddRange(_mapper.Map<List<DisDefinitionProductTypeDetail>>(request.ProductDisplay));
                disDefinitionProduct.AddRange(_mapper.Map<List<DisDefinitionProductTypeDetail>>(request.ProductReward));
                disDefinitionProduct.AddRange(_mapper.Map<List<DisDefinitionProductTypeDetail>>(request.ProductSaleOut));
                disDefinitionProduct.AddRange(_mapper.Map<List<DisDefinitionProductTypeDetail>>(request.ProductRewardReview));
                disDefinitionProduct.ForEach(item =>
                {
                    item.DisplayCode = displayCode;
                    item.LevelCode = levelCode;
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = userlogin;
                });
                _repositoryProductTypeDetail.InsertRange(disDefinitionProduct);

                // Display Guide Images
                var listAllGuide = _repositoryDisplayGuideImage.GetAllQueryable(x => x.DisplayCode == displayCode && x.LevelCode == levelCode);
                var maxGuideCode = (listAllGuide == null || !listAllGuide.Any()) ? 0 : listAllGuide.MaxAsync(x => Convert.ToInt64(x.Code)).Result;
                List<DisDefinitionGuideImage> listDefinitionGuide = _mapper.Map<List<DisDefinitionGuideImage>>(request.GuideImages);
                listDefinitionGuide.ForEach(item =>
                {
                    maxGuideCode++;
                    item.DisplayCode = displayCode;
                    item.LevelCode = levelCode;
                    item.Code = maxGuideCode.ToString();
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = userlogin;
                });
                _repositoryDisplayGuideImage.InsertRange(listDefinitionGuide);

                // Criteria Evaluate Image
                List<DisDefinitionCriteriaEvaluate> listDefinitionCriteria = _mapper.Map<List<DisDefinitionCriteriaEvaluate>>(request.CriteriaEvaluates);
                listDefinitionCriteria.ForEach(item =>
                {
                    item.DisplayCode = displayCode;
                    item.LevelCode = levelCode;
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = userlogin;
                });
                _repositoryCriteriaEvaluate.InsertRange(listDefinitionCriteria);

                // Weight For Extra Bonus
                List<DisWeightGetExtraRewardsDetail> listWeightGetExtra = _mapper.Map<List<DisWeightGetExtraRewardsDetail>>(request.WeightGetExtraRewards);
                listWeightGetExtra.ForEach(item =>
                {
                    item.DisplayCode = displayCode;
                    item.LevelCode = levelCode;
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = userlogin;
                });
                _repositoryWeightGetExtraRewardsDetail.InsertRange(listWeightGetExtra);

                return await Task.FromResult(levelCode);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Was an error occrred while create display structure");
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<string> UpdateDisDefinitionStructureAsync(DisDefinitionStructureModel request)
        {
            string userlogin = string.Empty;
            _logger.LogInformation("info: {request}", request);
            try
            {
                // Defined level
                var insertItem = _mapper.Map<DisDefinitionStructure>(request.Structure);
                if (insertItem.DeleteFlag != 1)
                {
                    insertItem.UpdatedBy = userlogin;
                    insertItem.UpdatedDate = DateTime.Now;
                }
                var result = _repository.Update(insertItem);
                insertItem = result ?? throw new ArgumentException("UpdateFailed");
                string displayCode = insertItem.DisplayCode;
                string levelCode = insertItem.LevelCode;

                #region Old data
                var productDisplay = await _repositoryProductTypeDetail.GetAllQueryable(x
                    => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0).AsNoTracking().ToListAsync();
                var criteriaEvaluate = await _repositoryCriteriaEvaluate.GetAllQueryable(x
                    => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0).AsNoTracking().ToListAsync();
                var weightGetExtraRewards = await _repositoryWeightGetExtraRewardsDetail.GetAllQueryable(x
                    => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0).AsNoTracking().ToListAsync();
                var displayGuideImage = await _repositoryDisplayGuideImage.GetAllQueryable(x
                    => x.DisplayCode == displayCode && x.LevelCode == levelCode && x.DeleteFlag == 0).AsNoTracking().ToListAsync();
                #endregion Old data

                // Products For Display
                // Reward Display
                // Sale Output
                // Reward Review
                #region Products Display
                List<DisDefinitionProductTypeDetail> productInput = new();
                productInput.AddRange(_mapper.Map<List<DisDefinitionProductTypeDetail>>(request.ProductDisplay));
                productInput.AddRange(_mapper.Map<List<DisDefinitionProductTypeDetail>>(request.ProductReward));
                productInput.AddRange(_mapper.Map<List<DisDefinitionProductTypeDetail>>(request.ProductSaleOut));
                productInput.AddRange(_mapper.Map<List<DisDefinitionProductTypeDetail>>(request.ProductRewardReview));

                var changesTrackerProduct = new ChangesTracker<DisDefinitionProductTypeDetail>(productDisplay, productInput, AreEqualProduct);
                List<DisDefinitionProductTypeDetail> productInsert = new();
                List<DisDefinitionProductTypeDetail> productUpdate = new();
                List<DisDefinitionProductTypeDetail> productDelete = new();
                productDelete.AddRange(changesTrackerProduct.RemovedItems);
                productUpdate.AddRange(changesTrackerProduct.UpdatedItems);
                productInsert.AddRange(changesTrackerProduct.AddedItems);

                productDelete.ForEach(item =>
                {
                    item.DeleteFlag = 1;
                });
                productUpdate.ForEach(item =>
                {
                    item.UpdatedBy = userlogin;
                    item.UpdatedDate = DateTime.Now;
                });
                productInsert.ForEach(item =>
                {
                    item.DisplayCode = displayCode;
                    item.LevelCode = levelCode;
                    item.CreatedBy = userlogin;
                    item.CreatedDate = DateTime.Now;
                });
                productUpdate.AddRange(productDelete);
                _repositoryProductTypeDetail.UpdateRange(productUpdate);
                _repositoryProductTypeDetail.InsertRange(productInsert);
                #endregion Products Display

                #region Display Guide Images
                var listAllGuide = _repositoryDisplayGuideImage.GetAllQueryable(x => x.DisplayCode == displayCode && x.LevelCode == levelCode);
                var maxGuideCode = (listAllGuide == null || !listAllGuide.Any()) ? 0 : listAllGuide.MaxAsync(x => Convert.ToInt64(x.Code)).Result;

                List<DisDefinitionGuideImage> listDefinitionGuide = _mapper.Map<List<DisDefinitionGuideImage>>(request.GuideImages);
                var changesTrackerGuide = new ChangesTracker<DisDefinitionGuideImage>(displayGuideImage, listDefinitionGuide, AreEqualGuideImage);
                List<DisDefinitionGuideImage> guideInsert = new();
                List<DisDefinitionGuideImage> guideUpdate = new();
                List<DisDefinitionGuideImage> guideDelete = new();
                guideDelete.AddRange(changesTrackerGuide.RemovedItems);
                guideUpdate.AddRange(changesTrackerGuide.UpdatedItems);
                guideInsert.AddRange(changesTrackerGuide.AddedItems);

                guideDelete.ForEach(item =>
                {
                    item.DeleteFlag = 1;
                });
                guideUpdate.ForEach(item =>
                {
                    item.UpdatedBy = userlogin;
                    item.UpdatedDate = DateTime.Now;
                });
                guideInsert.ForEach(item =>
                {
                    maxGuideCode++;
                    item.DisplayCode = displayCode;
                    item.LevelCode = levelCode;
                    item.Code = maxGuideCode.ToString();
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = userlogin;
                });
                guideUpdate.AddRange(guideDelete);
                _repositoryDisplayGuideImage.UpdateRange(guideUpdate);
                _repositoryDisplayGuideImage.InsertRange(guideInsert);
                #endregion Display Guide Images

                #region Criteria Evaluate Image
                List<DisDefinitionCriteriaEvaluate> listDefinitionCriteria = _mapper.Map<List<DisDefinitionCriteriaEvaluate>>(request.CriteriaEvaluates);
                var changesTrackerCriteria = new ChangesTracker<DisDefinitionCriteriaEvaluate>(criteriaEvaluate, listDefinitionCriteria, AreEqualCriteria);
                List<DisDefinitionCriteriaEvaluate> criteriaInsert = new();
                List<DisDefinitionCriteriaEvaluate> criteriaUpdate = new();
                List<DisDefinitionCriteriaEvaluate> criteriaDelete = new();
                criteriaDelete.AddRange(changesTrackerCriteria.RemovedItems);
                criteriaUpdate.AddRange(changesTrackerCriteria.UpdatedItems);
                criteriaInsert.AddRange(changesTrackerCriteria.AddedItems);

                criteriaDelete.ForEach(item =>
                {
                    item.DeleteFlag = 1;
                });
                criteriaUpdate.ForEach(item =>
                {
                    item.UpdatedBy = userlogin;
                    item.UpdatedDate = DateTime.Now;
                });
                criteriaInsert.ForEach(item =>
                {
                    item.DisplayCode = displayCode;
                    item.LevelCode = levelCode;
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = userlogin;
                });
                criteriaUpdate.AddRange(criteriaDelete);
                _repositoryCriteriaEvaluate.UpdateRange(criteriaUpdate);
                _repositoryCriteriaEvaluate.InsertRange(criteriaInsert);
                #endregion Criteria Evaluate Image

                #region Weight For Extra Bonus
                List<DisWeightGetExtraRewardsDetail> listWeightGetExtra = _mapper.Map<List<DisWeightGetExtraRewardsDetail>>(request.WeightGetExtraRewards);
                var changesTrackerWeight = new ChangesTracker<DisWeightGetExtraRewardsDetail>(weightGetExtraRewards, listWeightGetExtra, AreEqualWeight);
                List<DisWeightGetExtraRewardsDetail> weightInsert = new();
                List<DisWeightGetExtraRewardsDetail> weightUpdate = new();
                List<DisWeightGetExtraRewardsDetail> weightDelete = new();
                weightDelete.AddRange(changesTrackerWeight.RemovedItems);
                weightUpdate.AddRange(changesTrackerWeight.UpdatedItems);
                weightInsert.AddRange(changesTrackerWeight.AddedItems);

                weightDelete.ForEach(item =>
                {
                    item.DeleteFlag = 1;
                });
                weightUpdate.ForEach(item =>
                {
                    item.UpdatedBy = userlogin;
                    item.UpdatedDate = DateTime.Now;
                });
                weightInsert.ForEach(item =>
                {
                    item.DisplayCode = displayCode;
                    item.LevelCode = levelCode;
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = userlogin;
                });
                weightUpdate.AddRange(weightDelete);
                _repositoryWeightGetExtraRewardsDetail.UpdateRange(weightUpdate);
                _repositoryWeightGetExtraRewardsDetail.InsertRange(weightInsert);
                #endregion Weight For Extra Bonus

                return await Task.FromResult(levelCode);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Was an error occrred while update display structure");
                throw new ArgumentException(ex.Message);
            }
        }
        private bool AreEqualProduct(DisDefinitionProductTypeDetail a, DisDefinitionProductTypeDetail b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;
            return a.Id == b.Id;
        }
        private bool AreEqualGuideImage(DisDefinitionGuideImage a, DisDefinitionGuideImage b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;
            return a.Id == b.Id;
        }
        private bool AreEqualCriteria(DisDefinitionCriteriaEvaluate a, DisDefinitionCriteriaEvaluate b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;
            return a.Id == b.Id;
        }
        private bool AreEqualWeight(DisWeightGetExtraRewardsDetail a, DisWeightGetExtraRewardsDetail b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null)
                return false;
            return a.Id == b.Id;
        }
        protected class ChangesTracker<T>
        {
            private readonly IEnumerable<T> oldValues;
            private readonly IEnumerable<T> newValues;
            private readonly Func<T, T, bool> areEqual;

            public ChangesTracker(IEnumerable<T> oldValues, IEnumerable<T> newValues, Func<T, T, bool> areEqual)
            {
                this.oldValues = oldValues;
                this.newValues = newValues;
                this.areEqual = areEqual;
            }

            public IEnumerable<T> AddedItems
            {
                get => newValues.Where(n => oldValues.All(o => !areEqual(o, n)));
            }

            public IEnumerable<T> RemovedItems
            {
                get => oldValues.Where(n => newValues.All(o => !areEqual(n, o)));
            }

            public IEnumerable<T> UpdatedItems
            {
                get => newValues.Where(n => oldValues.Any(o => areEqual(o, n)));
            }
        }

        public async Task<List<DisDefinitionStructureModel>> GetAllDataDisDefinitionStructureListByDisplayCode(string displayCode)
        {
            List<DisDefinitionStructureModel> results = new List<DisDefinitionStructureModel>();
            var DefinitionStructures =  await _repository.GetAllQueryable(x => x.DisplayCode == displayCode).OrderBy(x => x.LevelCode).ToListAsync();
            foreach (var item in DefinitionStructures)
            {
                var detailsDefinitionStructure = await GetDisDefinitionStructureByCodeAsync(item.DisplayCode, item.LevelCode);
                results.Add(detailsDefinitionStructure);
            }
            return results;
        }
    }
}
