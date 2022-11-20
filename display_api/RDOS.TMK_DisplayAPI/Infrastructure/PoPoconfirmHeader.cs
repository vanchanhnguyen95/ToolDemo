using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PoPoconfirmHeader
    {
        public Guid Id { get; set; }
        public string PurchaseOrderConfirmNumber { get; set; }
        public string PoPrincipalNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PrincipalWareHouseCode { get; set; }
        public DateTime ExpectReDate { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public string PurchaseOrderNumber { get; set; }
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
