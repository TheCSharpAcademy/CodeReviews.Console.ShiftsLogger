using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShiftsLogger.Lonchanick.Models;

namespace ShiftsLogger.Lonchanick.ContextDataBase;

public class ContextDB : DbContext
{
    /*protected readonly IConfiguration Configuration;
    public ContextDB(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server with connection string from app settings
        options.UseSqlServer(Configuration.GetConnectionString("ConString"));
    }*/

    

    public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }

    public DbSet<Worker> worker { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var WorkerList= new List<Worker>{
            new(){Id =Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa97") ,Name="Leopoldo"},
            new(){Id =Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa02") ,Name="Ramon"},
            new(){Id =Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa03") ,Name="Trespatines"},
        };

        modelBuilder.Entity<Worker>(w =>
        {
            w.ToTable("Worker");
            w.HasKey(ob => ob.Id);
            w.Property(ob => ob.Name).IsRequired().HasMaxLength(200);
            w.HasData(WorkerList);
        });

        var ShiftList = new List<Shift>
        {
            new(){Id=Guid.Parse("85df9217-bc1a-4490-92c9-883b572bc001"), Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa97") },
            new(){Id=Guid.Parse("85df9217-bc1a-4490-92c9-883b572bc002"),Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa02") },
            new(){Id=Guid.Parse("85df9217-bc1a-4490-92c9-883b572bc003"),Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa03") }
        };

        modelBuilder.Entity<Shift>(sh =>
        {
            sh.ToTable("Shift");
            sh.HasKey(ob => ob.Id);
            sh.Property(ob => ob.Check);
            sh.Property(ob => ob.CheckTypeField);
            sh.HasOne(p => p.Worker).WithMany(p => p.Shifts).HasForeignKey(p => p.WorkerId);
            //tarea.HasOne(p => p.categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            sh.HasData(ShiftList);

        });
    }

}
