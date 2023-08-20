using Microsoft.EntityFrameworkCore;

namespace API.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ShiftsLoggerContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ShiftsLoggerContext>>()))
        {
            // Look for any Employees.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }
            context.Employees.AddRange(
                new Employee
                {
                    Name = "John Doe"
                },
                new Employee
                {
                    Name = "Charlie Brown"
                }
            );
            context.SaveChanges();
        }
    }
}