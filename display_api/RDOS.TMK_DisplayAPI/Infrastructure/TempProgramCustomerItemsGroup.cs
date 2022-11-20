using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempProgramCustomerItemsGroup
    {
        public Guid Id { get; set; }
        public string ProgramCustomerItemsGroupCode { get; set; }
        public string ProgramCustomersDetailCode { get; set; }
        public string ProgramDetailsItemsGroupKey { get; set; }
        public string PromotionRefNumber { get; set; }
        public int Quantities { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ItemGroupCode { get; set; }
        public Guid? ItemGroupId { get; set; }
        public string Uomcode { get; set; }
        public bool IsRequired { get; set; }
        public int FixedQuantities { get; set; }
        public bool IsDeleted { get; set; }
    }
}
