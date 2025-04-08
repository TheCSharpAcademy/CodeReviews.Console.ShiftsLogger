using Spectre.Console;
using UI.Models;
using UI.Service;

namespace UI.Control;

internal class AddShift
{
    public static void Insert()
    {
        Shift shift = new Shift();

        shift.Employee = Helpers.GetName();
        bool isValid = false;

        while (!isValid)
        {
            shift.StartTime = AnsiConsole.Ask<DateTime>("Enter start time. Format is MM/DD/YY HR:MM AM or PM");
            shift.EndTime = AnsiConsole.Ask<DateTime>("Enter end time. Format is MM/DD/YY HR:MM AM or PM");

            if (shift.StartTime < shift.EndTime)
            {
                isValid = true;
            }
            else
            {
                AnsiConsole.MarkupLine("[Red]End time must be after start time[/]\n");
            }
        }
        shift.Duration = shift.EndTime - shift.StartTime;

        try
        {
            ShiftService shiftService = new();
            shiftService.AddShift(shift).Wait();
            AnsiConsole.MarkupLine($"[Green]Shift added successfully[/]\n\n");
            AnsiConsole.WriteLine("Press any key to return");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[Red]There was a problem adding the shift[/]");
            AnsiConsole.MarkupLine($"[Red]{e.Message}\n\n[/]");
            AnsiConsole.WriteLine("Press any key to return");
            Console.ReadLine();
            return;
        }
    }
}
