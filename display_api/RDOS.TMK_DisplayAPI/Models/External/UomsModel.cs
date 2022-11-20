using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.External
{
    public class UomsModel
    {
        public Guid Id { get; set; }
        [MaxLength(10)]

        public string UomId { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public DateTime EffectiveDateFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
        public bool IsUse { get; set; }
    }

    public class InventoryItemUomModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid UomBaseId { get; set; }
        public string UomBaseCode { get; set; }
        public string UomBaseDescription { get; set; }
        public Guid UomSalesId { get; set; }
        public string UomSalesCode { get; set; }
        public string UomSalesDescription { get; set; }
        public Guid UomPurchaseId { get; set; }
        public string UomPurchaseCode { get; set; }
        public string UomPurchaseDescription { get; set; }
    }
}
