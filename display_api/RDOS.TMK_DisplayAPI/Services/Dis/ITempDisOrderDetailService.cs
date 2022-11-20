using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface ITempDisOrderDetailService
    {
        IQueryable<DisSettlementDetailModel> GetTempSettlementDetailAsync(TempDisOrderHeaderParameters parameters);

        Task<List<TempDisplaySaleOrOutputResponseModel>> GetDataSaleOrOutputByDisplayByPeriodCodeAsync(TempDisplaySaleOrOutputRequestModel parameters);
    }
}
