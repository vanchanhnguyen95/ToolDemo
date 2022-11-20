using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface ITempDisDisplaySupportToolService
    {
        Task<List<Temp_DisDisplaySupportTool>> GetDisDisplaySupportToolListAsync();
    }
}
