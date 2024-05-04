﻿using ShiftLoggerConsoleApp;
using ShiftLoggerConsoleApp.UI;
using Spectre.Console;

enum MenuOptions
{
    AddShift,
    DeleteShift,
    UpdateShift,
    ViewShift,
    ViewAllShifts,
    Quit
}

public static class Application
{
    public static async Task Main()
    {
        bool isAppRunning = true;
        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MenuOptions.AddShift,
                MenuOptions.DeleteShift,
                MenuOptions.UpdateShift,
                MenuOptions.ViewAllShifts,
                MenuOptions.ViewShift,
                MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddShift:
                    break;
                case MenuOptions.DeleteShift:
                    break;
                case MenuOptions.UpdateShift:
                    break;
                case MenuOptions.ViewShift:
                    var shifts = await ShiftLoggerService.GetShiftByName();
                    UserInterface.ShowShifts(shifts);
                    break;
                case MenuOptions.ViewAllShifts:
                    shifts = await ShiftLoggerService.GetShifts();
                    UserInterface.ShowShifts(shifts);
                    break;
                case MenuOptions.Quit:
                    isAppRunning = false;
                    break;
            }
        }
    }
}
