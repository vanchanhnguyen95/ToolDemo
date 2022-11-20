using RDOS.TMK_DisplayAPI.Models.Paging;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
	 public class ConfirmResultDetailReportRequest : EcoParameters
	 {
		  public string DisplayCode { get; set; }
		  public string ConfirmResultCode { get; set; }
	 }
}
