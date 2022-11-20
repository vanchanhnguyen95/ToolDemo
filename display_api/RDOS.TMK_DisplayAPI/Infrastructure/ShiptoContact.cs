using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ShiptoContact
    {
        public Guid Id { get; set; }
        public Guid ShiptoId { get; set; }
        public Guid ContactId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsMain { get; set; }

        public virtual DistributorContact Contact { get; set; }
        public virtual DistributorShipto Shipto { get; set; }
    }
}
