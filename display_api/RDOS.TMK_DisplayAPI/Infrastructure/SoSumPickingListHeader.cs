using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SoSumPickingListHeader
    {
        public Guid Id { get; set; }
        public string SumPickingRefNumber { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorShiptoId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Status { get; set; }
        public string Vehicle { get; set; }
        public string DriverCode { get; set; }
        public string WareHouseId { get; set; }
        public string NumberPlates { get; set; }
        public string VehicleLoad { get; set; }
        public string TotalWeight { get; set; }
        public bool IsPrinted { get; set; }
        public int PrintedCount { get; set; }
        public DateTime? LastedPrintDate { get; set; }
        public int TotalOrderQuantities { get; set; }
        public int TotalOriginOrderQuantities { get; set; }
        public int TotalShippedQuantities { get; set; }
        public int TotalFailedQuantities { get; set; }
        public int TotalShippingQuantities { get; set; }
        public int TotalRemainQuantities { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
