using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using Microsoft.EntityFrameworkCore;
using RDOS.TMK_DisplayAPI.Services.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class TempDisDisplaySupportToolService : ITempDisDisplaySupportToolService
    {
        private readonly IBaseRepository<Temp_DisDisplaySupportTool> _repository;

        public TempDisDisplaySupportToolService(IBaseRepository<Temp_DisDisplaySupportTool> repository)
        {
            _repository = repository;
        }

        public async Task<List<Temp_DisDisplaySupportTool>> GetDisDisplaySupportToolListAsync()
            => await _repository.GetAllQueryable().ToListAsync();
    }
}
