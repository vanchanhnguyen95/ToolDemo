using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Services.Base;
using RDOS.TMK_DisplayAPI.Models.SaleCalendar;
using System;
using System.Collections.Generic;
using System.Linq;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using Sys.Common.Constants;

namespace RDOS.TMK_DisplayAPI.Services.Common
{
    public class ExternalService : IExternalService
    {
        #region Property
        private readonly ILogger<ExternalService> _logger;
        private readonly IBaseRepository<SaleCalendarGenerate> _dbSaleCalendarGenerate;
        private readonly IBaseRepository<SaleCalendar> _dbSaleCalendar;
        private readonly IBaseRepository<DisDisplay> _dbDisplay;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ExternalService(ILogger<ExternalService> logger,
            IBaseRepository<SaleCalendarGenerate> dbSaleCalendarGenerate,
            IBaseRepository<SaleCalendar> dbSaleCalendar,
            IBaseRepository<DisDisplay> dbDisplay,
            IMapper mapper
            )
        {
            _logger = logger;
            _dbSaleCalendarGenerate = dbSaleCalendarGenerate;
            _dbSaleCalendar = dbSaleCalendar;
            _dbDisplay = dbDisplay;
            _mapper = mapper;
        }

        #endregion

        #region Method
        public SalePeriodModel GetCalendarByTypeByDate(string type, DateTime date)
        {
            var result = (from sc in _dbSaleCalendar.GetAllQueryable().AsNoTracking()
                          join scg in _dbSaleCalendarGenerate.GetAllQueryable().AsNoTracking()
                          on sc.Id equals scg.SaleCalendarId
                          where scg.Type.ToLower().Equals(type.ToLower()) && scg.StartDate.Value > date
                          orderby scg.StartDate
                          select new SalePeriodModel
                          {
                              Id = scg.Id,
                              SaleCalendarId = scg.SaleCalendarId,
                              Type = scg.Type,
                              Code = scg.Code,
                              StartDate = scg.StartDate,
                              EndDate = scg.EndDate,
                              Ordinal = scg.Ordinal,
                              SaleYear = sc.SaleYear
                          }).FirstOrDefault();
            return result;
        }

        public List<SalePeriodModel> GetCalendarByTypeByDisplayCode(string DisplayCode)
        {
            var display = _dbDisplay.FirstOrDefault(x => x.Code.ToLower().Equals(DisplayCode.ToLower()));
            var type = string.Empty;
            switch (display.FrequencyDisplay)
            {
                case CommonData.DisplaySetting.WEEK_VALUE:
                    type = CommonData.DisplaySetting.WEEK;
                    break;
                case CommonData.DisplaySetting.MONTH_VALUE:
                    type = CommonData.DisplaySetting.MONTH;
                    break;
                case CommonData.DisplaySetting.QUARTER_VALUE:
                    type = CommonData.DisplaySetting.QUARTER;
                    break;
                case CommonData.DisplaySetting.YEAR_VALUE:
                    type = CommonData.DisplaySetting.YEAR;
                    break;
                default:
                    break;
            }
            if (!display.ImplementationStartDate.HasValue || !display.ImplementationEndDate.HasValue)
            {
                return new List<SalePeriodModel>();
            }
            else
            {
                return (from sc in _dbSaleCalendar.GetAllQueryable().AsNoTracking()
                        join scg in _dbSaleCalendarGenerate.GetAllQueryable().AsNoTracking()
                        on sc.Id equals scg.SaleCalendarId
                        where scg.Type.ToLower().Equals(type.ToLower())
                        && scg.EndDate >= display.ImplementationStartDate.Value && scg.StartDate <= display.ImplementationEndDate
                        orderby scg.Code
                        select new SalePeriodModel
                        {
                            Id = scg.Id,
                            SaleCalendarId = scg.SaleCalendarId,
                            Type = scg.Type,
                            Code = scg.Code,
                            StartDate = scg.StartDate,
                            EndDate = scg.EndDate,
                            Ordinal = scg.Ordinal,
                            SaleYear = sc.SaleYear
                        }).ToList();
            }
        }
        #endregion
    }
}
