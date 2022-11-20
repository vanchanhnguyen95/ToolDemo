using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerContactEmail1
    {
        public Guid Id { get; set; }
        public string EmailType { get; set; }
        public string Email { get; set; }
        public Guid CustomerContactId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerContact1 CustomerContact { get; set; }
    }
}
