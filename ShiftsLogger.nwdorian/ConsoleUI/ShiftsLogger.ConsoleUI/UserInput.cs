using ShiftsLogger.ConsoleUI.Models;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class UserInput
{
	public static void PromptAnyKeyToContinue()
	{
		AnsiConsole.Write("Press any key to continue...");
		Console.ReadLine();
	}

	public static int PromptPositiveInteger(string displayMessage, AllowZero limit)
	{
		var prompt = new TextPrompt<int>(displayMessage);
		prompt.ValidationErrorMessage("[red]Input must be an integer![/]");

		if (limit == AllowZero.True)
		{
			prompt.Validate(Validation.IsGreaterOrEqualToZero);
		}

		if (limit == AllowZero.False)
		{
			prompt.Validate(Validation.IsGreaterThanZero);
		}

		return AnsiConsole.Prompt(prompt);
	}

	public static double PromptPositiveDouble(string displayMessage, AllowEmpty option = AllowEmpty.False)
	{
		var prompt = new TextPrompt<double>(displayMessage);
		prompt.ValidationErrorMessage("[red]Input must be a decimal number![/]");
		prompt.Validate(Validation.IsGreaterThanZero);

		if (option == AllowEmpty.True)
		{
			prompt.DefaultValue(0);
		}

		return AnsiConsole.Prompt(prompt);
	}

	public static string PromptString(string displayMessage, AllowEmpty option, Validate validation = Validate.None)
	{
		var prompt = new TextPrompt<string>(displayMessage);

		if (option == AllowEmpty.True)
		{
			prompt.AllowEmpty();
		}

		if (validation == Validate.Email)
		{
			prompt.Validate(Validation.IsValidEmail);
		}

		return AnsiConsole.Prompt(prompt);
	}

	public static DateTime PromptDateTime(string displayMessage, AllowEmpty option)
	{
		var prompt = new TextPrompt<DateTime>(displayMessage)
		.ValidationErrorMessage("[red]Invalid format![/]")
		.Validate(Validation.IsValidDateTime);

		if (option == AllowEmpty.True)
		{
			prompt.AllowEmpty();
		}

		return AnsiConsole.Prompt(prompt);
	}

	public static int GetValidListIndex<T>(int input, List<T> list) where T : class
	{
		while (input > list.Count)
		{
			AnsiConsole.MarkupLine($"[red]Invalid input![/]");
			input = PromptPositiveInteger("Select a valid Id (or press 0 to exit):", AllowZero.True);
		}
		return input - 1;
	}

	public static List<Shift> GetShiftsToUpdate(List<Shift> allShifts, List<Shift> userShifts)
	{
		var prompt = new MultiSelectionPrompt<Shift>();
		prompt.Title($"Select shifts:")
				.NotRequired()
				.InstructionsText(
				"[grey]Shifts already associated with a user are[/] pre-selected!\n" +
				"[grey](Press [blue]<space>[/] to toggle a shift,[green]<enter>[/] to accept)[/]"
				)
				.AddChoices(allShifts)
				.UseConverter(s => s.ToString());


		foreach (var shift in allShifts)
		{
			if (userShifts.Any(s => s.Id == shift.Id))
			{
				prompt.Select(shift);
			}
		}

		AnsiConsole.WriteLine();
		return AnsiConsole.Prompt(prompt);
	}

	public static List<User> GetUsersToUpdate(List<User> allUsers, List<User> shiftUsers)
	{
		var prompt = new MultiSelectionPrompt<User>();
		prompt.Title("Select users:")
				.NotRequired()
				.InstructionsText(
				"[grey]Users already associated with a shift are[/] pre-selected!\n" +
				"[grey](Press [blue]<space>[/] to toggle a shift,[green]<enter>[/] to accept)[/]"
				)
				.AddChoices(allUsers)
				.UseConverter(u => u.ToString());

		foreach (var user in allUsers)
		{
			if (shiftUsers.Any(u => u.Id == user.Id))
			{
				prompt.Select(user);
			}
		}

		AnsiConsole.WriteLine();
		return AnsiConsole.Prompt(prompt);
	}
}

public enum AllowZero
{
	False,
	True
}

public enum AllowEmpty
{
	False,
	True
}

public enum Validate
{
	Email,
	DateTime,
	None
}