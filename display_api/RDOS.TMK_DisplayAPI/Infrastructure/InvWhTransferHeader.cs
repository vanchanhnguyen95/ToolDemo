using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class InvWhTransferHeader
    {
        public Guid Id { get; set; }
        public string TransferCode { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string SalesPeriod { get; set; }
        public string DistributorCode { get; set; }
        public string FromWareHouseCode { get; set; }
        public string ToWareHouseCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
