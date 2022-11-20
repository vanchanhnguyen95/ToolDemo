using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisApproveRegistrationCustomerDetailService : IDisApproveRegistrationCustomerDetailService
    {
        private readonly ILogger<DisApproveRegistrationCustomerDetailService> _logger;
        private readonly IBaseRepository<DisApproveRegistrationCustomerDetail> _repository;

        public DisApproveRegistrationCustomerDetailService(ILogger<DisApproveRegistrationCustomerDetailService> logger, IBaseRepository<DisApproveRegistrationCustomerDetail> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public IQueryable<DisApproveRegistrationCustomerDetail> GetDisApproveRegistrationCustomerDetail()
        {
            return _repository.GetAllQueryable();
        }
        public void BulkInsert(IList<DisApproveRegistrationCustomerDetail> items)
        {
            try
            {
                _repository.InsertRange(items);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error while creating DisApproveRegistrationCustomerDetail");
                throw new ArgumentException(ex.Message);
            }
        }

        public Task<DisApproveRegistrationCustomerDetail> FindByDisplayCodeAsync(string displayCode)
        {
            return _repository.GetAllQueryable(x => x.DisplayCode == displayCode).FirstOrDefaultAsync();
        }

        public async Task<IList<string>> GetCustomerCodesAsync(IList<(string CustomerCode, string LevelCode)> items)
        {
            List<string> result = new();
            foreach (var (customerCode, levelCode) in items)
            {
                if (await _repository.GetAllQueryable(x => x.CustomerCode == customerCode && x.DisplayLevel == levelCode).AnyAsync())
                {
                    result.Add(customerCode);
                }
            }

            return result;
        }
    }
}