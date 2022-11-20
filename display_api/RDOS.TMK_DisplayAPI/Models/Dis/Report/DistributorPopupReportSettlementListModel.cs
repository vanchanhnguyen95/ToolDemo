using Sys.Common.Models;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
    public class DistributorPopupReportSettlementListModel
    {
        public string SettlementCode { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorName { get; set; }
        public bool Confirm { get; set; }
        public bool UnConfirm { get; set; }
    }

    public class ListDistributorPopupReportSettlementListModel
    {
        public List<DistributorPopupReportSettlementListModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
