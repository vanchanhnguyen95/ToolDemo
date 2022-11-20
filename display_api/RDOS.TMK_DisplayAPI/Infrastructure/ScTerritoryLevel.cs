using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ScTerritoryLevel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Source { get; set; }
        public bool Used { get; set; }
        public int Level { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
