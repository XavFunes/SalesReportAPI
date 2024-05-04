using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesReportAPI.Models.SalesRegion;
using SalesReportAPI.Services.SalesRegionServices;

namespace SalesReportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesRegionController : ControllerBase
    {
        private readonly ISalesRegionServices _salesRegionServices;

        public SalesRegionController(ISalesRegionServices salesRegionServices)
        {
            _salesRegionServices = salesRegionServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalesRegion>>> GetAll()
        {
            return await _salesRegionServices.GetAll();
        }

        [HttpGet("ExportExcel")]
        public ActionResult ExportExcel()
        {
            var _reportData = _salesRegionServices.GetReport();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_reportData, "Sales Region Services");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesReport.xlsx");

                }
            }
        }
    }
}
