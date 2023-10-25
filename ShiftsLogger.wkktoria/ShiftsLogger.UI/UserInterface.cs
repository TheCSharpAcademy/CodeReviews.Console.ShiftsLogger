using System.Text.RegularExpressions;
using ShiftsLogger.UI.Enums;
using ShiftsLogger.UI.Services;
using Spectre.Console;

namespace ShiftsLogger.UI;

public static partial class UserInterface
{
    public static void Start()
    {
        MainMenu();
    }

    private static void MainMenu()
    {
        var isAppRunning = true;

        while (isAppRunning)
        {
            Console.Clear();

            var option = GetOption(new List<string>
            {
                MainMenuOptions.ManageShifts.ToDescription(),
                MainMenuOptions.Quit.ToDescription()
            });

            switch (option)
            {
                case nameof(MainMenuOptions.ManageShifts):
                    ShiftsMenu();
                    break;
                case nameof(MainMenuOptions.Quit):
                    isAppRunning = false;
                    break;
            }
        }
    }

    private static void ShiftsMenu()
    {
        var goBack = false;

        while (!goBack)
        {
            Console.Clear();

            var option = GetOption(new List<string>
            {
                ShiftsMenuOptions.ViewAllShifts.ToDescription(),
                ShiftsMenuOptions.ViewShiftDetails.ToDescription(),
                ShiftsMenuOptions.AddShift.ToDescription(),
                ShiftsMenuOptions.GoBack.ToDescription()
            });

            switch (option)
            {
                case nameof(ShiftsMenuOptions.ViewAllShifts):
                    ShiftService.ShowShifts();
                    PressKeyToContinue();
                    break;
                case nameof(ShiftsMenuOptions.ViewShiftDetails):
                    ShiftService.ShowShiftDetails();
                    PressKeyToContinue();
                    break;
                case nameof(ShiftsMenuOptions.AddShift):
                    ShiftService.AddShift();
                    PressKeyToContinue();
                    break;
                case nameof(ShiftsMenuOptions.GoBack):
                    goBack = true;
                    break;
            }
        }
    }

    private static string GetOption(IEnumerable<string> choices)
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(choices)
        );

        return WhitespacesRegex().Replace(option, string.Empty);
    }

    private static void PressKeyToContinue()
    {
        AnsiConsole.Markup("\n[yellow]Press any key to continue...[/]");
        Console.ReadKey();
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex WhitespacesRegex();
}