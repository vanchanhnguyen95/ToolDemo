using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Feature
    {
        public Feature()
        {
            Menus = new HashSet<Menu>();
            PaginationConfigs = new HashSet<PaginationConfig>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string ActionJson { get; set; }
        public bool IsPrincipal { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DynamicFieldValue { get; set; }
        public bool IsRdos { get; set; }
        public string FeatureType { get; set; }
        public string ServiceUrl { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<PaginationConfig> PaginationConfigs { get; set; }
    }
}
