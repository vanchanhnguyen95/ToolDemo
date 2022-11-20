using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using System.Collections.Generic;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisSettlementDetailService : IDisSettlementDetailService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DisSettlementDetailService> _logger;
        private readonly IBaseRepository<DisSettlementDetail> _repository;
        private readonly IBaseRepository<SystemSetting> _dbSystemSetting;
        private readonly IBaseRepository<Distributor> _dbDistributor;
        private readonly IBaseRepository<Uom> _dbUom;
        private readonly IBaseRepository<InventoryItem> _dbInventoryItem;

        public DisSettlementDetailService(IMapper mapper,
            IBaseRepository<DisSettlementDetail> repository,
            ILogger<DisSettlementDetailService> logger,
            IBaseRepository<SystemSetting> dbSystemSetting,
            IBaseRepository<Distributor> dbDistributor,
            IBaseRepository<Uom> dbUom,
            IBaseRepository<InventoryItem> dbInventoryItem
            )
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _dbSystemSetting = dbSystemSetting;
            _dbDistributor = dbDistributor;
            _dbUom = dbUom;
            _dbInventoryItem = dbInventoryItem;
        }

        public IQueryable<DisSettlementDetailModel> GetListSettlementDetailByCodeAsync(string code)
        {
            var systemSettings = _dbSystemSetting.GetAllQueryable(x => x.IsActive).AsNoTracking().AsQueryable();
            var distributor = _dbDistributor.GetAllQueryable().AsNoTracking().AsQueryable();
            var inventoryItem = _dbInventoryItem.GetAllQueryable(x => x.DelFlg == 0).AsNoTracking().AsQueryable();
            var uom = _dbUom.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking().AsQueryable();

            var settlement = _repository.FirstOrDefault(x => x.DisSettlementCode.ToLower().Equals(code.ToLower()));
            if (settlement == null)
            {
                return (new List<DisSettlementDetailModel>()).AsQueryable();
            }

            return (from d in _repository.GetAllQueryable(x => x.DisSettlementCode == code).AsNoTracking()
                              join dis in distributor.Where(x => x.DeleteFlag == 0).AsNoTracking()
                               on d.DistributorCode equals dis.Code into emptyDistribu
                              from dis in emptyDistribu.DefaultIfEmpty()
                              join inv in inventoryItem.Where(x => x.DelFlg == 0).AsNoTracking()
                              on d.ProductCode equals inv.InventoryItemId into emptyInv
                              from inv in emptyInv.DefaultIfEmpty()
                              join uo in uom.Where(x => x.DeleteFlag == 0).AsNoTracking()
                               on d.PackageCode equals uo.UomId into emptyUo
                              from uo in emptyUo.DefaultIfEmpty()
                              select new DisSettlementDetailModel()
                              {
                                  Id = d.Id,
                                  DisSettlementCode = d.DisSettlementCode,
                                  OrdNbr = d.OrdNbr,
                                  DisplayCode = d.DisplayCode,
                                  DistributorCode = d.DistributorCode,
                                  ProductCode = d.ProductCode,
                                  PackageCode = d.PackageCode,
                                  Quantity = d.Quantity??0,
                                  Amount = d.Amount??0,
                                  DistributorName = dis.Name,
                                  ProductName = inv.Description,
                                  Packing = uo.Description,
                                  Status = d.Status,
                                  OrdDate = d.OrdDate,
                                  DisplayLevel = d.DisplayLevel,
                                  CustomerId = d.CustomerId,
                                  ShiptoId = d.ShiptoId
                              }).AsQueryable();
        }

    }
}