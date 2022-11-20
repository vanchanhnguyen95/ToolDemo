using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Settlement;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisSettlementService : IDisSettlementService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DisSettlementService> _logger;
        private readonly IBaseRepository<DisSettlement> _settlement;
        private readonly IBaseRepository<DisSettlementDetail> _settlementDetail;
        private readonly IBaseRepository<SystemSetting> _systemSettingService;
        private readonly IBaseRepository<DisPayReward> _payReward;
        private readonly IBaseRepository<DisDisplay> _display;
        private readonly IBaseRepository<Distributor> _dbDistributor;
        private readonly IBaseRepository<InventoryItem> _dbSku;
        private readonly IBaseRepository<Uom> _dbUom;
        private readonly IBaseRepository<CustomerShipto> _serviceCustomerShipto;
        private readonly IBaseRepository<CustomerInformation> _serviceCustomerInformation;
        private readonly IBaseRepository<DisDefinitionStructure> _serviceDisDefinitionStructure;

        public DisSettlementService(IMapper mapper,
            ILogger<DisSettlementService> logger,
            IBaseRepository<DisSettlement> settlement,
            IBaseRepository<DisSettlementDetail> settlementDetail,
            IBaseRepository<Distributor> dbDistributor,
            IBaseRepository<InventoryItem> dbSku,
            IBaseRepository<Uom> dbUom,
            IBaseRepository<DisDisplay> display,
            IBaseRepository<SystemSetting> systemSettingService,
            IBaseRepository<DisPayReward> payReward,
            IBaseRepository<CustomerShipto> serviceCustomerShipto,
            IBaseRepository<CustomerInformation> serviceCustomerInformation,
            IBaseRepository<DisDefinitionStructure> serviceDisDefinitionStructure
            )
        {
            _mapper = mapper;
            _logger = logger;
            _settlement = settlement;
            _settlementDetail = settlementDetail;
            _dbDistributor = dbDistributor;
            _dbSku = dbSku;
            _dbUom = dbUom;
            _display = display;
            _systemSettingService = systemSettingService;
            _payReward = payReward;
            _serviceCustomerShipto = serviceCustomerShipto;
            _serviceCustomerInformation = serviceCustomerInformation;
            _serviceDisDefinitionStructure = serviceDisDefinitionStructure;
        }

        public Task<IQueryable<DisSettlementModel>> GetListSettlementAsync()
        {
            var systemSettings = _systemSettingService.GetAllQueryable(x => x.IsActive && x.SettingType == CommonData.SystemSetting.SettlementStatus).AsQueryable();
            return Task.FromResult((
                from d in _settlement.GetAllQueryable(x => x.DeleteFlag == 0
                && (x.Status == CommonData.SettlementSetting.Defining || x.Status == CommonData.SettlementSetting.WaitConfirm)).AsNoTracking()
                join s in systemSettings on d.Status equals s.SettingKey
                select new DisSettlementModel
                {
                    Id = d.Id,
                    Code = d.Code,
                    Name = d.Name,
                    Status = d.Status,
                    DisplayCode = d.DisplayCode,
                    RewardPeriodCode = d.RewardPeriodCode,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    StatusName = s.Description
                }).AsQueryable());

        }
        public List<DisSettlementModel> GetListSettlementGeneralAsync()
        {
            return _settlement.GetAllQueryable(x => x.DeleteFlag == 0 && x.Status != CommonData.SettlementDisplay.Inprogress).AsNoTracking()
                .Select(x => new DisSettlementModel()
                {
                    Code = x.Code,
                    Name = x.Name
                }).ToList();
        }
        public void CreateSettlement(DisSettlementModel input, string userlogin)
        {
            // Create Settlement
            var settlement = _mapper.Map<DisSettlement>(input);

            settlement.Id = Guid.NewGuid();
            settlement.CreatedBy = userlogin;
            settlement.CreatedDate = DateTime.Now;
            settlement.DeleteFlag = 0;
            _settlement.Insert(settlement);

            // Create SettlementDetail
            var lstSettlementDetail = CreateSettlementDetail(input.DisSettlementDetail, settlement.Code, userlogin);
            _settlementDetail.InsertRange(lstSettlementDetail);
        }

        public void UpdateSettlement(DisSettlementModel input, string userlogin)
        {
            // Update settlement
            var settlement = _mapper.Map<DisSettlement>(input);
            settlement.UpdatedBy = userlogin;
            settlement.UpdatedDate = DateTime.Now;
            _settlement.Update(settlement);

            // Update SettlementDetail
            List<DisSettlementDetail> lstUpdate = _settlementDetail.GetAllQueryable(x => x.DisSettlementCode == settlement.Code).ToList();
            lstUpdate.ForEach(x => {
                x.UpdatedBy = userlogin;
                x.UpdatedDate = DateTime.Now;
            });
            _settlementDetail.UpdateRange(lstUpdate);

        }

        private List<DisSettlementDetail> CreateSettlementDetail(List<DisSettlementDetail> lstInput
            , string resultCode, string userlogin, bool isUpdate = false, bool isDelete = false)
        {
            var lstResultDetail = _mapper.Map<List<DisSettlementDetail>>(lstInput);
            if (isUpdate)
            {
                // Detail
                lstResultDetail.ForEach(x => {
                    x.UpdatedBy = userlogin;
                    x.UpdatedDate = DateTime.Now;
                });

                return lstResultDetail;

            }
            else if (isDelete)
            {
                // Detail
                lstResultDetail.ForEach(x => {
                    x.DeleteFlag = 1;
                });
            }
            else
            {
                // Detail
                lstResultDetail.ForEach(x => {
                    x.Id = Guid.NewGuid();
                    x.DisSettlementCode = resultCode;
                    x.CreatedBy = userlogin;
                    x.CreatedDate = DateTime.Now;
                    x.DeleteFlag = 0;
                });
            }
            return lstResultDetail;
        }

        public async Task<DisSettlementModel> GetSettlementByCodeAsync(string code)
        {
            var settlement = await _settlement.GetAllQueryable(x => x.Code.ToLower() == code.ToLower() && x.DeleteFlag == 0).AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<DisSettlementModel>(settlement);
        }

        public DisSettlementModel GetSettlementByCode(string code)
        {
            var returnValue = _mapper.Map<DisSettlementModel>(_settlement.GetAllQueryable().AsNoTracking()
               .Where(m => m.Code == code && m.DeleteFlag == 0).FirstOrDefault());
            return returnValue;
        }

        public void DeleteSettlement(DisSettlementModel input, string userlogin)
        {
            // Update Settlement
            var settlement = _mapper.Map<DisSettlement>(input);
            settlement.DeleteFlag = 1;
            _settlement.Update(settlement);

            // Update SettlementDetail
            List<DisSettlementDetail> lstUpdate = _settlementDetail.GetAllQueryable(x => x.DisSettlementCode == settlement.Code).ToList();
            lstUpdate.ForEach(x => { x.DeleteFlag = 1;});
            _settlementDetail.UpdateRange(lstUpdate);

        }

        public IQueryable<DisPayRewardDisplayModel> GetListPayRewardByDisplayCode(string displayCode)
        {
            if (string.IsNullOrEmpty(displayCode))
            {
                return (new List<DisPayRewardDisplayModel>()).AsQueryable();
            }
            else
            {
                return (
                    from d in _payReward.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisplayCode == displayCode && x.Status == CommonData.PayRewardSetting.Confirm).AsNoTracking()
                    select new DisPayRewardDisplayModel
                    {
                        Id = d.Id,
                        Code = d.Code,
                        Status = d.Status,
                        Name = d.Name,
                        DisplayCode = d.DisplayCode,
                        ConfirmResultDisplayCode = d.ConfirmResultDisplayCode,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        PayRewardMethod = d.PayRewardMethod
                    }).AsQueryable();
            }
        }

        #region DisSettlementConfirmByDistributor
        public IQueryable<DisSettlementConfirmModel> GetListSettlementConfirm()
        {
            var distributors = _dbDistributor.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking();
            var products = _dbSku.GetAllQueryable(x => x.DelFlg == 0).AsNoTracking();
            var display = _display.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking();
            var systemSettings = _systemSettingService.GetAllQueryable(x => x.IsActive && x.SettingType.Equals(CommonData.SystemSetting.DistributorConfirmSettlement)).AsQueryable();

            var result = (from sett in _settlement.GetAllQueryable(x => x.DeleteFlag == 0 && x.Status != CommonData.SettlementDisplay.Inprogress).AsNoTracking()
                          join settdet in _settlementDetail.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
                          on sett.Code.ToLower() equals settdet.DisSettlementCode.ToLower()
                          join sta in systemSettings on settdet.Status.ToLower() equals sta.SettingKey.ToLower()
                          join dis in display on settdet.DisplayCode.ToLower() equals dis.Code.ToLower()
                          join distributor in distributors on settdet.DistributorCode.ToLower() equals distributor.Code.ToLower()
                          join sku in products on settdet.ProductCode.ToLower() equals sku.InventoryItemId.ToLower() into emptySku
                          from sku in emptySku.DefaultIfEmpty()
                          join u in _dbUom.GetAllQueryable().AsNoTracking() on settdet.PackageCode.ToLower() equals u.UomId.ToLower() into emptyUom
                          from u in emptyUom.DefaultIfEmpty()
                          group settdet by new DisSettlementConfirmModel()
                          {
                              SettlementCode = sett.Code,
                              SettlementName = sett.Name,
                              DistributorCode = settdet.DistributorCode,
                              DistributorName = distributor.Name,
                              DisplayCode = settdet.DisplayCode,
                              DisplayName = dis.ShortName,
                              PackageCode = settdet.PackageCode,
                              PackageName = u != null ? u.Description : string.Empty,
                              ProductCode = settdet.ProductCode,
                              ProductName = sku != null ? sku.ShortName : string.Empty,
                              Status = settdet.Status,
                              StatusDescription = sta.Description
                          } into gsett
                          select new DisSettlementConfirmModel()
                          {
                              SettlementCode = gsett.Key.SettlementCode,
                              SettlementName = gsett.Key.SettlementName,
                              DistributorCode = gsett.Key.DistributorCode,
                              DistributorName = gsett.Key.DistributorName,
                              DisplayCode = gsett.Key.DisplayCode,
                              DisplayName = gsett.Key.DisplayName,
                              PackageCode = gsett.Key.PackageCode,
                              PackageName = gsett.Key.PackageName,
                              ProductCode = gsett.Key.ProductCode,
                              ProductName = gsett.Key.ProductName,
                              Status = gsett.Key.Status,
                              StatusDescription = gsett.Key.StatusDescription,
                              Quantity = (gsett.Select(x => x.Quantity).Sum() == null || gsett.Select(x => x.Quantity).Sum() == 0) ? null : gsett.Select(x => x.Quantity).Sum(),
                              Amount = (gsett.Select(x => x.Amount).Sum() == null || gsett.Select(x => x.Amount).Sum() == 0) ? null : gsett.Select(x => x.Amount).Sum()
                          }).AsQueryable();
            return result;
        }
        public IQueryable<DisSettlementDetailModel> GetListDetailSettlementConfirmByDistributor(string code, string distributorCode)
        {
            var listCustomerShipto = (from customershipto in _serviceCustomerShipto.GetAllQueryable().AsNoTracking()
                                      join customer in _serviceCustomerInformation.GetAllQueryable().AsNoTracking()
                                      on customershipto.CustomerInfomationId equals customer.Id into emptyCustomershipto
                                      from customer in emptyCustomershipto.DefaultIfEmpty()
                                      select new CustomerShiptoModel()
                                      {
                                          CustomerCode = customer.CustomerCode,
                                          CustomerName = customer.FullName,
                                          ShiptoCode = customershipto.ShiptoCode,
                                          Address = customershipto.Address
                                      }).AsNoTracking().AsQueryable();

            var result = (from td in _settlementDetail.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisSettlementCode.ToLower().Equals(code.ToLower())
                          && x.DistributorCode.ToLower().Equals(distributorCode.ToLower())).AsNoTracking()

                          join u in _dbUom.GetAllQueryable().AsNoTracking() on td.PackageCode.ToLower() equals u.UomId.ToLower()
                          into uData
                          from u in uData.DefaultIfEmpty()

                          join sku in _dbSku.GetAllQueryable(x => x.DelFlg == 0).AsNoTracking()
                          on td.ProductCode.ToLower() equals sku.InventoryItemId.ToLower()
                          into skuData
                          from sku in skuData.DefaultIfEmpty()

                          join customershipto in listCustomerShipto on
                          new { customer_code = td.CustomerId, customer_shipto_code = td.ShiptoId } equals
                          new { customer_code = customershipto.CustomerCode, customer_shipto_code = customershipto.ShiptoCode }
                          into temptyCustomerShipto
                          from customershipto in temptyCustomerShipto.DefaultIfEmpty()

                          join level in _serviceDisDefinitionStructure.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking() on
                          //on td.DisplayLevel equals level.LevelCode
                          new { levelCode = td.DisplayLevel, displayCode = td.DisplayCode } equals
                          new { levelCode = level.LevelCode, displayCode = level.DisplayCode }
                          into dataLevel
                          from level in dataLevel.DefaultIfEmpty()

                          join display in _display.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking() on td.DisplayCode.ToLower() equals display.Code.ToLower()
                          into dataDisplay
                          from display in dataDisplay.DefaultIfEmpty()

                          select new DisSettlementDetailModel()
                          {
                              Id = td.Id,
                              OrdNbr = string.IsNullOrEmpty(td.OrdNbr) ? string.Empty : td.OrdNbr,
                              OrdDate = td.OrdDate,
                              DisSettlementCode = td.DisSettlementCode,
                              DistributorCode = td.DistributorCode,
                              ProductCode = string.IsNullOrEmpty(td.ProductCode) ? string.Empty : td.ProductCode,
                              ProductName = (sku != null) ? sku.ShortName : string.Empty,
                              PackageCode = string.IsNullOrEmpty(td.PackageCode) ? string.Empty : td.PackageCode,
                              Packing = (u != null) ? u.Description : string.Empty,
                              Quantity = td.Quantity,
                              Amount = td.Amount,
                              CustomerId = string.IsNullOrEmpty(td.CustomerId) ? string.Empty : td.CustomerId,
                              CustomerName = (customershipto != null) ? customershipto.CustomerName : string.Empty,
                              DisplayLevel = string.IsNullOrEmpty(td.DisplayLevel) ? string.Empty : td.DisplayLevel,
                              DisplayLevelName = (level != null) ? level.LevelName : string.Empty,
                              DisplayCode = string.IsNullOrEmpty(td.DisplayCode) ? string.Empty : td.DisplayCode,
                              DisplayName = (display != null) ? display.ShortName : string.Empty,
                              ShiptoId = string.IsNullOrEmpty(td.ShiptoId) ? string.Empty : td.ShiptoId,
                              ShiptoName = (customershipto != null) ? customershipto.Address : string.Empty
                          }).AsQueryable().AsNoTracking();
            return result;
        }

        public IQueryable<DisSettlementConfirmModel> GetListSettlementConfirmByDistributor(string distributorCode)
        {
            var distributors = _dbDistributor.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking();
            var systemSettings = _systemSettingService.GetAllQueryable(x => x.IsActive).AsQueryable();

            var result = (from sett in _settlement.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
                          join settdet in _settlementDetail.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
                          on sett.Code.ToLower() equals settdet.DisSettlementCode.ToLower()
                          join sta in systemSettings.Where(x => x.SettingType == CommonData.SystemSetting.DistributorConfirmSettlement).AsNoTracking()
                          on settdet.Status.ToLower() equals sta.SettingKey.ToLower()
                          join dis in distributors on settdet.DistributorCode.ToLower() equals dis.Code.ToLower()
                          join display in _display.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking() on settdet.DisplayCode.ToLower() equals display.Code.ToLower()
                          into dataDisplay
                          from display in dataDisplay.DefaultIfEmpty()
                          where settdet.DistributorCode.ToLower().Equals(distributorCode.ToLower())
                          && settdet.Status.ToLower().Equals(CommonData.SettlementDisplay.Create.ToLower())
                          group settdet by new DisSettlementConfirmModel()
                          {
                              SettlementCode = sett.Code,
                              SettlementName = sett.Name,
                              DistributorCode = settdet.DistributorCode,
                              DistributorName = dis.Name,
                              DisplayCode = sett.DisplayCode,
                              DisplayName = (display != null) ? display.ShortName : string.Empty,
                              //ProductCode = settdet.ProductCode,
                              //PackageCode = settdet.PackageCode,
                              Status = settdet.Status,
                              StatusDescription = sta.Description
                          } into gsett
                          select new DisSettlementConfirmModel()
                          {
                              SettlementCode = gsett.Key.SettlementCode,
                              SettlementName = gsett.Key.SettlementName,
                              DistributorCode = gsett.Key.DistributorCode,
                              DistributorName = gsett.Key.DistributorName,
                              DisplayCode = gsett.Key.DisplayCode,
                              DisplayName = gsett.Key.DisplayName,
                              //ProductCode = gsett.Key.ProductCode,
                              //PackageCode = gsett.Key.PackageCode,
                              Status = gsett.Key.Status,
                              StatusDescription = gsett.Key.StatusDescription,
                              Quantity = (gsett.Select(x => x.Quantity).Sum() == null || gsett.Select(x => x.Quantity).Sum() == 0) ? null : gsett.Select(x => x.Quantity).Sum(),
                              Amount = (gsett.Select(x => x.Amount).Sum() == null || gsett.Select(x => x.Amount).Sum() == 0) ? null : gsett.Select(x => x.Amount).Sum()
                          }).AsQueryable();

            return result;
        }

        public BaseResultModel ConfirmSettlementByDistributor(string distributorCode, List<string> lstInput)
        {
            try
            {
                var lstSettlement = _settlement.GetAllQueryable(x => x.DeleteFlag == 0 && lstInput.Contains(x.Code)).ToList();
                if (lstSettlement != null && lstSettlement.Any())
                {
                    // update settlement
                    lstSettlement.ForEach(x => x.Status = CommonData.SettlementDisplay.confirmed);
                    _settlement.UpdateRange(lstSettlement);

                    // update settlement Details
                    var lstSettlementDetail = _settlementDetail.GetAllQueryable(x => x.DeleteFlag == 0 &&
                    x.DistributorCode.ToLower().Equals(distributorCode.ToLower()) && lstInput.Contains(x.DisSettlementCode)).ToList();

                    if (lstSettlementDetail != null && lstSettlementDetail.Any())
                    {
                        lstSettlementDetail.ForEach(x => x.Status = CommonData.SettlementDisplay.Confirm);
                        _settlementDetail.UpdateRange(lstSettlementDetail);
                    }
                }

                return new BaseResultModel
                {
                    IsSuccess = true,
                    Code = 201,
                    Message = "ConfirmSuccess"
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
        #endregion
    }
}