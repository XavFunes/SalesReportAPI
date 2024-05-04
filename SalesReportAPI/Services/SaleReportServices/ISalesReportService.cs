using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using SalesReportAPI.Models.SalesReport;
using System.Data;

namespace SalesReportAPI.Services.SaleReportServices
{
    public interface ISalesReportService
    {
        Task<List<SalesReport>> GetAll();
        Task<SalesReport> GetSingle(int id);
        Task<List<SalesReport>> FindReportBySalesPersonName(string name);
        Task<List<SalesReport>> FindReportByCategoryProduct(string categoryProductName);
        Task<List<SalesReport>> FindReportByCity(string cityName);

        DataTable GetReport();
        

       // FileResult GenerateExcel(string name , IEnumerable<SalesReport> records);




    }
}
