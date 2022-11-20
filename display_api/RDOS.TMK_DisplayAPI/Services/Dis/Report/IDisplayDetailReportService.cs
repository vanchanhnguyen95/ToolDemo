using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public interface IDisplayDetailReportService
    {
        public IQueryable<DisplayDetailReportListModel> GetDisplayDetailReport(DisplayReportEcoParameters request);
    }
}
