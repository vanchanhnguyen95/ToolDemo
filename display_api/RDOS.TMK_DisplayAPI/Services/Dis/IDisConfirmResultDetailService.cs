using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisConfirmResultDetailService
    {
        Task<bool> CreateAsync(DisConfirmResultDetailDisplayRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsByCodeAsync(string code, Guid? id = default);
        Task<DisConfirmResultDetail> FindByIdAsync(Guid id);
        Task<DisConfirmResultDetailDisplayModel> UpdateAsync(DisConfirmResultDetail request);
        IQueryable<DisConfirmResultDetailDisplayModel> GetListConfirmResultDetailAsync();
        IQueryable<DisConfirmResultDetailDisplayModel> GetListConfirmResultDetailByResultCodeAsync(string code);
        public IQueryable<DisConfirmResultDetailDisplayModel> GetListConfirmResultDetail(TempDisOrderHeaderParameters request);
		  IQueryable<DisConfirmResultDetailGrouped> GetConfirmResultDetailGrouped(DisDisplayModel display, string confirmResultCode);
        Task<List<ConfirmResultDetailJoinReport>> GetConfirmResultDetailsReportAsync(string confirmResultCode, string levelCode, bool passed, bool isIndependent);
    }
}
