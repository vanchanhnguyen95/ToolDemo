using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Dsageographical
    {
        public Guid Id { get; set; }
        public Guid DistributorSellingAreaId { get; set; }
        public Guid GeographicalMasterId { get; set; }
        public Guid GeographicalStructureId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
