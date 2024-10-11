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

      foreach (var shift in shifts)
      {
        table.AddRow(shift.Worker, shift.StartShift, shift.EndShift);
      }

      AnsiConsole.Write(table);
    }
  }
}
