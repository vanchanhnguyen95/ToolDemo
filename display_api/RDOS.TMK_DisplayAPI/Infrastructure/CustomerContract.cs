using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerContract
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string ContractId { get; set; }
        public string ContractType { get; set; }
        public DateTime ContractEffectiveDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerInformation Customer { get; set; }
    }
}
