using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using Sys.Common.Constants;
using System.Collections.Generic;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public interface IDisBudgetService
    {
        public List<DisBudgetModel> GetListDisBudgets(string displayCode, int type = CommonData.DisplaySetting.TypeBudgetNow, int adjustmentsCount = 0);
        public IQueryable<BudgetAdjustmentListModel> GetListDisBudgetForAdjustment(string displayCode, int type);
        public BaseResultModel SaveDisBudget(DisBudgetModel input, string userLogin);
        public BaseResultModel SaveDisBudgets(List<DisBudgetModel> lstInput, string userLogin);
        public BaseResultModel SaveDisBudgetsForAdjustment(DisBudgetForAdjustmentModel input, string userLogin);
        public BaseResultModel DeleteDisBudgets(DeleteDisBudgetsModel input);
    }
}
