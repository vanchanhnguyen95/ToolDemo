using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DataTypeDefinition
    {
        public DataTypeDefinition()
        {
            CustomerAdjustmentDataTypes = new HashSet<CustomerAdjustmentDataType>();
        }

        public Guid Id { get; set; }
        public string DataTypeSettingKey { get; set; }
        public string Source { get; set; }
        public string[] ValidField { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<CustomerAdjustmentDataType> CustomerAdjustmentDataTypes { get; set; }
    }
}
