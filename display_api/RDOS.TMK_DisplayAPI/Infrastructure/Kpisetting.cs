using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Kpisetting
    {
        public Guid Id { get; set; }
        public int SaleYear { get; set; }
        public string BasedValue { get; set; }
        public int BasedPastMonths { get; set; }
        public int BasedPastYears { get; set; }
        public string PicsaleTerritory { get; set; }
        public int DecimalRounding { get; set; }
        public int DateConfirmTargetTo { get; set; }
        public int DateConfirmTargetFrom { get; set; }
        public float OutletPerRouteSales { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string SalesIn { get; set; }
        public string SalesOut { get; set; }
    }
}
