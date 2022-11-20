using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class RzRouteZoneShipto
    {
        public Guid Id { get; set; }
        public string RouteZoneCode { get; set; }
        public Guid ShiptoId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string VisitFrequencyCode { get; set; }
        public string VisitWeek { get; set; }
        public string WeekToVisit { get; set; }
        public string ShiptoWareHouse { get; set; }
    }
}
