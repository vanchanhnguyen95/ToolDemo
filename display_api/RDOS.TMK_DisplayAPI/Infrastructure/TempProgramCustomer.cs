using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempProgramCustomer
    {
        public Guid Id { get; set; }
        public string ProgramCustomersKey { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramsType { get; set; }
        public string ProgramsDescription { get; set; }
        public string ProgramsItemScope { get; set; }
        public string ProgramsBuyType { get; set; }
        public string ProgramsGivingType { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
