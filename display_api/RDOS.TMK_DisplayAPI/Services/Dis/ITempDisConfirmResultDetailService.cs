using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using RDOS.TMK_DisplayAPI.Models.Dis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RDOS.TMK_DisplayAPI.Models.Dis.ConfirmResultDetailListModel;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface ITempDisConfirmResultDetailService
    {
        Task<IQueryable<TempDisConfirmResultDetail>> GetListTempDisConfirmResultDetailAsync();
        public Task<List<DisConfirmResultDetailValueModel>> GetListTempDisConfirmResult(string DisplayCode, string PeriodCode);
    }
}
