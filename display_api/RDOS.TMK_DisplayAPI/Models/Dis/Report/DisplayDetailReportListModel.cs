using Sys.Common.Models;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
    public class DisplayDetailReportListModel
    {
        public string DisplayCode { get; set; }
        public string DisplayCodeLevel { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public decimal BudgetSalePoint { get; set; }
    }
    public class ListDisplayDetailReportListModel
    {
        public List<DisplayDetailReportListModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
