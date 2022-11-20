using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Service
    {
        public Service()
        {
            ApplicationServices = new HashSet<ApplicationService>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int Apikind { get; set; }
        public int InternetType { get; set; }
        public string Versions { get; set; }
        public string Ecrurl { get; set; }
        public string Ecrversion { get; set; }

        public virtual ICollection<ApplicationService> ApplicationServices { get; set; }
    }
}
