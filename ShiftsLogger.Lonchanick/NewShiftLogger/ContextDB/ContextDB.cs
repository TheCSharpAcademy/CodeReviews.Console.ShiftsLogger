using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Lonchanick.Models;

namespace ShiftsLogger.Lonchanick.ContextDataBase;

public class ContextDB : DbContext
{

    public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }

    public DbSet<Worker> worker { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>(w =>
        {
            w.ToTable("Worker");
            w.HasKey(ob => ob.Id);
            w.Property(ob => ob.Name).IsRequired().HasMaxLength(200);
        });
        
        modelBuilder.Entity<Shift>(sh =>
        {
            sh.ToTable("Shift");
            sh.HasKey(ob => ob.Id);
            sh.Property(ob => ob.Check);
            sh.Property(ob => ob.CheckTypeField);
            sh.HasOne(p => p.Worker).WithMany(p => p.Shifts).HasForeignKey(p => p.WorkerId);
        });
    }

}
