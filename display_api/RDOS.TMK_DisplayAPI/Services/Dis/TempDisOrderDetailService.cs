using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class TempDisOrderDetailService : ITempDisOrderDetailService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TempDisOrderDetailService> _logger;
        private readonly IBaseRepository<TempDisOrderHeader> _tempOrderHeader;
        private readonly IBaseRepository<TempDisOrderDetail> _tempOrderDetail;
        private readonly IBaseRepository<SystemSetting> _dbSystemSetting;

        public TempDisOrderDetailService(IMapper mapper,
            IBaseRepository<TempDisOrderHeader> tempOrderHeader,
            IBaseRepository<TempDisOrderDetail> tempOrderDetail,
            ILogger<TempDisOrderDetailService> logger,
            IBaseRepository<SystemSetting> dbSystemSetting
            )
        {
            _mapper = mapper;
            _tempOrderHeader = tempOrderHeader;
            _tempOrderDetail = tempOrderDetail;
            _logger = logger;
            _dbSystemSetting = dbSystemSetting;
        }

        public IQueryable<DisSettlementDetailModel> GetTempSettlementDetailAsync(TempDisOrderHeaderParameters parameters)
        {
            var listresult = (from detail in _tempOrderDetail.GetAllQueryable(x => x.TMKType == CommonData.TmktypeSetting.Display
                               && x.RewardPeriodCode == parameters.RewardPeriodCode && x.IsFree).AsNoTracking()
                              join header in _tempOrderHeader.GetAllQueryable(x => x.Status == CommonData.SettlementSetting.Defining
                              && string.IsNullOrEmpty(x.RecallOrderCode)
                              && (x.OrdDate >= parameters.StartDate && x.OrdDate <= parameters.EndDate)
                              ).AsNoTracking()
                               on detail.OrdNbr equals header.OrdNbr into emptyHeader
                              from header in emptyHeader.DefaultIfEmpty()
                              select new DisSettlementDetailModel()
                              {
                                  Id = Guid.NewGuid(),
                                  DisSettlementCode = parameters.Code,
                                  OrdNbr = detail.OrdNbr,
                                  DisplayCode = detail.DiscountId,
                                  DistributorCode = header.DistyBilltoCode??string.Empty,
                                  ProductCode = detail.InventoryId??string.Empty,
                                  PackageCode = detail.Uom??string.Empty,
                                  Quantity = detail.ShippedQty,
                                  Amount = detail.ShippedLineDiscAmt,
                                  DistributorName = header.DistyBilltoName ?? string.Empty,// Nha phan phoi
                                  ProductName = detail.InventoryName?? string.Empty,
                                  Packing = detail.UomName?? string.Empty,

                                  Status = CommonData.SettlementSetting.Defining,
                                  OrdDate = header.OrdDate,
                                  DisplayLevel = detail.DisplayLevel?? string.Empty,
                                  CustomerId = header.CustomerId ?? string.Empty,
                                  ShiptoId = header.ShiptoId ?? string.Empty
                              }).AsQueryable();
            return listresult;
        }

        public async Task<List<TempDisplaySaleOrOutputResponseModel>> GetDataSaleOrOutputByDisplayByPeriodCodeAsync(TempDisplaySaleOrOutputRequestModel parameters)
        {
            return await (from tdoh in _tempOrderHeader.GetAllQueryable(x => x.TMKType == parameters.ProgramType
                         && x.DiscountCode.ToLower().Equals(parameters.DisplayCode.ToLower())
                         && x.Status == CommonData.DisplaySetting.StatusActive
                         && string.IsNullOrEmpty(x.RecallOrderCode)
                         && x.PeriodCode.ToLower().Equals(parameters.PeriodCode.ToLower()))
                          join tdod in _tempOrderDetail.GetAllQueryable(x => x.IsFree)
                          on tdoh.OrdNbr equals tdod.OrdNbr
                          group new { tdoh, tdod } by new
                          {
                              tdoh.TMKType,
                              tdoh.DiscountCode,
                              tdoh.CustomerId,
                              tdoh.ShiptoId,
                              tdoh.DisplayLevel,
                              tdoh.DisplayLevelName
                          } into grp
                          orderby grp.Key.TMKType, grp.Key.DiscountCode, grp.Key.DisplayLevel
                          select new TempDisplaySaleOrOutputResponseModel()
                          {
                              ProgramType = grp.Key.TMKType,
                              DisplayCode = grp.Key.DiscountCode,
                              DisplayLevel = grp.Key.DisplayLevel,
                              CustomerCode = grp.Key.CustomerId,
                              ShiptoCode = grp.Key.ShiptoId,
                              SumSales = grp.Sum(x => x.tdod.ShippedLineDiscAmt).Value,
                              SumOutput = grp.Sum(x => x.tdod.ShippedQty).Value
                          }
                          ).ToListAsync();
        }
    }
}