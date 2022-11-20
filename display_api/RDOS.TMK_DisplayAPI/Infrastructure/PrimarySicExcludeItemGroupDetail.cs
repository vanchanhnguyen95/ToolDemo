using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PrimarySicExcludeItemGroupDetail
    {
        public Guid Id { get; set; }
        public Guid PrimarySicId { get; set; }
        public Guid ItemGroupId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public int? DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual PrimarySic PrimarySic { get; set; }
    }
}
