using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerAdjustmentDataType
    {
        public Guid Id { get; set; }
        public Guid DataTypeDefinitionId { get; set; }
        public Guid CustomerAdjustmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerAdjustment CustomerAdjustment { get; set; }
        public virtual DataTypeDefinition DataTypeDefinition { get; set; }
    }
}
