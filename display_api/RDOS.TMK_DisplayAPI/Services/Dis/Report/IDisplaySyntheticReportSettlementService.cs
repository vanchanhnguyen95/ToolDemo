using System.Linq;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public interface IDisplaySyntheticReportSettlementService
    {
        public IQueryable<DisplaySyntheticReportSettlementListModel> GetDisplayDetailReport(DisplayReportEcoParameters request);
        public IQueryable<DistributorPopupReportSettlementListModel> GetListDistributorPopupReportSettlement(string settlementCode);

    }
}
