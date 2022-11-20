using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using RDOS.TMK_DisplayAPI.Services.Base;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.TempDis
{
    public interface ITempDisApproveRegistrationCustomerService
    {
        IQueryable<TempDisApproveRegistrationCustomer> GetDisApproveRegistrationCustomer();
    }

    public class TempDisApproveRegistrationCustomerService : ITempDisApproveRegistrationCustomerService
    {
        private readonly IBaseRepository<TempDisApproveRegistrationCustomer> _repository;

        public TempDisApproveRegistrationCustomerService(IBaseRepository<TempDisApproveRegistrationCustomer> repository)
        {
            _repository = repository;
        }

        public IQueryable<TempDisApproveRegistrationCustomer> GetDisApproveRegistrationCustomer()
        {
            return _repository.GetAllQueryable();
        }
    }
}