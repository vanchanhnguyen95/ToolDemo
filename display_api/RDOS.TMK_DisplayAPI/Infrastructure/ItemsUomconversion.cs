using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ItemsUomconversion
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid FromUnit { get; set; }
        public Guid ToUnit { get; set; }
        public int Dm { get; set; }
        public decimal ConversionFactor { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
