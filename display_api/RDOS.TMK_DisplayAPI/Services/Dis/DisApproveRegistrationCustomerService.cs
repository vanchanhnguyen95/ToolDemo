using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using System;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisApproveRegistrationCustomerService : IDisApproveRegistrationCustomerService
    {
        private readonly ILogger<DisApproveRegistrationCustomerService> _logger;
        private readonly IBaseRepository<DisApproveRegistrationCustomer> _repository;

        public DisApproveRegistrationCustomerService(ILogger<DisApproveRegistrationCustomerService> logger, IBaseRepository<DisApproveRegistrationCustomer> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task<DisApproveRegistrationCustomer> CreateAsync(DisApproveRegistrationCustomer entity)
        {
            try
            {
                return Task.FromResult(_repository.Insert(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating DisApproveRegistrationCustomer");
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<DisApproveRegistrationCustomer> FindByDisplayCodeAsync(string displayCode)
        {
            return await _repository.GetAllQueryable().FirstOrDefaultAsync(x => x.DisplayCode == displayCode);
        }

        public Task<DisApproveRegistrationCustomer> FindByIdAsync(Guid id)
        {
            return _repository.GetAllQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<DisApproveRegistrationCustomer> UpdateAsync(DisApproveRegistrationCustomer entity)
        {
            try
            {
                return Task.FromResult(_repository.Update(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating DisApproveRegistrationCustomer");
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
