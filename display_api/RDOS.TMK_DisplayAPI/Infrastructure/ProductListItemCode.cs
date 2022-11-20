using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ProductListItemCode
    {
        public Guid Id { get; set; }
        public string ProductListCode { get; set; }
        public bool IsUsed { get; set; }
        public string ItemCode { get; set; }
        public string FromSalesPeriod { get; set; }
        public string ToSalesPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
