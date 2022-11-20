using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerAttribute
    {
        public CustomerAttribute()
        {
            CustomerDmsAttributes = new HashSet<CustomerDmsAttribute>();
            CustomerHierarchyMappings = new HashSet<CustomerHierarchyMapping>();
            DistributorHierarchyMappings = new HashSet<DistributorHierarchyMapping>();
            InverseParentCustomerAttribute = new HashSet<CustomerAttribute>();
        }

        public Guid Id { get; set; }
        public string AttributeMaster { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public bool IsDistributorAttribute { get; set; }
        public bool IsCustomerAttribute { get; set; }
        public Guid? ParentCustomerAttributeId { get; set; }
        public Guid CustomerSettingId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerSetting CustomerSetting { get; set; }
        public virtual CustomerAttribute ParentCustomerAttribute { get; set; }
        public virtual ICollection<CustomerDmsAttribute> CustomerDmsAttributes { get; set; }
        public virtual ICollection<CustomerHierarchyMapping> CustomerHierarchyMappings { get; set; }
        public virtual ICollection<DistributorHierarchyMapping> DistributorHierarchyMappings { get; set; }
        public virtual ICollection<CustomerAttribute> InverseParentCustomerAttribute { get; set; }
    }
}
