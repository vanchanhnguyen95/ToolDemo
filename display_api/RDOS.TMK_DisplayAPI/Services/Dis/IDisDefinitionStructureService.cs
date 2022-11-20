using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisDefinitionStructureService
    {
        Task<bool> ExistsByCodeAsync(string displayCode, string levelCode, Guid? id = default);
        Task<DisDefinitionStructure> FindByIdAsync(Guid id);
        Task<List<DisDefinitionStructure>> GetDisDefinitionStructureListAsync(string displayCode);
        Task<DisDefinitionStructureModel> GetDisDefinitionStructureByCodeAsync(string displayCode, string levelCode);
        Task<string> CreateDisDefinitionStructureAsync(DisDefinitionStructureModel request);
        Task<string> UpdateDisDefinitionStructureAsync(DisDefinitionStructureModel request);

        Task<List<DisDefinitionStructureModel>> GetAllDataDisDefinitionStructureListByDisplayCode(string displayCode);
    }
}
