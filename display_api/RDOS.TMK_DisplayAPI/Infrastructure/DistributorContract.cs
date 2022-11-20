using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DistributorContract
    {
        public Guid Id { get; set; }
        public Guid DistributorId { get; set; }
        public string ContractCode { get; set; }
        public string ContractType { get; set; }
        public string ContractDescription { get; set; }
        public string BilltoCode { get; set; }
        public string Name { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public string Representative { get; set; }
        public string Phone { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Distributor Distributor { get; set; }
    }
}
