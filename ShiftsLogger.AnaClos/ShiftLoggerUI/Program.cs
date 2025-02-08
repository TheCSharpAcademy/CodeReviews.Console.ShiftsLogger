using ShiftLoggerUI.Controllers;
using ShiftLoggerUI.Services;

List<string> mainOptions = new List<string> { "Manage Employees", "Manage Shifts", "Exit Program" };
List<string> menuOptions = new List<string> { "Add", "Delete", "Update", "View", "View All", "Exit Menu" };

var consoleController = new ConsoleController();
var employeesService = new EmployeesService();
var shiftsService = new ShiftsService();

var employeesController = new EmployeesController(consoleController, employeesService);
var shiftsController = new ShiftsController(consoleController, employeesController, shiftsService);

string option;

do
{
    option = consoleController.Menu("What would you like to do?", "blue", mainOptions);
    switch (option)
    {
        case "Manage Employees":
            Menu(option, employeesController);
            break;
        case "Manage Shifts":
            Menu(option, shiftsController);
            break;
    }

} while (option != "Exit Program");

string Menu(string mainOption, IController controller)
{
    string option;
    do
    {
        option = consoleController.Menu(mainOption, "blue", menuOptions);
        switch (option)
        {
            case "Add":
                controller.Add();
                break;
            case "Delete":
                controller.Delete();
                break;
            case "Update":
                controller.Update();
                break;
            case "View":
                controller.View();
                break;
            case "View All":
                controller.ViewAll();
                break;
        }
    } while (option != "Exit Menu");

    return option;
}

Console.ReadKey();