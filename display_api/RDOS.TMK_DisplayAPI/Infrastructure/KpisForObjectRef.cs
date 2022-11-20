using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class KpisForObjectRef
    {
        public Guid Id { get; set; }
        public string KpisObjectCode { get; set; }
        public string KpisObjectDescription { get; set; }
        public string SalesTerritoryStructure { get; set; }
        public string ObjectCode { get; set; }
        public DateTime FromDay { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string SalesWeek { get; set; }
        public string SalesBiWeek { get; set; }
        public string SalesPeriod { get; set; }
        public string SalesQuater { get; set; }
        public string SalesYear { get; set; }
        public string Status { get; set; }
        public string SostructureCode { get; set; }
        public bool? IsManual { get; set; }
    }
}
