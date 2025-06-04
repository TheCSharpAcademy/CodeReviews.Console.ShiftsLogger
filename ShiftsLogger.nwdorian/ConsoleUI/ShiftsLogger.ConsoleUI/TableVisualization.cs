using ShiftsLogger.ConsoleUI.Models;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class TableVisualization
{
	public static void DisplayUsersTable(List<User> users)
	{
		var table = new Table();
		table.Title = new TableTitle("Users Table", "bold");
		table.AddColumns("Id", "Name", "Email");

		foreach (var (index, user) in users.Index())
		{
			table.AddRow((index + 1).ToString(), user.ToString(), user.Email ?? "Email not found!");
		}
		AnsiConsole.Clear();
		AnsiConsole.Write(table);
	}

	public static void DisplayUserTable<T>(T user) where T : IUser
	{
		var table = new Table();
		table.AddColumns("Name", "Email");
		table.AddRow(user.ToString() ?? "Name not found", user.Email ?? "Email not found");

		AnsiConsole.Clear();
		AnsiConsole.Write(table);
	}

	public static void DisplayShiftsTable(List<Shift> shifts)
	{
		var table = new Table();
		table.Title = new TableTitle("Shifts");
		table.AddColumns("Id", "Start date", "Start time", "End date", "End time");

		foreach (var (index, shift) in shifts.Index())
		{
			table.AddRow
					(
					(index + 1).ToString(),
					shift.StartTime.ToShortDateString(),
					shift.StartTime.ToShortTimeString(),
					shift.EndTime.ToShortDateString(),
					shift.EndTime.ToShortTimeString()
					);
		}
		AnsiConsole.Write(table);
	}

	public static void DisplayShiftTable<T>(T shift) where T : IShift
	{
		var table = new Table();
		table.AddColumns("Start date", "Start time", "End date", "End time");
		table.AddRow
				(
				shift.StartTime.ToShortDateString(),
				shift.StartTime.ToShortTimeString(),
				shift.EndTime.ToShortDateString(),
				shift.EndTime.ToShortTimeString()
				);

		AnsiConsole.Clear();
		AnsiConsole.Write(table);
	}

	public static void DisplayUserDetailsTable(User user, List<Shift> shifts)
	{
		var shiftsTable = new Table();
		shiftsTable.Border = TableBorder.MinimalHeavyHead;
		shiftsTable.AddColumns("Start date", "Start time", "End date", "End time");

		foreach (var shift in shifts)
		{
			shiftsTable.AddRow
					(
					shift.StartTime.ToShortDateString(),
					shift.StartTime.ToShortTimeString(),
					shift.EndTime.ToShortDateString(),
					shift.EndTime.ToShortTimeString()
					);
		}

		var panel = new Panel(shiftsTable);
		panel.Header = new PanelHeader(user.ToString()).Centered();
		AnsiConsole.Clear();
		AnsiConsole.Write(panel);
	}

	internal static void DisplayShiftDetailsTable(Shift shift, List<User> shiftUsers)
	{
		var usersTable = new Table();
		usersTable.Border = TableBorder.MinimalHeavyHead;
		usersTable.AddColumns("Name", "Email");

		foreach (var user in shiftUsers)
		{
			usersTable.AddRow(user.ToString(), user.Email ?? "Email not found!");
		}

		var panel = new Panel(usersTable);
		panel.Header = new PanelHeader(shift.ToString()).Centered();
		AnsiConsole.Clear();
		AnsiConsole.Write(panel);
	}
}
