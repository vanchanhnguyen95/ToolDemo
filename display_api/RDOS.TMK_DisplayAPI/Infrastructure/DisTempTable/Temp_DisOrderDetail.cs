using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempDisOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string OrdNbr { get; set; }

        [MaxLength(10)]
        public string InventoryId { get; set; }

        [MaxLength(200)]
        public string InventoryName { get; set; }

        [MaxLength(10)]
        public string Uom { get; set; }

        [MaxLength(200)]
        public string UomName { get; set; }

        public bool IsFree { get; set; }

        public decimal? ShippedQty { get; set; }

        public decimal UnitPrice { get; set; }

        [MaxLength(10)]
        public string TMKType { get; set; }

        [MaxLength(10)]
        public string DiscountId { get; set; }

        [MaxLength(200)]
        public string DiscountName { get; set; }

        [MaxLength(10)]
        public string DisplayLevel { get; set; }

        [MaxLength(200)]
        public string DisplayLevelName { get; set; }

        [MaxLength(10)]
        public string DiscountType { get; set; }

        [MaxLength(10)]
        public string DiscountSchemeId { get; set; }

        public decimal? ShippedLineDiscAmt { get; set; }

        [MaxLength(10)]
        public string PromotionLevel { get; set; }

        [MaxLength(10)]
        public string RewardPeriodCode { get; set; }

        [MaxLength(200)]
        public string RewardPeriodName { get; set; }

        public decimal? ShippedLineExtendAmt { get; set; }

    }
}
