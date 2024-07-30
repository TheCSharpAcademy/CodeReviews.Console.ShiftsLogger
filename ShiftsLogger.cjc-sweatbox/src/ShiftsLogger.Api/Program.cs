using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Api.Configurations;
using ShiftsLogger.Api.Installers;
using ShiftsLogger.Data.Contexts;
using ShiftsLogger.Data.Entities;
using ShiftsLogger.Data.Services;

namespace ShiftsLogger.Api;

public class Program
{
    #region Methods

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.InstallServicesInAssembly();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            var swaggerOptions = new SwaggerOptions();
            builder.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
                options.RoutePrefix = string.Empty;
            });
        }

        app.MapControllers();

        // Perform any database migrations.
        using (var serviceScope = app.Services.CreateScope())
        {
            var databaseContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

            await databaseContext.Database.MigrateAsync();

            // Perform any database seeding.
            var databaseOptions = new DatabaseOptions();
            builder.Configuration.GetSection(nameof(DatabaseOptions)).Bind(databaseOptions);
            
            // Only seed if configured to and no data already exists.
            if (databaseOptions.SeedDatabase && !databaseContext.Shift.Any())
            {
                var databaseService = serviceScope.ServiceProvider.GetRequiredService<IShiftService>();

                // Seed 6 months of data.
                DateTime seedDateTime = DateTime.Now.AddMonths(-6).AddDays(-1);
                while(seedDateTime.Date < DateTime.Now.Date)
                {
                    // Increment so we are not in infinite loop doom land.
                    seedDateTime = seedDateTime.AddDays(1);

                    // Worker does't work weekends.
                    if (seedDateTime.DayOfWeek is DayOfWeek.Sunday || seedDateTime.DayOfWeek is DayOfWeek.Saturday)
                    {
                        continue;
                    }

                    // Get random offsets.
                    var startOffsetSeconds = Random.Shared.Next(0, 1000);
                    var endOffsetSeconds = Random.Shared.Next(0, 1000);

                    // 1 in 10 times, be late for a shift.
                    var isLate = Random.Shared.Next(0, 10) == 0;

                    // 1 in 10 times, leave shift early.
                    var leftEarly = Random.Shared.Next(0, 10) == 0;

                    // Suppose the worker has to start work at 9am until 6pm.
                    var startTime = new DateTime(seedDateTime.Year, seedDateTime.Month, seedDateTime.Day, 9, 0, 0);
                    var endTime = new DateTime(seedDateTime.Year, seedDateTime.Month, seedDateTime.Day, 18, 0, 0);

                    // Apply the offsets.
                    startTime = isLate ? startTime.AddSeconds(startOffsetSeconds) : startTime.AddSeconds(-startOffsetSeconds);
                    endTime = leftEarly ? endTime.AddSeconds(-endOffsetSeconds) : endTime.AddSeconds(endOffsetSeconds);

                    // Add to database.
                    var shift = new Shift
                    {
                        Id = Guid.NewGuid(),
                        StartTime = startTime,
                        EndTime = endTime,
                    };
                    await databaseService.CreateAsync(shift);
                }
            }
        }

        app.Run();
    }

    #endregion
}
