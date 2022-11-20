using RDOS.TMK_DisplayAPI.Models.Paging;
using System;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
    public class DisplayReportEcoParameters : EcoParameters
    {
        public string DisplayCode { get; set; }
        public string Displaylevel { get; set; }
        public string SaleOrg { get; set; }
        public string ScopeType { get; set; }
        public string ApplicableObjectType { get; set; }
        public List<string> ListScope { get; set; }
        public List<string> ListApplicableObject { get; set; }
    }
}
