
using Spectre.Console;
using WorkerShiftsUI.Services;

namespace WorkerShiftsUI.Views
{
    public class ShiftsView : IShiftView
    {
        private readonly IShiftService? _shiftService;

        public ShiftsView(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        public async Task ShiftsMenu()
        {
            var isShiftViewRunning = true;

            while (isShiftViewRunning)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Shift Logger Shifts Menu")
                    .AddChoices(
                        "View Shifts",
                        "Add Shifts",
                        "Update Shifts",
                        "Delete Shifts",
                        "Go Back")
                    .UseConverter(x => x.ToString())
                );

                switch (choice)
                {
                    case "View Shifts":
                        await _shiftService!.ViewShifts();
                        break;
                    case "Add Shifts":
                        await _shiftService!.AddShift();
                        break;
                    case "Update Shifts":
                        await _shiftService!.UpdateShift();
                        break;
                    case "Delete Shifts":
                        await _shiftService!.DeleteShift();
                        break;
                    case "Go Back":
                        isShiftViewRunning = false;
                        return;
                }
            }
        }
    }
}