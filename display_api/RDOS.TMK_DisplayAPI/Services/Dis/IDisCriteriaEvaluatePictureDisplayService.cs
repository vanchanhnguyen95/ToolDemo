using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisCriteriaEvaluatePictureDisplayService
    {
        IQueryable<DisCriteriaEvaluatePictureDisplayModel> GetListCriteriaEvaluatePictureDisplay();
        Task<bool> CreateAsync(DisCriteriaEvaluatePictureDisplayRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsByCodeAsync(string code, Guid? id = default);
        Task<DisCriteriaEvaluatePictureDisplay> FindByIdAsync(Guid id);
        Task<DisCriteriaEvaluatePictureDisplayModel> GetCriteriaEvaluatePictureDisplayByCodeAsync(string code);
        Task<DisCriteriaEvaluatePictureDisplayModel> UpdateAsync(DisCriteriaEvaluatePictureDisplay request);
    }
}
