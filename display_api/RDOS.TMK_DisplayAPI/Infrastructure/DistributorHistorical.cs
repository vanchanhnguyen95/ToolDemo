using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DistributorHistorical
    {
        public Guid Id { get; set; }
        public Guid DistributorId { get; set; }
        public string BusinessPartnerType { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string BilltoCodeonErp { get; set; }
        public Guid? DistributorShiptoId { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual DistributorShipto DistributorShipto { get; set; }
    }
}
