using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            JobTitleRoles = new HashSet<JobTitleRole>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string DefaultUserRole { get; set; }
        public bool? AllowLoginWeb { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<JobTitleRole> JobTitleRoles { get; set; }
    }
}
