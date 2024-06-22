using ShiftsLoggerUi.Services;
using Spectre.Console;

namespace ShiftsLoggerUi
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://localhost:44310/api/") };
        private static readonly ShiftService ShiftService = new ShiftService(Client);

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
                        await ShiftService.ListShifts();
                        break;
                    case "Start Shift":
                        await ShiftService.StartShift();
                        break;
                    case "End Shift":
                        await ShiftService.EndShift();
                        break;
                    case "Update Shift":
                        await ShiftService.UpdateShift();
                        break;
                    case "Delete Shift":
                        await ShiftService.DeleteShift();
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