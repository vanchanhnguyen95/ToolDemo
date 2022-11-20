using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.TempDis
{
    public class TempDisCusShiptoSaleOrQuantityService : ITempDisCusShiptoSaleOrQuantityService
    {
        #region Property
        private readonly IBaseRepository<TempDisCustomerShiptoSaleOrQuantity> _dbTempDisCusShiptoSaleOrQuantity;
        private readonly IMapper _mapper;
        #endregion

        public TempDisCusShiptoSaleOrQuantityService(
            IMapper mapper,
            IBaseRepository<TempDisCustomerShiptoSaleOrQuantity> dbTempDisCusShiptoSaleOrQuantity
            )
        {
            _dbTempDisCusShiptoSaleOrQuantity = dbTempDisCusShiptoSaleOrQuantity;
            _mapper = mapper;
        }

        public IQueryable<TempDisCustomerShiptoSaleOrQuantityModel> GetListTempDisCusShiptoSaleOrQuantity(TempDisCustomerShiptoSaleOrQuantitySeachModel search)
        {
            IQueryable<TempDisCustomerShiptoSaleOrQuantity> result = null;
            result = _dbTempDisCusShiptoSaleOrQuantity.GetAllQueryable(x =>
                            x.SaleOrgCode.ToLower().Equals(search.SaleOrgCode.ToLower())).AsNoTracking().AsQueryable();

            if (search.ScopeType.Equals(CommonData.DisplaySetting.ScopeSalesTerritoryLevel))
            {
                switch (search.SaleTerritoryLevel)
                {
                    case CommonData.TerritoryLevelSetting.Branch:
                        result = result.Where(x => search.ListSaleTerritoryValues.Contains(x.BranchCode)).AsNoTracking().AsQueryable();
                        break;
                    case CommonData.TerritoryLevelSetting.Region:
                        result = result.Where(x => search.ListSaleTerritoryValues.Contains(x.RegionCode)).AsNoTracking().AsQueryable();
                        break;
                    case CommonData.TerritoryLevelSetting.SubRegion:
                        result = result.Where(x => search.ListSaleTerritoryValues.Contains(x.SubRegionCode)).AsNoTracking().AsQueryable();
                        break;
                    case CommonData.TerritoryLevelSetting.Area:
                        result = result.Where(x => search.ListSaleTerritoryValues.Contains(x.AreaCode)).AsNoTracking().AsQueryable();
                        break;
                    case CommonData.TerritoryLevelSetting.SubArea:
                        result = result.Where(x => search.ListSaleTerritoryValues.Contains(x.SubAreaCode)).AsNoTracking().AsQueryable();
                        break;
                    default:
                        break;
                }
            }
            else if (search.ScopeType.Equals(CommonData.DisplaySetting.ScopeDSA))
            {
                result = result.Where(x => search.ListDsaValues.Contains(x.DsaCode)).AsNoTracking().AsQueryable();
            }

            if (search.IsSale)
            {
                result = result.Where(x => x.QuantityNumbers >= search.QuantityNumbers);
            }
            else
            {
                result = result.Where(x => x.SaleNumbers >= search.SaleNumbers);
            }
            return result.ProjectTo<TempDisCustomerShiptoSaleOrQuantityModel>(_mapper.ConfigurationProvider);
        }
    }
}
