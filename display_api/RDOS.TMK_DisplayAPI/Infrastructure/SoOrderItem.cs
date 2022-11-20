using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SoOrderItem
    {
        public Guid Id { get; set; }
        public string OrderRefNumber { get; set; }
        public string InventoryId { get; set; }
        public Guid KitId { get; set; }
        public bool IsKit { get; set; }
        public bool IsPromotion { get; set; }
        public string LocationId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Uom { get; set; }
        public int UnitRate { get; set; }
        public int OriginalOrderQuantities { get; set; }
        public int OriginalOrderBaseQuantities { get; set; }
        public int OrderQuantities { get; set; }
        public int OrderBaseQuantities { get; set; }
        public int ShippedQuantities { get; set; }
        public int ShippedBaseQuantities { get; set; }
        public int FailedQuantities { get; set; }
        public int ShippingQuantities { get; set; }
        public int RemainQuantities { get; set; }
        public decimal VatValue { get; set; }
        public string Vatcode { get; set; }
        public bool IsFree { get; set; }
        public string PromotionType { get; set; }
        public string PromotionDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public string DiscountId { get; set; }
        public string DiscountType { get; set; }
        public string DiscountSchemeId { get; set; }
        public string DiscountDealId { get; set; }
        public decimal OrigOrdLineAmt { get; set; }
        public decimal OrdLineAmt { get; set; }
        public decimal ShippedLineAmt { get; set; }
        public decimal OrigOrdLineDiscAmt { get; set; }
        public decimal OrdLineDiscAmt { get; set; }
        public decimal ShippedLineDiscAmt { get; set; }
        public decimal OrigOrdLineExtendAmt { get; set; }
        public decimal OrdLineExtendAmt { get; set; }
        public decimal ShippedLineExtendAmt { get; set; }
        public Guid InventoryAttibute1 { get; set; }
        public Guid InventoryAttibute2 { get; set; }
        public Guid InventoryAttibute3 { get; set; }
        public Guid InventoryAttibute4 { get; set; }
        public Guid InventoryAttibute5 { get; set; }
        public Guid InventoryAttibute6 { get; set; }
        public Guid InventoryAttibute7 { get; set; }
        public Guid InventoryAttibute8 { get; set; }
        public Guid InventoryAttibute9 { get; set; }
        public Guid InventoryAttibute10 { get; set; }
        public string ItemGroupCode { get; set; }
        public decimal ItemPoint { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? BaseUnit { get; set; }
        public decimal DisCountAmount { get; set; }
        public double DiscountPercented { get; set; }
        public Guid? Hierarchy { get; set; }
        public string KitKey { get; set; }
        public string ProgramCustomersDetailCode { get; set; }
        public string PromotionCode { get; set; }
        public Guid? PurchaseUnit { get; set; }
        public int ReturnBaseQuantities { get; set; }
        public int ReturnQuantities { get; set; }
        public Guid? SalesUnit { get; set; }
        public Guid? VatId { get; set; }
        public decimal Vat { get; set; }
    }
}
