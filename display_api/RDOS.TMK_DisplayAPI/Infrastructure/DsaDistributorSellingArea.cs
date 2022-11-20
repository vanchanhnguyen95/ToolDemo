using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DsaDistributorSellingArea
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string TypeDSA { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string MappingNode { get; set; }
        public string SOStructureCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? UntilDate { get; set; }
        public string WareHouseCode { get; set; }
        public Guid? WareHouseId { get; set; }
    }
}
