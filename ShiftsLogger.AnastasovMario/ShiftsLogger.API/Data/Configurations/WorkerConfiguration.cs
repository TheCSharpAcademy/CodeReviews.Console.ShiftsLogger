using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShiftsLogger.API.Data.Configurations
{
  public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
  {
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
      builder.HasData(SeedData());
    }

    private Worker[] SeedData()
    {
      return new Worker[]
      {
        new Worker
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Shifts = new List<Shift>()
        },
        new Worker
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Shifts = new List<Shift>()
        },
        new Worker
        {
            Id = 3,
            FirstName = "Alice",
            LastName = "Johnson",
            Shifts = new List<Shift>()
        },
        new Worker
        {
            Id = 4,
            FirstName = "Bob",
            LastName = "Brown",
            Shifts = new List<Shift>()
        },
        new Worker
        {
            Id = 5,
            FirstName = "Charlie",
            LastName = "Davis",
            Shifts = new List<Shift>()
        }
      };
    }

  }
}
