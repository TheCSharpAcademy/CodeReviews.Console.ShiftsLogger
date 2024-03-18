using Spectre.Console;
using ShiftsLogger.frockett.UI.Helpers;
using ShiftsLogger.frockett.UI.Dtos;

namespace ShiftsLogger.frockett.UI.UI;

public class UserInput
{
    private readonly InputValidation inputValidation;

    public UserInput(InputValidation inputValidation)
    {
        this.inputValidation = inputValidation;
    }

    public EmployeeCreateDto GetNewEmployee()
    {
        string name = AnsiConsole.Ask<string>("Enter employee's name: ");

        EmployeeCreateDto employee = new EmployeeCreateDto();
        employee.Name = name;
        return employee;
    }

    public ShiftCreateDto GetNewShift()
    {
        throw new NotImplementedException();
    }

}
