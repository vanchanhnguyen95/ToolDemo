using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerHierarchy1
    {
        public CustomerHierarchy1()
        {
            CustomerDmsAttributes = new HashSet<CustomerDmsAttribute>();
            CustomerHierarchyMappings = new HashSet<CustomerHierarchyMapping>();
        }

        public Guid Id { get; set; }
        public int NodeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<CustomerDmsAttribute> CustomerDmsAttributes { get; set; }
        public virtual ICollection<CustomerHierarchyMapping> CustomerHierarchyMappings { get; set; }
    }
}
