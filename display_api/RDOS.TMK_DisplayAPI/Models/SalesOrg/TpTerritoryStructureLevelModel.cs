using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.SalesOrg
{
    public class TpTerritoryStructureLevelModel
    {
        public string Id { get; set; }
        public string TerritoryStructureCode { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public string TerritoryLevelCode { get; set; }
    }

    public class TpTerritoryStructureDetailListModel
    {
        public List<TpTerritoryStructureLevelModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
