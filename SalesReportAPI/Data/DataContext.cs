using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using SalesReportAPI.Models.SalesReport;
using SalesReportAPI.Models.SalesRegion;

namespace SalesReportAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=FUNES\\SQLEXPRESS;Database=AdventureWorks2022;Trusted_Connection=true;Trust Server Certificate=true;");
        }


        //Views
        //public DbSet<SalesReport> SalesReports { get; set; }

        public virtual DbSet<SalesReport> SalesReports { get; set; }
        public virtual DbSet<SalesRegion> SalesRegions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
