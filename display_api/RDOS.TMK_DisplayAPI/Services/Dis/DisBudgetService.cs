using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisBudgetService : IDisBudgetService
    {
        #region Property
        private readonly IMapper _mapper;
        private readonly ILogger<DisBudgetService> _logger;
        private readonly IBaseRepository<DisDisplay> _dbDisplay;
        private readonly IBaseRepository<DisBudget> _dbDisBudget;
        private readonly IBaseRepository<DisBudgetForScopeDsa> _dbDisBudgetForScopeDsa;
        private readonly IBaseRepository<DisBudgetForScopeTerritory> _dbDisBudgetForScopeTerritory;
        private readonly IBaseRepository<DisBudgetForCusAttribute> _dbDisBudgetForCusAttribute;
        private readonly IBaseRepository<ScTerritoryValue> _serviceTerritoryValue;
        private readonly IBaseRepository<DsaDistributorSellingArea> _serviceDsa;
        private readonly IBaseRepository<CustomerSetting> _serviceCustomerSetting;
        private readonly IBaseRepository<CustomerAttribute> _serviceCustomerAttribute;
        private readonly IBaseRepository<DisDefinitionStructure> _serviceDisDefinitionStructure;
        #endregion

        #region Constructor
        public DisBudgetService(IMapper mapper,
            ILogger<DisBudgetService> logger,
            IBaseRepository<DisDisplay> dbDisplay,
            IBaseRepository<DisBudget> dbDisBudget,
            IBaseRepository<DisBudgetForScopeDsa> dbDisBudgetForScopeDsa,
            IBaseRepository<DisBudgetForScopeTerritory> dbDisBudgetForScopeTerritory,
            IBaseRepository<DisBudgetForCusAttribute> dbDisBudgetForCusAttribute,
            IBaseRepository<ScTerritoryValue> serviceTerritoryValue,
            IBaseRepository<DsaDistributorSellingArea> serviceDsa,
            IBaseRepository<CustomerSetting> serviceCustomerSetting,
            IBaseRepository<CustomerAttribute> serviceCustomerAttribute,
            IBaseRepository<DisDefinitionStructure> serviceDisDefinitionStructure)
        {
            _mapper = mapper;
            _logger = logger;
            _dbDisplay = dbDisplay;
            _dbDisBudget = dbDisBudget;
            _dbDisBudgetForScopeDsa = dbDisBudgetForScopeDsa;
            _dbDisBudgetForScopeTerritory = dbDisBudgetForScopeTerritory;
            _dbDisBudgetForCusAttribute = dbDisBudgetForCusAttribute;
            _serviceTerritoryValue = serviceTerritoryValue;
            _serviceDsa = serviceDsa;
            _serviceCustomerSetting = serviceCustomerSetting;
            _serviceCustomerAttribute = serviceCustomerAttribute;
            _serviceDisDefinitionStructure = serviceDisDefinitionStructure;
        }
        #endregion

        #region Method
        public BaseResultModel DeleteDisBudgets(DeleteDisBudgetsModel input)
        {
            try
            {
                var data = _dbDisBudget.GetAllQueryable(x => x.DisplayCode == input.DisplayCode && input.DisplayLevelCodes.Contains(x.DisplayLevelCode)).ToList();
                if (data != null && data.Any())
                {
                    _dbDisBudget.DeleteRange(data);

                    var dataScopeTerritories = _dbDisBudgetForScopeTerritory.GetAllQueryable(x => x.DisplayCode == input.DisplayCode &&
                                                input.DisplayLevelCodes.Contains(x.DisplayLevelCode));
                    var dataScopeDsas = _dbDisBudgetForScopeDsa.GetAllQueryable(x => x.DisplayCode == input.DisplayCode &&
                                        input.DisplayLevelCodes.Contains(x.DisplayLevelCode));
                    var dataCusAttributes = _dbDisBudgetForCusAttribute.GetAllQueryable(x => x.DisplayCode == input.DisplayCode &&
                                            input.DisplayLevelCodes.Contains(x.DisplayLevelCode));

                    DeleteRangeDisBudgetForScopeTerritory(dataScopeTerritories);
                    DeleteRangeDisBudgetForScopeDsa(dataScopeDsas);
                    DeleteRangeDisBudgetForCusAttribute(dataCusAttributes);
                }

                return new BaseResultModel
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "DeleteSuccess"
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
        public List<DisBudgetModel> GetListDisBudgets(string displayCode, int type = CommonData.DisplaySetting.TypeBudgetNow, int adjustmentsCount = 0)
        {
            var display = _dbDisplay.FirstOrDefault(x => x.DeleteFlag == 0 && x.Code.ToLower().Equals(displayCode.ToLower()));

            var data = (from budget in _dbDisBudget.GetAllQueryable(x => x.DisplayCode == displayCode && x.DeleteFlag == 0 && x.Type == type).AsNoTracking()
                        join level in _serviceDisDefinitionStructure.GetAllQueryable(x => x.DisplayCode == displayCode && x.DeleteFlag == 0).AsNoTracking()
                        on budget.DisplayLevelCode equals level.LevelCode into dataLevel
                        from level in dataLevel.DefaultIfEmpty()
                        select new DisBudgetModel()
                        {
                            Id = budget.Id,
                            DisplayCode = budget.DisplayCode,
                            DisplayLevelCode = budget.DisplayLevelCode,
                            DisplayLevelName = (level != null) ? level.LevelName : string.Empty,
                            TotalBudget = budget.TotalBudget,
                            NewTotalBudget = budget.NewTotalBudget,
                            BudgetQuantityUsed = budget.BudgetQuantityUsed,
                            Type = budget.Type,
                            AdjustmentsCount = budget.AdjustmentsCount
                        });

            if (type != CommonData.DisplaySetting.TypeBudgetNow)
            {
                data = data.Where(x => x.AdjustmentsCount == adjustmentsCount).AsNoTracking();
            }

            var result = new List<DisBudgetModel>();
            result.AddRange(data.ToList());

            if (result.Any())
            {
                var lstScopeTerritories = new List<DisBudgetForScopeTerritoryModel>();
                var lstScopeDsas = new List<DisBudgetForScopeDsaModel>();
                var lstCusAttributes = new List<DisBudgetForCusAttributeModel>();
                var now = DateTime.Now;

                var dataScopeTerritories = (from scope in _dbDisBudgetForScopeTerritory
                                            .GetAllQueryable(x => x.DeleteFlag == 0 && x.DisplayCode.ToLower().Equals(displayCode.ToLower())).AsNoTracking()
                                            join territory in _serviceTerritoryValue.GetAllQueryable(x => !x.IsDeleted
                                            && x.EffectiveDate <= now && (!x.UntilDate.HasValue || x.UntilDate.Value >= now) && x.TerritoryLevelCode == display.ScopeSaleTerritoryLevel).AsNoTracking()
                                            on scope.ScopeValue equals territory.Code into emptyScope
                                            from territory in emptyScope.DefaultIfEmpty()
                                            where scope.Type == type
                                            select new DisBudgetForScopeTerritoryModel()
                                            {
                                                Id = scope.Id,
                                                ScopeValue = scope.ScopeValue,
                                                AdjustmentDate = scope.AdjustmentDate,
                                                AdjustmentsCount = scope.AdjustmentsCount,
                                                BudgetQuantity = scope.BudgetQuantity,
                                                DisplayCode = scope.DisplayCode,
                                                DisplayLevelCode = scope.DisplayLevelCode,
                                                NewBudgetQuantity = scope.NewBudgetQuantity,
                                                Type = scope.Type,
                                                BudgetQuantityUsed = scope.BudgetQuantityUsed,
                                                ScopeDescription = (territory != null) ? territory.Description : string.Empty
                                            }).AsNoTracking();

                var dataScopeDsas = (from scope in _dbDisBudgetForScopeDsa
                                     .GetAllQueryable(x => x.DeleteFlag == 0 && x.DisplayCode.ToLower().Equals(displayCode.ToLower())).AsNoTracking()
                                     join dsa in _serviceDsa.GetAllQueryable(x => x.EffectiveDate <= now && (!x.UntilDate.HasValue || x.UntilDate.Value >= now)).AsNoTracking()
                                     on scope.ScopeValue equals dsa.Code into emptyScope
                                     from dsa in emptyScope.DefaultIfEmpty()
                                     where scope.Type == type
                                     select new DisBudgetForScopeDsaModel()
                                     {
                                         Id = scope.Id,
                                         ScopeValue = scope.ScopeValue,
                                         AdjustmentDate = scope.AdjustmentDate,
                                         AdjustmentsCount = scope.AdjustmentsCount,
                                         BudgetQuantity = scope.BudgetQuantity,
                                         DisplayCode = scope.DisplayCode,
                                         DisplayLevelCode = scope.DisplayLevelCode,
                                         NewBudgetQuantity = scope.NewBudgetQuantity,
                                         Type = scope.Type,
                                         BudgetQuantityUsed = scope.BudgetQuantityUsed,
                                         ScopeDescription = (dsa != null) ? dsa.Description : string.Empty
                                     }).AsNoTracking();

                var dataCusAttributes = (from customervalue in _dbDisBudgetForCusAttribute.GetAllQueryable(x => x.DeleteFlag == 0 &&
                                         x.DisplayCode.ToLower().Equals(displayCode.ToLower())).AsNoTracking()

                                         join customersetting in _serviceCustomerSetting.GetAllQueryable().AsNoTracking()
                                         on customervalue.CustomerLevel equals customersetting.AttributeId

                                         join customerattribute in _serviceCustomerAttribute.GetAllQueryable(x => x.EffectiveDate <= now &&
                                         (!x.ValidUntil.HasValue || x.ValidUntil.Value >= now)).AsNoTracking() on
                                         new { customerLevel = customersetting.Id, customerValue = customervalue.CustomerValue } equals
                                         new { customerLevel = customerattribute.CustomerSettingId, customerValue = customerattribute.Code }
                                         into emptyCustomervalue
                                         from customerattribute in emptyCustomervalue.DefaultIfEmpty()

                                         where customervalue.Type == type
                                         select new DisBudgetForCusAttributeModel()
                                         {
                                             Id = customervalue.Id,
                                             DisplayCode = customervalue.DisplayCode,
                                             DisplayLevelCode = customervalue.DisplayLevelCode,
                                             ScopeValue = customervalue.ScopeValue,
                                             CustomerLevel = customervalue.CustomerLevel,
                                             CustomerValue = customervalue.CustomerValue,
                                             CustomerValueDescription = (customerattribute != null) ? customerattribute.Description : string.Empty,
                                             BudgetQuantity = customervalue.BudgetQuantity,
                                             NewBudgetQuantity = customervalue.NewBudgetQuantity,
                                             BudgetQuantityUsed = customervalue.BudgetQuantityUsed,
                                             AdjustmentsCount = customervalue.AdjustmentsCount,
                                             AdjustmentDate = customervalue.AdjustmentDate,
                                             Type = customervalue.Type,
                                         }).AsNoTracking();
                if (type != CommonData.DisplaySetting.TypeBudgetNow)
                {
                    dataScopeTerritories = dataScopeTerritories.Where(x => x.AdjustmentsCount == adjustmentsCount).AsNoTracking();
                    dataScopeDsas = dataScopeDsas.Where(x => x.AdjustmentsCount == adjustmentsCount).AsNoTracking();
                    dataCusAttributes = dataCusAttributes.Where(x => x.AdjustmentsCount == adjustmentsCount).AsNoTracking();
                }
                lstScopeTerritories.AddRange(dataScopeTerritories.ToList());
                lstScopeDsas.AddRange(dataScopeDsas.ToList());
                lstCusAttributes.AddRange(dataCusAttributes.ToList());

                foreach (var item in result)
                {
                    item.DisBudgetForScopeTerritories = lstScopeTerritories.Where(x => x.DisplayLevelCode == item.DisplayLevelCode).ToList();
                    item.DisBudgetForScopeDsas = lstScopeDsas.Where(x => x.DisplayLevelCode == item.DisplayLevelCode).ToList();
                    item.DisBudgetForCusAttributes = lstCusAttributes.Where(x => x.DisplayLevelCode == item.DisplayLevelCode).ToList();
                }
            }
            return result;
        }

        public IQueryable<BudgetAdjustmentListModel> GetListDisBudgetForAdjustment(string displayCode, int type)
        {
            var data = (from b in _dbDisBudget.GetAllQueryable(x => x.DisplayCode == displayCode && x.DeleteFlag == 0 && x.Type == type).AsNoTracking()
                        group b by new
                        {
                            b.DisplayCode,
                            b.AdjustmentDate,
                            b.AdjustmentsAccount,
                            b.AdjustmentsReason,
                            b.AdjustmentsFilePath,
                            b.AdjustmentsFileName,
                            b.AdjustmentsCount,
                            b.Type
                        } into bData
                        select new BudgetAdjustmentListModel()
                        {
                            DisplayCode = bData.Key.DisplayCode,
                            AdjustmentDate = bData.Key.AdjustmentDate,
                            AdjustmentsAccount = bData.Key.AdjustmentsAccount,
                            AdjustmentsReason = bData.Key.AdjustmentsReason,
                            AdjustmentsFilePath = bData.Key.AdjustmentsFilePath,
                            AdjustmentsFileName = bData.Key.AdjustmentsFileName,
                            AdjustmentsCount = bData.Key.AdjustmentsCount,
                            Type = bData.Key.Type
                        });
            return data;
        }
        public BaseResultModel SaveDisBudget(DisBudgetModel input, string userLogin)
        {
            try
            {
                InsertOrUpdateDisBudget(input, userLogin);
                return new BaseResultModel
                {
                    IsSuccess = true,
                    Code = 201,
                    Message = "SaveSuccess"
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

        public BaseResultModel SaveDisBudgets(List<DisBudgetModel> lstInput, string userLogin)
        {
            try
            {
                foreach (var item in lstInput)
                {
                    InsertOrUpdateDisBudget(item, userLogin);
                }

                return new BaseResultModel
                {
                    IsSuccess = true,
                    Code = 201,
                    Message = "SaveSuccess"
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

        public BaseResultModel SaveDisBudgetsForAdjustment(DisBudgetForAdjustmentModel input, string userLogin)
        {
            try
            {
                var data = _dbDisBudget.GetAllQueryable(x => x.DisplayCode == input.DisplayCode && x.DeleteFlag == 0).ToList();
                if (data != null && data.Any())
                {
                    #region Delete Old Data with Type = TypeBudgetNow
                    _dbDisBudget.DeleteRange(data.Where(x => x.Type == CommonData.DisplaySetting.TypeBudgetNow));

                    var dataScopeTerritories = _dbDisBudgetForScopeTerritory.GetAllQueryable(x => x.DisplayCode == input.DisplayCode && x.DeleteFlag == 0 && x.Type == CommonData.DisplaySetting.TypeBudgetNow);
                    var dataScopeDsas = _dbDisBudgetForScopeDsa.GetAllQueryable(x => x.DisplayCode == input.DisplayCode && x.DeleteFlag == 0 && x.Type == CommonData.DisplaySetting.TypeBudgetNow);
                    var dataCusAttributes = _dbDisBudgetForCusAttribute.GetAllQueryable(x => x.DisplayCode == input.DisplayCode && x.DeleteFlag == 0 && x.Type == CommonData.DisplaySetting.TypeBudgetNow);

                    DeleteRangeDisBudgetForScopeTerritory(dataScopeTerritories);
                    DeleteRangeDisBudgetForScopeDsa(dataScopeDsas);
                    DeleteRangeDisBudgetForCusAttribute(dataCusAttributes);
                    #endregion

                    #region Insert New Data
                    var dateTime = DateTime.Now;
                    var adjustmentsCount = data.Max(x => x.AdjustmentsCount) + 1;
                    foreach (var item in input.DisBudgets)
                    {
                        #region DisBuget
                        // Insert Disbudget with Type = Type of input
                        var entityDisBudget = GetDisBudget(item, input.Type, adjustmentsCount, dateTime, userLogin);
                        _dbDisBudget.Insert(entityDisBudget);
                        //Insert Clone DisBudget with Type = BudgetNow
                        entityDisBudget.Id = Guid.NewGuid();
                        if (input.Type == CommonData.DisplaySetting.TypeBudgetAdjustment)
                        {
                            entityDisBudget.TotalBudget = entityDisBudget.NewTotalBudget;
                            entityDisBudget.NewTotalBudget = 0;
                        }
                        entityDisBudget.Type = CommonData.DisplaySetting.TypeBudgetNow;
                        _dbDisBudget.Insert(entityDisBudget);
                        #endregion

                        #region DisBudgetForScopeTerritory
                        if (item.DisBudgetForScopeTerritories != null && item.DisBudgetForScopeTerritories.Any())
                        {
                            // InsertRange DisBudgetForScopeTerritory with Type = Type of input
                            var dataScope = GetDisBudgetForScopeTerritorys(item.DisBudgetForScopeTerritories, input.Type, adjustmentsCount, dateTime, userLogin);
                            _dbDisBudgetForScopeTerritory.InsertRange(dataScope);

                            // InsertRange DisBudgetForScopeTerritory with Type = BudgetNow
                            dataScope.ForEach(x =>
                            {
                                x.Id = Guid.NewGuid();
                                x.BudgetQuantity = x.NewBudgetQuantity;
                                x.NewBudgetQuantity = 0;
                                x.Type = CommonData.DisplaySetting.TypeBudgetNow;
                            });
                            _dbDisBudgetForScopeTerritory.InsertRange(dataScope);
                        }
                        #endregion

                        #region DisBudgetForScopeDsa
                        if (item.DisBudgetForScopeDsas != null && item.DisBudgetForScopeDsas.Any())
                        {
                            // InsertRange DisBudgetForScopeDsa with Type = Type of input
                            var dataScope = GetDisBudgetForScopeDsas(item.DisBudgetForScopeDsas, input.Type, adjustmentsCount, dateTime, userLogin);
                            _dbDisBudgetForScopeDsa.InsertRange(dataScope);

                            // InsertRange DisBudgetForScopeDsa with Type = BudgetNow
                            dataScope.ForEach(x =>
                            {
                                x.Id = Guid.NewGuid();
                                x.BudgetQuantity = x.NewBudgetQuantity;
                                x.NewBudgetQuantity = 0;
                                x.Type = CommonData.DisplaySetting.TypeBudgetNow;
                            });
                            _dbDisBudgetForScopeDsa.InsertRange(dataScope);
                        }
                        #endregion

                        #region DisBudgetForCusAttribute
                        if (item.DisBudgetForCusAttributes != null && item.DisBudgetForCusAttributes.Any())
                        {
                            // InsertRange DisBudgetForCusAttribute with Type = Type of input
                            var dataCusAttr = GetDisBudgetForCusAttributes(item.DisBudgetForCusAttributes, input.Type, adjustmentsCount, dateTime, userLogin);
                            _dbDisBudgetForCusAttribute.InsertRange(dataCusAttr);

                            // InsertRange DisBudgetForCusAttribute with Type = BudgetNow
                            dataCusAttr.ForEach(x =>
                            {
                                x.Id = Guid.NewGuid();
                                x.BudgetQuantity = x.NewBudgetQuantity;
                                x.NewBudgetQuantity = 0;
                                x.Type = CommonData.DisplaySetting.TypeBudgetNow;
                            });
                            _dbDisBudgetForCusAttribute.InsertRange(dataCusAttr);
                        }
                        #endregion
                    }
                    #endregion
                }
                return new BaseResultModel
                {
                    IsSuccess = true,
                    Code = 201,
                    Message = "SaveSuccess"
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

        private void InsertOrUpdateDisBudget(DisBudgetModel input, string userLogin)
        {
            var dateTime = DateTime.Now;

            var data = _dbDisBudget.FirstOrDefault(x => x.DisplayCode == input.DisplayCode && x.DisplayLevelCode == input.DisplayLevelCode
                       && x.DeleteFlag == 0 && x.Type == CommonData.DisplaySetting.TypeBudgetNow);

            if (data == null)
            {
                _dbDisBudget.Insert(GetDisBudget(input, CommonData.DisplaySetting.TypeBudgetNow, 0, dateTime, userLogin));
            }
            else
            {
                var idTemp = data.Id;
                var entityDisBudget = _mapper.Map<DisBudget>(input);
                entityDisBudget.Id = idTemp;
                entityDisBudget.UpdatedDate = dateTime;
                entityDisBudget.UpdatedBy = userLogin;
                _dbDisBudget.Update(entityDisBudget);

                var dataScopeTerritories = _dbDisBudgetForScopeTerritory.GetAllQueryable(x => x.DisplayCode == input.DisplayCode &&
                                           x.DisplayLevelCode == input.DisplayLevelCode && x.DeleteFlag == 0 && x.Type == CommonData.DisplaySetting.TypeBudgetNow);
                var dataScopeDsas = _dbDisBudgetForScopeDsa.GetAllQueryable(x => x.DisplayCode == input.DisplayCode &&
                                   x.DisplayLevelCode == input.DisplayLevelCode && x.DeleteFlag == 0 && x.Type == CommonData.DisplaySetting.TypeBudgetNow);
                var dataCusAttributes = _dbDisBudgetForCusAttribute.GetAllQueryable(x => x.DisplayCode == input.DisplayCode &&
                                   x.DisplayLevelCode == input.DisplayLevelCode && x.DeleteFlag == 0 && x.Type == CommonData.DisplaySetting.TypeBudgetNow);

                DeleteRangeDisBudgetForScopeTerritory(dataScopeTerritories);
                DeleteRangeDisBudgetForScopeDsa(dataScopeDsas);
                DeleteRangeDisBudgetForCusAttribute(dataCusAttributes);
            }

            if (input.DisBudgetForScopeTerritories != null && input.DisBudgetForScopeTerritories.Any())
            {
                _dbDisBudgetForScopeTerritory.InsertRange(GetDisBudgetForScopeTerritorys(input.DisBudgetForScopeTerritories, CommonData.DisplaySetting.TypeBudgetNow, 0, dateTime, userLogin));
            }

            if (input.DisBudgetForScopeDsas != null && input.DisBudgetForScopeDsas.Any())
            {
                _dbDisBudgetForScopeDsa.InsertRange(GetDisBudgetForScopeDsas(input.DisBudgetForScopeDsas, CommonData.DisplaySetting.TypeBudgetNow, 0, dateTime, userLogin));
            }

            if (input.DisBudgetForCusAttributes != null && input.DisBudgetForCusAttributes.Any())
            {
                _dbDisBudgetForCusAttribute.InsertRange(GetDisBudgetForCusAttributes(input.DisBudgetForCusAttributes, CommonData.DisplaySetting.TypeBudgetNow, 0, dateTime, userLogin));
            }
        }

        #region DeleteRange
        private void DeleteRangeDisBudgetForScopeTerritory(IQueryable<DisBudgetForScopeTerritory> input)
        {
            if (input != null && input.Any())
            {
                _dbDisBudgetForScopeTerritory.DeleteRange(input);
            }
        }

        private void DeleteRangeDisBudgetForScopeDsa(IQueryable<DisBudgetForScopeDsa> input)
        {
            if (input != null && input.Any())
            {
                _dbDisBudgetForScopeDsa.DeleteRange(input);
            }
        }

        private void DeleteRangeDisBudgetForCusAttribute(IQueryable<DisBudgetForCusAttribute> input)
        {
            if (input != null && input.Any())
            {
                _dbDisBudgetForCusAttribute.DeleteRange(input);
            }
        }
        #endregion

        #region MapData
        private DisBudget GetDisBudget(DisBudgetModel input, int type, int adjustmentsCount, DateTime dateTime, string userLogin)
        {
            var entity = _mapper.Map<DisBudget>(input);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = userLogin;
            entity.CreatedDate = dateTime;
            entity.DeleteFlag = 0;
            entity.Type = type;
            entity.AdjustmentsCount = adjustmentsCount;
            entity.AdjustmentDate = dateTime;
            return entity;
        }

        private List<DisBudgetForScopeTerritory> GetDisBudgetForScopeTerritorys(List<DisBudgetForScopeTerritoryModel> lstInput, int type, int adjustmentsCount, DateTime dateTime, string userLogin)
        {
            var lstData = new List<DisBudgetForScopeTerritory>();
            foreach (var item in lstInput)
            {
                var entity = _mapper.Map<DisBudgetForScopeTerritory>(item);
                entity.Id = Guid.NewGuid();
                entity.AdjustmentsCount = adjustmentsCount;
                entity.AdjustmentDate = dateTime;
                entity.Type = type;
                entity.CreatedBy = userLogin;
                entity.CreatedDate = dateTime;
                entity.DeleteFlag = 0;
                lstData.Add(entity);
            }
            return lstData;
        }

        private List<DisBudgetForScopeDsa> GetDisBudgetForScopeDsas(List<DisBudgetForScopeDsaModel> lstInput, int type, int adjustmentsCount, DateTime dateTime, string userLogin)
        {
            var lstData = new List<DisBudgetForScopeDsa>();
            foreach (var item in lstInput)
            {
                var entity = _mapper.Map<DisBudgetForScopeDsa>(item);
                entity.Id = Guid.NewGuid();
                entity.AdjustmentsCount = adjustmentsCount;
                entity.AdjustmentDate = dateTime;
                entity.Type = type;
                entity.CreatedBy = userLogin;
                entity.CreatedDate = dateTime;
                entity.DeleteFlag = 0;
                lstData.Add(entity);
            }
            return lstData;
        }

        private List<DisBudgetForCusAttribute> GetDisBudgetForCusAttributes(List<DisBudgetForCusAttributeModel> lstInput, int type, int adjustmentsCount, DateTime dateTime, string userLogin)
        {
            var lstData = new List<DisBudgetForCusAttribute>();
            foreach (var item in lstInput)
            {
                var entity = _mapper.Map<DisBudgetForCusAttribute>(item);
                entity.Id = Guid.NewGuid();
                entity.CreatedBy = userLogin;
                entity.AdjustmentsCount = adjustmentsCount;
                entity.AdjustmentDate = dateTime;
                entity.Type = type;
                entity.CreatedDate = dateTime;
                entity.DeleteFlag = 0;
                lstData.Add(entity);
            }
            return lstData;
        }
        #endregion

        #endregion
    }
}
