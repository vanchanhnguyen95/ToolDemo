using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.SaleCalendar
{
    public class SalePeriodModel
    {
        public Guid Id { get; set; }
        public Guid SaleCalendarId { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public int? Ordinal { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SaleYear { get; set; }
    }

    public class ListSalePeriodModel
    {
        public List<SalePeriodModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
