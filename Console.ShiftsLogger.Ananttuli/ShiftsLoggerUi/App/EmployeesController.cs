using ShiftsLoggerUi.Api.Employees;
using ShiftsLoggerUi.App.Utils;
using Spectre.Console;

namespace ShiftsLoggerUi.App;

public class EmployeesController
{
    public static EmployeeDto? SelectEmployee(List<EmployeeDto> employees)
    {
        var backButton = new EmployeeDto("", ConsoleUtil.MenuBackButtonText, []);

        var selectedEmployee = AnsiConsole.Prompt(
            new SelectionPrompt<EmployeeDto>()
                .Title("\nE M P L O Y E E S")
                .AddChoices([
                    backButton,
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

        return selectedEmployee;
    }

    public static void PrintEmployeeInfo(EmployeeDto employee)
    {
        List<string> shifts = new();

        shifts.Add($"Start time     End time     Duration");

        foreach (var shift in employee.Shifts)
        {
            shifts.Add($"{shift.StartTime.ToShortTimeString()}     {shift.EndTime.ToShortTimeString()}     {shift.Duration.Hours}h {shift.Duration.Minutes}m");
        }

        var table = new Table();

        table.AddColumns(["Employee ID", "Name", "Shifts"]);

        table.AddRow([
            employee.EmployeeId.ToString(),
            employee.Name,
            string.Join("\n", shifts)
        ]);

        AnsiConsole.Write(table);
    }


    //     public static CategoryDto? SelectEmployee(List<CategoryDto> categories)
    //     {
    //         var backButton = new CategoryDto(Utils.ConsoleUtil.MenuBackButtonText);

    //         var selectedCategory = AnsiConsole.Prompt(
    //             new SelectionPrompt<CategoryDto>()
    //                 .Title("\nCATEGORIES")
    //                 .AddChoices([
    //                     backButton,
    //                     ..categories
    //                 ])
    //                 .EnableSearch()
    //         );

    //         if (
    //             selectedCategory == null ||
    //             selectedCategory.Equals(backButton)
    //         )
    //         {
    //             return null;
    //         }

    //         return selectedCategory;
    //     }

    //     public static DrinkFilterListItemDto? SelectDrinkFromCategory(CategoryDto category, List<DrinkFilterListItemDto> drinksInCategory)
    //     {
    //         var selectedDrink = AnsiConsole.Prompt(
    //             new SelectionPrompt<DrinkFilterListItemDto>()
    //                 .Title($"\n{category.StrCategory}   -   DRINKS\n")
    //                 .AddChoices([
    //                     new DrinkFilterListItemDto("", Utils.ConsoleUtil.MenuBackButtonText),
    //                     ..drinksInCategory
    //                 ])
    //                 .EnableSearch()
    //         );

    //         if (selectedDrink == null || selectedDrink.IdDrink.Equals(""))
    //         {
    //             return null;
    //         }

    //         return selectedDrink;
    //     }
}