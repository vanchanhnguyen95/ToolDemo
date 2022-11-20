using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.External
{
    public class CustomerSettingModel
    {
        public Guid Id { get; set; }
        public string AttributeID { get; set; }
        public string AttributeName { get; set; }
        public string Description { get; set; }
        public bool IsDistributorAttribute { get; set; } = false;
        public bool IsCustomerAttribute { get; set; } = false;
        public bool Used { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsEditable { get; set; } = false;
        public bool IsDistributorAttributeEditable { get; set; }
        public bool IsCustomerAttributeEditable { get; set; }
        public bool IsUsedEditable { get; set; }
        public bool IsChecked { get; set; }
        public Guid CustomerSettingId { get; set; }
    }

    public class CustomerSettingListModel
    {
        public List<CustomerSettingModel> Items { get; set; } = new List<CustomerSettingModel>();
    }

    public class CustomerSettingMasterDataModel
    {
        public string AttributeID { get; set; }
        public string Description { get; set; }
    }

    public class CustomerSettingUpdateRequest
    {
        public string AttributeID { get; set; }
        public string Description { get; set; }
        public bool IsDistributorAttribute { get; set; }
        public bool IsCustomerAttribute { get; set; }
        public bool Used { get; set; } = false;
    }
}
