using ShiftLoggerConsole.Services;
using ShiftsLoggerConsole.Models;
using Spectre.Console;

namespace ShiftLoggerConsole.Controllers;

public class WorkerController
{
    public WorkerService workerService = new();

    public async Task Get()
    {
        try
        {
            var result = await workerService.GetAsync();
            if( result is not null)
            {
                Worker.printTable(result);
            }else
            {
                WriteLine("No hayregistros en tabla Worker");
            }
        }catch(Exception e)
        {
            WriteLine("Something went wrong:\n {0}",e.Message);
        }
        
    }

    public async Task Add()
    {
        Worker worker=new();
        worker.Name = AnsiConsole.Ask<string>("Worker name: ");
        await workerService.Add(worker);
        
    }
    public async Task GetById()
    {
        try
        {
            int id = AnsiConsole.Ask<int>("Worker id: ");
            Worker? worker = await workerService.GetById(id);
            Worker.PrintWorker(worker);
        }catch(Exception e)
        {
            WriteLine("Something went wrong: {0}", e.Message);
        }
    }

    public async Task Update()
    {
        try
        {
            int id = AnsiConsole.Ask<int>("Worker id: ");
            if(id != 0 )
            {
                await workerService.Update(id);
            }else
            {
                WriteLine("Id must be != 0");
            }

        }catch(Exception e)
        {
            WriteLine("Something went wrong: {0}", e.Message);
        }

    }

    public async Task Delete()
    {
        try
        {
            int id = AnsiConsole.Ask<int>("Worker id: ");
            if(id != 0 )
                await workerService.Delete(id);
            else
                WriteLine("Id must be != 0");

        }catch(Exception e)
        {
            WriteLine("Something went wrong: {0}", e.Message);
        }
    }

}