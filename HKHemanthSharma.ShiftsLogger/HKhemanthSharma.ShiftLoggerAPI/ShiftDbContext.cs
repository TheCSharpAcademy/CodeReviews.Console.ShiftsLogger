using HKhemanthSharma.ShiftLoggerAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HKhemanthSharma.ShiftLoggerAPI
{
    public class ShiftDbContext : DbContext
    {
        public ShiftDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Worker> Workers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shift>()
                .HasKey(x => x.ShiftId);
            modelBuilder.Entity<Shift>()
                .Property(x => x.ShiftDate)
                .HasColumnType("Date")
                .IsRequired();
            modelBuilder.Entity<Shift>()
                .Property(x => x.ShiftStartTime)
                .HasColumnType("DateTime")
                .IsRequired();
            modelBuilder.Entity<Shift>()
                .Property(x => x.ShiftEndTime)
                .HasColumnType("DateTime")
                .IsRequired();
            modelBuilder.Entity<Worker>()
                .HasKey(x => x.WorkerId);
            modelBuilder.Entity<Worker>()
                .HasMany(x => x.Shifts)
                .WithOne(x => x.Worker)
                .HasForeignKey(x => x.WorkerId);
        }
        public override int SaveChanges()
        {
            var Entities = ChangeTracker.Entries<Shift>().Where(x => new List<EntityState> { EntityState.Added, EntityState.Modified, EntityState.Deleted }.Contains(x.State))
            .Select(x => x).ToList();
            foreach (var entity in Entities)
            {
                entity.Entity.CalculateDuration();
                entity.Entity.ShiftDate = entity.Entity.ShiftDate == null ? DateTime.Now.Date : entity.Entity.ShiftDate;
            }
            base.SaveChanges();
            return Entities.Count();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Get all Shift entities that are added, modified, or deleted
            var shiftEntries = ChangeTracker.Entries<Shift>()
                .Where(x => x.State == EntityState.Added ||
                           x.State == EntityState.Modified ||
                           x.State == EntityState.Deleted)
                .ToList();

            // Process each entity
            foreach (var entry in shiftEntries)
            {
                // Skip deleted entities if you only want to process added/modified
                if (entry.State == EntityState.Deleted)
                    continue;

                var shift = entry.Entity;

                // Calculate duration
                shift.CalculateDuration();

                // Set default date if null
                shift.ShiftDate ??= DateTime.Now.Date;
            }

            // Save changes and return the actual number of affected rows
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
