using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesReportAPI.Data;
using SalesReportAPI.Models.SalesReport;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesReportAPI.Services.SaleReportServices
{
    public class SalesReportService : ISalesReportService
    {

        private readonly DataContext _context;
        public SalesReportService(DataContext contex)
        {
            _context = contex;
        }

        public async Task<SalesReport> GetSingle(int id)
        {
            var repor = await _context.SalesReports.FindAsync(id);
            if (repor == null)
            {
                return null;
            }

            return repor;
        }

        public async Task<List<SalesReport>> GetAll()
        {
            var repor = await _context.SalesReports.ToListAsync();
            return repor;
        }

        public Task<List<SalesReport>> FindReportBySalesPersonName(string name)
        {
            var repor = _context.SalesReports.Where(e => e.SalesPersonName.Contains(name)).ToListAsync();
            return repor;
        }

        public Task<List<SalesReport>> FindReportByCategoryProduct(string categoryProductName)
        {
            var repor = _context.SalesReports.Where(e => e.CategoryName.Contains(categoryProductName)).ToListAsync();
            return repor;
        }

        public Task<List<SalesReport>> FindReportByCity(string cityName)
        {
            var repor = _context.SalesReports.Where(e => e.city.Contains(cityName)).ToListAsync();  
            return repor;
        }

        public DataTable GetReport()
        {
            DataTable dt = new DataTable();
            dt.TableName = "SaleReport";
            dt.Columns.Add("OrderID");
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("CustomerID");
            dt.Columns.Add("ProductID");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("UnitPrice");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("TotalPrice");
            dt.Columns.Add("SalesPersonID");
            dt.Columns.Add("SalesPersonName");
            dt.Columns.Add("ShippingAddress");
            dt.Columns.Add("City");
            dt.Columns.Add("BillToAddressID");

            var _list = this._context.SalesReports.ToList();
            if( _list.Count > 0 )
            {
                _list.ForEach(record =>
                {
                    dt.Rows.Add(
                     record.OrderID,
                     record.OrderDate,
                     record.CustomerID,
                     record.ProductID,
                     record.ProductName,
                     record.CategoryName,
                     record.UnitPrice,
                     record.Quantity,
                     record.TotalPrice,
                     record.SalesPersonID,
                     record.SalesPersonName,
                     record.ShippingAddress,
                     record.city,
                     record.BillToAddressID
                     );
              
                });
            }



            return dt;
        }


        /* public FileResult GenerateExcel(string name, IEnumerable<SalesReport> records)
         {
             DataTable dataTable = new DataTable("SalesReport");
             dataTable.Columns.AddRange(new DataColumn[] 
             {
                 new DataColumn("OrderID"),
                 new DataColumn("OrderDate"),
                 new DataColumn("CustomerID"),
                 new DataColumn("ProductID"),
                 new DataColumn("ProductName"),
                 new DataColumn("CategoryName"),
                 new DataColumn("UnitPrice"),
                 new DataColumn("Quantity"),
                 new DataColumn("TotalPrice"),
                 new DataColumn("SalesPersonID"),
                 new DataColumn("SalesPersonName"),
                 new DataColumn("ShippingAddress"),
                 new DataColumn("City"),
                 new DataColumn("BillToAddressID")

             });

             foreach (var record in records)
             {
                 dataTable.Rows.Add(

                     record.OrderID,
                     record.OrderDate,
                     record.CustomerID,
                     record.ProductID,
                     record.ProductName,
                     record.CategoryName,
                     record.UnitPrice,
                     record.Quantity,
                     record.TotalPrice,
                     record.SalesPersonID,
                     record.SalesPersonName,
                     record.ShippingAddress,
                     record.city,
                     record.BillToAddressID
                     );
             }

             using (XLWorkbook wb = new XLWorkbook())
             {
                 wb.Worksheets.Add(dataTable);

                 using(MemoryStream stream = new MemoryStream())
                 {
                     wb.SaveAs(stream);
                     var path = (stream.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",name);
                     return File(path);
                 }
             }
         }
        */

    }
}

