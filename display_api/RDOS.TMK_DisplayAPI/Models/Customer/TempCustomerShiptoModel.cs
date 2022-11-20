using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.Customer
{
    public class TempCustomerShiptoModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerShiptoCode { get; set; }
        public string CustomerShiptoAddress { get; set; }
    }
}
