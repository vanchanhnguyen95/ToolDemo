using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TpPromotionDefinitionProductForSale
    {
        public Guid Id { get; set; }
        public string PromotionCode { get; set; }
        public string LevelCode { get; set; }
        public string ProductCode { get; set; }
        public string Packing { get; set; }
        public int SellNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int DeleteFlag { get; set; }
    }
}
