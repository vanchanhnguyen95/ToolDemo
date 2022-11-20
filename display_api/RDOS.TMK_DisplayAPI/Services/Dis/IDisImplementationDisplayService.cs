
using RDOS.TMK_DisplayAPI.Models.Dis;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisImplementationDisplayService
    {
        public IQueryable<DisDisplayModel> GetListDisplay();
        public IQueryable<DisDisplayModel> GetListDisplayCode();
        public DisDisplayModel GetDisplayByCode(string code);
        public bool UpdateDataDisplay(DisDisplayUpdModel input);

    }
}
