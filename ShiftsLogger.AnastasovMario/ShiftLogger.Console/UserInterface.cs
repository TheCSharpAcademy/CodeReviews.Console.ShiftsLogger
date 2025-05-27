using ShiftsLogger.API.Models.Shifts;
using Spectre.Console;

namespace ShiftLoggerConsoleUI
{
  public static class UserInterface
  {
    public static void ShowShift(ShiftDto shift)
    {
      var table = new Table();
      table.AddColumn("Name");
      table.AddColumn("Start Shift");
      table.AddColumn("End Shift");
      table.AddColumn("Duration");

      string durationText;
      if (shift.Duration.Days == 1)
      {
        durationText = $"{shift.Duration.Days} day {shift.Duration.Hours:d2}:{shift.Duration.Minutes:D2}:{shift.Duration.Seconds:d2}";
      }
      else if (shift.Duration.Days > 1)
      {
        durationText = $"{shift.Duration.Days} days {shift.Duration.Hours:d2}:{shift.Duration.Minutes:D2}:{shift.Duration.Seconds:d2}";
      }
      else
      {
        durationText = shift.Duration.ToString();
      }

      table.AddRow(shift.Worker, shift.StartShift, shift.EndShift, durationText);
      AnsiConsole.Write(table);
    }

    public static void ShowShifts(IEnumerable<ShiftDto> shifts)
    {
      var table = new Table();
      table.AddColumn("Name");
      table.AddColumn("Start Shift");
      table.AddColumn("End Shift");
      table.AddColumn("Duration");

      foreach (var shift in shifts)
      {
        string durationText;
        if (shift.Duration.Days == 1)
        {
          durationText = $"{shift.Duration.Days} day {shift.Duration.Hours:d2}:{shift.Duration.Minutes:D2}:{shift.Duration.Seconds:d2}";
        }
        else if (shift.Duration.Days > 1)
        {
          durationText = $"{shift.Duration.Days} days {shift.Duration.Hours:d2}:{shift.Duration.Minutes:D2}:{shift.Duration.Seconds:d2}";
        }
        else
        {
          durationText = shift.Duration.ToString();
        }

        table.AddRow(shift.Worker, shift.StartShift, shift.EndShift, durationText);
      }
      AnsiConsole.Write(table);
    }
  }
}
