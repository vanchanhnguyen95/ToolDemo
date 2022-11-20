using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerApplyToValue1
    {
        public Guid Id { get; set; }
        public string Master { get; set; }
        public string Values { get; set; }
        public string Description { get; set; }
        public Guid CustomerAdjustmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerAdjustment1 CustomerAdjustment { get; set; }
    }
}
