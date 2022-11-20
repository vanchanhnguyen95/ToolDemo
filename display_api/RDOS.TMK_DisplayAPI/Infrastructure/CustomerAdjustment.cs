using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerAdjustment
    {
        public CustomerAdjustment()
        {
            CustomerAdjustmentDataTypes = new HashSet<CustomerAdjustmentDataType>();
            CustomerAdjustmentShiptos = new HashSet<CustomerAdjustmentShipto>();
            CustomerApplyToValues = new HashSet<CustomerApplyToValue>();
        }

        public Guid Id { get; set; }
        public string AdjustmentId { get; set; }
        public string Status { get; set; }
        public string[] DataType { get; set; }
        public string UpdateBy { get; set; }
        public string ApproveBy { get; set; }
        public bool RequiredVerify { get; set; }
        public string ApplyTo { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<CustomerAdjustmentDataType> CustomerAdjustmentDataTypes { get; set; }
        public virtual ICollection<CustomerAdjustmentShipto> CustomerAdjustmentShiptos { get; set; }
        public virtual ICollection<CustomerApplyToValue> CustomerApplyToValues { get; set; }
    }
}
