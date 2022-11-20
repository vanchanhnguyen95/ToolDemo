using System;

namespace RDOS.TMK_DisplayAPI.Models.External
{
    public class SaleCalendarByTyeModel
    {
        public Guid Id { get; set; }
        public Guid SaleCalendarId { get; set; }
        public string SaleCalendar { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Ordinal { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
