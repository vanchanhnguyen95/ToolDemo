using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable
{
    public class TempDisApproveRegistrationCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string DisplayCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DisplayLevelName { get; set; }
        public string DisplayLevelCode { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerShipToCode { get; set; }
        public string SalesTeritoryLevel { get; set; }
        public string SalesTeritoryValue { get; set; }
        public string Status { get; set; }
    }
}
