using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TpDiscountStructureDetail
    {
        public Guid Id { get; set; }
        public string DiscountCode { get; set; }
        public string SicCode { get; set; }
        public int DiscountType { get; set; }
        public string NameDiscountLevel { get; set; }
        public decimal DiscountCheckValue { get; set; }
        public decimal DiscountAmount { get; set; }
        public int DiscountPercent { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int DeleteFlag { get; set; }
    }
}
