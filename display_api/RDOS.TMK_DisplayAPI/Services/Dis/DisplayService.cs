using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.SalesOrg;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisplayService : IDisplayService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DisplayService> _logger;
        private readonly IBaseRepository<DisDisplay> _repository;
        private readonly IBaseRepository<DisScopeTerritory> _dbDisplayScopeTerritory;
        private readonly IBaseRepository<DisScopeDsa> _dbDisplayScopeDsa;
        private readonly IBaseRepository<DisCustomerAttributeLevel> _dbDisCustomerAttributeLevel;
        private readonly IBaseRepository<DisCustomerAttributeValue> _dbDisCustomerAttributeValue;
        private readonly IBaseRepository<CustomerSetting> _dbCustomerSetting;
        private readonly IBaseRepository<CustomerAttribute> _dbCustomerAttribute;
        private readonly IBaseRepository<SystemSetting> _dbSystemSetting;
        private readonly IBaseRepository<ScTerritoryStructureDetail> _dbTerritoryLevel;
        private readonly IBaseRepository<ScTerritoryValue> _dbTerritoryValue;
        private readonly IBaseRepository<DsaDistributorSellingArea> _dbScopeDsa;
        private readonly IBaseRepository<ScTerritoryStructureDetail> _dbsalesOrg;
        private readonly IBaseRepository<PrimarySic> _dbSic;
        private readonly IBaseRepository<ScSalesOrganizationStructure> _dbOrg;
        private readonly IBaseRepository<DisBudget> _dbDisBudget;
        private readonly IBaseRepository<DisDefinitionStructure> _dbDisDefinitionStructure;
        private readonly IBaseRepository<DisApproveRegistrationCustomerDetail> _dbDisApproveRegistrationCustomerDetail;

        public DisplayService(IMapper mapper,
            IBaseRepository<DisDisplay> repository,
            IBaseRepository<DisScopeTerritory> dbDisplayScopeTerritory,
            IBaseRepository<DisScopeDsa> dbDisplayScopeDsa,
            IBaseRepository<DisCustomerAttributeLevel> dbDisCustomerAttributeLevel,
            IBaseRepository<DisCustomerAttributeValue> dbDisCustomerAttributeValue,
            IBaseRepository<CustomerSetting> dbCustomerSetting,
            IBaseRepository<CustomerAttribute> dbCustomerAttribute,
            IBaseRepository<SystemSetting> dbSystemSetting,
            IBaseRepository<ScTerritoryStructureDetail> dbTerritoryLevel,
            IBaseRepository<ScTerritoryValue> dbTerritoryValue,
            IBaseRepository<DsaDistributorSellingArea> dbScopeDsa,
            ILogger<DisplayService> logger,
            IBaseRepository<ScTerritoryStructureDetail> dbsalesOrg,
            IBaseRepository<PrimarySic> dbSic,
            IBaseRepository<ScSalesOrganizationStructure> dbOrg,
            IBaseRepository<DisBudget> dbDisBudget,
            IBaseRepository<DisDefinitionStructure> serviceDisDefinitionStructure,
            IBaseRepository<DisApproveRegistrationCustomerDetail> dbDisApproveRegistrationCustomerDetail
            )
        {
            _mapper = mapper;
            _repository = repository;
            _dbDisplayScopeTerritory = dbDisplayScopeTerritory;
            _dbDisplayScopeDsa = dbDisplayScopeDsa;
            _dbDisCustomerAttributeLevel = dbDisCustomerAttributeLevel;
            _dbDisCustomerAttributeValue = dbDisCustomerAttributeValue;
            _dbCustomerSetting = dbCustomerSetting;
            _dbCustomerAttribute = dbCustomerAttribute;
            _dbSystemSetting = dbSystemSetting;
            _dbTerritoryLevel = dbTerritoryLevel;
            _dbTerritoryValue = dbTerritoryValue;
            _dbScopeDsa = dbScopeDsa;
            _logger = logger;
            _dbsalesOrg = dbsalesOrg;
            _dbSic = dbSic;
            _dbOrg = dbOrg;
            _dbDisBudget = dbDisBudget;
            _dbDisDefinitionStructure = serviceDisDefinitionStructure;
            _dbDisApproveRegistrationCustomerDetail = dbDisApproveRegistrationCustomerDetail;
        }

        #region Display List
        public IQueryable<DisplayGeneralModel> GetListDisplayGeneral()
        {
            var systemSettings = _dbSystemSetting.GetAllQueryable(x => x.IsActive).AsNoTracking().AsQueryable();
            var sics = _dbSic.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking().AsQueryable();
            var saleOrgs = _dbOrg.GetAllQueryable(x => !x.IsDeleted).AsNoTracking().AsQueryable();

            var results = (from d in _repository.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
                           join status in systemSettings.Where(x => x.SettingType.ToLower().Equals(CommonData.SystemSetting.DisplayStatus.ToLower()))
                           on d.Status equals status.SettingKey into emptyStatus
                           from status in emptyStatus.DefaultIfEmpty()
                           join scope in systemSettings.Where(x => x.SettingType.ToLower().Equals(CommonData.SystemSetting.MktScope.ToLower()))
                           on d.ScopeType equals scope.SettingKey into emptyScope
                           from scope in emptyScope.DefaultIfEmpty()
                           join applicable in systemSettings.Where(x => x.SettingType.ToLower().Equals(CommonData.SystemSetting.ApplicableObject.ToLower()))
                           on d.ApplicableObjectType equals applicable.SettingKey into emptyApplicable
                           from applicable in emptyApplicable.DefaultIfEmpty()
                           join fre in systemSettings.Where(x => x.SettingType.ToLower().Equals(CommonData.SystemSetting.DisFrequency.ToLower()))
                           on d.FrequencyDisplay equals fre.SettingKey into emptyFrequency
                           from fre in emptyFrequency.DefaultIfEmpty()
                           join sic in sics
                           on d.SicCode equals sic.Code into emptySic
                           from sic in emptySic.DefaultIfEmpty()
                           join org in saleOrgs
                           on d.SaleOrg equals org.Code into emptySaleOrg
                           from org in emptySaleOrg.DefaultIfEmpty()
                           select new DisplayGeneralModel()
                           {
                               Id = d.Id,
                               Code = d.Code,
                               FullName = d.FullName,
                               ShortName = d.ShortName,
                               Status = d.Status,
                               StatusDescription = (status != null) ? status.Description : string.Empty,
                               SaleOrg = d.SaleOrg,
                               SaleOrgDescription = (org != null) ? org.Description : string.Empty,
                               SicCode = d.SicCode,
                               SicCodeDescription = (sic != null) ? sic.Description : string.Empty,
                               FrequencyDisplay = d.FrequencyDisplay,
                               FrequencyDescription = (fre != null) ? fre.Description : string.Empty,
                               ScopeType = d.ScopeType,
                               ScopeTypeDescription = (scope != null) ? scope.Description : string.Empty,
                               ApplicableObjectType = d.ApplicableObjectType,
                               ApplicableObjectTypeDescription = (applicable != null) ? applicable.Description : string.Empty
                           }).AsQueryable();

            return results;
        }

        public List<DisplayAutoSearchModel> GetListDisplayAutoSearchModel()
        {
            var lstDisplays = _repository.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking().ToList();
            return _mapper.Map<List<DisplayAutoSearchModel>>(lstDisplays);
        }
        #endregion

        public async Task<bool> ExistsByCodeAsync(string code, Guid? id = null)
            => await _repository.GetAllQueryable(x => (id == null || x.Id != id) && x.Code == code).AnyAsync();

        public async Task<DisDisplay> FindByIdAsync(Guid id)
            => await _repository.GetAllQueryable(x => x.Id == id).FirstOrDefaultAsync();

        public Task<bool> CreateDisplayAsync(DisDisplay request)
        {
            string userlogin = string.Empty;
            _logger.LogInformation("info: {request}", request);
            try
            {
                request.CreatedDate = DateTime.Now;
                request.CreatedBy = userlogin;
                request.Status = CommonData.DisplaySetting.Inprogress;

                var result = _repository.Insert(request);

                return Task.FromResult(result != null);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Was an error occrred while create display");
                throw new ArgumentException(ex.Message);
            }
        }

        public Task<DisDisplayModel> UpdateDisplayAsync(DisDisplay request)
        {
            string userlogin = string.Empty;
            _logger.LogInformation("info: {request}", request);
            try
            {
                if (request.DeleteFlag != 1)
                {
                    request.UpdatedBy = userlogin;
                    request.UpdatedDate = DateTime.Now;
                }
                var result = _repository.Update(request);
                return Task.FromResult(_mapper.Map<DisDisplayModel>(result));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Was an error occrred while update display");
                throw new ArgumentException(ex.Message);
            }
        }

        public Task<bool> DeleteDisplayAsync(Guid id)
        {
            try
            {
                return Task.FromResult(_repository.Delete(id) != null);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Was an error occurred while delete display");
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<DisDisplayModel> GetDisplayByCodeAsync(string code)
        {
            var data = await _repository.GetAllQueryable(x => x.Code == code && x.DeleteFlag == 0).AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<DisDisplayModel>(data);

        }
        public DisDisplayModel GetDetailDisplayByCode(string code)
        {
            var display = _repository.GetAllQueryable(x => x.Code.Equals(code)).AsNoTracking().FirstOrDefault();
            return _mapper.Map<DisDisplayModel>(display);
        }

        public async Task<DisDisplayModel> FindByCodeAsync(string code)
        {
            var data = await _repository.GetAllQueryable(x => x.Code == code).AsNoTracking().FirstOrDefaultAsync();
            if (data != null)
            {
                var disDisplay = _mapper.Map<DisDisplayModel>(data);
                if (disDisplay.ScopeType == CommonData.DisplaySetting.ScopeNationwide)
                {
                    disDisplay.TerritoryStructureLevels = await _dbsalesOrg
                               .GetAllQueryable(x => x.TerritoryStructureCode == disDisplay.TerritoryStructureCode)
                               .Select(x => new TpTerritoryStructureLevelModel()
                               {
                                   Id = x.Id.ToString(),
                                   TerritoryStructureCode = x.TerritoryStructureCode,
                                   Description = x.Description,
                                   Level = x.Level,
                                   TerritoryLevelCode = x.TerritoryLevelCode
                               }).ToListAsync();
                }
                else
                {
                    disDisplay.DisplayScopes = await GetListDisplayScopeByDisplayCode(code);
                }

                return disDisplay;
            }

            return null;
        }

        public Task<IQueryable<DisplayPopupModel>> GetListDisplayAsync()
        {
            var systemSettings = _dbSystemSetting.GetAllQueryable(x => x.IsActive).AsNoTracking().AsQueryable();
            var display = _repository.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking();
            var popupData = (from dis in display
                             join status in systemSettings.Where(x => x.SettingType.ToLower().Equals(CommonData.SystemSetting.DisplayMaintenanceStatus.ToLower()))
                             on dis.Status equals status.SettingKey into emptyStatus
                             from status in emptyStatus.DefaultIfEmpty()
                             select new DisplayPopupModel()
                             {
                                 Code = dis.Code,
                                 ShortName = dis.ShortName,
                                 Status = dis.Status,
                                 StatusDescription = (status != null) ? status.Description : string.Empty,
                                 ScopeType = dis.ScopeType,
                                 //ScopeTypeDescription = (scope != null) ? scope.Description : string.Empty,
                                 ApplicableObjectType = dis.ApplicableObjectType,
                                 //ApplicableObjectTypeDescription = (applicable != null) ? applicable.Description : string.Empty,
                                 IsOverbudget = dis.IsOverbudget.HasValue && dis.IsOverbudget.Value,
                                 ImplementationStartDate = dis.ImplementationStartDate,
                                 ImplementationEndDate = dis.ImplementationEndDate,
                                 //UserName = dis.UserName
                             }).AsQueryable();
            return Task.FromResult(popupData);
        }

        public Task<IQueryable<DisplayPopupModel>> GetListDisplayForReportAsync(List<string> lstStatus)
        {
            var systemSettings = _dbSystemSetting.GetAllQueryable(x => x.IsActive).AsNoTracking().AsQueryable();
            var display = _repository.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking();
            var popupData = (from dis in display
                             join status in systemSettings.Where(x => x.SettingType.ToLower().Equals(CommonData.SystemSetting.DisplayMaintenanceStatus.ToLower()))
                             on dis.Status equals status.SettingKey into emptyStatus
                             from status in emptyStatus.DefaultIfEmpty()
                             where lstStatus.Contains(dis.Status)
                             select new DisplayPopupModel()
                             {
                                 Code = dis.Code,
                                 ShortName = dis.ShortName,
                                 ScopeType = dis.ScopeType,
                                 ApplicableObjectType = dis.ApplicableObjectType,
                                 Status = dis.Status,
                                 StatusDescription = (status != null) ? status.Description : string.Empty
                             }).AsQueryable();
            return Task.FromResult(popupData);
        }

        public bool ConfirmDisplay(string code)
        {
            try
            {
                var display = _repository.FirstOrDefault(x => x.DeleteFlag == 0 && x.Code.ToLower().Equals(code.ToLower()));
                display.Status = CommonData.DisplaySetting.Confirmed;
                var result = _repository.Update(display);

                return result != null;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Was an error occurred while delete display");
                throw new ArgumentException(ex.Message);
            }
        }

        #region Scope
        public Task<bool> CreateDisplayScopes(DisDisplayModel request)
        {
            try
            {
                var display = _repository.FirstOrDefault(x => x.DeleteFlag == 0 && x.Code.ToLower().Equals(request.Code.ToLower()));
                if (display == null)
                {
                    return Task.FromResult(false);
                }
                else
                {
                    display.ScopeType = request.ScopeType;
                    display.ScopeSaleTerritoryLevel = request.ScopeSaleTerritoryLevel;
                    _repository.Update(display);
                }

                // Remove All Display Scope
                var lstScopeTerritorys = _dbDisplayScopeTerritory.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisplayCode.ToLower().Equals(request.Code.ToLower()));
                _dbDisplayScopeTerritory.DeleteRange(lstScopeTerritorys);

                var lstCopeDsa = _dbDisplayScopeDsa.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisplayCode.ToLower().Equals(request.Code.ToLower()));
                _dbDisplayScopeDsa.DeleteRange(lstCopeDsa);

                if (request.ScopeType.Equals(CommonData.DisplaySetting.ScopeSalesTerritoryLevel))
                {
                    List<DisScopeTerritory> listScopeTerritotys = new List<DisScopeTerritory>();
                    if (request.ListScopeSalesTerritory != null && request.ListScopeSalesTerritory.Count > 0)
                    {
                        foreach (var item in request.ListScopeSalesTerritory)
                        {
                            listScopeTerritotys.Add(new DisScopeTerritory()
                            {
                                Id = Guid.NewGuid(),
                                DisplayCode = request.Code,
                                TerritoryStructureCode = request.TerritoryStructureCode,
                                SalesTerritoryValue = item.Code,
                                CreatedBy = request.CreatedBy,
                                CreatedDate = DateTime.Now,
                                DeleteFlag = 0
                            });
                        }

                        _dbDisplayScopeTerritory.InsertRange(listScopeTerritotys);
                    }
                }
                else if (request.ScopeType.Equals(CommonData.DisplaySetting.ScopeDSA))
                {
                    List<DisScopeDsa> listScopeDsas = new List<DisScopeDsa>();
                    if (request.ListScopeDSA != null && request.ListScopeDSA.Count > 0)
                    {
                        foreach (var item in request.ListScopeDSA)
                        {
                            listScopeDsas.Add(new DisScopeDsa()
                            {
                                Id = Guid.NewGuid(),
                                DisplayCode = request.Code,
                                ScopeDsaValue = item.Code,
                                CreatedBy = request.CreatedBy,
                                CreatedDate = DateTime.Now,
                                DeleteFlag = 0
                            });
                        }

                        _dbDisplayScopeDsa.InsertRange(listScopeDsas);
                    }
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ArgumentException(ex.Message);
            }
        }

        public Task<List<DisplayScopeModel>> GetListDisplayScopeByDisplayCode(string displayCode)
        {
            List<DisplayScopeModel> result = new List<DisplayScopeModel>();
            DateTime now = DateTime.Now;
            var systemSettings = _dbSystemSetting.GetAllQueryable(x => x.IsActive && x.SettingType.ToLower().Equals(CommonData.SystemSetting.MktScope.ToLower())).AsNoTracking().AsQueryable();
            var display = _repository.FirstOrDefault(x => x.DeleteFlag == 0 && x.Code.ToLower().Equals(displayCode.ToLower()));
            if (display == null)
            {
                return Task.FromResult(result);
            }

            var scopeTypeName = systemSettings.FirstOrDefault(x => x.SettingKey.Equals(display.ScopeType))?.Description;
            if (display.ScopeType.Equals(CommonData.DisplaySetting.ScopeSalesTerritoryLevel))
            {
                var territoryLevels = _dbTerritoryLevel.GetAllQueryable(x => !x.IsDeleted).AsNoTracking().AsQueryable();
                var territoryLevelName = string.Empty;
                if (!string.IsNullOrEmpty(display.ScopeSaleTerritoryLevel))
                {
                    territoryLevelName = territoryLevels.FirstOrDefault(x => x.TerritoryLevelCode.ToLower().Equals(display.ScopeSaleTerritoryLevel.ToLower())).Description;
                }

                result = (from x in _dbDisplayScopeTerritory.GetAllQueryable(x => x.DeleteFlag == 0
                             && x.DisplayCode.ToLower().Equals(displayCode.ToLower())
                             && x.TerritoryStructureCode.ToLower().Equals(display.TerritoryStructureCode.ToLower())).AsNoTracking()
                          join value in _dbTerritoryValue.GetAllQueryable(x => !x.IsDeleted
                          && x.EffectiveDate <= now && (!x.UntilDate.HasValue || x.UntilDate.Value >= now) && x.TerritoryLevelCode == display.ScopeSaleTerritoryLevel).AsNoTracking()
                          on x.SalesTerritoryValue equals value.Code into emptyValue
                          from value in emptyValue.DefaultIfEmpty()
                          select new DisplayScopeModel()
                          {
                              Id = x.Id,
                              ScopeType = display.ScopeType,
                              ScopeTypeName = (scopeTypeName != null) ? scopeTypeName : string.Empty,
                              TerritoryLevel = display.ScopeSaleTerritoryLevel,
                              TerritoryLevelName = (territoryLevelName != null) ? territoryLevelName : string.Empty,
                              ScopeCode = x.SalesTerritoryValue,
                              ScopeName = value.Description
                          }).ToList();
            }

            if (display.ScopeType.Equals(CommonData.DisplaySetting.ScopeDSA))
            {
                result = (from x in _dbDisplayScopeDsa.GetAllQueryable(x => x.DeleteFlag == 0
                             && x.DisplayCode.ToLower().Equals(displayCode.ToLower())).AsNoTracking()
                          join value in _dbScopeDsa.GetAllQueryable(x => x.EffectiveDate <= now && (!x.UntilDate.HasValue || x.UntilDate.Value >= now)).AsNoTracking()
                          on x.ScopeDsaValue equals value.Code into emptyValue
                          from value in emptyValue.DefaultIfEmpty()
                          select new DisplayScopeModel()
                          {
                              Id = x.Id,
                              ScopeType = display.ScopeType,
                              ScopeTypeName = (scopeTypeName != null) ? scopeTypeName : string.Empty,
                              TerritoryLevel = string.Empty,
                              TerritoryLevelName = string.Empty,
                              ScopeCode = x.ScopeDsaValue,
                              ScopeName = value.Description
                          }).ToList();
            }

            return Task.FromResult(result);
        }
        #endregion


        #region Applicable Object
        public Task<bool> CreateDisplayApplicableObject(DisDisplayModel request)
        {
            try
            {
                var display = _repository.FirstOrDefault(x => x.DeleteFlag == 0 && x.Code.ToLower().Equals(request.Code.ToLower()));
                if (display == null)
                {
                    return Task.FromResult(false);
                }
                else
                {
                    display.ApplicableObjectType = request.ApplicableObjectType;
                    _repository.Update(display);
                }

                // Remove All Applicable Object
                var objectCusSettings = _dbDisCustomerAttributeLevel.GetAllQueryable(x => x.DisplayCode.ToLower().Equals(request.Code.ToLower()));
                _dbDisCustomerAttributeLevel.DeleteRange(objectCusSettings);

                var objectCusAttributes = _dbDisCustomerAttributeValue.GetAllQueryable(x => x.DisplayCode.ToLower().Equals(request.Code.ToLower()));
                _dbDisCustomerAttributeValue.DeleteRange(objectCusAttributes);

                if (request.ApplicableObjectType.ToLower().Equals(CommonData.PromotionSetting.ObjectCustomerAttributes.ToLower()))
                {
                    // Insert Customer Setting
                    if (request.ListCustomerSetting != null && request.ListCustomerSetting.Count > 0)
                    {
                        List<DisCustomerAttributeLevel> listCustomerSetting = new List<DisCustomerAttributeLevel>();
                        foreach (var item in request.ListCustomerSetting.Where(x => x.IsChecked))
                        {
                            listCustomerSetting.Add(new DisCustomerAttributeLevel()
                            {
                                Id = Guid.NewGuid(),
                                DisplayCode = request.Code,
                                CustomerAttributerLevel = item.AttributeID,
                                IsApply = item.IsChecked,
                                CreatedBy = request.CreatedBy,
                                CreatedDate = DateTime.Now,
                                DeleteFlag = 0
                            });
                        }

                        _dbDisCustomerAttributeLevel.InsertRange(listCustomerSetting);
                    }

                    // Insert Customer Attribute
                    if (request.ListCustomerAttribute != null && request.ListCustomerAttribute.Count > 0)
                    {
                        List<DisCustomerAttributeValue> listCustomerAttribute = new List<DisCustomerAttributeValue>();
                        foreach (var item in request.ListCustomerAttribute)
                        {
                            listCustomerAttribute.Add(new DisCustomerAttributeValue()
                            {
                                Id = Guid.NewGuid(),
                                DisplayCode = request.Code,
                                CustomerAttributerLevel = item.AttributeMaster,
                                CustomerAttributerValue = item.Code,
                                CreatedBy = request.CreatedBy,
                                CreatedDate = DateTime.Now,
                                DeleteFlag = 0
                            });
                        }

                        _dbDisCustomerAttributeValue.InsertRange(listCustomerAttribute);
                    }
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ArgumentException(ex.Message);
            }
        }

        public Task<List<DisplayCustomerSettingModel>> GetListDisplayCustomerAttributeByDisplayCode(string displayCode)
        {
            List<DisplayCustomerSettingModel> resultData = new List<DisplayCustomerSettingModel>();
            DateTime now = DateTime.Now;

            var display = _repository.FirstOrDefault(x => x.DeleteFlag == 0 && x.Code.ToLower().Equals(displayCode.ToLower()));
            if (display == null)
            {
                return Task.FromResult(resultData);
            }

            if (display.ApplicableObjectType.Equals(CommonData.DisplaySetting.ObjectCustomerAttributes))
            {
                resultData = (from cusSetting in _dbCustomerSetting.GetAllQueryable().AsNoTracking()
                              join cusLevel in _dbDisCustomerAttributeLevel.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisplayCode.ToLower().Equals(displayCode.ToLower())).AsNoTracking()
                                 on cusSetting.AttributeId equals cusLevel.CustomerAttributerLevel into emptyCusLevel
                              from cusLevel in emptyCusLevel.DefaultIfEmpty()
                              select new DisplayCustomerSettingModel()
                              {
                                  CustomerAttributeId = cusSetting.Id,
                                  CustomerLevelCode = cusSetting.AttributeId,
                                  CustomerLevelName = cusSetting.Description,
                                  IsCustomerAttribute = cusSetting.IsCustomerAttribute,
                                  IsApply = (cusLevel != null) ? cusLevel.IsApply : false
                              }).ToList();

                var lstCusAttributes = _dbCustomerAttribute.GetAllQueryable(x => x.EffectiveDate <= now && (!x.ValidUntil.HasValue || x.ValidUntil.Value >= now)).AsNoTracking();
                foreach (var item in resultData.Where(x => x.IsApply))
                {
                    var result = (from cusValue in _dbDisCustomerAttributeValue.GetAllQueryable(x => x.DeleteFlag == 0
                                  && x.DisplayCode.ToLower().Equals(displayCode.ToLower()) && x.CustomerAttributerLevel.ToLower().Equals(item.CustomerLevelCode.ToLower())).AsNoTracking()
                                  join cusAttribute in lstCusAttributes.Where(x => x.CustomerSettingId.Equals(item.CustomerAttributeId))
                                  on cusValue.CustomerAttributerValue.ToLower() equals cusAttribute.Code.ToLower()
                                  select new DisplayCustomerAttributeModel()
                                  {
                                      CustomerLevelCode = cusValue.CustomerAttributerLevel,
                                      CustomerValueCode = cusValue.CustomerAttributerValue,
                                      CustomerValueName = cusAttribute.Description
                                  }).ToList();

                    item.CustomerAttributeModels.AddRange(result);
                }
            }
            return Task.FromResult(resultData);
        }

        #endregion


        #region Display Report Header
        public async Task<DisplayHeaderReportModel> GetDisplayReportHeaderByCode(string code)
        {
            var data = await _repository.GetAllQueryable(x => x.Code == code).AsNoTracking().FirstOrDefaultAsync();
            if (data != null)
            {
                var systemSettings = _dbSystemSetting.GetAllQueryable(x => x.IsActive).AsNoTracking().AsQueryable();
                var scopeTypeName = systemSettings.FirstOrDefault(x
                    => x.SettingType.ToLower().Equals(CommonData.SystemSetting.MktScope.ToLower())
                    && x.SettingKey.Equals(data.ScopeType))?.Description;
                var applicable = systemSettings.FirstOrDefault(x
                    => x.SettingType.ToLower().Equals(CommonData.SystemSetting.ApplicableObject.ToLower())
                    && x.SettingKey.Equals(data.ApplicableObjectType))?.Description;

                const int defaultType = 1;
                var dataBudget = (from budget in _dbDisBudget.GetAllQueryable(x => x.DisplayCode == code && x.DeleteFlag == 0 && x.Type == defaultType).AsNoTracking()
                            join level in _dbDisDefinitionStructure.GetAllQueryable(x => x.DisplayCode == code && x.DeleteFlag == 0).AsNoTracking()
                            on budget.DisplayLevelCode equals level.LevelCode into dataLevel
                            from level in dataLevel.DefaultIfEmpty()
                            select new DisBudgetReportModel()
                            {
                                Id = budget.Id,
                                DisplayCode = budget.DisplayCode,
                                DisplayLevelCode = budget.DisplayLevelCode,
                                DisplayLevelName = (level != null) ? level.LevelName : string.Empty,
                                TotalBudget = budget.TotalBudget,
                                BudgetQuantityUsed = budget.BudgetQuantityUsed,
                            }).ToList();

                var approveRegistrationCustomerDetail = await _dbDisApproveRegistrationCustomerDetail.GetAllQueryable(x => x.DisplayCode == code).AsNoTracking().ToListAsync();

                var headerReportModel = _mapper.Map<DisplayHeaderReportModel>(data);
                headerReportModel.ScopeTypeName = scopeTypeName ?? string.Empty;
                headerReportModel.ApplicableObjectTypeDescription = applicable ?? string.Empty;
                headerReportModel.ListDataBudget = dataBudget;
                headerReportModel.ApproveRegistrationCustomerDetails = approveRegistrationCustomerDetail;
                if (headerReportModel.ScopeType == CommonData.DisplaySetting.ScopeNationwide)
                {
                    headerReportModel.TerritoryStructureLevels = await _dbsalesOrg
                               .GetAllQueryable(x => x.TerritoryStructureCode == headerReportModel.TerritoryStructureCode)
                               .Select(x => new TpTerritoryStructureLevelModel()
                               {
                                   Id = x.Id.ToString(),
                                   TerritoryStructureCode = x.TerritoryStructureCode,
                                   Description = x.Description,
                                   Level = x.Level,
                                   TerritoryLevelCode = x.TerritoryLevelCode
                               }).ToListAsync();
                }
                else
                {
                    headerReportModel.ListScope = await GetListDisplayScopeByDisplayCode(code);
                }
                if (headerReportModel.ApplicableObjectType.Equals(CommonData.DisplaySetting.ObjectCustomerAttributes))
                {
                    DateTime now = DateTime.Now;
                    var resultData = (from cusSetting in _dbCustomerSetting.GetAllQueryable().AsNoTracking()
                                  join cusLevel in _dbDisCustomerAttributeLevel.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisplayCode == code).AsNoTracking()
                                     on cusSetting.AttributeId equals cusLevel.CustomerAttributerLevel into emptyCusLevel
                                  from cusLevel in emptyCusLevel.DefaultIfEmpty()
                                  select new DisplayCustomerSettingModel()
                                  {
                                      CustomerAttributeId = cusSetting.Id,
                                      CustomerLevelCode = cusSetting.AttributeId,
                                      CustomerLevelName = cusSetting.AttributeName,
                                      IsCustomerAttribute = cusSetting.IsCustomerAttribute,
                                      IsApply = (cusLevel != null) ? cusLevel.IsApply : false
                                  }).ToList();

                    var lstCusAttributes = _dbCustomerAttribute.GetAllQueryable(x => x.EffectiveDate <= now && (!x.ValidUntil.HasValue || x.ValidUntil.Value >= now)).AsNoTracking();
                    foreach (var item in resultData.Where(x => x.IsApply))
                    {
                        var result = (from cusValue in _dbDisCustomerAttributeValue.GetAllQueryable(x => x.DeleteFlag == 0
                                      && x.DisplayCode == code && x.CustomerAttributerLevel.ToLower().Equals(item.CustomerLevelCode.ToLower())).AsNoTracking()
                                      join cusAttribute in lstCusAttributes.Where(x => x.CustomerSettingId.Equals(item.CustomerAttributeId))
                                      on cusValue.CustomerAttributerValue.ToLower() equals cusAttribute.Code.ToLower()
                                      select new DisplayCustomerAttributeModel()
                                      {
                                          CustomerLevelCode = cusValue.CustomerAttributerLevel,
                                          CustomerValueCode = cusValue.CustomerAttributerValue,
                                          CustomerValueName = cusAttribute.Description
                                      }).ToList();

                        item.CustomerAttributeModels.AddRange(result);
                    }
                    headerReportModel.ListApplicableObject = resultData;
                }
                return await Task.FromResult(headerReportModel);
            }

            return null;
        }
        #endregion Display Report Header
    }
}
