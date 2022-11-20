using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ParameterWithSiref
    {
        public Guid Id { get; set; }
        public string SirefCode { get; set; }
        public string SitypeCode { get; set; }
        public string ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public string Constraint { get; set; }
        public bool IsCustomerOrderOutofWorkPlan { get; set; }
        public bool IsSalesmanOrderCreatedbyDistributor { get; set; }
        public bool IsDistributorOrderforCustomerofSalesman { get; set; }
        public bool IsSkuConditions { get; set; }
        public string SkuConditionsValueType { get; set; }
        public int? SkuConditionsValue { get; set; }
        public bool IsPcconditions { get; set; }
        public string Pcconditions { get; set; }
        public string PcconditionsValueType { get; set; }
        public int? PcconditionsInvoiceValue { get; set; }
        public int? PcconditionsNoofSku { get; set; }
        public string RecognizedDateforPc { get; set; }
        public string RecognizedDateforSov { get; set; }
        public string Scconditions { get; set; }
        public string ScconditionsValueType { get; set; }
        public int? ScconditionsOrderValue { get; set; }
        public int? ScconditionsNoofSku { get; set; }
        public string SovcoditionsValueType { get; set; }
        public bool IsSovconditionsOrderWithoutVisit { get; set; }
        public string Sovcoditions { get; set; }
        public int? SovconditionsInvoiceValue { get; set; }
        public int? SovconditionsNoofSku { get; set; }
        public bool IsSovsumWithPassedSkuConditions { get; set; }
        public string LppcconditionsCaculationFormula { get; set; }
        public int? LppcconditionsOrderorInvoiceValue { get; set; }
        public bool IsLppcconditionsOrderWithoutVisit { get; set; }
        public string Lppcconditions { get; set; }
        public string LppcconditionsValueType { get; set; }
        public int? LppcconditionsNoofSku { get; set; }
        public string SivconditionsValueType { get; set; }
        public int? SivconditionsValue { get; set; }
        public bool IsVisitDuration { get; set; }
        public string VisitDurationValueType { get; set; }
        public int? VisitDurationValue { get; set; }
        public int? VisitDurationFrom { get; set; }
        public int? VisitDurationTo { get; set; }
        public bool ByProduct { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
