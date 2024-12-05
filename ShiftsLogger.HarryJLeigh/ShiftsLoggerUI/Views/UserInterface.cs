using Enums;
using ShiftsLoggerUI.Controllers;
using Spectre.Console;
using ShiftsLoggerUI.Services;
using ShiftsLoggerUI.Utilities;

namespace ShiftsLoggerUI.Views;

public class UserInterface
{
    private readonly ShiftService _shiftService = new ShiftService(new ShiftController());

    internal void Run()
    {
        bool endApp = false;
        while (!endApp)
        {
            Console.Clear();
            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                    .Title("What would you like to do?")
                    .AddChoices(Enum.GetValues<MenuOption>()));

            AnsiConsole.MarkupLine($"You selected: {menuChoice}");

            switch (menuChoice)
            {
                case MenuOption.View:
                    _shiftService.ViewAllShifts();
                    Util.AskUserToContinue();
                    break;
                case MenuOption.Create:
                    _shiftService.CreateShift();
                    break;
                case MenuOption.Update:
                    _shiftService.UpdateShift();
                    break;
                case MenuOption.Delete:
                    _shiftService.DeleteShift();
                    break;
                case MenuOption.Exit:
                    endApp = true;
                    break;
            }
        }
    }
}