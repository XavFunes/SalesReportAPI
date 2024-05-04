using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesReportAPI.Models.SalesReport;
using SalesReportAPI.Services.SaleReportServices;

namespace SalesReportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReportController : ControllerBase
    {
        private readonly ISalesReportService _salesReportService;

        public SalesReportController(ISalesReportService salesReportService)
        {
            _salesReportService = salesReportService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalesReport>>> GetAll()
        {
            return await  _salesReportService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SalesReport>>> GetById(int id)
        {
            var result = await _salesReportService.GetSingle(id);
            if (result is null)
            {
                return BadRequest("Order not found");
            }
            return Ok(result);
        }

        [HttpGet("FindBySalesPersonName")]
        public async Task<ActionResult<List<SalesReport>>> FindByPerson(string name)
        {
            var result = await _salesReportService.FindReportBySalesPersonName(name);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("FindByCategoryName")]
        public async Task<ActionResult<List<SalesReport>>> FindByCategoryName(string name)
        {
            var result = await _salesReportService.FindReportByCategoryProduct(name);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("FindByCityName")]
        public async Task<ActionResult<List<SalesReport>>> FindByCity(string name)
        {
            var result = await _salesReportService.FindReportByCity(name);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("ExportExcel")]
        public ActionResult ExportExcel()
        {
            var _reportData = _salesReportService.GetReport();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_reportData, "Sales Repots");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesReport.xlsx");

                }
            }
        }


    }
}
