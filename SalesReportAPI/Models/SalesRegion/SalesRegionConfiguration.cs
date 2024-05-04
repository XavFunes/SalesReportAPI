using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesReportAPI.Models.SalesRegion
{
    public class SalesRegionConfiguration : IEntityTypeConfiguration<SalesRegion>
    {
        public void Configure(EntityTypeBuilder<SalesRegion> builder)
        {
            builder.ToView(nameof(SalesRegion), "Sales");
            builder.HasKey(p => p.ProductName);
        }
    }
}
