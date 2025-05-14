using ShiftLogger.Brozda.API.Data;
using ShiftLogger.Brozda.API.Models;
using System.Text.Json;

namespace ShiftLogger.Brozda.API.Brozda
{
    [Obsolete("Seeding done in on model creating")]
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Automatically seeds database upon creation with default data
        /// </summary>
        /// <param name="defaultDataPath"><see cref="string"/> value containing default data</param>
        public static async Task SeedDatabase(this IApplicationBuilder app, string defaultDataPath)
        {
            var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ShiftLoggerContext>();

            var path = Path.Combine(AppContext.BaseDirectory, "Resources", "SeedData.json");

            if (File.Exists(path))
            {
                var rawData = await File.ReadAllTextAsync(path);
                var seedData = JsonSerializer.Deserialize<SeedData>(rawData);

                if (seedData is not null)
                {
                    dbContext.ShiftTypes.AddRange(seedData.ShiftTypes);
                    dbContext.Workers.AddRange(seedData.Workers);
                    dbContext.AssignedShifts.AddRange(seedData.AssignedShifts);

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}