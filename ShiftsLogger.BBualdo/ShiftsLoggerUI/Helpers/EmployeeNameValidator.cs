using Spectre.Console;

namespace ShiftsLoggerUI.Helpers;

internal static class EmployeeNameValidator
{
  internal static bool IsValid(string name)
  {
    if (int.TryParse(name, out _))
    {
      AnsiConsole.Markup("\n[red]Name can't be numeric value.[/]\n");
      return false;
    }

    return true;
  }
}