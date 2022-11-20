using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class AutoKpisTargetBusinessModelValue
    {
        public Guid Id { get; set; }
        public string AutoKpisTargetCode { get; set; }
        public string TerritoryStructureCode { get; set; }
        public string Source { get; set; }
        public string TerritoryValueBySource { get; set; }
        public string BusinessModel { get; set; }
        public decimal? Jan { get; set; }
        public decimal? Feb { get; set; }
        public decimal? Mar { get; set; }
        public decimal? Apr { get; set; }
        public decimal? May { get; set; }
        public decimal? Jun { get; set; }
        public decimal? Jul { get; set; }
        public decimal? Aug { get; set; }
        public decimal? Sep { get; set; }
        public decimal? Oct { get; set; }
        public decimal? Nov { get; set; }
        public decimal? Dec { get; set; }
        public decimal? AchievableValueCurrentYear { get; set; }
        public decimal? TargetValue { get; set; }
        public decimal? ValueByNorm { get; set; }
        public decimal? BaseVpo { get; set; }
        public decimal? TargetVpo { get; set; }
        public decimal? NewTargetVpo { get; set; }
        public decimal? BaseAso { get; set; }
        public decimal? TargetAso { get; set; }
        public decimal? NewTargetAso { get; set; }
        public decimal? BasePc { get; set; }
        public decimal? TargetPc { get; set; }
        public decimal? NewTargetPc { get; set; }
        public decimal? BaseLppc { get; set; }
        public decimal? TargetLppc { get; set; }
        public decimal? NewTargetLppc { get; set; }
        public decimal? BaseLppcvalue { get; set; }
        public decimal? TargetLppcvalue { get; set; }
        public decimal? NewTargetLppcvalue { get; set; }
        public int? Year { get; set; }
        public int IsCommited { get; set; }
        public int? Level { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime? CommitedTime { get; set; }
        public string SalesPeriod { get; set; }
        public decimal? CommitedTarget { get; set; }
    }
}
