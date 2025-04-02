using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace ShiftsLoggerUI.Services;

public class SeedingService : BaseService
{
    public SeedingService(IConfiguration configuration) : base(configuration)
    {
    }
    
    public async Task SeedDbAsync(int randRowNumber)
    {
        try
        {
            var empSeedResponse = await _client.PostAsync("api/Seed/SeedEmployees", null);
            var shiftSeedResponse = await _client.PostAsync($"api/Seed/{randRowNumber}/SeedShifts", null);

            if (empSeedResponse.IsSuccessStatusCode && shiftSeedResponse.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine($"[green]Data base seeded successfully![/]");
                Console.ReadLine();
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{empSeedResponse.StatusCode.ToString() ?? empSeedResponse.StatusCode.ToString()}[/]");
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
            Console.ReadLine();
        }
    }
}
