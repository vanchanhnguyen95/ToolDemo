using Sys.Common.Models;
using System;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
    public class DisplayProgressReportListModel
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public DateTime? RegistrationStartDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public DateTime? ImplementationStartDate { get; set; }

        public DateTime? ImplementationEndDate { get; set; }

        public DateTime? ProgramCloseDate { get; set; }
        public string Scope { get; set; }
        public string ScopeValue { get; set; }
        public string ApplicableObject { get; set; }
        public string ApplicableObjectValue { get; set; }
        public string DisplayLevel { get; set; }
        public decimal BudgetQuantity { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public decimal SalesPoint { get; set; }
        public string Status { get; set; }
    }
    public class ListDisplayProgressReportListModel
    {
        public List<DisplayProgressReportListModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
