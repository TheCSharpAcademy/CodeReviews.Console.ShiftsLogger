using System.Globalization;
using ShiftLogger.Console_App.Models;
using Spectre.Console;

namespace ShiftLogger.Console_App;

public static class Validation
{
     public static DateTime? CheckDate(string point)
     {
          DateTime time;
          var start = AnsiConsole.Ask<string>($"[yellow bold]Enter {point} Time: [/]");

          while (!DateTime.TryParseExact(start, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
          {
               if (start == "0") return null;

               Console.Clear();
               AnsiConsole.MarkupLine("[red bold]Invalid time format! Please try again.[/]\n");
               AnsiConsole.MarkupLine("[grey](Tips: Don't Forget to add 'AM' or 'PM' after the input time) [/]\n");
               start = AnsiConsole.Ask<string>($"[yellow bold]Enter {point} Time: [/]");
          }

          return time;

     }

     public static bool CheckId(List<Shift> shifts, int id)
     {
          var shift = shifts.FirstOrDefault(s => s.Id == id);
          return shift == null;
     }

     public static bool UpdateConfirm(string title, string color)
     {
          var confirmation = AnsiConsole.Prompt(
                new TextPrompt<bool>($"[{color} bold]{title}[/]")
               .AddChoice(true)
               .AddChoice(false)
               .DefaultValue(true)
               .WithConverter(choice => choice ? "y" : "n"));

          return confirmation;
     }
}
