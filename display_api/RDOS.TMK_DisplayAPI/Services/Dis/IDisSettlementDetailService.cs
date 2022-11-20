using RDOS.TMK_DisplayAPI.Models.Dis;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisSettlementDetailService
    {
        IQueryable<DisSettlementDetailModel> GetListSettlementDetailByCodeAsync(string code);
    }
}
