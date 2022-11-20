using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class AutoKpisTargetContributionBySic
    {
        public Guid Id { get; set; }
        public string AutoKpisTargetCode { get; set; }
        public string Sic { get; set; }
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
        public decimal? TargetValueNextYear { get; set; }
        public decimal? ValueByNorm { get; set; }
        public string Source { get; set; }
        public string TerritoryStructureCode { get; set; }
        public string TerritoryValueBySource { get; set; }
        public int Year { get; set; }
        public string SicDescription { get; set; }
    }
}
