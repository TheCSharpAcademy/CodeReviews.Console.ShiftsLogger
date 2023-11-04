using ShiftLoggerConsole.Services;
using ShiftsLoggerConsole.Models;
using Spectre.Console;

namespace ShiftLoggerConsole.Controllers;

public class ShiftController
{
    ShiftService shiftService = new();
    public string[] chechInType ={"Check In", "Check Out"};
    public async Task Add()
    {
        Shift shift=new();
        shift.WorkerId= AnsiConsole.Ask<int>("Worker Id: ");
        // shift.CheckTypeField = 
        // = AnsiConsole.Ask<string>("Worker Id: ");

        var option = AnsiConsole.Prompt(new SelectionPrompt<String>()
			.Title("ChechIn or CheckOut")
			.AddChoices(chechInType));

        if(option.Equals(chechInType[0]))
            shift.CheckTypeField=CheckType.CheckIn;
        else
            shift.CheckTypeField=CheckType.CheckOut;

        try
        {
            await shiftService.Add(shift);

        }catch(Exception e)
        {
            WriteLine(e);
        }
        
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
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

    public Task<Worker?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id)
    {
        throw new NotImplementedException();
    }
}