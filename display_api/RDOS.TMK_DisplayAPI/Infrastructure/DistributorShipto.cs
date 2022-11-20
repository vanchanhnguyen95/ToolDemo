using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DistributorShipto
    {
        public DistributorShipto()
        {
            DistributorHistoricals = new HashSet<DistributorHistorical>();
            ShiptoContacts = new HashSet<ShiptoContact>();
        }

        public Guid Id { get; set; }
        public string ShiptoCode { get; set; }
        public Guid DistributorId { get; set; }
        public string ShiptoCodeOnErp { get; set; }
        public string ShiptoName { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public Guid Country { get; set; }
        public Guid? Province { get; set; }
        public Guid? District { get; set; }
        public Guid? Wards { get; set; }
        public Guid? City { get; set; }
        public Guid? Region { get; set; }
        public Guid? State { get; set; }
        public string Street { get; set; }
        public string DepartmentNumber { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string FullAddress { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public Guid? Chanels { get; set; }
        public Guid? SubChanels { get; set; }
        public Guid? SicId { get; set; }
        public decimal? AcrLong { get; set; }
        public decimal? AcrWidth { get; set; }
        public int? AcrNumberFloor { get; set; }
        public int DeleteFlg { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual ICollection<DistributorHistorical> DistributorHistoricals { get; set; }
        public virtual ICollection<ShiptoContact> ShiptoContacts { get; set; }
    }
}
