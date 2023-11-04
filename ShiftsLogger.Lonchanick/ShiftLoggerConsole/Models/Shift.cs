using System.Text.Json.Serialization;
using Spectre.Console;

namespace ShiftsLoggerConsole.Models;


public class Shift
{
    public int Id { get; set; }
    public DateTime Check { get; set; }
    public CheckType CheckTypeField {get; set;} 
    public int WorkerId { get; set; }

    [JsonIgnore]
    public virtual Worker Worker { get; set; }

    public static void printTable(IEnumerable<Shift> shifts)
    {
        var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Check Date");
		table.AddColumn("Chech Type Field");
		table.AddColumn("Worker Id");

		foreach (var shift in shifts)
		{
			table.AddRow(shift.Id.ToString(),
				shift.Check.ToShortDateString(),
                shift.CheckTypeField.ToString(),
                shift.WorkerId.ToString()); 
		}

		AnsiConsole.Write(table);
		Console.Write("Press any Key to continue");
		Console.ReadLine();
    }

//     public static void PrintWorker(Worker worker)
// 	{
// 		var panel = new Panel
// ($@"Id: {worker.Id}
// Name: {worker.Name}");
//         panel.Padding = new Padding(0, 0, 0, 0);
//         AnsiConsole.Write(panel);
//         Console.WriteLine("Press any key to continue ...");
//         Console.ReadLine();
//     }

//     public static explicit operator Worker(Task<Worker?> v)
//     {
//         throw new NotImplementedException();
//     }
//     public static Worker WorkerMenuPickable(IEnumerable<Worker> workers)
// 	{
// 		var option = AnsiConsole.Prompt(new SelectionPrompt<Worker>()
// 			.Title("Choose any Coffee")
// 			.AddChoices(workers));

// 		return option;
// 	}

    public override string  ToString()
    {
        return$"{this.Id} - {this.WorkerId} - {this.Check} - {this.CheckTypeField}";
    }



}

public enum CheckType{
    CheckIn,
    CheckOut
}

