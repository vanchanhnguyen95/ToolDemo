using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerDmsAttribute
    {
        public Guid Id { get; set; }
        public Guid CustomerShiptoId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid CustomerAttributeId { get; set; }

        public virtual CustomerAttribute CustomerAttribute { get; set; }
        public virtual CustomerShipto CustomerShipto { get; set; }
    }
}
