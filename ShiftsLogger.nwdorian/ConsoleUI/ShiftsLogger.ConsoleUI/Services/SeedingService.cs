using ShiftsLogger.ConsoleUI.RefitClients;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Services;
public class SeedingService
{
	private readonly IShiftsLoggerClient _apiClient;

	public SeedingService(IShiftsLoggerClient apiClient)
	{
		_apiClient = apiClient;
	}
	public async Task SeedDatabase()
	{
		try
		{
			var response = await _apiClient.SeedDatabase();

			if (response.IsSuccessful)
			{
				AnsiConsole.MarkupLine($"[green]{response.Content}[/]");
				UserInput.PromptAnyKeyToContinue();
			}
			else
			{
				AnsiConsole.MarkupLine($"[red]{response.Error.Content ?? response.Error.Message}[/]");
				UserInput.PromptAnyKeyToContinue();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("There was an error: " + ex.Message);
			Console.ReadLine();
		}
	}
}
