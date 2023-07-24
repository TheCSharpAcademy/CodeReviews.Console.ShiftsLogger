using Spectre.Console;

namespace ShiftLoggerApp;
public class Program
{
    public static bool isAppRunning = true;
    public static void Main(string[] args)
    {
        DisplayMainMenu();
    }

    public static void DisplayMainMenu()
    {
        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>().Title("Shift Logger \n\nWelcome to Shift Logger! What do you want to do?")
                .AddChoices(
                    MenuOptions.AddShift,
                    MenuOptions.DeleteShift,
                    MenuOptions.UpdateShift,
                    MenuOptions.ViewShift,
                    MenuOptions.ViewAllYourShifts,
                    MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddShift:
                    ShfitLoggerService.CreateShift();
                    break;
                case MenuOptions.DeleteShift:
                    ShfitLoggerService.DeleteShift();
                    break;
                case MenuOptions.UpdateShift:
                    ShfitLoggerService.UpdateShift();
                    break;
                case MenuOptions.ViewShift:
                    ShfitLoggerService.ReadShift();
                    break;
                case MenuOptions.ViewAllYourShifts:
                    ShfitLoggerService.ReadShifts();
                    Console.WriteLine("\nEnter any key to go back to the main menu.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case MenuOptions.Quit:
                    isAppRunning = false;
                    Console.WriteLine("You quit the app successfully!");
                    break;
            }
        }
    }

}

enum MenuOptions
{
    AddShift,
    DeleteShift,
    UpdateShift,
    ViewShift,
    ViewAllYourShifts,
    Quit
}

enum YesNoOptions
{
    Yes,
    No
}