using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI;

internal static class ConsoleEngine
{
  internal static string GetChoiceSelector(string title, string[] choices)
  {
    SelectionPrompt<string> prompt = new SelectionPrompt<string>()
                                     .Title(title)
                                     .AddChoices(choices)
                                     .HighlightStyle(Color.Cyan1);

    string choice = AnsiConsole.Prompt(prompt);

    return choice;
  }

  internal static bool ShowShiftsTable(List<Shift>? shifts)
  {
    if (shifts == null)
    {
      AnsiConsole.Markup("[red]Shifts not found.[/] Try to create one first.");
      return false;
    }

    Table table = new();
    table.Title("SHIFTS");
    table.AddColumn(new TableColumn("[cyan1]ID[/]"));
    table.AddColumn(new TableColumn("[cyan1]Employee[/]"));
    table.AddColumn(new TableColumn("[cyan1]Start Date[/]"));
    table.AddColumn(new TableColumn("[cyan1]End Date[/]"));

    foreach (Shift shift in shifts)
    {
      table.AddRow(shift.ShiftId.ToString(), shift.EmployeeName, shift.StartDate.ToString("dd-MM-yyyy HH:mm"), shift.EndDate.ToString("dd-MM-yyyy HH:mm"));
    }

    AnsiConsole.Write(table);
    return true;
  }

  internal static void ShowTitle()
  {
    AnsiConsole.Clear();

    Rule rule = new("Shifts Logger");
    rule.Centered().HeavyBorder().Style = new Style(Color.Cyan1);

    AnsiConsole.Write(rule);
  }
}