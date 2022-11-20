﻿using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerSettingHierarchy
    {
        public CustomerSettingHierarchy()
        {
            CustomerHierarchyMappings = new HashSet<CustomerHierarchyMapping>();
            DistributorHierarchyMappings = new HashSet<DistributorHierarchyMapping>();
        }

        public Guid Id { get; set; }
        public int Type { get; set; }
        public int? HierarchyLevel { get; set; }
        public Guid CustomerSettingId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerSetting CustomerSetting { get; set; }
        public virtual ICollection<CustomerHierarchyMapping> CustomerHierarchyMappings { get; set; }
        public virtual ICollection<DistributorHierarchyMapping> DistributorHierarchyMappings { get; set; }
    }
}
