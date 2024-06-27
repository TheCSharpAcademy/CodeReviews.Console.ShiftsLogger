using ShiftsLoggerUi.Api;
using ShiftsLoggerUi.App;
using ShiftsLoggerUi.App.Utils;
using Spectre.Console;

namespace Program;

public class Program
{
    static readonly Api Api = new(new Client());

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
                case MainMenu.ViewEmployees:
                    await ViewEmployees();
                    break;
                case MainMenu.Exit:
                    shouldExit = true;
                    break;
            }
        }
        while (shouldExit == false);

    }

    public static async Task ViewEmployees()
    {
        var (success, employees) = await Api.GetEmployees();

        if (!success || employees == null)
        {
            Utils.Text.Error("Could not fetch");
            ConsoleUtil.PressAnyKeyToClear(
                "Press any key to go back"
            );
            return;
        }

        while (true)
        {
            var selectedEmployee = EmployeesController.SelectEmployee(employees);

            if (selectedEmployee == null)
            {
                break;
            }

            EmployeesController.PrintEmployeeInfo(selectedEmployee);
            Console.WriteLine(selectedEmployee);
            // await ShowDrinksInCategory(SelectEmployee);
        }
    }

    // public static async Task ShowDrinksInCategory(CategoryDto category)
    // {
    //     var (success, drinksInCategory) = await Api.FetchDrinksInCategory(category);

    //     if (!success)
    //     {
    //         Utils.Text.Error($"Could not fetch drinks for category {category.StrCategory}");
    //         Utils.ConsoleUtil.PressAnyKeyToClear(
    //             "Press any key to go back"
    //         );
    //         return;
    //     }

    //     while (true)
    //     {
    //         var selectedDrink = CategoriesController.SelectDrinkFromCategory(category, drinksInCategory);

    //         if (selectedDrink == null)
    //         {
    //             break;
    //         }

    //         await OpenDrinkInfo(selectedDrink);
    //     }

    // }

    // public static async Task OpenDrinkInfo(DrinkFilterListItemDto drink)
    // {
    //     var (success, drinkInfo) = await Api.FetchDrinkInfo(drink.IdDrink);

    //     if (drinkInfo == null || !success)
    //     {
    //         Utils.Text.Error($"Could not load info for drink ID {drink.IdDrink}");
    //         return;
    //     }

    //     DrinksController.PrintDrinkInfo(drinkInfo);

    //     Utils.ConsoleUtil.PressAnyKeyToClear();
    // }
}
