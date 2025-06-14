
using Spectre.Console;

public class DisplayTable()
{
    public static void Worker(List<Worker> workers)
    {
        Table table = new();
        table.AddColumns(["Id", "Employee Id", "Employee Name"]);

        foreach (Worker worker in workers)
        {
            table.AddRow(worker.Id.ToString(), worker.WorkerId.ToString(), worker.WorkerName);
        }

        AnsiConsole.Write(table);
    }

    public static void Shift(List<Shift> shifts)
    {
        Table table = new();
        table.AddColumns(["Id", "Worker Id", "Start Time", "End Time"]);

        foreach (Shift shift in shifts)
        {
            table.AddRow(
                shift.Id.ToString(), 
                shift.WorkerId.ToString(), 
                shift.StartDateTime.ToString(), 
                shift.EndDateTime.ToString());
        }

        AnsiConsole.Write(table);
    }
}