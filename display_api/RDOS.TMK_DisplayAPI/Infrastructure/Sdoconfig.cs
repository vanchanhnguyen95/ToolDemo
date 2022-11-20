using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Sdoconfig
    {
        public Guid Id { get; set; }
        public string Sdocode { get; set; }
        public bool Status { get; set; }
        public DateTime? InactiveDate { get; set; }
        public string Sdodescription { get; set; }
        public string FromSalesPeriod { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string ProductGainSdocoverageType { get; set; }
        public int? ProductGainSdocoverageValue { get; set; }
        public bool IsMainProductGainSdocoverage { get; set; }
        public string ProductGainSdogrowthType { get; set; }
        public int? ProductGainSdogrowthValue { get; set; }
        public bool IsMainProductGainSdogrowth { get; set; }
        public int? MinimumNumberSdo { get; set; }
        public int? NumberSdotoDo { get; set; }
        public bool IsMaintainSdo { get; set; }
        public string SourceType { get; set; }
        public int? InventoryCheckValue { get; set; }
        public int? EvaluationPhotoValue { get; set; }
        public string EvaluationProductLevel { get; set; }
        public string ProductHierachyLevel { get; set; }
        public bool SourceTypeInvoice { get; set; }
        public bool SourceTypeInventoryCheck { get; set; }
        public bool SourceTypeEvaluationPhoto { get; set; }
        public bool? IsDelete { get; set; }
    }
}
