using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using RDOS.TMK_DisplayAPI.Services.Base;
using RDOS.TMK_DisplayAPI.Services.Dis.Report;
using Sys.Common.Constants;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisplaySyntheticReportSettlementService : IDisplaySyntheticReportSettlementService
    {
        #region Property
        private readonly ILogger<DisplaySyntheticReportSettlementService> _logger;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<DisSettlement> _settlement;
        private readonly IBaseRepository<DisSettlementDetail> _settlementDetail;
        private readonly IBaseRepository<Distributor> _dbDistributor;
        #endregion

        #region Constructor
        public DisplaySyntheticReportSettlementService(ILogger<DisplaySyntheticReportSettlementService> logger,
            IMapper mapper,
            IBaseRepository<DisSettlement> settlement,
            IBaseRepository<DisSettlementDetail> settlementDetail,
            IBaseRepository<Distributor> dbDistributor)
        {
            _logger = logger;
            _mapper = mapper;
            _settlement = settlement;
            _settlementDetail = settlementDetail;
            _dbDistributor = dbDistributor;
        }
        #endregion

        #region method
        public IQueryable<DisplaySyntheticReportSettlementListModel> GetDisplayDetailReport(DisplayReportEcoParameters request)
        {
            var resultData = from sm in _settlement.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
                             where sm.DisplayCode.ToLower().Equals(request.DisplayCode.ToLower())
                             join smd in _settlementDetail.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
                             on sm.Code equals smd.DisSettlementCode
                             group smd by new
                             {
                                 SettlementCode = sm.Code,
                                 SettlementName = sm.Name,
                                 RewwardPeriodCode = sm.RewardPeriodCode,
                             } into gsett
                             select new DisplaySyntheticReportSettlementListModel()
                             {
                                 Code = gsett.Key.SettlementCode,
                                 Name = gsett.Key.SettlementName,
                                 RewwardPeriodCode = gsett.Key.RewwardPeriodCode,
                                 DistributorQuantity = gsett.Select(x => x.DistributorCode).Distinct().Count(),
                                 DistributorQuantityConfirm = gsett.Where(x => x.Status.Equals(CommonData.SettlementDisplay.Confirm)).Select(x => x.DistributorCode).Distinct().Count(),
                                 DistributorQuantityUnConfirm = gsett.Where(x => x.Status.Equals(CommonData.SettlementDisplay.Create)).Select(x => x.DistributorCode).Distinct().Count()
                             };

            return resultData;
        }

        public IQueryable<DistributorPopupReportSettlementListModel> GetListDistributorPopupReportSettlement(string settlementCode)
        {
            var resultData = (from smd in _settlementDetail.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
                              join db in _dbDistributor.GetAllQueryable(x => x.DeleteFlag == 0)
                              on smd.DistributorCode equals db.Code into emptyDistributor
                              from db in emptyDistributor.DefaultIfEmpty()
                              where smd.DisSettlementCode.ToLower().Equals(settlementCode.ToLower())
                              select new DistributorPopupReportSettlementListModel()
                              {
                                  SettlementCode = smd.DisSettlementCode,
                                  DistributorCode = smd.DistributorCode,
                                  DistributorName = db.Name,
                                  Confirm = smd.Status.Equals(CommonData.SettlementDisplay.Confirm),
                                  UnConfirm = smd.Status.Equals(CommonData.SettlementDisplay.Create)
                              }).Distinct();

            return resultData;
        }
        #endregion
    }
}
