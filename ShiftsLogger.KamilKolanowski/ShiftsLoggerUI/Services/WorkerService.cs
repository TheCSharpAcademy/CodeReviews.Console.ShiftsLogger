using ShiftsLogger.KamilKolanowski.Models;
using ShiftsLogger.KamilKolanowski.Services;
using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI.Services;

internal class WorkerService
{
    private readonly DataFetcher _dataFetcher = new();
    private readonly DeserializeJson _deserializeJson = new();

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
            Role = role,
        };
    }

    internal async Task UpdateWorker(WorkerDto workerDto)
    {
        await _dataFetcher.PutAsync(workerDto);
    }

    internal async Task<List<WorkerDto>> GetWorkersAsync()
    {
        string response = await _dataFetcher.GetAsync("workers");
        List<WorkerDto> workerDtos = await _deserializeJson.DeserializeAsync<List<WorkerDto>>(
            response
        );

        return workerDtos;
    }

    internal async Task<WorkerDto> GetWorkerAsync(int id)
    {
        string response = await _dataFetcher.GetAsync($"workers/{id}");
        WorkerDto workerDto = await _deserializeJson.DeserializeAsync<WorkerDto>(response);
        return workerDto;
    }

    internal async Task CreateTable(List<WorkerDto> workerDtos)
    {
        var table = new Table();

        table.AddColumn("Id");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Email");
        table.AddColumn("Role");

        int idx = 1;
        foreach (var workerDto in workerDtos)
        {
            table.AddRow(
                idx.ToString(),
                workerDto.FirstName,
                workerDto.LastName,
                workerDto.Email,
                workerDto.Role
            );
            idx++;
        }

        table.Border(TableBorder.Rounded);

        AnsiConsole.Write(table);
    }
}
