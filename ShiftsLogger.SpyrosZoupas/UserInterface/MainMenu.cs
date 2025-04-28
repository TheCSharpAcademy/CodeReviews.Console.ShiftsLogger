using Spectre.Console;
using UserInterface.SpyrosZoupas.Util;

namespace UserInterface.SpyrosZoupas
{
    public class MainMenu
    {
        private readonly ShiftService _shiftService;

        public MainMenu(ShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        public void ShiftsMenu()
        {
            var isContactMenuRunning = true;
            while (isContactMenuRunning)
            {
                Console.Clear();
                var option = AnsiConsole.Prompt(
                new SelectionPrompt<ShiftMenuOptions>()
                .Title("Products Menu")
                .AddChoices(
                    ShiftMenuOptions.AddShift,
                    ShiftMenuOptions.DeleteShift,
                    ShiftMenuOptions.UpdateShift,
                    ShiftMenuOptions.ViewAllShifts,
                    ShiftMenuOptions.ViewShift,
                    ShiftMenuOptions.Quit));

                switch (option)
                {
                    case ShiftMenuOptions.AddShift:
                        _shiftService.InsertShift();
                        break;
                    case ShiftMenuOptions.DeleteShift:
                        _shiftService.DeleteShift();
                        break;
                    case ShiftMenuOptions.UpdateShift:
                        _shiftService.UpdateShift();
                        break;
                    case ShiftMenuOptions.ViewShift:
                        var shift = _shiftService.GetShift();
                        Console.WriteLine(shift.Result.ShiftId);
                        break;
                    case ShiftMenuOptions.ViewAllShifts:
                        _shiftService.GetAllShifts();
                        break;
                    case ShiftMenuOptions.Quit:
                        isContactMenuRunning = false;
                        break;
                }
            }
        }

        private void ShowShiftTable(object value)
        {
            throw new NotImplementedException();
        }

        private void ShowShift(object value)
        {
            throw new NotImplementedException();
        }
    }
}
