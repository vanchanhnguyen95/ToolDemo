using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Customer;
using System.Collections.Generic;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisCustomerShiptoService
    {
        public List<DisCustomerShiptoModel> GetListDisCustomerShiptoByDisplayCode(string displayCode);
        public BaseResultModel SaveDisCustomerShipto(DisCustomerShiptoModel input, string userLogin);
        public BaseResultModel DeleteDisCustomerShiptos(DeleteDisCustomerShiptosModel input);
        public BaseResultModel DeleteAllDisCustomerShiptoByDisplayCode(string displayCode);

        #region DIS.02.06
        public IQueryable<DisScopeCustomerShiptoModel> GetListCustomerShiptoByScope(CustomerShiptoByScopeSearchModel input);
        #endregion
    }
}
