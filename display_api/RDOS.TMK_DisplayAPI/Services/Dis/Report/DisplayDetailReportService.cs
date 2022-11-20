using AutoMapper;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Services.Base;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public class DisplayDetailReportService : IDisplayDetailReportService
    {
        #region Property
        private readonly IMapper _mapper;
        private readonly ILogger<DisplayDetailReportService> _logger;
        private readonly IBaseRepository<DisCustomerShipto> _dbDisCustomerShipto;
        private readonly IBaseRepository<DisCustomerShiptoDetail> _dbDisCustomerShiptoDetail;
        private readonly IBaseRepository<CustomerShipto> _serviceCustomerShipto;
        private readonly IBaseRepository<CustomerInformation> _serviceCustomerInformation;

        private readonly IBaseRepository<ScSalesOrganizationStructure> _dbSaleOrg;
        private readonly IBaseRepository<DsaDistributorSellingArea> _dbDsa;
        private readonly IBaseRepository<RzRouteZoneInfomation> _dbRzInfo;
        private readonly IBaseRepository<RzRouteZoneShipto> _dbRzShipto;
        private readonly IBaseRepository<ScTerritoryMapping> _dbScTerritoryMapping;
        private readonly IBaseRepository<ScTerritoryValue> _dbScTerritoryValue;

        private readonly IBaseRepository<DisBudget> _disBudget;
        #endregion

        #region Constructor
        public DisplayDetailReportService(IMapper mapper,
            ILogger<DisplayDetailReportService> logger,
            IBaseRepository<DisCustomerShipto> dbDisCustomerShipto,
            IBaseRepository<DisCustomerShiptoDetail> dbDisCustomerShiptoDetail,
            IBaseRepository<CustomerShipto> serviceCustomerShipto,
            IBaseRepository<CustomerInformation> serviceCustomerInformation,

            IBaseRepository<ScSalesOrganizationStructure> dbSaleOrg,
            IBaseRepository<DsaDistributorSellingArea> dbDsa,
            IBaseRepository<RzRouteZoneInfomation> dbRzInfo,
            IBaseRepository<RzRouteZoneShipto> dbRzShipto,
            IBaseRepository<ScTerritoryMapping> dbScTerritoryMapping,
            IBaseRepository<ScTerritoryValue> dbScTerritoryValue,
            IBaseRepository<DisBudget> disBudget
            )
        {
            _mapper = mapper;
            _logger = logger;
            _dbDisCustomerShipto = dbDisCustomerShipto;
            _dbDisCustomerShiptoDetail = dbDisCustomerShiptoDetail;
            _serviceCustomerShipto = serviceCustomerShipto;
            _serviceCustomerInformation = serviceCustomerInformation;

            _dbSaleOrg = dbSaleOrg;
            _dbDsa = dbDsa;
            _dbRzInfo = dbRzInfo;
            _dbRzShipto = dbRzShipto;
            _dbScTerritoryMapping = dbScTerritoryMapping;
            _dbScTerritoryValue = dbScTerritoryValue;

            _disBudget = disBudget;
        }
        #endregion
        public IQueryable<DisplayDetailReportListModel> GetDisplayDetailReport(DisplayReportEcoParameters request)
        {
            var data = _dbDisCustomerShipto.GetAllQueryable(x => x.DisplayCode == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking()
                       .ProjectTo<DisCustomerShiptoModel>(_mapper.ConfigurationProvider).ToList();
            var dataReport = (from db in _disBudget.GetAllQueryable(x => x.DisplayCode == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking()
                              join dt in _dbDisCustomerShipto.GetAllQueryable(x => x.DisplayCode == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking()
                              on db.DisplayLevelCode equals dt.DisplayLevelCode
                              select new DisplayDetailReportListModel()
                              {
                                  DisplayCode = db.DisplayCode,
                                  DisplayCodeLevel = db.DisplayLevelCode,
                                  BudgetQuantityUsed = db.BudgetQuantityUsed,
                                  BudgetSalePoint = dt.TotalSalePoint
                              }).AsQueryable();
            if (data != null && data.Any())
            {
                var listCustomerShipto = (from customershipto in _serviceCustomerShipto.GetAllQueryable().AsNoTracking()
                                          join customer in _serviceCustomerInformation.GetAllQueryable().AsNoTracking()
                                          on customershipto.CustomerInfomationId equals customer.Id into emptyCustomershipto
                                          from customer in emptyCustomershipto.DefaultIfEmpty()
                                          select new CustomerShiptoModel()
                                          {
                                              CustomerCode = customer.CustomerCode,
                                              ShiptoCode = customershipto.ShiptoCode,
                                              Address = customershipto.Address
                                          }).AsNoTracking().AsQueryable();

                var lstDataCustomerShipto = (from detail in _dbDisCustomerShiptoDetail.GetAllQueryable(x => x.DisplayCode.ToLower().Equals(request.DisplayCode.ToLower())).AsNoTracking()
                                             join customershipto in listCustomerShipto on
                                             new { customer_code = detail.CustomerCode, customer_shipto_code = detail.CustomerShiptoCode } equals
                                             new { customer_code = customershipto.CustomerCode, customer_shipto_code = customershipto.ShiptoCode }
                                             into temptyCustomerShipto
                                             from customershipto in temptyCustomerShipto.DefaultIfEmpty()
                                             select new DisCustomerShiptoDetailModel()
                                             {
                                                 Id = detail.Id,
                                                 DisplayCode = detail.DisplayCode,
                                                 DisplayLevelCode = detail.DisplayLevelCode,
                                                 CustomerCode = detail.CustomerCode,
                                                 CustomerShiptoCode = detail.CustomerShiptoCode,
                                                 Address = (customershipto != null) ? customershipto.Address : string.Empty
                                             }).AsNoTracking().ToList();

                foreach (var item in data)
                {
                    item.DisCustomerShiptoDetailModels = lstDataCustomerShipto.Where(x => x.DisplayLevelCode == item.DisplayLevelCode).ToList();
                }
            }
            return dataReport;
        }
    }
}
