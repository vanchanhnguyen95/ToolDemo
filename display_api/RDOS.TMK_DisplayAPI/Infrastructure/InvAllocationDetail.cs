using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class InvAllocationDetail
    {
        public Guid Id { get; set; }
        public string ItemKey { get; set; }
        public Guid ItemId { get; set; }
        public string ItemCode { get; set; }
        public string BaseUom { get; set; }
        public string ItemDescription { get; set; }
        public string WareHouseCode { get; set; }
        public string LocationCode { get; set; }
        public string DistributorCode { get; set; }
        public int OnHand { get; set; }
        public int OnSoShipping { get; set; }
        public int OnSoBooked { get; set; }
        public int Available { get; set; }
        public Guid? Attribute1 { get; set; }
        public Guid? Attribute2 { get; set; }
        public Guid? Attribute3 { get; set; }
        public Guid? Attribute4 { get; set; }
        public Guid? Attribute5 { get; set; }
        public Guid? Attribute6 { get; set; }
        public Guid? Attribute7 { get; set; }
        public Guid? Attribute8 { get; set; }
        public Guid? Attribute9 { get; set; }
        public Guid? Attribute10 { get; set; }
        public string ItemGroupCode { get; set; }
        public string Dsacode { get; set; }
        public string ShortName { get; set; }
        public Guid? Hierarchy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
