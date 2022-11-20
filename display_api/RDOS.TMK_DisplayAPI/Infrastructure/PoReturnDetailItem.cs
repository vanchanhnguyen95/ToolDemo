using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PoReturnDetailItem
    {
        public Guid Id { get; set; }
        public int ReturnQuantity { get; set; }
        public int ReturnBaseQuantity { get; set; }
        public int ReceiptQuantity { get; set; }
        public int ReceiptBaseQuantity { get; set; }
        public string Location { get; set; }
        public string WareHouse { get; set; }
        public string PurchaseReturnNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Uom { get; set; }
        public int OrderQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Vat { get; set; }
        public string VatCode { get; set; }
        public bool IsFree { get; set; }
    }
}
