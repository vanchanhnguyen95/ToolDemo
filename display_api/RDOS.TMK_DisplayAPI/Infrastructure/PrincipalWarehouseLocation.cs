using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PrincipalWarehouseLocation
    {
        public Guid Id { get; set; }
        public string Decscription { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long Code { get; set; }
        public bool? IsDefault { get; set; }
    }
}
