using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempCheckInventoryVisit
    {
        public Guid Id { get; set; }
        public string VisitCode { get; set; }
        public string CustomerCode { get; set; }
        public string InventoryItem { get; set; }
        public int InventoryNumber { get; set; }
        public string UoM { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime InventoryDate { get; set; }
    }
}
