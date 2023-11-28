using Spectre.Console;

namespace ShiftsLoggerConsole.Models;


public class Shift
{
    public int Id { get; set; }
    public DateTime Check { get; set; }
    public CheckType CheckTypeField { get; set; }
    public int WorkerId { get; set; }

    public virtual Worker Worker { get; set; }

    public static void printTable(IEnumerable<Shift> shifts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Check Date");
        table.AddColumn("Chech Type Field");
        table.AddColumn("Worker Id");
        table.AddColumn("Name");

        foreach (var shift in shifts)
        {
            table.AddRow(shift.Id.ToString(),
                shift.Check.ToString(), 
                shift.CheckTypeField.ToString(),
                shift.WorkerId.ToString(),
                shift.Worker.Name.ToString());
        }

        AnsiConsole.Write(table);
        Console.Write("Press any Key to continue");
        Console.ReadLine();
    }

    public override string ToString()
    {
        string worker = $"{this.Id} - {this.WorkerId} - {this.Check} - {this.CheckTypeField}";
        return worker;
    }
}

public enum CheckType
{
    CheckIn,
    CheckOut
}

