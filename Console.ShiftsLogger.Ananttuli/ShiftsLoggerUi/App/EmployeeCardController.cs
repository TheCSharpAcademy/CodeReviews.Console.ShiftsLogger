using ShiftsLoggerUi.Api;
using ShiftsLoggerUi.Api.Employees;
using ShiftsLoggerUi.Api.Shifts;
using ShiftsLoggerUi.App.Utils;
using Spectre.Console;

namespace ShiftsLoggerUi.App;

public class EmployeeCardController(EmployeesApi EmployeesApi, ShiftsController ShiftsController)
{
    public async Task ShowEmployeeCard(EmployeeDto employeeItem)
    {
        var keepCardOpen = true;

        while (keepCardOpen)
        {
            Console.Clear();

            var (_, employeeWithDetails) = await EmployeesApi.GetEmployee(employeeItem.EmployeeId);

            var employee = employeeWithDetails ?? employeeItem;

            RenderEmployeeTables(employee);

            keepCardOpen = await ShowEmployeeCardOps(employee);
        }
    }

    private void RenderEmployeeTables(EmployeeDto employee)
    {
        var infoTable = new Table();

        infoTable.AddColumns(["Employee ID", "Name"]);

        infoTable.AddRow([
            employee.EmployeeId.ToString(),
            employee.Name,
        ]);

        AnsiConsole.Write(infoTable);

        var shiftsTable = new Table();

        shiftsTable.AddColumns(["Start time", "End time", "Duration"]);

        foreach (var shift in employee.Shifts)
        {
            shiftsTable.AddRow(ShiftCardController.GetShiftRow(shift));
        }

        if (employee.Shifts.Count > 0)
        {
            AnsiConsole.Write(shiftsTable);
        }
        else
        {
            Utils.Text.Error("No shifts recorded");
        }
    }

    public async Task<bool> ShowEmployeeCardOps(EmployeeDto employee)
    {
        const string backButton = "Back";
        const string editEmployee = "Edit Employee";
        const string deleteEmployee = "Delete Employee";
        const string logShift = "Log new shift";
        const string manageShiftsForEmployee = "Edit/delete past shifts";

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices([
                    backButton,
                    editEmployee,
                    deleteEmployee,
                    logShift,
                    manageShiftsForEmployee
                ])
        );

        switch (option)
        {
            case backButton:
                Console.Clear();
                return false;
            case editEmployee:
                await UpdateEmployee(employee);
                break;
            case deleteEmployee:
                await DeletedEmployee(employee.EmployeeId);
                Console.Clear();
                return false;
            case logShift:
                await ShiftsController.CreateShift(employee.EmployeeId);
                break;
            case manageShiftsForEmployee:
                await ManageEmployeeShifts(employee);
                break;
            default:
                return false;
        }

        ConsoleUtil.PressAnyKeyToClear();
        return true;
    }

    public async Task ManageEmployeeShifts(EmployeeDto employee)
    {
        var keepManageMenuOpen = true;

        do
        {
            var latestShifts = await FetchEmployeeShiftDtos(employee.EmployeeId);
            keepManageMenuOpen = await ShiftsController.ManageShifts(latestShifts);
        } while (keepManageMenuOpen);
    }

    public async Task UpdateEmployee(EmployeeDto employee)
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Full name? ")
            .DefaultValue(employee.Name ?? "")
        );

        var result = await EmployeesApi.UpdateEmployee(new EmployeeUpdateDto(employee.EmployeeId, name));

        if (result.Success)
        {
            AnsiConsole.MarkupLine($"Employee [green]ID {result?.Data?.EmployeeId} updated[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"{result?.Message ?? "Error"}");
        }
    }

    private async Task DeletedEmployee(int id)
    {
        var deleteResult = await EmployeesApi.DeleteEmployee(id);
        AnsiConsole.MarkupLine(deleteResult.Success ?
            "Deleted" :
            deleteResult.Message ?? "Error"
        );
    }

    private async Task<List<ShiftDto>> FetchEmployeeShiftDtos(int employeeId)
    {
        var (_, employeeWithDetails) = await EmployeesApi.GetEmployee(employeeId);
        return employeeWithDetails?.Shifts.Select(s =>
            new ShiftDto(
                s.ShiftId, s.StartTime, s.EndTime,
                new EmployeeCoreDto(
                    employeeWithDetails.EmployeeId,
                    employeeWithDetails.Name
                ),
                s.Duration
            )
        ).ToList() ?? [];
    }
}