using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class InvSellInLotByDate
    {
        public Guid Id { get; set; }
        public string ItemKeyLot { get; set; }
        public DateTime TransactionDate { get; set; }
        public int InputQuantity { get; set; }
        public string DistributorCode { get; set; }
        public string WareHouseCode { get; set; }
        public string LocationCode { get; set; }
        public string ItemCode { get; set; }
        public string Lot { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
