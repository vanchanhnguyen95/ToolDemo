using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DmsAttributeCustomerAttribute
    {
        public Guid Id { get; set; }
        public Guid CustomerDmsAttributeId { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid CustomerSettingHierarchyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerAttribute CustomerAttribute { get; set; }
        public virtual CustomerDmsAttribute CustomerDmsAttribute { get; set; }
        public virtual CustomerSettingHierarchy CustomerSettingHierarchy { get; set; }
    }
}
