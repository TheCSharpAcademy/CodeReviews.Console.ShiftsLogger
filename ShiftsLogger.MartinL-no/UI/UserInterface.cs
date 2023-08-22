using Spectre.Console;

using static UI.Enums;

namespace UI;

static internal class UserInterface
{
    static public void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MainMenuOptions.AddShift,
                MainMenuOptions.UpdateShift,
                MainMenuOptions.DeleteShift,
                MainMenuOptions.ViewAllShifts,
                MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.AddShift:
                    AddShift();
                    break;
                case MainMenuOptions.UpdateShift:
                    UpdateShift();
                    break;
                case MainMenuOptions.DeleteShift:
                    DeleteShift();
                    break;
                case MainMenuOptions.ViewAllShifts:
                    ViewAllShifts();
                    break;
                case MainMenuOptions.Quit:
                    Console.WriteLine("Goodbye");
                    isAppRunning = false;
                    break;
            }
        }
    }

    private static void ViewAllShifts()
    {
        throw new NotImplementedException();
    }

    private static void DeleteShift()
    {
        throw new NotImplementedException();
    }

    private static void UpdateShift()
    {
        throw new NotImplementedException();
    }

    private static void AddShift()
    {
        throw new NotImplementedException();
    }
}