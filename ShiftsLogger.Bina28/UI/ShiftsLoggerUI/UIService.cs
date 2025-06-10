using Spectre.Console;

namespace ShiftsLoggerUI;

internal static class UIService
{
	internal static async Task AddInputAsync()
	{
		try
		{
			var shift = new ShiftModel
			{
				EmployeeId = AnsiConsole.Ask<int>("Enter the employee's ID: "),
				StartTime = AnsiConsole.Ask<DateTime>("Enter the start time in format YYYY-MM-DD HH:MM: "),
				EndTime = AnsiConsole.Ask<DateTime>("Enter the end time in format YYYY-MM-DD HH:MM: "),
				ShiftType = AnsiConsole.Ask<string>("Enter the shift type (day, night, evening): "),
				Notes = AnsiConsole.Ask<string>("Enter the notes: ")
			};

			// Add contact to controller or service
			var result = await UIController.AddAsync(shift);

			if (result)
			{
				AnsiConsole.MarkupLine("[green]Shift added successfully![/]");
			}
			else
			{
				AnsiConsole.MarkupLine("[yellow]Failed to add shift. Try again later.[/]");
			}
		}
		catch (Exception ex)
		{
			AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
		}
		finally
		{
			AwaitKeyPress();
		}
	}

	internal static async Task DeleteInputAsync()
	{
		var shift = await GetShiftOptionInputAsync();
		if (shift == null)
		{
			Console.WriteLine("The shift to be removed does not exist.");
			AwaitKeyPress();
			return;
		}
		await UIController.RemoveAsync(shift);
		AnsiConsole.MarkupLine("[green]Shift removed successfully![/]");
		AwaitKeyPress();
	}

	static private async Task<ShiftModel?> GetShiftOptionInputAsync()
	{
		// Get the list of shifts asynchronously
		var shifts = await UIController.GetAsync();

		// Check if there are no shifts
		if (!shifts.Any())
		{
			AnsiConsole.MarkupLine("[red]No shifts available.[/]");
			return null;
		}

		// Create a selection prompt for shift IDs
		var shiftOption = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Select a shift: ")
				.AddChoices(shifts.Select(x => x.Id.ToString()))
		);

		// Convert the selected option back to an integer ID
		int selectedId = int.Parse(shiftOption);

		// Retrieve the selected shift based on the ID
		var selectedShift = shifts.FirstOrDefault(x => x.Id == selectedId);

		return selectedShift;
	}

	internal static async Task GetShiftsAsync()
	{
		Console.Clear();
		var shifts = await UIController.GetAsync();
		UserInterface.ShowShiftsTable(shifts.ToList());
	}

	internal static async Task GetShiftInputAsync()
	{
		var shift = await GetShiftOptionInputAsync();
		if (shift == null)
		{
			Console.WriteLine("The shift does not exist.");
			AwaitKeyPress();
			return;
		}
		UserInterface.ShowShift(shift);
	}

	internal static async Task UpdateInputAsync()
	{
		var shift = await GetShiftOptionInputAsync();
		if (shift == null)
		{
			Console.WriteLine("The shift to be updated does not exist.");
			AwaitKeyPress();
			return;
		}
		UserInterface.UpdateInputAsync(shift);
		AwaitKeyPress();
	}

	static internal void AwaitKeyPress()
	{
		Console.WriteLine("\nPress any key to return to the menu...");
		Console.ReadKey();
	}
}


