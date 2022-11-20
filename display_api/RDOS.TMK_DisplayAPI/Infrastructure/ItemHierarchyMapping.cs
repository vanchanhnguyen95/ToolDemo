using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ItemHierarchyMapping
    {
        public Guid Id { get; set; }
        public int NodeId { get; set; }
        public bool IsSaleHierarchy { get; set; }
        public string HierarchyAttribute1 { get; set; }
        public string HierarchyAttribute2 { get; set; }
        public string HierarchyAttribute3 { get; set; }
        public string HierarchyAttribute4 { get; set; }
        public string HierarchyAttribute5 { get; set; }
        public string HierarchyAttribute6 { get; set; }
        public string HierarchyAttribute7 { get; set; }
        public string HierarchyAttribute8 { get; set; }
        public string HierarchyAttribute9 { get; set; }
        public string HierarchyAttribute10 { get; set; }
        public Guid ValuesAttribute1 { get; set; }
        public Guid ValuesAttribute2 { get; set; }
        public Guid ValuesAttribute3 { get; set; }
        public Guid ValuesAttribute4 { get; set; }
        public Guid ValuesAttribute5 { get; set; }
        public Guid ValuesAttribute6 { get; set; }
        public Guid ValuesAttribute7 { get; set; }
        public Guid ValuesAttribute8 { get; set; }
        public Guid ValuesAttribute9 { get; set; }
        public Guid ValuesAttribute10 { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
