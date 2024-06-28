using ShiftsLoggerUi.Api;
using ShiftsLoggerUi.App;
using Spectre.Console;

namespace ShiftsLoggerUi;

public class Program
{
    static readonly Client client = new Client();
    static readonly EmployeesApi EmployeesApi = new(client);
    static readonly ShiftsApi ShiftsApi = new(client);
    static readonly ShiftsController ShiftsController = new(ShiftsApi, EmployeesApi);
    static readonly EmployeesController EmployeesController = new(EmployeesApi, ShiftsController);

    public static void Main()
    {
        StartApp().GetAwaiter().GetResult();
    }

    public static async Task StartApp()
    {
        var shouldExit = false;
        do
        {
            Console.Clear();
            AnsiConsole.MarkupLine("\t\tS H I F T S     L O G G E R");

            string response = MainMenu.Prompt();

            switch (response)
            {
                case MainMenu.ManageEmployees:
                    await EmployeesController.ManageEmployees();
                    break;
                case MainMenu.ManageShifts:
                    await ShiftsController.ManageAllShifts();
                    break;
                case MainMenu.Exit:
                    shouldExit = true;
                    break;
            }
        }
        while (shouldExit == false);

        Console.Clear();
    }
}
