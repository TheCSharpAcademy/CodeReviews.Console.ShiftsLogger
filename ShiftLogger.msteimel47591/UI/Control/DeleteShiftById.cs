using Spectre.Console;
using UI.Service;

namespace UI.Control;

internal class DeleteShiftById
{
    public static void Delete()
    {
        Helpers.PrintHeader();
        bool isDeleted = false;
        ShiftService shiftService = new();

        int id = AnsiConsole.Ask<int>("Enter the ID of the shift you want to delete");

        try
        {
            isDeleted = shiftService.DeleteShift(id).Result;

            if (!isDeleted)
            {
                AnsiConsole.MarkupLine($"[Red]Shift with ID {id} not found[/]\n\n");
                AnsiConsole.WriteLine("Press any key to return");
                Console.ReadLine();
                return;
            }

            AnsiConsole.MarkupLine($"[Green]Shift deleted successfully[/]\n\n");
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
