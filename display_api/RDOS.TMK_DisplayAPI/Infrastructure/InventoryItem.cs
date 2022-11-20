using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class InventoryItem
    {
        public Guid Id { get; set; }
        public string InventoryItemId { get; set; }
        public string Status { get; set; }
        public string ShortName { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public string Erpcode { get; set; }
        public string DistribiutorCode { get; set; }
        public bool IsStock { get; set; }
        public string ItemType { get; set; }
        public bool OrderItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool Lsnumber { get; set; }
        public bool Competitor { get; set; }
        public Guid Vat { get; set; }
        public Guid BaseUnit { get; set; }
        public Guid SalesUnit { get; set; }
        public Guid PurchaseUnit { get; set; }
        public Guid Attribute1 { get; set; }
        public Guid Attribute2 { get; set; }
        public Guid Attribute3 { get; set; }
        public Guid Attribute4 { get; set; }
        public Guid Attribute5 { get; set; }
        public Guid Attribute6 { get; set; }
        public Guid Attribute7 { get; set; }
        public Guid Attribute8 { get; set; }
        public Guid Attribute9 { get; set; }
        public Guid Attribute10 { get; set; }
        public string Note { get; set; }
        public int DelFlg { get; set; }
        public string Avatar { get; set; }
        public string GroupId { get; set; }
        public decimal Point { get; set; }
        public Guid Hierarchy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
