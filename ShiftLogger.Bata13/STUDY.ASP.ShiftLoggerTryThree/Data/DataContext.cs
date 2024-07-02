using Microsoft.EntityFrameworkCore;
using STUDY.ASP.ShiftLoggerTryThree.Models;

namespace STUDY.ASP.ShiftLoggerTryThree.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\LocalDBDemo;Database=shiftloggerdb;Trusted_Connection=true;TrustServerCertificate=true;");
        }
        public DbSet<ShiftLogger> Shifts { get; set; }
    }
}
