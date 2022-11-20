using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public interface IDisplayListCustomerReportService
    {
        public IQueryable<DisplayListCustomerListModel> GetDisplayDetailReport(DisplayReportEcoParameters request);
        public IQueryable<ListCustomerConfirmModel> GetListCustomerConfirmReport(DisplayReportEcoParameters request);
    }
}
