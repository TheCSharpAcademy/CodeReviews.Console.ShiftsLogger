using ShiftsLoggerUi.Services;
using Spectre.Console;

namespace ShiftsLoggerUi
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:44310/api/") };
        private static readonly ShiftService shiftService = new ShiftService(client);

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[blue]What do you want to do?[/]");
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices("List Shifts", "Start Shift", "End Shift", "Update Shift", "Delete Shift", "Exit"));

                switch (choice)
                {
                    case "List Shifts":
                        await shiftService.ListShifts();
                        break;
                    case "Start Shift":
                        await shiftService.StartShift();
                        break;
                    case "End Shift":
                        await shiftService.EndShift();
                        break;
                    case "Update Shift":
                        await shiftService.UpdateShift();
                        break;
                    case "Delete Shift":
                        await shiftService.DeleteShift();
                        break;
                    case "Exit":
                        return;
                }

                AnsiConsole.MarkupLine("[blue]Press any key to continue...[/]");
                Console.ReadKey();
            }
        }
    }
}