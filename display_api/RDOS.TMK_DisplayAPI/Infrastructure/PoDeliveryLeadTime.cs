using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PoDeliveryLeadTime
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string TerritoryValueKey { get; set; }
        public string TerritoryLevelValue { get; set; }
        public int WorkingHours { get; set; }
        public string MasterValue { get; set; }
        public string GeoMaster { get; set; }
        public string SalesOrgId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
