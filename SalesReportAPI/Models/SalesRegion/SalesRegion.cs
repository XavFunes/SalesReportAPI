namespace SalesReportAPI.Models.SalesRegion
{
    public class SalesRegion
    {
        public required string ProductName { get; set; }
        public string? ProductCategory {  get; set; }
        public decimal? TotalSales { get; set; }
        public decimal? TotalSalesInRegion { get; set; }
        public decimal? PercentageOfTotalSalesInRegion { get; set; }
        public decimal? PercentageOfTotalCategorySalesInRegion { get; set; }
    }
}
