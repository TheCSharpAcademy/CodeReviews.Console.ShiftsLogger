using ShiftsLogger.KamilKolanowski.Models;
using Spectre.Console;

namespace ShiftsLogger.KamilKolanowski.Services;

internal class WorkerService
{
    internal WorkerDto CreateWorker()
    {
        var firstName = AnsiConsole.Ask<string>("Enter first name:");
        var lastName = AnsiConsole.Ask<string>("Enter first name:");
        var mail = firstName.ToLower() + "." + lastName.ToLower() + "@thecsharpacademy.com";
        var role = AnsiConsole.Ask<string>("Enter role:");

        return new WorkerDto
        {
            FirstName = firstName,
            LastName = lastName,
            Email = mail,
            Role = role
        };
    }

    internal WorkerDto UpdateWorker(WorkerDto workerDto)
    {
        return new WorkerDto
        {
            FirstName = workerDto.FirstName,
            LastName = workerDto.LastName,
            Email = workerDto.Email,
            Role = workerDto.Role
        };
    }
    
}