using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Models.SaleCalendar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Common
{
    public interface IExternalService
    {
        public SalePeriodModel GetCalendarByTypeByDate(string type, DateTime date);
        public List<SalePeriodModel> GetCalendarByTypeByDisplayCode(string DisplayCode);
    }
}
