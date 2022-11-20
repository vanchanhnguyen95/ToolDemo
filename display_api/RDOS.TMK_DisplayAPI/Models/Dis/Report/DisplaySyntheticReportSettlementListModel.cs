using Sys.Common.Models;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
    public class DisplaySyntheticReportSettlementListModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string RewwardPeriodCode { get; set; }
        public decimal? DistributorQuantity { get; set; }
        public decimal? DistributorQuantityConfirm { get; set; }
        public decimal? DistributorQuantityUnConfirm { get; set; }
    }

    public class ListDisplaySyntheticReportSettlementListModel
    {
        public List<DisplaySyntheticReportSettlementListModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
