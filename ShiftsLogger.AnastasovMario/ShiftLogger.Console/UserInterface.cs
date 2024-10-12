using ShiftsLogger.API.Models.Shifts;
using Spectre.Console;

namespace ShiftLoggerConsoleUI
{
  public static class UserInterface
  {
    public static void ShowShifts(IEnumerable<ShiftDto> shifts)
    {
      var table = new Table();
      table.AddColumn("Name");
      table.AddColumn("Start Shift");
      table.AddColumn("End Shift");
      table.AddColumn("Duration");

      foreach (var shift in shifts)
      {
        table.AddRow(shift.Worker, shift.StartShift, shift.EndShift, shift.Duration.ToString());
      }

      AnsiConsole.Write(table);
    }
  }
}
