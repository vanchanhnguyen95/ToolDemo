using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class BasePriceGrossMargin
    {
        public Guid Id { get; set; }
        public DateTime EffectiveTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public decimal GrossMargin { get; set; }
        public int LineNumber { get; set; }
        public Guid BasePriceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual BasePrice BasePrice { get; set; }
    }
}
