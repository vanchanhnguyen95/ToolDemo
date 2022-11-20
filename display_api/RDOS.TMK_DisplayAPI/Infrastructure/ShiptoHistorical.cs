using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ShiptoHistorical
    {
        public Guid Id { get; set; }
        public Guid ShiptoId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ValidDate { get; set; }
        public string ShiptoCodeonErp { get; set; }
    }
}
