using Spectre.Console;

namespace STUDY.ASP.ShiftLoggerTryThreeUserInterface;
internal class MainMenu
{
    public static void ShowMainMenu(HttpClient client, string ApiBaseUrl)
    {
        Console.WriteLine("Welcome to Shift Logger User Interface");

        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices(@"View All Shift Logs",
                        "View Specific Shift Log",
                        "Add Shift Log",
                        "Update Shift Log",
                        "Delete Shift Log",
                        "Quit"));

            switch (choice)
            {
                case "View All Shift Logs":
                    ProgramEngine.ViewAllShiftLogs(client, ApiBaseUrl);
                    break;
                case "View Specific Shift Log":
                    ProgramEngine.ViewSpecificShiftLog(client, ApiBaseUrl);
                    break;
                case "Add Shift Log":
                    ProgramEngine.AddShiftLog(client, ApiBaseUrl);
                    break;
                case "Update Shift Log":
                    ProgramEngine.UpdateShiftLog(client, ApiBaseUrl);
                    break;
                case "Delete Shift Log":
                    ProgramEngine.DeleteShiftLog(client, ApiBaseUrl);
                    break;
                case "Quit":
                    Environment.Exit(0);
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
