using ShiftsLogger.ConsoleUI.Models;
using ShiftsLogger.ConsoleUI.RefitClients;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Services;
public class ShiftsService
{
	private readonly IShiftsLoggerClient _apiClient;

	public ShiftsService(IShiftsLoggerClient apiClient)
	{
		_apiClient = apiClient;
	}

	public async Task<List<Shift>> GetAll()
	{
		var shifts = new List<Shift>();
		try
		{
			var response = await _apiClient.GetAllShifts();

			if (response.IsSuccessful)
			{
				shifts = response.Content;
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
			UserInput.PromptAnyKeyToContinue();
		}
		return shifts;
	}

	public async Task<List<User>> GetUsersByShiftId(Guid id)
	{
		var users = new List<User>();
		try
		{
			var response = await _apiClient.GetUsersByShiftId(id);

			if (response.IsSuccessful)
			{
				users = response.Content;
			}
			else
			{
				AnsiConsole.MarkupLine($"[red]{response.Error.Content ?? response.Error.Message}[/]");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("There was an error: " + ex.Message);
			UserInput.PromptAnyKeyToContinue();
		}
		return users;
	}


	public async Task CreateShift(ShiftCreate shift)
	{
		try
		{
			var response = await _apiClient.CreateShift(shift);

			if (response.IsSuccessful)
			{
				AnsiConsole.MarkupLine("[green]New shift created successfully![/]");
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
			UserInput.PromptAnyKeyToContinue();
		}
	}

	public async Task DeleteShift(Guid id)
	{
		try
		{
			var response = await _apiClient.DeleteShift(id);

			if (response.IsSuccessful)
			{
				AnsiConsole.MarkupLine($"[green]Shift deleted successfully![/]");
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
			UserInput.PromptAnyKeyToContinue();
		}
	}

	public async Task UpdateShift(Guid id, ShiftUpdate shift)
	{
		try
		{
			var response = await _apiClient.UpdateShift(id, shift);

			if (response.IsSuccessful)
			{
				AnsiConsole.MarkupLine($"[green]Shift updated successfully![/]");
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
			UserInput.PromptAnyKeyToContinue();
		}
	}

	public async Task UpdateShiftUsers(Guid id, List<User> users)
	{
		try
		{
			var response = await _apiClient.UpdateShiftUsers(id, users);

			if (response.IsSuccessful)
			{
				AnsiConsole.MarkupLine($"[green]Shift users updated successfully![/]");
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
			UserInput.PromptAnyKeyToContinue();
		}
	}
}
