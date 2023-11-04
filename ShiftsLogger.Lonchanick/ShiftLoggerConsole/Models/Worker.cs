using System.Text.Json.Serialization;
using Spectre.Console;

namespace ShiftsLoggerConsole.Models;

public class Worker
{

    public int Id { get; set; }
    public string? Name { get; set; }
    
    [JsonIgnore]
    public IEnumerable<Shift> Shifts { get; set; }

    public static void printTable(IEnumerable<Worker> wokers)
    {
        var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Name");

		foreach (var worker in wokers)
		{
			table.AddRow
				(worker.Id.ToString(),
				worker.Name); 
		}

		AnsiConsole.Write(table);
		Console.Write("Press any Key to continue");
		Console.ReadLine();
    }

    public static void PrintWorker(Worker worker)
	{
		var panel = new Panel
($@"Id: {worker.Id}
Name: {worker.Name}");
        panel.Padding = new Padding(0, 0, 0, 0);
        AnsiConsole.Write(panel);
        Console.WriteLine("Press any key to continue ...");
        Console.ReadLine();
    }

    public static explicit operator Worker(Task<Worker?> v)
    {
        throw new NotImplementedException();
    }
    public static Worker WorkerMenuPickable(IEnumerable<Worker> workers)
	{
		var option = AnsiConsole.Prompt(new SelectionPrompt<Worker>()
			.Title("Choose any Coffee")
			.AddChoices(workers));

		return option;
	}

    public override string  ToString()
    {
        return$"{this.Id} - {this.Name}";
    }

}
