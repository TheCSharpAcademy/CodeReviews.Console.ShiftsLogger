using ShiftsLoggerUi.Api;
using ShiftsLoggerUi.Api.Employees;
using ShiftsLoggerUi.App.Utils;
using Spectre.Console;

namespace ShiftsLoggerUi.App;

public class EmployeesController
{
    public EmployeesApi EmployeesApi { get; set; }
    ShiftsController ShiftsController { get; set; }
    public EmployeeCardController EmployeeCardController { get; set; }

    public EmployeesController(EmployeesApi employeesApi, ShiftsController shiftsController)
    {
        EmployeesApi = employeesApi;
        ShiftsController = shiftsController;
        EmployeeCardController = new EmployeeCardController(EmployeesApi, ShiftsController);
    }
    public async Task ManageEmployees()
    {
        while (true)
        {
            var (success, employees) = await EmployeesApi.GetEmployees();

            if (!success || employees == null)
            {
                App.Utils.Text.Error("Could not fetch");
                ConsoleUtil.PressAnyKeyToClear(
                    "Press any key to go back"
                );
                return;
            }

            var selectedEmployee = await SelectEmployee(employees);

            if (selectedEmployee == null)
            {
                break;
            }

            await EmployeeCardController.ShowEmployeeCard(selectedEmployee);
        }
    }

    public async Task<EmployeeDto?> SelectEmployee(List<EmployeeDto> employees)
    {
        var backButton = new EmployeeDto(-1, ConsoleUtil.MenuBackButtonText, []);
        var newEmployeeButton = new EmployeeDto(-2, "[green]Create new employee[/]", []);

        var selectedEmployee = AnsiConsole.Prompt(
            new SelectionPrompt<EmployeeDto>()
                .Title("E M P L O Y E E S")
                .UseConverter(item =>
                {
                    var isBackButton = backButton.Equals(item);
                    var isNewButton = newEmployeeButton.Equals(item);

                    var idCol = isBackButton ? "[red]<-[/]" :
                        isNewButton ? "[red]+[/]" :
                        item.EmployeeId.ToString();

                    return $"{idCol}    {item.Name}";
                })
                .AddChoices([
                    backButton,
                    newEmployeeButton,
                    ..employees
                ])
                .EnableSearch()
        );

        if (
            selectedEmployee == null ||
            selectedEmployee.Equals(backButton)
        )
        {
            return null;
        }

        if (selectedEmployee.Equals(newEmployeeButton))
        {
            return await CreateEmployee();
        }

        return selectedEmployee;
    }


    public async Task<EmployeeDto?> CreateEmployee()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Full name? ")
        );

        var result = await EmployeesApi.CreateEmployee(new EmployeeCreateDto(name));
        var createdEmployee = result.Data;

        if (result.Success && createdEmployee != null)
        {
            AnsiConsole.MarkupLine(
                $"Employee [green] ID {result?.Data?.EmployeeId} created[/]");
            return createdEmployee;
        }
        else
        {
            AnsiConsole.MarkupLine($"{result?.Message ?? "Error"}");
            return null;
        }
    }
}
