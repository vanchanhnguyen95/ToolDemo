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
    public class TempDisCusShiptoNotHaveService : ITempDisCusShiptoNotHaveService
    {
        #region Property
        private readonly IBaseRepository<TempDisCustomerShiptoNotHave> _dbTempDisCustomerShiptoNotHave;
        private readonly IMapper _mapper;
        #endregion

        public TempDisCusShiptoNotHaveService(
            IMapper mapper,
            IBaseRepository<TempDisCustomerShiptoNotHave> dbTempDisCustomerShiptoNotHave
            )
        {
            _dbTempDisCustomerShiptoNotHave = dbTempDisCustomerShiptoNotHave;
            _mapper = mapper;
        }

        public IQueryable<TempDisCustomerShiptoNotHaveModel> GetListTempDisCusShiptoNotHave(TempDisCusShiptoNotHaveSearchModel search)
        {
            IQueryable<TempDisCustomerShiptoNotHave> result = null;
            result = _dbTempDisCustomerShiptoNotHave.GetAllQueryable(x =>
                               x.SaleOrgCode.ToLower().Equals(search.SaleOrgCode.ToLower())
                               && !search.ListInventoryCodes.Contains(x.InventoryItemCode)).AsNoTracking().AsQueryable();

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

            return result.ProjectTo<TempDisCustomerShiptoNotHaveModel>(_mapper.ConfigurationProvider);
        }
    }
}
