using Spectre.Console;

namespace ShiftLogger.Console.Handlers
{
  public static class Validator
  {
    public static DateTime ValidateDate(string shift)
    {

      if (!DateTime.TryParseExact(shift, "MM/dd/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime shiftDatetime))
      {
        AnsiConsole.MarkupLine("[red]Invalid start date format. Please use MM/dd/yyyy HH:mm.[/]");

        ValidateDate(AnsiConsole.Ask<string>("Enter shift in the correct format (MM/dd/yyyy HH:mm): "));
      }

      return shiftDatetime;
    }

    public static bool ValidateDuration(DateTime startDateTime, DateTime endDateTime)
    {
      if (endDateTime <= startDateTime)
      {
        AnsiConsole.MarkupLine("[red]End shift must be after the start shift.[/]");
        return false;
      }

      return true;
    }
  }
}
