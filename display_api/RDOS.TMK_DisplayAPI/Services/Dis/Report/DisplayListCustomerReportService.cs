using AutoMapper;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using System.Linq;
using RDOS.TMK_DisplayAPI.Models.External;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public class DisplayListCustomerReportService : IDisplayListCustomerReportService
    {
        #region Property
        private readonly IMapper _mapper;
        private readonly ILogger<DisplayListCustomerReportService> _logger;
        private readonly IBaseRepository<DisApproveRegistrationCustomerDetail> _disApproveRegistrationCustomerDetail;
        private readonly IBaseRepository<DisDefinitionStructure> _disDefinitionStructure;
        private readonly IBaseRepository<DisBudget> _disBudget;
        #endregion

        #region Constructor
        public DisplayListCustomerReportService(IMapper mapper,
            ILogger<DisplayListCustomerReportService> logger,
            IBaseRepository<DisApproveRegistrationCustomerDetail> disApproveRegistrationCustomerDetail,
            IBaseRepository<DisDefinitionStructure> disDefinitionStructure,
            IBaseRepository<DisBudget> disBudget
            )
        {
            _mapper = mapper;
            _logger = logger;
            _disApproveRegistrationCustomerDetail = disApproveRegistrationCustomerDetail;
            _disDefinitionStructure = disDefinitionStructure;
            _disBudget = disBudget;
        }
        #endregion
        public IQueryable<DisplayListCustomerListModel> GetDisplayDetailReport(DisplayReportEcoParameters request)
        {
            var dataReport = (from db in _disBudget.GetAllQueryable(x => x.DisplayCode == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking()
                                  //join dds in _disDefinitionStructure.GetAllQueryable(x => x.DisplayCode == request.DisplayCode && x.DeleteFlag == 0).AsNoTracking()
                                  //on db.DisplayCode equals dds.DisplayCode
                              join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable(x => x.DisplayCode == request.DisplayCode).AsNoTracking()
                              on db.DisplayLevelCode equals darcd.DisplayLevel
                              group darcd by new
                              {
                                  DisplayLevelCode = db.DisplayLevelCode,
                                  //DisplayLevelName = dds.LevelName,
                                  BudgetQuantityUsed = db.BudgetQuantityUsed
                              } into dis19
                              select new DisplayListCustomerListModel()
                              {
                                  DisplayCodeLevel = dis19.Key.DisplayLevelCode,
                                  //DisplayLevelName = dis19.Key.DisplayLevelName,
                                  BudgetQuantityUsed = dis19.Key.BudgetQuantityUsed,
                                  QuantityCustomer = dis19.Select(x => x.CustomerCode).Count()
                              }).AsQueryable();
            return dataReport;
        }
        public IQueryable<ListCustomerConfirmModel> GetListCustomerConfirmReport(DisplayReportEcoParameters request)
        {
            var dataReport = (from darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable(x => x.DisplayCode == request.DisplayCode).AsNoTracking()
                              where darcd.DisplayLevel.Equals(request.Displaylevel)
                              //group darcd by new
                              //{
                              //    DisplayLevelCode = darcd.DisplayLevel,
                              //    Customer = darcd.CustomerCode,
                              //    CustomerShipto = darcd.CustomerShipToCode,
                              //    //CustomerShiptoName = darcd.ad
                              //} into dis19
                              select new ListCustomerConfirmModel()
                              {
                                  Customer = darcd.CustomerCode,
                                  CustomerShipto = darcd.CustomerShipToCode
                              }).AsQueryable();
            return dataReport;
        }
    }
}
