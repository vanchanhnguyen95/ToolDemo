using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public interface IDisplayProgressReportService
    {
        public IQueryable<DisplayProgressReportListModel> GetDisplayDetailForDisplayProgressReport(DisplayReportEcoParameters request);
    }
}
