using SalesReportAPI.Models.SalesRegion;
using System.Data;

namespace SalesReportAPI.Services.SalesRegionServices
{
    public interface ISalesRegionServices
    {
        Task<List<SalesRegion>> GetAll();
        DataTable GetReport();
    }
}
