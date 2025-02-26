using ShiftLoggerUi.Services;
using Spectre.Console;
using ShiftLoggerUi.DTOs;
using static ShiftLoggerUi.Utilities;
using static ShiftLoggerUi.Enums;

namespace ShiftLoggerUi.UserInterfaces;

internal class DepartmentsMenuUi
{
    static internal void DepartmentsMenu()
    {
        var isDepartmentsMenuRunning = true;
        while (isDepartmentsMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<DepartmentMenu>()
                    .Title("Departments Menu")
                    .AddChoices(
                        DepartmentMenu.AddDepartment,
                        DepartmentMenu.ViewAllDepartments,
                        DepartmentMenu.UpdateDepartment,
                        DepartmentMenu.DeleteDepartment,
                        DepartmentMenu.GoBack));

            switch (option)
            {
                case DepartmentMenu.AddDepartment:
                    CreateDepartment();
                    break;
                case DepartmentMenu.ViewAllDepartments:
                    GetAllDepartments();
                    break;
                case DepartmentMenu.UpdateDepartment:
                    UpdateDepartment();
                    break;
                case DepartmentMenu.DeleteDepartment:
                    DeleteDepartment();
                    break;
                case DepartmentMenu.GoBack:
                    isDepartmentsMenuRunning = false;
                    break;
            }
        }
    }

    public static void CreateDepartment()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[cyan]Creating a new department[/]");

        string departmentName = UserInput.GetStringInput("Enter department name:");

        var department = new DepartmentDto
        {
            DepartmentName = departmentName
        };

        var departmentService = new DepartmentService();
        var createdDepartment = departmentService.CreateDepartment(department);

        if (createdDepartment != null)
            AnsiConsole.MarkupLine("[green]Department created successfully![/]");
        else
            DisplayMessage("Failed to create department.", "red");

        DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }

    public static void GetAllDepartments()
    {
        var departmentService = new DepartmentService();
        var departments = departmentService.GetAllDepartments();

        if (departments.Count == 0)
            DisplayMessage("No departments found.", "red");
        else
            ShowTable(departments, new[] { "Department ID", "Department Name" },
                d => new[] { d.DepartmentId.ToString(), d.DepartmentName });
    }

    public static void UpdateDepartment()
    {
        Console.Clear();
        DisplayMessage("Select a department to update:", "cyan");
        var selectedDepartment = UserInput.GetDepartmentOptionInput();
        if (selectedDepartment == null) return;

        Console.Clear();
        string departmentName = UserInput.GetStringInput("Enter new department name:");

        var updatedDepartment = new DepartmentDto
        {
            DepartmentId = selectedDepartment.DepartmentId,
            DepartmentName = departmentName
        };

        var departmentService = new DepartmentService();
        if (departmentService.UpdateDepartment(selectedDepartment.DepartmentId, updatedDepartment))
            DisplayMessage("Department updated successfully!", "green");
        else
            DisplayMessage("Failed to update department.", "red");

        DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }

    public static void DeleteDepartment()
    {
        Console.Clear();
        DisplayMessage("Select a department to delete:", "cyan");
        var selectedDepartment = UserInput.GetDepartmentOptionInput();
        if (selectedDepartment == null) return;

        var departmentService = new DepartmentService();
        if (departmentService.DeleteDepartment(selectedDepartment.DepartmentId))
            DisplayMessage("Department deleted successfully!", "green");
        else
            DisplayMessage("Failed to delete department.", "red");

        DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }
}
