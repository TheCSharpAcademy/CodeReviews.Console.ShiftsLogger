using ShiftsLogger.KamilKolanowski.Models;
using ShiftsLogger.KamilKolanowski.Models.Data;
using Spectre.Console;

namespace ShiftsLogger.KamilKolanowski.Services;

internal class WorkerService
{
    internal void AddWorker()
    {
        using (var context = new ShiftsLoggerDb.ShiftsLoggerDbContext())
        {
            var newWorker = CreateWorker();
            context.Workers.Add(newWorker);
            context.SaveChanges();
        }

        ReturnStatusMessage("added");
    }

    internal Worker CreateWorker()
    {
        var firstName = AnsiConsole.Ask<string>("Enter first name:");
        var lastName = AnsiConsole.Ask<string>("Enter first name:");
        var mail = firstName.ToLower() + "." + lastName.ToLower() + "@thecsharpacademy.com";
        var role = AnsiConsole.Ask<string>("Enter role:");

        return new Worker
        {
            FirstName = firstName,
            LastName = lastName,
            Email = mail,
            Role = role
        };
    }

    internal void ReturnStatusMessage(string message)
    {
        AnsiConsole.MarkupLine($"[green]Successfully {message} worker![/]");
        AnsiConsole.MarkupLine("Press any key to continue...");
        Console.ReadKey();
    }
}