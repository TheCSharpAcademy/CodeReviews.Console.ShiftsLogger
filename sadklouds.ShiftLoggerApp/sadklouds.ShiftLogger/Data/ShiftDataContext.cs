namespace sadklouds.ShiftLogger.Data;
public class ShiftDataContext : DbContext
{
    public ShiftDataContext(DbContextOptions<ShiftDataContext> options) : base(options)
    {
        
    }
    public DbSet<Shift> Shifts { get; set; }
}
