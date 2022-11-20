using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Temp
{
    public class TempDisplayModel
    {
    }

    public class TempDisplaySaleOrOutputRequestModel
    {
        public string ProgramType { get; set; }
        public string DisplayCode { get; set; }
        public string PeriodCode { get; set; }
    }

    public class TempDisplayConfirmRequestModel
    {
        public string DisplayCode { get; set; }
        public string SalesCalendarCode { get; set; }
    }

    public class TempDisplaySaleOrOutputResponseModel
    {
        public string ProgramType { get; set; }
        public string DisplayCode { get; set; }
        public string DisplayLevel { get; set; }
        public string CustomerCode { get; set; }
        public string ShiptoCode { get; set; }
        public decimal SumSales { get; set; }
        public decimal SumOutput { get; set; }
    }
}
