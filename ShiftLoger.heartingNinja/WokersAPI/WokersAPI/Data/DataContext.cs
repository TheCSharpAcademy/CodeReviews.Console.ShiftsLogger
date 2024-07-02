namespace WokersAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<SuperHero> SuperHeroes { get; set; }
    public DbSet<WorkerShift> WorkerShift { get; set; }
}
