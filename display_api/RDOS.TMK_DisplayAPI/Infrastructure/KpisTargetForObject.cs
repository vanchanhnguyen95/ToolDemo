using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class KpisTargetForObject
    {
        public Guid Id { get; set; }
        public string KpisTargetCode { get; set; }
        public string FrequencyCode { get; set; }
        public string Object { get; set; }
        public string ObjectCode { get; set; }
        public string ObjectDescription { get; set; }
        public string KpisCode { get; set; }
        public string TargetDisplayType { get; set; }
        public decimal KpisTargetValue { get; set; }
        public decimal KpisTargetValueOriginal { get; set; }
        public bool IsRepeatTarget { get; set; }
        public string PicsalesTerritory { get; set; }
        public string Sic { get; set; }
        public string TerritoryStructureCode { get; set; }
        public string SalesOrgCode { get; set; }
    }
}
