using ShiftLoggerCarDioLogicsUI;
using Spectre.Console;

bool appRuning = true;
string name;
int iD;
DateTime startDate, endDate;
bool isValidSD, isValidED;
Service service = new Service(new HttpClient());

do 
{
    Console.Clear();

    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MainMenu>()
        .Title("Choose an Option:")
        .AddChoices(
            MainMenu.addShift,
            MainMenu.updateShift,
            MainMenu.deleteShift,
            MainMenu.viewShifts,
            MainMenu.exit));

    switch (option)
    {
        case MainMenu.addShift:
            UserInputShift(out name, out startDate, out endDate);
            service.ShiftAddition(name, startDate, endDate);
            break;

        case MainMenu.updateShift:
            await service.ShowAllShifts();
            iD = await service.SelectID();
            UserInputShift(out name, out startDate, out endDate);
            service.ShiftUpdate(iD, name, startDate, endDate);
            break;

        case MainMenu.deleteShift:
            await service.ShowAllShifts();
            iD = await service.SelectID();
            await service.ShiftDeletion(iD);
            break;
        case MainMenu.viewShifts:
            await service.ShowAllShifts();
            Console.Clear();
            break;
        case MainMenu.exit:
            appRuning = false;
            Console.WriteLine("App terminated!");
            Console.ReadLine();
            break;
    }
} while (appRuning == true);

void UserInputShift(out string name, out DateTime startDate, out DateTime endDate)
{
    Console.Clear();

    name = AnsiConsole.Ask<string>("What's your [green]name[/]?");

    do
    {
        string startDateString = AnsiConsole.Ask<string>("What's the time the shift [green]started[/] (format dd/MM/yyyy HH:mm)?");
        isValidSD = Validator.IsValidDateInput(startDateString, out startDate);
    } while (isValidSD == false);

    do
    {
        string endDateString = AnsiConsole.Ask<string>("What's the time the shift [green]ended[/] (format dd/MM/yyyy HH:mm)?");
        isValidED = Validator.IsValidDateInput(endDateString, out endDate);

        if(endDate < startDate)
        {
            Console.WriteLine("The end date should be later than the start date");
            Console.ReadLine();
        }
    } while (isValidED == false || endDate < startDate);

    Console.Clear();
}

enum MainMenu
{
    addShift,
    updateShift,
    deleteShift,
    viewShifts,
    exit
}
