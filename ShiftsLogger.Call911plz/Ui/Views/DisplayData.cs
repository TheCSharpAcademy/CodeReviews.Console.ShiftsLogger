
using Spectre.Console;

public class DisplayTable()
{
    public static void Worker(List<Worker> workers)
    {
        Table table = new();
        table.AddColumns(["Id", "Employee Id", "Employee Name"]);

        foreach (Worker worker in workers)
        {
            table.AddRow(worker.Id.ToString(), worker.EmployeeId.ToString(), worker.EmployeeName);
        }

        AnsiConsole.Write(table);
    }
}