using ShiftsLoggerUi.Api;
using ShiftsLoggerUi.App;

namespace ShiftsLoggerUi;

public class Program
{
    static readonly Client client = new Client();
    static readonly EmployeesApi EmployeesApi = new(client);
    static readonly ShiftsApi ShiftsApi = new(client);
    static readonly ShiftsController shiftsController = new(ShiftsApi, EmployeesApi);
    static readonly EmployeesController EmployeesController = new(EmployeesApi, shiftsController);

    public static void Main()
    {
        StartApp().GetAwaiter().GetResult();
    }

    public static async Task StartApp()
    {
        var shouldExit = false;
        do
        {
            string response = MainMenu.Prompt();

            switch (response)
            {
                case MainMenu.ManageEmployees:
                    await EmployeesController.ManageEmployees();
                    break;
                case MainMenu.ManageShifts:
                    break;
                case MainMenu.Exit:
                    shouldExit = true;
                    break;
            }
        }
        while (shouldExit == false);

    }
}
