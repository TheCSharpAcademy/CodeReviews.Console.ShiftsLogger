using ShiftLoggerClient.ClientControllers;
using ShiftLoggerClient.Models;
using Spectre.Console;
using static ShiftLoggerClient.Models.Enums;

namespace ShiftLoggerClient.ClientServices;

internal class EmployeeClientService
{
    internal static EmployeeDTO AddEmployeeService()
    {
        var newEmployee = new EmployeeDTO();
        newEmployee.FirstName = AnsiConsole.Ask<string>("First Name:");
        newEmployee.LastName = AnsiConsole.Ask<string>("Last Name:");
        newEmployee = EmployeeClientController.AddEmployeeDTO(newEmployee);
        return newEmployee;
    }

   

    internal static void EmployeeTable(List<EmployeeDTO> employees)
    {
        var table = new Spectre.Console.Table();

        table.AddColumns("ID", "FirstName", "LastName");

        foreach (var employee in employees)
        {
            table.AddRow($@"{employee.Id}",
                         $@"{employee.FirstName}",
                         $@"{employee.LastName}");
        }
        AnsiConsole.Write(table);
    }

    internal static void SingleEmployeeTable(EmployeeDTO employee)
    {
        var table = new Spectre.Console.Table();

        table.AddColumns("ID", "FirstName", "LastName");

        table.AddRow($@"{employee.Id}",
                     $@"{employee.FirstName}",
                     $@"{employee.LastName}");

        AnsiConsole.Write(table);
    }

    internal static void UpdateEmployeeService()
    {
        var exitMenu = false;
        while (!exitMenu)
        {
            Console.WriteLine("Please enter the ID for the employee you would like to update.");
            int employeeId = AnsiConsole.Ask<int>("Employee Id:");

            var employee = EmployeeClientController.GetEmployeeDTO(employeeId);
            Console.Clear();
            if (employee.Id == 0)
            {
                Console.WriteLine($"An Employee with the ID: {employeeId} Does not exist.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                return;
            }
            else
            {
                SingleEmployeeTable(employee);

                var option = AnsiConsole.Prompt(
                       new SelectionPrompt<ContinueBackOptions>().Title("Is this the correct employee?")
                       .AddChoices(
                       ContinueBackOptions.Continue,
                       ContinueBackOptions.Back));

                switch (option)
                {
                    case ContinueBackOptions.Continue:
                        UpdateUserInput(employee);
                        Console.WriteLine("The Employee has been updated.");
                        exitMenu = true;
                        Console.ReadKey();
                        break;
                    case ContinueBackOptions.Back:
                        exitMenu = true;
                        break;

                }
            }
        }
    }

    internal static void UpdateUserInput(EmployeeDTO employeeToUpdate)
    {
        employeeToUpdate.FirstName = AnsiConsole.Confirm("Update First Name?")
            ? employeeToUpdate.FirstName = AnsiConsole.Ask<string>("Employee's First Name")
            : employeeToUpdate.FirstName;
        employeeToUpdate.LastName = AnsiConsole.Confirm("Update Last Name ?")
            ? employeeToUpdate.LastName = AnsiConsole.Ask<string>("Employee's Last Name")
            : employeeToUpdate.LastName;

        EmployeeClientController.ModifyEmployeeDTO(employeeToUpdate);
    }

    internal static void DeleteEmployeeService()
    {

        var exitMenu = false;
        while (!exitMenu)
        {
            Console.WriteLine("Please enter the Id for the employee you would like to delete.");
            int employeeId = AnsiConsole.Ask<int>("Employee Id:");

            var employee = EmployeeClientController.GetEmployeeDTO(employeeId);
            Console.Clear();
            if (employee.Id == 0)
            {
                Console.WriteLine($"A shift with the ID of {employeeId} does not exist");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                return;
            }
            else
            {
                SingleEmployeeTable(employee);

                var option = AnsiConsole.Prompt(
                       new SelectionPrompt<ContinueBackOptions>().Title("Is this the correct employee?")
                       .AddChoices(
                       ContinueBackOptions.Continue,
                       ContinueBackOptions.Back));

                switch (option)
                {
                    case ContinueBackOptions.Continue:
                        EmployeeClientController.DeleteEmployeeDTO(employee);
                        Console.WriteLine("The Employee has been deleted.");
                        exitMenu = true;
                        Console.ReadKey();
                        break;
                    case ContinueBackOptions.Back:
                        exitMenu = true;
                        break;

                }
            }

        }

    }
}
