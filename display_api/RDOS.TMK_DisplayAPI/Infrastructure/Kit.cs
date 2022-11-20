using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Kit
    {
        public Guid Id { get; set; }
        public string ItemKitId { get; set; }
        public string Status { get; set; }
        public bool IsNonStock { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public Guid Vat { get; set; }
        public decimal Point { get; set; }
        public bool OrderItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool Competitor { get; set; }
        public bool Lsnumber { get; set; }
        public Guid BaseUnit { get; set; }
        public Guid SalesUnit { get; set; }
        public Guid PurchaseUnit { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
