using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public class DisplayProgressReportService : IDisplayProgressReportService
    {
        #region Property
        private readonly ILogger<DisplayProgressReportService> _logger;
        private readonly IBaseRepository<DisDisplay> _disDisplay;
        private readonly IBaseRepository<DisBudget> _disBudget;
        private readonly IBaseRepository<DisBudgetForScopeTerritory> _disBudgetForScopeTerritory;
        private readonly IBaseRepository<DisBudgetForScopeDsa> _disBudgetForScopeDsa;
        private readonly IBaseRepository<DisCustomerAttributeValue> _disBudgetForCusAttribute;
        private readonly IBaseRepository<DisApproveRegistrationCustomerDetail> _disApproveRegistrationCustomerDetail;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public DisplayProgressReportService(ILogger<DisplayProgressReportService> logger,
            IBaseRepository<DisDisplay> disDisplay,
            IBaseRepository<DisBudget> disBudget,
            IBaseRepository<DisBudgetForScopeTerritory> disBudgetForScopeTerritory,
            IBaseRepository<DisBudgetForScopeDsa> disBudgetForScopeDsa,
            IBaseRepository<DisCustomerAttributeValue> disBudgetForCusAttribute,
            IBaseRepository<DisApproveRegistrationCustomerDetail> disApproveRegistrationCustomerDetail,
            IMapper mapper
            )
        {
            _logger = logger;
            _disDisplay = disDisplay;
            _disBudget = disBudget;
            _disBudgetForScopeTerritory = disBudgetForScopeTerritory;
            _disBudgetForScopeDsa = disBudgetForScopeDsa;
            _disBudgetForCusAttribute = disBudgetForCusAttribute;
            _disApproveRegistrationCustomerDetail = disApproveRegistrationCustomerDetail;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public IQueryable<DisplayProgressReportListModel> GetDisplayDetailForDisplayProgressReport(DisplayReportEcoParameters request)
        {
            if ((request.ScopeType.Equals(CommonData.DisplaySetting.ScopeNationwide) || request.ScopeType.Equals(CommonData.DisplaySetting.ScopeSalesTerritoryLevel))
                && (request.ApplicableObjectType.Equals(CommonData.DisplaySetting.ObjectAllSalePoint) || request.ApplicableObjectType.Equals(CommonData.DisplaySetting.ObjectCustomerShipto)))
            {
                var query = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                             join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                             on dd.Code equals db.DisplayCode
                             join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                             on db.DisplayLevelCode equals darcd.DisplayLevel
                             where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                             && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                             && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                             && db.Type.Equals(1)
                             group darcd by new
                             {
                                 DisplayCode = dd.Code,
                                 ShortName = dd.ShortName,
                                 RegistrationStartDate = dd.RegistrationStartDate,
                                 RegistrationEndDate = dd.RegistrationEndDate,
                                 ImplementationStartDate = dd.ImplementationStartDate,
                                 ImplementationEndDate = dd.ImplementationEndDate,
                                 ProgramCloseDate = dd.ProgramCloseDate,
                                 ScopeType = dd.ScopeType,
                                 ApplicableObject = dd.ApplicableObjectType,
                                 DisplayLevel = darcd.DisplayLevel,
                                 BudgetQuantity = db.TotalBudget,
                                 BudgetQuantityUsed = db.BudgetQuantityUsed,
                                 Status = dd.Status
                             } into modelDIS15
                             select new DisplayProgressReportListModel()
                             {
                                 Code = modelDIS15.Key.DisplayCode,
                                 ShortName = modelDIS15.Key.ShortName,
                                 RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                 RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                 ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                 ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                 ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                 Scope = modelDIS15.Key.ScopeType,
                                 ApplicableObject = modelDIS15.Key.ApplicableObject,
                                 DisplayLevel = modelDIS15.Key.DisplayLevel,
                                 BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                 BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                 SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                 Status = modelDIS15.Key.Status
                             }).AsQueryable().Distinct();


                var queryIsListScope = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                        join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                        on dd.Code equals db.DisplayCode
                                        join dbfst in _disBudgetForScopeTerritory.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                        on db.DisplayLevelCode equals dbfst.DisplayLevelCode
                                        join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                        on db.DisplayLevelCode equals darcd.DisplayLevel
                                        where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                        && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                        && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                        && db.Type.Equals(1)
                                        group darcd by new
                                        {
                                            DisplayCode = dd.Code,
                                            ShortName = dd.ShortName,
                                            RegistrationStartDate = dd.RegistrationStartDate,
                                            RegistrationEndDate = dd.RegistrationEndDate,
                                            ImplementationStartDate = dd.ImplementationStartDate,
                                            ImplementationEndDate = dd.ImplementationEndDate,
                                            ProgramCloseDate = dd.ProgramCloseDate,
                                            ScopeType = dd.ScopeType,
                                            scopeValue = dbfst.ScopeValue,
                                            ApplicableObject = dd.ApplicableObjectType,
                                            DisplayLevel = darcd.DisplayLevel,
                                            BudgetQuantity = db.TotalBudget,
                                            BudgetQuantityUsed = db.BudgetQuantityUsed,
                                            Status = dd.Status
                                        } into modelDIS15
                                        select new DisplayProgressReportListModel()
                                        {
                                            Code = modelDIS15.Key.DisplayCode,
                                            ShortName = modelDIS15.Key.ShortName,
                                            RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                            RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                            ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                            ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                            ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                            Scope = modelDIS15.Key.ScopeType,
                                            ScopeValue = modelDIS15.Key.scopeValue,
                                            ApplicableObject = modelDIS15.Key.ApplicableObject,
                                            DisplayLevel = modelDIS15.Key.DisplayLevel,
                                            BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                            BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                            SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                            Status = modelDIS15.Key.Status
                                        }).AsQueryable();
                if (!string.IsNullOrEmpty(request.ScopeType) && request.ListScope is not null && request.ListScope.Any())
                {
                    queryIsListScope = queryIsListScope.Where(x => request.ListScope.Contains(x.ScopeValue));
                    return queryIsListScope;
                }
                else return query;
            }

            else if ((request.ScopeType.Equals(CommonData.DisplaySetting.ScopeNationwide) || request.ScopeType.Equals(CommonData.DisplaySetting.ScopeSalesTerritoryLevel))
                && (request.ApplicableObjectType.Equals(CommonData.DisplaySetting.ObjectCustomerAttributes)))
            {
                var query = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                             join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                             on dd.Code equals db.DisplayCode
                             join dbfst in _disBudgetForScopeTerritory.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                             on db.DisplayLevelCode equals dbfst.DisplayLevelCode
                             join dbfca in _disBudgetForCusAttribute.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                             on db.DisplayCode equals dbfca.DisplayCode
                             join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                             on db.DisplayLevelCode equals darcd.DisplayLevel
                             where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                             && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                             && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                             && db.Type.Equals(1)
                             group darcd by new
                             {
                                 DisplayCode = dd.Code,
                                 ShortName = dd.ShortName,
                                 RegistrationStartDate = dd.RegistrationStartDate,
                                 RegistrationEndDate = dd.RegistrationEndDate,
                                 ImplementationStartDate = dd.ImplementationStartDate,
                                 ImplementationEndDate = dd.ImplementationEndDate,
                                 ProgramCloseDate = dd.ProgramCloseDate,
                                 ScopeType = dd.ScopeType,
                                 ScopeValue = dbfst.ScopeValue,
                                 ApplicableObject = dd.ApplicableObjectType,
                                 ApplicableObjectValue = dbfca.CustomerAttributerValue,
                                 DisplayLevel = darcd.DisplayLevel,
                                 BudgetQuantity = db.TotalBudget,
                                 BudgetQuantityUsed = db.BudgetQuantityUsed,
                                 Status = dd.Status
                             } into modelDIS15
                             select new DisplayProgressReportListModel()
                             {
                                 Code = modelDIS15.Key.DisplayCode,
                                 ShortName = modelDIS15.Key.ShortName,
                                 RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                 RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                 ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                 ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                 ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                 Scope = modelDIS15.Key.ScopeType,
                                 ScopeValue = modelDIS15.Key.ScopeValue,
                                 ApplicableObject = modelDIS15.Key.ApplicableObject,
                                 ApplicableObjectValue = modelDIS15.Key.ApplicableObjectValue,
                                 DisplayLevel = modelDIS15.Key.DisplayLevel,
                                 BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                 BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                 SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                 Status = modelDIS15.Key.Status
                             }).AsQueryable();

                var queryIsScope = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                    join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                    on dd.Code equals db.DisplayCode
                                    join dbfst in _disBudgetForScopeTerritory.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                    on db.DisplayLevelCode equals dbfst.DisplayLevelCode
                                    join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                    on db.DisplayLevelCode equals darcd.DisplayLevel
                                    where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                    && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                    && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                    && db.Type.Equals(1)
                                    group darcd by new
                                    {
                                        DisplayCode = dd.Code,
                                        ShortName = dd.ShortName,
                                        RegistrationStartDate = dd.RegistrationStartDate,
                                        RegistrationEndDate = dd.RegistrationEndDate,
                                        ImplementationStartDate = dd.ImplementationStartDate,
                                        ImplementationEndDate = dd.ImplementationEndDate,
                                        ProgramCloseDate = dd.ProgramCloseDate,
                                        ScopeType = dd.ScopeType,
                                        ApplicableObject = dd.ApplicableObjectType,
                                        DisplayLevel = darcd.DisplayLevel,
                                        BudgetQuantity = db.TotalBudget,
                                        BudgetQuantityUsed = db.BudgetQuantityUsed,
                                        Status = dd.Status
                                    } into modelDIS15
                                    select new DisplayProgressReportListModel()
                                    {
                                        Code = modelDIS15.Key.DisplayCode,
                                        ShortName = modelDIS15.Key.ShortName,
                                        RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                        RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                        ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                        ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                        ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                        Scope = modelDIS15.Key.ScopeType,
                                        ApplicableObject = modelDIS15.Key.ApplicableObject,
                                        DisplayLevel = modelDIS15.Key.DisplayLevel,
                                        BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                        BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                        SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                        Status = modelDIS15.Key.Status
                                    }).AsQueryable();

                var queryIsObject = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                     join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                     on dd.Code equals db.DisplayCode
                                     join dbfca in _disBudgetForCusAttribute.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                     on db.DisplayCode equals dbfca.DisplayCode
                                     join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                     on db.DisplayLevelCode equals darcd.DisplayLevel
                                     where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                     && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                     && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                     && db.Type.Equals(1)
                                     group darcd by new
                                     {
                                         DisplayCode = dd.Code,
                                         ShortName = dd.ShortName,
                                         RegistrationStartDate = dd.RegistrationStartDate,
                                         RegistrationEndDate = dd.RegistrationEndDate,
                                         ImplementationStartDate = dd.ImplementationStartDate,
                                         ImplementationEndDate = dd.ImplementationEndDate,
                                         ProgramCloseDate = dd.ProgramCloseDate,
                                         ScopeType = dd.ScopeType,
                                         ApplicableObject = dd.ApplicableObjectType,
                                         ApplicableObjectValue = dbfca.CustomerAttributerValue,
                                         DisplayLevel = darcd.DisplayLevel,
                                         BudgetQuantity = db.TotalBudget,
                                         BudgetQuantityUsed = db.BudgetQuantityUsed,
                                         Status = dd.Status
                                     } into modelDIS15
                                     select new DisplayProgressReportListModel()
                                     {
                                         Code = modelDIS15.Key.DisplayCode,
                                         ShortName = modelDIS15.Key.ShortName,
                                         RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                         RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                         ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                         ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                         ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                         Scope = modelDIS15.Key.ScopeType,
                                         ApplicableObject = modelDIS15.Key.ApplicableObject,
                                         ApplicableObjectValue = modelDIS15.Key.ApplicableObjectValue,
                                         DisplayLevel = modelDIS15.Key.DisplayLevel,
                                         BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                         BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                         SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                         Status = modelDIS15.Key.Status
                                     }).AsQueryable();

                var queryIsNot = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                  join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                  on dd.Code equals db.DisplayCode
                                  join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                  on db.DisplayLevelCode equals darcd.DisplayLevel
                                  where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                  && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                  && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                  && db.Type.Equals(1)
                                  group darcd by new
                                  {
                                      DisplayCode = dd.Code,
                                      ShortName = dd.ShortName,
                                      RegistrationStartDate = dd.RegistrationStartDate,
                                      RegistrationEndDate = dd.RegistrationEndDate,
                                      ImplementationStartDate = dd.ImplementationStartDate,
                                      ImplementationEndDate = dd.ImplementationEndDate,
                                      ProgramCloseDate = dd.ProgramCloseDate,
                                      ScopeType = dd.ScopeType,
                                      ApplicableObject = dd.ApplicableObjectType,
                                      DisplayLevel = darcd.DisplayLevel,
                                      BudgetQuantity = db.TotalBudget,
                                      BudgetQuantityUsed = db.BudgetQuantityUsed,
                                      Status = dd.Status
                                  } into modelDIS15
                                  select new DisplayProgressReportListModel()
                                  {
                                      Code = modelDIS15.Key.DisplayCode,
                                      ShortName = modelDIS15.Key.ShortName,
                                      RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                      RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                      ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                      ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                      ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                      Scope = modelDIS15.Key.ScopeType,
                                      ApplicableObject = modelDIS15.Key.ApplicableObject,
                                      DisplayLevel = modelDIS15.Key.DisplayLevel,
                                      BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                      BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                      SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                      Status = modelDIS15.Key.Status
                                  }).AsQueryable();

                if ((!string.IsNullOrEmpty(request.ScopeType) && request.ListScope is not null && request.ListScope.Any())
                    && (!string.IsNullOrEmpty(request.ApplicableObjectType) && request.ListApplicableObject is not null && request.ListApplicableObject.Any()))
                {
                    query = query.Where(x => request.ListScope.Contains(x.ScopeValue));
                    query = query.Where(x => request.ListApplicableObject.Contains(x.ApplicableObjectValue));
                    return query;
                }
                else if (!string.IsNullOrEmpty(request.ScopeType) && request.ListScope is not null && request.ListScope.Any())
                {
                    queryIsScope = queryIsScope.Where(x => request.ListScope.Contains(x.ScopeValue));
                    return queryIsScope;
                }
                else if (!string.IsNullOrEmpty(request.ApplicableObjectType) && request.ListApplicableObject is not null && request.ListApplicableObject.Any())
                {
                    queryIsObject = queryIsObject.Where(x => request.ListApplicableObject.Contains(x.ApplicableObjectValue));
                    return queryIsObject;
                }
                else return queryIsNot;
            }

            else if (request.ScopeType.Equals(CommonData.DisplaySetting.ScopeDSA)
                && (request.ApplicableObjectType.Equals(CommonData.DisplaySetting.ObjectAllSalePoint) || request.ApplicableObjectType.Equals(CommonData.DisplaySetting.ObjectCustomerShipto)))
            {
                var querylistScope = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                      join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                      on dd.Code equals db.DisplayCode
                                      join dbfsd in _disBudgetForScopeDsa.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                      on db.DisplayLevelCode equals dbfsd.DisplayLevelCode
                                      join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                      on db.DisplayLevelCode equals darcd.DisplayLevel
                                      where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                        && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                        && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                      && db.Type.Equals(1)
                                      group darcd by new
                                      {
                                          DisplayCode = dd.Code,
                                          ShortName = dd.ShortName,
                                          RegistrationStartDate = dd.RegistrationStartDate,
                                          RegistrationEndDate = dd.RegistrationEndDate,
                                          ImplementationStartDate = dd.ImplementationStartDate,
                                          ImplementationEndDate = dd.ImplementationEndDate,
                                          ProgramCloseDate = dd.ProgramCloseDate,
                                          ScopeType = dd.ScopeType,
                                          ApplicableObject = dd.ApplicableObjectType,
                                          DisplayLevel = darcd.DisplayLevel,
                                          BudgetQuantity = db.TotalBudget,
                                          BudgetQuantityUsed = db.BudgetQuantityUsed,
                                          Status = dd.Status
                                      } into modelDIS15
                                      select new DisplayProgressReportListModel()
                                      {
                                          Code = modelDIS15.Key.DisplayCode,
                                          ShortName = modelDIS15.Key.ShortName,
                                          RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                          RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                          ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                          ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                          ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                          Scope = modelDIS15.Key.ScopeType,
                                          ApplicableObject = modelDIS15.Key.ApplicableObject,
                                          DisplayLevel = modelDIS15.Key.DisplayLevel,
                                          BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                          BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                          SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                          Status = modelDIS15.Key.Status
                                      }).AsQueryable();
                var queryIsNot = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                  join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                  on dd.Code equals db.DisplayCode
                                  join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                  on db.DisplayLevelCode equals darcd.DisplayLevel
                                  where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                    && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                    && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                  && db.Type.Equals(1)
                                  group darcd by new
                                  {
                                      DisplayCode = dd.Code,
                                      ShortName = dd.ShortName,
                                      RegistrationStartDate = dd.RegistrationStartDate,
                                      RegistrationEndDate = dd.RegistrationEndDate,
                                      ImplementationStartDate = dd.ImplementationStartDate,
                                      ImplementationEndDate = dd.ImplementationEndDate,
                                      ProgramCloseDate = dd.ProgramCloseDate,
                                      ScopeType = dd.ScopeType,
                                      ApplicableObject = dd.ApplicableObjectType,
                                      DisplayLevel = darcd.DisplayLevel,
                                      BudgetQuantity = db.TotalBudget,
                                      BudgetQuantityUsed = db.BudgetQuantityUsed,
                                      Status = dd.Status
                                  } into modelDIS15
                                  select new DisplayProgressReportListModel()
                                  {
                                      Code = modelDIS15.Key.DisplayCode,
                                      ShortName = modelDIS15.Key.ShortName,
                                      RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                      RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                      ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                      ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                      ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                      Scope = modelDIS15.Key.ScopeType,
                                      ApplicableObject = modelDIS15.Key.ApplicableObject,
                                      DisplayLevel = modelDIS15.Key.DisplayLevel,
                                      BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                      BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                      SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                      Status = modelDIS15.Key.Status
                                  }).AsQueryable();
                if (!string.IsNullOrEmpty(request.ScopeType) && request.ListScope is not null && request.ListScope.Any())
                {
                    querylistScope = querylistScope.Where(x => request.ListScope.Contains(x.ScopeValue));
                    return querylistScope;
                }
                else return queryIsNot;
            }

            else if (request.ScopeType.Equals(CommonData.DisplaySetting.ScopeDSA)
                && (request.ApplicableObjectType.Equals(CommonData.DisplaySetting.ObjectCustomerAttributes)))
            {
                var query = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                             join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                             on dd.Code equals db.DisplayCode
                             join dbfsd in _disBudgetForScopeDsa.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                             on db.DisplayLevelCode equals dbfsd.DisplayLevelCode
                             join dbfca in _disBudgetForCusAttribute.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                             on db.DisplayCode equals dbfca.DisplayCode
                             join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                             on db.DisplayLevelCode equals darcd.DisplayLevel
                             where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                             && db.Type.Equals(1)
                             group darcd by new
                             {
                                 DisplayCode = dd.Code,
                                 ShortName = dd.ShortName,
                                 RegistrationStartDate = dd.RegistrationStartDate,
                                 RegistrationEndDate = dd.RegistrationEndDate,
                                 ImplementationStartDate = dd.ImplementationStartDate,
                                 ImplementationEndDate = dd.ImplementationEndDate,
                                 ProgramCloseDate = dd.ProgramCloseDate,
                                 ScopeType = dd.ScopeType,
                                 ScopeValue = dbfsd.ScopeValue,
                                 ApplicableObject = dd.ApplicableObjectType,
                                 ApplicableObjectValue = dbfca.CustomerAttributerValue,
                                 DisplayLevel = darcd.DisplayLevel,
                                 BudgetQuantity = db.TotalBudget,
                                 BudgetQuantityUsed = db.BudgetQuantityUsed,
                                 Status = dd.Status
                             } into modelDIS15
                             select new DisplayProgressReportListModel()
                             {
                                 Code = modelDIS15.Key.DisplayCode,
                                 ShortName = modelDIS15.Key.ShortName,
                                 RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                 RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                 ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                 ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                 ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                 Scope = modelDIS15.Key.ScopeType,
                                 ScopeValue = modelDIS15.Key.ScopeValue,
                                 ApplicableObject = modelDIS15.Key.ApplicableObject,
                                 ApplicableObjectValue = modelDIS15.Key.ApplicableObjectValue,
                                 DisplayLevel = modelDIS15.Key.DisplayLevel,
                                 BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                 BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                 SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                 Status = modelDIS15.Key.Status
                             }).AsQueryable();
                var queryIsScope = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                    join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                    on dd.Code equals db.DisplayCode
                                    join dbfsd in _disBudgetForScopeDsa.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                    on db.DisplayLevelCode equals dbfsd.DisplayLevelCode
                                    join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                    on db.DisplayLevelCode equals darcd.DisplayLevel
                                    where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                    && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                    && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                    && db.Type.Equals(1)
                                    group darcd by new
                                    {
                                        DisplayCode = dd.Code,
                                        ShortName = dd.ShortName,
                                        RegistrationStartDate = dd.RegistrationStartDate,
                                        RegistrationEndDate = dd.RegistrationEndDate,
                                        ImplementationStartDate = dd.ImplementationStartDate,
                                        ImplementationEndDate = dd.ImplementationEndDate,
                                        ProgramCloseDate = dd.ProgramCloseDate,
                                        ScopeType = dd.ScopeType,
                                        ScopeValue = dbfsd.ScopeValue,
                                        ApplicableObject = dd.ApplicableObjectType,
                                        DisplayLevel = darcd.DisplayLevel,
                                        BudgetQuantity = db.TotalBudget,
                                        BudgetQuantityUsed = db.BudgetQuantityUsed,
                                        Status = dd.Status
                                    } into modelDIS15
                                    select new DisplayProgressReportListModel()
                                    {
                                        Code = modelDIS15.Key.DisplayCode,
                                        ShortName = modelDIS15.Key.ShortName,
                                        RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                        RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                        ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                        ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                        ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                        Scope = modelDIS15.Key.ScopeType,
                                        ScopeValue = modelDIS15.Key.ScopeValue,
                                        ApplicableObject = modelDIS15.Key.ApplicableObject,
                                        DisplayLevel = modelDIS15.Key.DisplayLevel,
                                        BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                        BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                        SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                        Status = modelDIS15.Key.Status
                                    }).AsQueryable();
                var queryIsObject = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                     join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                     on dd.Code equals db.DisplayCode
                                     join dbfca in _disBudgetForCusAttribute.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                     on db.DisplayCode equals dbfca.DisplayCode
                                     join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                     on db.DisplayLevelCode equals darcd.DisplayLevel
                                     where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                        && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                        && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                     && db.Type.Equals(1)
                                     group darcd by new
                                     {
                                         DisplayCode = dd.Code,
                                         ShortName = dd.ShortName,
                                         RegistrationStartDate = dd.RegistrationStartDate,
                                         RegistrationEndDate = dd.RegistrationEndDate,
                                         ImplementationStartDate = dd.ImplementationStartDate,
                                         ImplementationEndDate = dd.ImplementationEndDate,
                                         ProgramCloseDate = dd.ProgramCloseDate,
                                         ScopeType = dd.ScopeType,
                                         ApplicableObject = dd.ApplicableObjectType,
                                         ApplicableObjectValue = dbfca.CustomerAttributerValue,
                                         DisplayLevel = darcd.DisplayLevel,
                                         BudgetQuantity = db.TotalBudget,
                                         BudgetQuantityUsed = db.BudgetQuantityUsed,
                                         Status = dd.Status
                                     } into modelDIS15
                                     select new DisplayProgressReportListModel()
                                     {
                                         Code = modelDIS15.Key.DisplayCode,
                                         ShortName = modelDIS15.Key.ShortName,
                                         RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                         RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                         ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                         ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                         ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                         Scope = modelDIS15.Key.ScopeType,
                                         ApplicableObject = modelDIS15.Key.ApplicableObject,
                                         ApplicableObjectValue = modelDIS15.Key.ApplicableObjectValue,
                                         DisplayLevel = modelDIS15.Key.DisplayLevel,
                                         BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                         BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                         SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                         Status = modelDIS15.Key.Status
                                     }).AsQueryable();
                var queryIsNot = (from dd in _disDisplay.GetAllQueryable(x => x.Status.Equals(CommonData.DisplaySetting.Implementation) && x.DeleteFlag.Equals(0)).AsNoTracking()
                                  join db in _disBudget.GetAllQueryable(x => x.DeleteFlag.Equals(0)).AsNoTracking()
                                  on dd.Code equals db.DisplayCode
                                  join darcd in _disApproveRegistrationCustomerDetail.GetAllQueryable().AsNoTracking()
                                  on db.DisplayLevelCode equals darcd.DisplayLevel
                                  where dd.Code.ToLower().Equals(db.DisplayCode.ToLower())
                                    && db.DisplayCode.ToLower().Equals(darcd.DisplayCode.ToLower())
                                    && request.DisplayCode.ToLower().Equals(dd.Code.ToLower())
                                  && db.Type.Equals(1)
                                  group darcd by new
                                  {
                                      DisplayCode = dd.Code,
                                      ShortName = dd.ShortName,
                                      RegistrationStartDate = dd.RegistrationStartDate,
                                      RegistrationEndDate = dd.RegistrationEndDate,
                                      ImplementationStartDate = dd.ImplementationStartDate,
                                      ImplementationEndDate = dd.ImplementationEndDate,
                                      ProgramCloseDate = dd.ProgramCloseDate,
                                      ScopeType = dd.ScopeType,
                                      ApplicableObject = dd.ApplicableObjectType,
                                      DisplayLevel = darcd.DisplayLevel,
                                      BudgetQuantity = db.TotalBudget,
                                      BudgetQuantityUsed = db.BudgetQuantityUsed,
                                      Status = dd.Status
                                  } into modelDIS15
                                  select new DisplayProgressReportListModel()
                                  {
                                      Code = modelDIS15.Key.DisplayCode,
                                      ShortName = modelDIS15.Key.ShortName,
                                      RegistrationStartDate = modelDIS15.Key.RegistrationStartDate,
                                      RegistrationEndDate = modelDIS15.Key.RegistrationEndDate,
                                      ImplementationStartDate = modelDIS15.Key.ImplementationStartDate,
                                      ImplementationEndDate = modelDIS15.Key.ImplementationEndDate,
                                      ProgramCloseDate = modelDIS15.Key.ProgramCloseDate,
                                      Scope = modelDIS15.Key.ScopeType,
                                      ApplicableObject = modelDIS15.Key.ApplicableObject,
                                      DisplayLevel = modelDIS15.Key.DisplayLevel,
                                      BudgetQuantity = modelDIS15.Key.BudgetQuantity,
                                      BudgetQuantityUsed = modelDIS15.Key.BudgetQuantityUsed,
                                      SalesPoint = modelDIS15.Select(x => x.CustomerCode).Count(),
                                      Status = modelDIS15.Key.Status
                                  }).AsQueryable();
                if ((!string.IsNullOrEmpty(request.ScopeType) && request.ListScope is not null && request.ListScope.Any())
                    && (!string.IsNullOrEmpty(request.ApplicableObjectType) && request.ListApplicableObject is not null && request.ListApplicableObject.Any()))
                {
                    query = query.Where(x => request.ListScope.Contains(x.ScopeValue));
                    query = query.Where(x => request.ListApplicableObject.Contains(x.ApplicableObjectValue));
                    return query;
                }
                else if ((!string.IsNullOrEmpty(request.ScopeType) && request.ListScope is not null && request.ListScope.Any()))
                {
                    queryIsScope = queryIsScope.Where(x => request.ListScope.Contains(x.ScopeValue));
                    return queryIsScope;
                }
                else if (!string.IsNullOrEmpty(request.ApplicableObjectType) && request.ListApplicableObject is not null && request.ListApplicableObject.Any())
                {
                    queryIsObject = queryIsObject.Where(x => request.ListApplicableObject.Contains(x.ApplicableObjectValue));
                    return queryIsObject;
                }
                else return queryIsNot;
            }

            else return null;
        }
        #endregion
    }
}
