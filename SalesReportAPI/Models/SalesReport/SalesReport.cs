using System.Data.SqlTypes;

namespace SalesReportAPI.Models.SalesReport
{
    public class SalesReport
    {
        public required int OrderID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int SalesPersonID { get; set; }
        public string? SalesPersonName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? city { get; set; }
        public int BillToAddressID { get; set; }

    }
}
