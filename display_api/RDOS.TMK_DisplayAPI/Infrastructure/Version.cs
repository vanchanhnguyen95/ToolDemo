using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Version
    {
        public Version()
        {
            ApplicationServices = new HashSet<ApplicationService>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ApplicationService> ApplicationServices { get; set; }
    }
}
