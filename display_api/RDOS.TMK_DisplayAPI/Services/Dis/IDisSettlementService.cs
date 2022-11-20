
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Settlement;
using RDOS.TMK_DisplayAPI.Models.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisSettlementService
    {
        Task<IQueryable<DisSettlementModel>> GetListSettlementAsync();
        List<DisSettlementModel> GetListSettlementGeneralAsync();
        public IQueryable<DisPayRewardDisplayModel> GetListPayRewardByDisplayCode(string displayCode);
        Task<DisSettlementModel> GetSettlementByCodeAsync(string code);
        public DisSettlementModel GetSettlementByCode(string code);
        
        public void CreateSettlement(DisSettlementModel input, string userlogin);
        public void UpdateSettlement(DisSettlementModel input, string userlogin);
        public void DeleteSettlement(DisSettlementModel input, string userlogin);

        #region DisSettlementConfirmByDistributor
        public IQueryable<DisSettlementConfirmModel> GetListSettlementConfirm();
        public IQueryable<DisSettlementDetailModel> GetListDetailSettlementConfirmByDistributor(string code, string distributorCode);
        public IQueryable<DisSettlementConfirmModel> GetListSettlementConfirmByDistributor(string distributorCode);
        public BaseResultModel ConfirmSettlementByDistributor(string distributorCode, List<string> lstInput);
        #endregion
    }
}
