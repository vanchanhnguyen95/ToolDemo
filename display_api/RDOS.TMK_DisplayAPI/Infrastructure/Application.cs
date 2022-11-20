using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Application
    {
        public Application()
        {
            ApplicationServices = new HashSet<ApplicationService>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SecretId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string AndroidPackageName { get; set; }
        public string IosbundleId { get; set; }
        public string Code { get; set; }
        public bool AllowMultiplePrincipal { get; set; }
        public int Sort { get; set; }

        public virtual ICollection<ApplicationService> ApplicationServices { get; set; }
    }
}
