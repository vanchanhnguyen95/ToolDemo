using RDOS.TMK_DisplayAPI.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Temp
{
    public class TempDisPosmForCusShiptoSearchModel : EcoParameters
    {
        public string SaleOrgCode { get; set; }
        public string ScopeType { get; set; }
        public string SaleTerritoryLevel { get; set; }
        public List<string> ListSaleTerritoryValues { get; set; }
        public List<string> ListDsaValues { get; set; }
        public string PosmCode { get; set; }
    }
}
