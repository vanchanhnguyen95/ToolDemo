using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisplayService
    {
        #region Display List
        IQueryable<DisplayGeneralModel> GetListDisplayGeneral();
        List<DisplayAutoSearchModel> GetListDisplayAutoSearchModel();
        #endregion

        Task<DisDisplayModel> GetDisplayByCodeAsync(string code);
        DisDisplayModel GetDetailDisplayByCode(string code);
        Task<IQueryable<DisplayPopupModel>> GetListDisplayAsync();
        Task<IQueryable<DisplayPopupModel>> GetListDisplayForReportAsync(List<string> lstStatus);
        Task<bool> DeleteDisplayAsync(Guid id);
        Task<bool> CreateDisplayAsync(DisDisplay request);
        Task<DisDisplayModel> UpdateDisplayAsync(DisDisplay request);
        Task<bool> ExistsByCodeAsync(string code, Guid? id = default);
        Task<DisDisplay> FindByIdAsync(Guid id);
        Task<DisDisplayModel> FindByCodeAsync(string code);
        bool ConfirmDisplay(string code);

        //public bool UpdateDisplay(DisDisplayModel input, string userLogin);
        //public BaseResultModel CreateDisplay(DisDisplayModel input, string userLogin);
        //public IQueryable<DisplayGeneralModel> GetListDisplayGeneral();
        //public DisDisplayModel GetDisplayById(Guid id);
        //public DisDisplayModel GetGeneralDisplayByCode(string code);
        //public DisDisplayModel GetDetailDisplayByCode(string code);
        //public BaseResultModel CreateDisplay(DisDisplayModel input, string userLogin);
        //public bool UpdateDisplay(DisDisplayModel input, string userLogin);



        #region Scope
        Task<bool> CreateDisplayScopes(DisDisplayModel request);
        Task<List<DisplayScopeModel>> GetListDisplayScopeByDisplayCode(string displayCode);
        #endregion

        #region Applicable Object
        Task<bool> CreateDisplayApplicableObject(DisDisplayModel request);
        Task<List<DisplayCustomerSettingModel>> GetListDisplayCustomerAttributeByDisplayCode(string displayCode);
        #endregion

        #region Display Report Header
        Task<DisplayHeaderReportModel> GetDisplayReportHeaderByCode(string code);
        #endregion Display Report Header
    }
}
