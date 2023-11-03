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
        //var WorkerList= new List<Worker>{
        //    new(){Name="Leopoldo"},
        //    new(){Name="Ramon"},
        //    new(){Name="Trespatines"},

        //    /*new(){Id =1 ,Name="Leopoldo"},
        //    new(){Id =2 ,Name="Ramon"},
        //    new(){Id =3 ,Name="Trespatines"},*/
        //};

        modelBuilder.Entity<Worker>(w =>
        {
            w.ToTable("Worker");
            w.HasKey(ob => ob.Id);
            w.Property(ob => ob.Name).IsRequired().HasMaxLength(200);
            //w.HasData(WorkerList);
        });

        /* var ShiftList = new List<Shift>
         {
             new(){Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=1},
             new(){Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=2},
             new(){Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=3}

             *//*new(){Id=1, Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa97") },
             new(){Id=2,Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa02") },
             new(){Id=3,Check=DateTime.Now, CheckTypeField=CheckType.CheckIn, WorkerId=Guid.Parse("2928fb74-46c1-439c-b8ad-b9aee833fa03") }*//*
         };*/

        modelBuilder.Entity<Shift>(sh =>
        {
            sh.ToTable("Shift");
            sh.HasKey(ob => ob.Id);
            sh.Property(ob => ob.Check);
            sh.Property(ob => ob.CheckTypeField);
            sh.HasOne(p => p.Worker).WithMany(p => p.Shifts).HasForeignKey(p => p.WorkerId);
            //sh.HasData(ShiftList);
        });
    }

}
