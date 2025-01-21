using Spectre.Console;

namespace ShiftsLoggerUI;

internal class UserInterface
{
	internal static void ShowShift(ShiftModel shift)
	{
		var panel = new Panel($@"Id: {shift.Id}
Employee's id: {shift.EmployeeId}
Shift's start: {shift.StartTime}
Shift's end: {shift.EndTime}
Shift's type: {shift.ShiftType}
Duraton: {shift.Duration}");
		panel.Header = new PanelHeader("Contact Information");
		panel.Padding = new Padding(2, 2, 2, 2);

		AnsiConsole.Write(panel);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
	}

	internal static void ShowShiftsTable(List<ShiftModel> shifts)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Employee's Id");
		table.AddColumn("Start");
		table.AddColumn("End");
		table.AddColumn("Type");
		table.AddColumn("Duration");

		foreach (var shift in shifts)
		{
			string formattedDuration = shift.Duration.ToString(@"hh\:mm");
			table.AddRow(shift.Id.ToString(), shift.EmployeeId.ToString(), shift.StartTime.ToString(), shift.EndTime.ToString(), shift.ShiftType, formattedDuration);
		}

		AnsiConsole.Write(table);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
	}

	internal static async Task UpdateInputAsync(ShiftModel shift)
	{
		shift.EmployeeId = AnsiConsole.Confirm("Update employee's Id?")
		? AnsiConsole.Ask<int>("Enter a new Id: ")
		: shift.EmployeeId;
		shift.StartTime = AnsiConsole.Confirm("Update start time?")
				? AnsiConsole.Ask<DateTime>("Enter a new start time: ")
				: shift.StartTime;
		shift.EndTime = AnsiConsole.Confirm("Update end time?")
			? AnsiConsole.Ask<DateTime>("Enter a new end time: ")
			: shift.EndTime;
		shift.ShiftType = AnsiConsole.Confirm("Update shift type?")
			? AnsiConsole.Ask<string>("Enter a new shift type: ")
			: shift.ShiftType;
		shift.Notes = AnsiConsole.Confirm("Update notes?")
			? AnsiConsole.Ask<string>("Enter new notes: ")
			: shift.Notes;


		await UIController.UpdateAsync(shift);
		AnsiConsole.MarkupLine("[green]Shift updated successfully![/]");
	}
}
