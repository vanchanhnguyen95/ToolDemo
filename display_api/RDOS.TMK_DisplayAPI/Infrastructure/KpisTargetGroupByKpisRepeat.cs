using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class KpisTargetGroupByKpisRepeat
    {
        public Guid Id { get; set; }
        public string KpisTargetCode { get; set; }
        public string FrequencyCode { get; set; }
        public string ObjectType { get; set; }
        public string ObjectCode { get; set; }
        public string KpisCode { get; set; }
        public string KpisDisplayType { get; set; }
        public string KpisType { get; set; }
        public decimal KpisRepeatTargetValue { get; set; }
        public string PicsalesTerritory { get; set; }
        public string Sic { get; set; }
        public string TerritoryStructureCode { get; set; }
        public string SalesOrgCode { get; set; }
    }
}
