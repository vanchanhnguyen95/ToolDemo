using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempProgram
    {
        public Guid Id { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramsType { get; set; }
        public string Description { get; set; }
        public string ItemScope { get; set; }
        public string BuyType { get; set; }
        public string GivingType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
