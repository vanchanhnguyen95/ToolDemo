using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisConfirmResultService
    {
        Task<IQueryable<DisConfirmResult>> GetListConfirmResultAsync();
        Task<IQueryable<DisConfirmResultsModel>> GetListConfirmResultViewAsync();
        Task<DisConfirmResultsModel> GetConfirmResultByCodeAsync(string code);
        Task<List<DisConfirmResultDisplayModel>> GetConfirmResultByDisplayCodeAsync(string DisCode);
        public DisConfirmResultDisplayModel GetConfirmResultByCode(string code);
        Task<DisConfirmResultDisplayModel> GetConfirmResultByDisplayCodeSaleCalendar(TempDisplayConfirmRequestModel parameters);
        public void DeleteConfirmResult(DisConfirmResultDisplayModel input, string userlogin);

        public void CreateDisConfirmResult(DisConfirmResultsModel input, string userlogin);
        public void UpdateDisConfirmResult(DisConfirmResultsModel input, string userlogin);
	 }
}
