using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Menu
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public Guid? FeatureId { get; set; }
        public string Description { get; set; }
        public bool Expanded { get; set; }
        public bool IsPrincipal { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDefault { get; set; }
        public Guid ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DynamicFieldValue { get; set; }
        public bool IsRdos { get; set; }
        public bool Is1Sprincipal { get; set; }

        public virtual Feature Feature { get; set; }
    }
}
