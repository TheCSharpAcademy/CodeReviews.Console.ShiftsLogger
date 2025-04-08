using Spectre.Console;
using UI.Models;
using UI.Service;

namespace UI.Control;

internal class UpdateShift
{
    public static void Update()
    {
        Helpers.PrintHeader();
        ShiftService shiftService = new();
        int id = AnsiConsole.Ask<int>("Enter the ID of the shift you want to update");
        Shift shift;

        try
        {
            shift = shiftService.GetShiftById(id).Result;
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[Red]There was a problem getting information from the server[/]");
            AnsiConsole.MarkupLine($"[Red]{e.Message}\n\n[/]");
            AnsiConsole.WriteLine("Press any key to return");
            Console.ReadLine();
            return;
        }
        if (shift == null)
        {
            AnsiConsole.MarkupLine($"[Red]Shift with ID {id} not found[/]\n\n");
            AnsiConsole.WriteLine("Press any key to return");
            Console.ReadLine();
            return;
        }
        AnsiConsole.WriteLine("Enter the new values for the shift\n\n");

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
            shiftService.UpdateShift(shift).Wait();
            AnsiConsole.MarkupLine($"[Green]Shift updated successfully[/]\n\n");
            AnsiConsole.WriteLine("Press any key to return");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[Red]There was a problem getting information from the server[/]");
            AnsiConsole.MarkupLine($"[Red]{e.Message}\n\n[/]");
            AnsiConsole.WriteLine("Press any key to return");
            Console.ReadLine();
            return;
        }
    }
}
