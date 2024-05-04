using Microsoft.EntityFrameworkCore;
using SalesReportAPI.Data;
using SalesReportAPI.Models.SalesRegion;
using System.Data;

namespace SalesReportAPI.Services.SalesRegionServices
{
    public class SalesRegionServices : ISalesRegionServices
    {

        private readonly DataContext _context;

        public SalesRegionServices(DataContext context)
        {
            _context = context;
        }
        public Task<List<SalesRegion>> GetAll()
        {
            var repo = _context.SalesRegions.ToListAsync();
            return repo;
        }

        public DataTable GetReport()
        {
            DataTable dt = new DataTable();
            dt.TableName = "SaleRegion";
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ProductCategory");
            dt.Columns.Add("TotalSales");
            dt.Columns.Add("TotalSalesInRegion");
            dt.Columns.Add("PercentageOfTotalSalesInRegion");
            dt.Columns.Add("PercentageOfTotalCategorySalesInRegion");

            var _list = this._context.SalesRegions.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(record =>
                {
                    dt.Rows.Add(
                     record.ProductName,
                     record.ProductCategory,
                     record.TotalSales,
                     record.TotalSalesInRegion,
                     record.PercentageOfTotalSalesInRegion,
                     record.PercentageOfTotalCategorySalesInRegion

                     );

                });
            }



            return dt;
        }
    }
}
