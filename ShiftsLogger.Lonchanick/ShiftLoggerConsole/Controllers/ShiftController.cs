using ShiftLoggerConsole.Services;
using ShiftsLoggerConsole.Models;
using Spectre.Console;

namespace ShiftLoggerConsole.Controllers;

public class ShiftController
{
    ShiftService shiftService = new();
    public string[] checkType ={"Check In", "Check Out"};
    public async Task Add()
    {
        Shift shift = new();
        shift.WorkerId = AnsiConsole.Ask<int>("Type your Worker Id: ");

        var option = AnsiConsole.Prompt(new SelectionPrompt<String>()
            .AddChoices(checkType));

        if (option.Equals(checkType[0]))
            shift.CheckTypeField = CheckType.CheckIn;
        else
            shift.CheckTypeField = CheckType.CheckOut;


        if (await shiftService.Add(shift) == -1)
        {
            WriteLine("You can't checkin o checkout twice!");
            Console.Write("Press any Key to continue");
            Console.ReadLine();
        }
        else
        {
            WriteLine("Record Inserted!");
            Console.Write("Press any Key to continue");
            Console.ReadLine();
        }

        

    }

    public async Task GetAsync()
    {
        try
        {
            var result = await shiftService.GetAsync();
            if( result is not null)
            {
                Shift.printTable(result);
            }else
            {
                WriteLine("No hayregistros en tabla Shift");
            }
        }catch(Exception e)
        {
            WriteLine("Something went wrong:\n {0}",e.Message);
        }

    }
}