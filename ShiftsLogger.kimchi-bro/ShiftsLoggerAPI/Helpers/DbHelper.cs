using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Helpers;

internal static class DbHelper
{
    internal static void CheckDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ShiftContext>();
        if (dbContext != null)
        {
            try
            {
                var created = dbContext.Database.EnsureCreated();
                if (!created) dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                EnvExit();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to get database context.");
            EnvExit();
        }
    }

    private static void EnvExit()
    {
        Console.ResetColor();
        Console.Write("\nPress any key to exit the app. ");
        Console.ReadKey(true);
        Environment.Exit(0);
    }
}
