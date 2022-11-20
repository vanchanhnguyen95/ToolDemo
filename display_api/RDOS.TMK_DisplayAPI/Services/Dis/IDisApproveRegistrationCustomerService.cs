using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using System;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisApproveRegistrationCustomerService
    {
        Task<DisApproveRegistrationCustomer> CreateAsync(DisApproveRegistrationCustomer entity);
        Task<DisApproveRegistrationCustomer> FindByDisplayCodeAsync(string displayCode);
        Task<DisApproveRegistrationCustomer> FindByIdAsync(Guid id);
        Task<DisApproveRegistrationCustomer> UpdateAsync(DisApproveRegistrationCustomer entity);
    }
}
