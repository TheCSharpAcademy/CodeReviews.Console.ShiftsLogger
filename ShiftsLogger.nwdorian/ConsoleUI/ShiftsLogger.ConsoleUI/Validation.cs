using System.Net.Mail;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class Validation
{
	public static ValidationResult IsValidEmail(string input)
	{
		if (string.IsNullOrWhiteSpace(input))
		{
			return ValidationResult.Success();
		}

		try
		{
			var email = new MailAddress(input);
			return ValidationResult.Success();
		}
		catch
		{
			return ValidationResult.Error("[red]Invalid email format! (example: username@domain.com)[/]");

		}

	}

	public static ValidationResult IsValidDateTime(DateTime input)
	{
		if (input == DateTime.MinValue)
		{
			return ValidationResult.Success();
		}

		var timeOnly = new TimeOnly(input.Hour, input.Minute);
		if (timeOnly.Hour == 0 && timeOnly.Minute == 0)
		{
			return ValidationResult.Error("[red]Start time was not set![/]");
		}
		return ValidationResult.Success();
	}

	public static ValidationResult IsGreaterOrEqualToZero(int input)
	{
		return input switch
		{
			< 0 => ValidationResult.Error("[red]Input must be greater than or equal to 0![/]"),
			_ => ValidationResult.Success()
		};
	}

	public static ValidationResult IsGreaterThanZero(int input)
	{
		return input switch
		{
			< 1 => ValidationResult.Error("[red]Input must be greater than 0![/]"),
			_ => ValidationResult.Success()
		};
	}

	public static ValidationResult IsGreaterThanZero(double input)
	{
		return input switch
		{
			<= 0 => ValidationResult.Error("[red]Input must be greater than 0![/]"),
			_ => ValidationResult.Success()
		};
	}
}
