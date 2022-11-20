using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class GeographicalMapping
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Guid ParentMasterId { get; set; }
        public Guid ParentId { get; set; }
        public Guid ValueMasterId { get; set; }
        public Guid ValueId { get; set; }
        public DateTime EffectiveDateFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public int? DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
