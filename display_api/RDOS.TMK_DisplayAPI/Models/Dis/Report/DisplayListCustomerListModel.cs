using Sys.Common.Models;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
    public class DisplayListCustomerListModel
    {
        public string DisplayCodeLevel { get; set; }
        public string DisplayLevelName { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public decimal QuantityCustomer { get; set; }
    }
    public class ListDisplayListCustomerListModel
    {
        public List<DisplayListCustomerListModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
    public class ListCustomerConfirmModel
    {
        public string Customer { get; set; }
        public string CustomerShipto { get; set; }
        public string CustomerShiptoName { get; set; }
    }
    public class ListListCustomerConfirmModel
    {
        public List<ListCustomerConfirmModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}