using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class RzRouteZoneInfomation
    {
        public Guid Id { get; set; }
        public string RouteZoneCode { get; set; }
        public string LocationCode { get; set; }
        public string Description { get; set; }
        public string RouteZoneTypeCode { get; set; }
        public string DSACode { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string Status { get; set; }
        public string SICCode { get; set; }
        public string TempSICCode { get; set; }
        public string WareHouse { get; set; }
        public string CurrentSupervisorCode { get; set; }
        public DateTime CurrentSupervisorEffectiveDate { get; set; }
        public DateTime? CurrentSupervisorValidUntil { get; set; }
        public string PreviousSupervisorCode { get; set; }
        public DateTime? PreviousSupervisorEffectiveDate { get; set; }
        public DateTime? PreviousSupervisorValidUntil { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
