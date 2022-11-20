using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.SalesOrg
{
    public class TpSalesOrgDsaModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string TypeDSA { get; set; }
        public bool IsActive { get; set; }
        public string MappingNode { get; set; }
        public string SOStructureCode { get; set; }
        public DateTime EffectiveDate { get; set; }
        public object UntilDate { get; set; }
        public bool IsChecked { get; set; }
    }

    public class TpSalesOrgDsaListModel
    {
        public List<TpSalesOrgDsaModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
