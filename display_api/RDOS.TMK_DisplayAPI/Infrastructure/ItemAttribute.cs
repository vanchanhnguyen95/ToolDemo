using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ItemAttribute
    {
        public ItemAttribute()
        {
            PrimarySicIncludeDetails = new HashSet<PrimarySicIncludeDetail>();
        }

        public Guid Id { get; set; }
        public string ItemAttributeMaster { get; set; }
        public string ItemAttributeCode { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntilDate { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<PrimarySicIncludeDetail> PrimarySicIncludeDetails { get; set; }
    }
}
