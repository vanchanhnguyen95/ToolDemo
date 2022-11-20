using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class InvWhTransferDetail
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string DistributorCode { get; set; }
        public string WareHouseCode { get; set; }
        public string LocationCode { get; set; }
        public string Uom { get; set; }
        public int Quantity { get; set; }
        public int BaseQuantity { get; set; }
        public string TransactionType { get; set; }
        public string ItemKey { get; set; }
        public string Dsacode { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string WhTransferCode { get; set; }
        public string EmployeeTransferCode { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Key { get; set; }
        public int? BegQty { get; set; }
        public int? EndQty { get; set; }
        public int? Issue { get; set; }
        public int? Receipt { get; set; }
    }
}
