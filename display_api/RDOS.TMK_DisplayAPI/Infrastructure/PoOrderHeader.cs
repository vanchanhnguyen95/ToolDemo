using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PoOrderHeader
    {
        public Guid Id { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string PrincipalWareHouseCode { get; set; }
        public string Type { get; set; }
        public DateTime PoDate { get; set; }
        public DateTime ExpectReDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorShiptoCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string DistributorShiptoDescription { get; set; }
    }
}
