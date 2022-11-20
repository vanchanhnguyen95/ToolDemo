using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TpObjectSalesAttributeDiscount
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string SalesAttributeCode { get; set; }
        public string SalesAttributeValueCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int DeleteFlag { get; set; }
    }
}
