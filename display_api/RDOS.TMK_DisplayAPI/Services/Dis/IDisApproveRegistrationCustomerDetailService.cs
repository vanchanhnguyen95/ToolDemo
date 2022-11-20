using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisApproveRegistrationCustomerDetailService
    {
        Task<DisApproveRegistrationCustomerDetail> FindByDisplayCodeAsync(string displayCode);
        IQueryable<DisApproveRegistrationCustomerDetail> GetDisApproveRegistrationCustomerDetail();
        void BulkInsert(IList<DisApproveRegistrationCustomerDetail> items);
        Task<IList<string>> GetCustomerCodesAsync(IList<(string CustomerCode, string LevelCode)> items);
    }
}
