using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerShiptoContact
    {
        public Guid Id { get; set; }
        public Guid CustomerShiptoId { get; set; }
        public Guid CustomerContactId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerContact CustomerContact { get; set; }
        public virtual CustomerShipto CustomerShipto { get; set; }
    }
}
