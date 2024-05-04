using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesReportAPI.Models.SalesReport
{
    public class SalesReportConfiguration : IEntityTypeConfiguration<SalesReport>
    {
        public void Configure(EntityTypeBuilder<SalesReport> builder)
        {
            builder.ToView(nameof(SalesReport), "Sales");

            builder.HasKey(e => e.OrderID);
            builder.Property(e => e.OrderID).HasColumnName("OrderID");
        }
    }
}
