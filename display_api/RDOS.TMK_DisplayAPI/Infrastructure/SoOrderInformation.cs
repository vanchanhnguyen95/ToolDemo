using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SoOrderInformation
    {
        public Guid Id { get; set; }
        public string OrderRefNumber { get; set; }
        public string ReferenceRefNbr { get; set; }
        public string CancelNumber { get; set; }
        public string ReasonCode { get; set; }
        public DateTime CancelDate { get; set; }
        public bool NotInSubRoute { get; set; }
        public bool IsDirect { get; set; }
        public string OrderType { get; set; }
        public string PeriodId { get; set; }
        public string WareHouseId { get; set; }
        public string PrincipalId { get; set; }
        public string DistributorCode { get; set; }
        public string DistyBilltoId { get; set; }
        public DateTime DeliveredDate { get; set; }
        public bool IsReturn { get; set; }
        public string Status { get; set; }
        public bool IsPrintedDeliveryNote { get; set; }
        public int PrintedDeliveryNoteCount { get; set; }
        public DateTime LastedDeliveryNotePrintDate { get; set; }
        public string SalesOrgId { get; set; }
        public string TerritoryStrId { get; set; }
        public string TerritoryValueKey { get; set; }
        public string Dsaid { get; set; }
        public string NsdId { get; set; }
        public string BranchManagerId { get; set; }
        public string RegionManagerId { get; set; }
        public string SubRegionManagerId { get; set; }
        public string AreaManagerId { get; set; }
        public string SubAreaManagerId { get; set; }
        public string DsaManagerId { get; set; }
        public string RzSuppervisorId { get; set; }
        public string SicId { get; set; }
        public string SalesRepId { get; set; }
        public string RouteZoneId { get; set; }
        public string RouteZoneType { get; set; }
        public string RouteZonelocation { get; set; }
        public string CustomerId { get; set; }
        public string CustomerShiptoId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string MenuType { get; set; }
        public DateTime ExpectShippedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitId { get; set; }
        public string ExternalOrdNbr { get; set; }
        public string CreatedBy { get; set; }
        public string OwnerId { get; set; }
        public string Source { get; set; }
        public int OrigOrdSkus { get; set; }
        public int OrdSkus { get; set; }
        public int ShippedSkus { get; set; }
        public int OrigOrdQty { get; set; }
        public int OrdQty { get; set; }
        public int ShippedQty { get; set; }
        public int OrigPromotionQty { get; set; }
        public int PromotionQty { get; set; }
        public int ShippedPromotionQty { get; set; }
        public decimal OrigOrdAmt { get; set; }
        public decimal OrdAmt { get; set; }
        public decimal ShippedAmt { get; set; }
        public decimal PromotionAmt { get; set; }
        public decimal OrigOrdDiscAmt { get; set; }
        public decimal OrdDiscAmt { get; set; }
        public decimal ShippedDiscAmt { get; set; }
        public decimal OrigOrdlineDiscAmt { get; set; }
        public decimal OrdlineDiscAmt { get; set; }
        public decimal ShippedLineDiscAmt { get; set; }
        public decimal OrigOrdExtendAmt { get; set; }
        public decimal OrdExtendAmt { get; set; }
        public decimal ShippedExtendAmt { get; set; }
        public decimal TotalVat { get; set; }
        public int ConfirmCount { get; set; }
        public string PromotionRefNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy1 { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? ShiptoAttribute1 { get; set; }
        public Guid? ShiptoAttribute10 { get; set; }
        public Guid? ShiptoAttribute2 { get; set; }
        public Guid? ShiptoAttribute3 { get; set; }
        public Guid? ShiptoAttribute4 { get; set; }
        public Guid? ShiptoAttribute5 { get; set; }
        public Guid? ShiptoAttribute6 { get; set; }
        public Guid? ShiptoAttribute7 { get; set; }
        public Guid? ShiptoAttribute8 { get; set; }
        public Guid? ShiptoAttribute9 { get; set; }
    }
}
