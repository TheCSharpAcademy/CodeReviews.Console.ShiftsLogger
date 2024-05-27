using System.Data.Common;
using Spectre.Console;
namespace ShiftsLoggerUI;

public class UserInterface
{
    internal static bool MainMenu(bool appRunning = true)
    {
        var option = AnsiConsole.Prompt(new SelectionPrompt<Menu>()
        .Title("Choose")
        .AddChoices(
            Menu.ViewShifts,
            Menu.EnterShift,
            Menu.UpdateShift,
            Menu.DeleteShift,
            Menu.Quit
        ));
        switch (option)
        {
            case Menu.ViewShifts:
                Service.SendGetRequest();
                break;
            case Menu.EnterShift:
                EnterShift();
                break;
            case Menu.DeleteShift:
                DeleteShift();
                break;
            case Menu.UpdateShift:
                UpdateShift();
                break;
            case Menu.Quit:
                return !appRunning;
        }
        return appRunning;
    }
    internal static async void EnterShift()
    {
        var date = DateTime.Now.Date;
        var startTime = GetStartTime();
        var endTime = GetEndTime(startTime);
        var duration = endTime - startTime;

        ShiftModel shiftModel = new()
        {
            date = date,
            startTime = startTime,
            endTime = endTime,
            duration = duration
        };
        await Service.SendPostRequest(shiftModel);
    }
    internal static async void DeleteShift()
    {
        Service.SendGetRequest();
        Console.WriteLine("Type 0 to go back to main menu.");
        var userInput = AnsiConsole.Ask<string>("Enter the id you want to delete:");
        if (userInput == "0") MainMenu();
        while (!Validation.ValidateId(userInput))
        {
            Console.WriteLine("Invalid id.");
            userInput = AnsiConsole.Ask<string>("Enter the id you want to delete:");
        }
        await Service.SendDeleteRequest(int.Parse(userInput));
    }
    internal static async void UpdateShift()
    {
        List<ShiftModel> shifts = Service.SendGetRequest();
        Console.WriteLine("Type 0 to go back to main menu.");
        var userInput = AnsiConsole.Ask<string>("Enter the id you want to update:");
        if (userInput == "0") MainMenu();
        while (!Validation.ValidateId(userInput))
        {
            Console.WriteLine("Invalid id.");
            userInput = AnsiConsole.Ask<string>("Enter the id you want to update:");
        }

        long id = long.Parse(userInput);

        var date = AnsiConsole.Confirm("Update date?")
        ? GetDate()
        : shifts[int.Parse(userInput) - 1].date;

        var startTime = AnsiConsole.Confirm("Update startTime?")
        ? GetStartTime()
        : shifts[int.Parse(userInput) - 1].startTime;

        var endTime = AnsiConsole.Confirm("Update endTime?")
        ? GetEndTime(startTime)
        : shifts[int.Parse(userInput) - 1].endTime;

        var duration = endTime - startTime;

        ShiftModel shiftModel = new()
        {
            id = id,
            date = date,
            startTime = startTime,
            endTime = endTime,
            duration = duration
        };
        await Service.SendPutRequest(id, shiftModel);
    }

    private static DateTime GetEndTime(DateTime startTime)
    {
        var endTime = AnsiConsole.Ask<string>("Enter the time you ended your shift(format: HH:mm): ");
        var parsedTime = Validation.ValidateEndTime(endTime, startTime);
        return parsedTime;
    }

    private static DateTime GetStartTime()
    {
        Console.WriteLine("Type 0 to go back to main menu.");
        var startTime = AnsiConsole.Ask<string>("Enter the time you started your shift(format: HH:mm): ");
        if (startTime == "0") MainMenu();
        var parsedTime = Validation.ValidateStartTime(startTime);
        return parsedTime;
    }

    private static DateTime GetDate()
    {
        var date = AnsiConsole.Ask<string>("Enter the date(format dd-MM-yyyy):");
        var parsedDate = Validation.ValidateDate(date);
        return parsedDate;
    }
}