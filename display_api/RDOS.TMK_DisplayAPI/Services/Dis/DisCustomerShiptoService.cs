using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Services.Base;
using RDOS.TMK_DisplayAPI.Models.Customer;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisCustomerShiptoService : IDisCustomerShiptoService
    {
        #region Property
        private readonly IMapper _mapper;
        private readonly ILogger<DisCustomerShiptoService> _logger;
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
        private readonly IBaseRepository<DisDefinitionStructure> _serviceDisDefinitionStructure;
        #endregion

        #region Constructor
        public DisCustomerShiptoService(IMapper mapper,
            ILogger<DisCustomerShiptoService> logger,
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
            IBaseRepository<DisDefinitionStructure> serviceDisDefinitionStructure
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
            _serviceDisDefinitionStructure = serviceDisDefinitionStructure;
        }
        #endregion

        #region Method
        public BaseResultModel DeleteDisCustomerShiptos(DeleteDisCustomerShiptosModel input)
        {
            try
            {
                var data = _dbDisCustomerShipto.GetAllQueryable(x => x.DisplayCode == input.DisplayCode && input.DisplayLevelCodes.Contains(x.DisplayLevelCode)).ToList();
                if (data != null && data.Any())
                {
                    _dbDisCustomerShipto.DeleteRange(data);

                    var dataScopeTerritories = _dbDisCustomerShiptoDetail.GetAllQueryable(x => x.DisplayCode == input.DisplayCode &&
                                                input.DisplayLevelCodes.Contains(x.DisplayLevelCode)).ToList();
                    if (dataScopeTerritories != null && dataScopeTerritories.Any())
                    {
                        _dbDisCustomerShiptoDetail.DeleteRange(dataScopeTerritories);
                    }
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

        public BaseResultModel DeleteAllDisCustomerShiptoByDisplayCode(string displayCode)
        {
            try
            {
                var data = _dbDisCustomerShipto.GetAllQueryable(x => x.DisplayCode == displayCode).ToList();
                if (data != null && data.Any())
                {
                    _dbDisCustomerShipto.DeleteRange(data);

                    var dataScopeTerritories = _dbDisCustomerShiptoDetail.GetAllQueryable(x => x.DisplayCode == displayCode).ToList();
                    if (dataScopeTerritories != null && dataScopeTerritories.Any())
                    {
                        _dbDisCustomerShiptoDetail.DeleteRange(dataScopeTerritories);
                    }
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

        public List<DisCustomerShiptoModel> GetListDisCustomerShiptoByDisplayCode(string displayCode)
        {
            var data = (from shipto in _dbDisCustomerShipto.GetAllQueryable(x => x.DisplayCode == displayCode && x.DeleteFlag == 0).AsNoTracking()
                        join level in _serviceDisDefinitionStructure.GetAllQueryable(x => x.DisplayCode == displayCode && x.DeleteFlag == 0).AsNoTracking()
                        on shipto.DisplayLevelCode equals level.LevelCode into dataLevel
                        from level in dataLevel.DefaultIfEmpty()
                        select new DisCustomerShiptoModel()
                        {
                            Id = shipto.Id,
                            DisplayCode = shipto.DisplayCode,
                            DisplayLevelCode = shipto.DisplayLevelCode,
                            DisplayLevelName = (level != null) ? level.LevelName : string.Empty,
                            IsSales = shipto.IsSales,
                            NumberSalesHas = shipto.NumberSalesHas,
                            SaleUnit = shipto.SaleUnit,
                            TotalSalePoint = shipto.TotalSalePoint,
                            TotalSalePointWithoutPOSM = shipto.TotalSalePointWithoutPOSM,
                            TotalSalePointWithPOSM = shipto.TotalSalePointWithPOSM,
                            TypeSalePoint = shipto.TypeSalePoint
                        }).ToList();

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

                var lstDataCustomerShipto = (from detail in _dbDisCustomerShiptoDetail.GetAllQueryable(x => x.DisplayCode.ToLower().Equals(displayCode.ToLower())).AsNoTracking()
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
            return data;
        }

        public BaseResultModel SaveDisCustomerShipto(DisCustomerShiptoModel input, string userLogin)
        {
            try
            {
                var data = _dbDisCustomerShipto.FirstOrDefault(x => x.DisplayCode == input.DisplayCode && x.DisplayLevelCode == input.DisplayLevelCode
                       && x.DeleteFlag == 0);
                if (data == null)
                {
                    var entityDisBudget = _mapper.Map<DisCustomerShipto>(input);
                    entityDisBudget.Id = Guid.NewGuid();
                    entityDisBudget.CreatedBy = userLogin;
                    entityDisBudget.CreatedDate = DateTime.Now;
                    entityDisBudget.DeleteFlag = 0;
                    _dbDisCustomerShipto.Insert(entityDisBudget);
                }
                else
                {
                    var idTemp = data.Id;
                    var entityDisBudget = _mapper.Map<DisCustomerShipto>(input);
                    entityDisBudget.Id = idTemp;
                    entityDisBudget.UpdatedDate = DateTime.Now;
                    entityDisBudget.UpdatedBy = userLogin;
                    _dbDisCustomerShipto.Update(entityDisBudget);

                    var dataCustomerShiptoDetails = _dbDisCustomerShiptoDetail.GetAllQueryable(x => x.DisplayCode == entityDisBudget.DisplayCode &&
                                               x.DisplayLevelCode == input.DisplayLevelCode && x.DeleteFlag == 0);
                    if (dataCustomerShiptoDetails != null && dataCustomerShiptoDetails.Any())
                    {
                        _dbDisCustomerShiptoDetail.DeleteRange(dataCustomerShiptoDetails);
                    }

                }
                InsertRangeDisCustomerShiptoDetail(input.DisCustomerShiptoDetailModels, userLogin);

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

        private void InsertRangeDisCustomerShiptoDetail(List<DisCustomerShiptoDetailModel> input, string userLogin)
        {
            if (input != null && input.Any())
            {
                _dbDisCustomerShiptoDetail.InsertRange(GetDisCustomerShiptoDetails(input, userLogin));
            }
        }

        private List<DisCustomerShiptoDetail> GetDisCustomerShiptoDetails(List<DisCustomerShiptoDetailModel> lstInput, string userLogin)
        {
            var lstData = new List<DisCustomerShiptoDetail>();
            foreach (var item in lstInput)
            {
                var entity = _mapper.Map<DisCustomerShiptoDetail>(item);
                entity.Id = Guid.NewGuid();
                entity.CreatedBy = userLogin;
                entity.CreatedDate = DateTime.Now;
                entity.DeleteFlag = 0;
                lstData.Add(entity);
            }
            return lstData;
        }

        #region DIS.02.06
        public IQueryable<DisScopeCustomerShiptoModel> GetListCustomerShiptoByScope(CustomerShiptoByScopeSearchModel input)
        {
            var result = (from ssos in _dbSaleOrg.GetAllQueryable(x => x.Code.ToLower().Equals(input.SaleOrgCode.ToLower())).AsNoTracking()
                          join ddsa in _dbDsa.GetAllQueryable().AsNoTracking()
                          on ssos.TerritoryStructureCode equals ddsa.SOStructureCode
                          join rrzi in _dbRzInfo.GetAllQueryable().AsNoTracking()
                          on ddsa.Code equals rrzi.DSACode
                          join rrzs in _dbRzShipto.GetAllQueryable().AsNoTracking()
                          on rrzi.RouteZoneCode equals rrzs.RouteZoneCode
                          join stm in _dbScTerritoryMapping.GetAllQueryable().AsNoTracking()
                          on ddsa.MappingNode equals stm.MappingNode
                          join stv in _dbScTerritoryValue.GetAllQueryable().AsNoTracking()
                          on stm.TerritoryValueKey equals stv.Key
                          join cs in _serviceCustomerShipto.GetAllQueryable().AsNoTracking()
                          on rrzs.ShiptoId equals cs.Id
                          join ci in _serviceCustomerInformation.GetAllQueryable().AsNoTracking()
                          on cs.CustomerInfomationId equals ci.Id
                          select new DisScopeCustomerShiptoModel()
                          {
                              Id = cs.Id,
                              CustomerId = cs.CustomerInfomationId,
                              CustomerCode = ci.CustomerCode,
                              CustomerName = ci.ShortName,
                              ShiptoCode = cs.ShiptoCode,
                              ShiptoName = cs.ShiptoName,
                              Status = (Models.External.Enum.Status)cs.Status,
                              Address = cs.Address,
                              SaleTerritoryLevel = stv.TerritoryLevelCode,
                              SaleTerritoryValue = stv.Code,
                              DsaCode = ddsa.Code
                          }).AsQueryable();

            if (input.ScopeType.Equals(CommonData.DisplaySetting.ScopeSalesTerritoryLevel))
            {
                result = result.Where(x => x.SaleTerritoryLevel.Equals(input.SaleTerritoryLevel) && input.ListSaleTerritoryValues.Contains(x.SaleTerritoryValue));
            }
            else if (input.ScopeType.Equals(CommonData.DisplaySetting.ScopeDSA))
            {
                result = result.Where(x => input.ListDsaValues.Contains(x.DsaCode));
            }

            return result;
        }
        #endregion
        #endregion
    }
}
