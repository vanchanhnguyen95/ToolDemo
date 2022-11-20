namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
	 public class PayRewardReportResponse
	 {
		  public string Customer { get; set; }
		  public string CustomerShipTo { get; set; }
		  public string CustomerShipToName { get; set; }
		  public string ProductName { get; set; }
		  public string Packing { get; set; }
		  public decimal Quantity { get; set; }
		  public decimal Amount { get; set; }
		  public string LevelName { get; set; }
		  public string LevelCode { get; set; }
	 }

	 public class PayRewardReportLevelGrouped
	 {
		  public string LevelName { get; set; }
		  public string LevelCode { get; set; }
		  public int TotalCustomer { get; set; }
	 }
}
