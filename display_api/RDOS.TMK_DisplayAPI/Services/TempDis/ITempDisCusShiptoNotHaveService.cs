using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.TempDis
{
    public interface ITempDisCusShiptoNotHaveService
    {
        public IQueryable<TempDisCustomerShiptoNotHaveModel> GetListTempDisCusShiptoNotHave(TempDisCusShiptoNotHaveSearchModel search);
    }
}
