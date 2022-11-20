using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public interface IDisplayPeriodTrackingReportService
    {
        Task<IQueryable<DisplayPeriodTrackingReportListModel>> GetListDisplayPeriodTrackingReportAsync(string DisplayCode);
    }
}
