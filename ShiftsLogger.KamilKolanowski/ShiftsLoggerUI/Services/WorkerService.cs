using System.Net.Mail;
using System.Text.RegularExpressions;
using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI.Services;

internal class WorkerService
{
    private readonly ApiDataService _apiDataService = new();
    private readonly DeserializeJson _deserializeJson = new();

    internal async Task<WorkerDto?> CreateWorker(string customEmail = "")
    {
        try
        {
            var worker = GetUserInputForWorkerCreation();

            worker.Email =
                customEmail != ""
                    ? customEmail
                    : await GenerateUniqueEmail(worker.FirstName, worker.LastName);

            var createdWorker = await _apiDataService.PostWorkerAsync(worker);

            AnsiConsole.MarkupLine(
                "\n[green]Successfully added worker![/]"
            );
            return createdWorker;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to add worker due to:[/] {ex.Message}");
            AnsiConsole.MarkupLine("Press any key to continue...");
            return null;
        }
    }

    internal async Task EditWorker()
    {
        try
        {
            await CreateWorkersTable();
            var workers = await GetWorkersAsync();

            while (true)
            {
                var workerId = AnsiConsole.Ask<int>("Choose [yellow2]worker id[/] to edit:");

                if (!workers.Any(w => w.WorkerId == workerId))
                {
                    AnsiConsole.MarkupLine("[red]There is no worker with the specified id![/]");
                    continue;
                }

                var workerDtoToUpdate = await GetWorkerAsync(workerId);

                var columnToUpdate = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose the property to edit")
                        .AddChoices("FirstName", "LastName", "Email", "Role")
                );
                var newValue = String.Empty;
                if (columnToUpdate == "Email")
                {
                    newValue = AnsiConsole.Prompt(
                        new TextPrompt<string>(
                            "Enter email address [yellow](e.g. john.doe@gmail.com)[/]: "
                        ).Validate(input =>
                        {
                            var formatValidation = ValidateEmail(input);
                            if (!formatValidation.Successful)
                                return formatValidation;

                            var existsValidation = ValidateIfUserWithEmailExists(workers, input);
                            return existsValidation;
                        })
                    );
                }
                else
                {
                    newValue = AnsiConsole.Ask<string>(
                        $"Provide new value for {columnToUpdate} property: "
                    );
                }

                switch (columnToUpdate)
                {
                    case "FirstName":
                        workerDtoToUpdate.FirstName = newValue;
                        break;
                    case "LastName":
                        workerDtoToUpdate.LastName = newValue;
                        break;
                    case "Email":
                        workerDtoToUpdate.Email = newValue;
                        break;
                    case "Role":
                        workerDtoToUpdate.Role = newValue;
                        break;
                }

                await _apiDataService.PutWorkerAsync(workerDtoToUpdate);

                AnsiConsole.MarkupLine(
                    "\n[green]Successfully edited worker![/]\nPress any key to continue..."
                );
                return;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to edit worker, due to:[/] {ex.Message}");
            AnsiConsole.MarkupLine("Press any key to continue...");
        }
    }

    internal async Task DeleteWorker()
    {
        try
        {
            await CreateWorkersTable();
            var workers = await GetWorkersAsync();

            while (true)
            {
                var workerId = AnsiConsole.Ask<int>("Choose [yellow2]worker id[/] to delete:");

                if (!workers.Any(w => w.WorkerId == workerId))
                {
                    AnsiConsole.MarkupLine("[red]There is no worker with the specified id![/]");
                    continue;
                }

                await _apiDataService.DeleteWorkerAsync(workerId);
                AnsiConsole.MarkupLine(
                    "\n[green]Successfully deleted worker![/]\nPress any key to continue..."
                );
                return;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to delete worker, due to:[/] {ex.Message}");
            AnsiConsole.MarkupLine("Press any key to continue...");
        }
    }

    internal async Task CreateWorkersTable()
    {
        var table = new Table { Title = new TableTitle("[yellow]Workers[/]") };

        table.AddColumn("[cyan2]Worker Id[/]");
        table.AddColumn("[cyan2]FirstName[/]");
        table.AddColumn("[cyan2]LastName[/]");
        table.AddColumn("[cyan2]Email[/]");
        table.AddColumn("[cyan2]Role[/]");

        var workers = await GetWorkersAsync();

        foreach (var workerDto in workers)
        {
            table.AddRow(
                workerDto.WorkerId.ToString(),
                workerDto.FirstName,
                workerDto.LastName,
                workerDto.Email,
                workerDto.Role
            );
        }

        table.Border(TableBorder.HeavyEdge);

        AnsiConsole.Write(table);
    }

    internal async Task<List<WorkerDto>> GetWorkersAsync()
    {
        string response = await _apiDataService.GetWorkerAsync("workers");
        List<WorkerDto> workerDtos = await _deserializeJson.DeserializeAsync<List<WorkerDto>>(
            response
        );

        return workerDtos;
    }

    private async Task<WorkerDto> GetWorkerAsync(int id)
    {
        string response = await _apiDataService.GetWorkerAsync($"workers/{id}");
        WorkerDto workerDto = await _deserializeJson.DeserializeAsync<WorkerDto>(response);
        return workerDto;
    }

    private WorkerDto GetUserInputForWorkerCreation()
    {
        var firstName = AnsiConsole.Ask<string>("Enter first name:");
        var lastName = AnsiConsole.Ask<string>("Enter last name:");
        var role = AnsiConsole.Ask<string>("Enter role:");

        return new WorkerDto
        {
            FirstName = firstName,
            LastName = lastName,
            Role = role,
        };
    }

    private async Task<string> GenerateUniqueEmail(string firstName, string lastName)
    {
        var workers = await GetWorkersAsync();

        var baseEmail = $"{firstName.ToLower()}.{lastName.ToLower()}";
        var domain = "@thecsharpacademy.com";
        var email = baseEmail + domain;
        var counter = 1;

        while (workers.Any(w => string.Equals(w.Email, email, StringComparison.OrdinalIgnoreCase)))
        {
            email = $"{baseEmail}{counter}{domain}";
            counter++;
        }

        return email;
    }

    internal ValidationResult ValidateIfUserWithEmailExists(List<WorkerDto> workers, string email)
    {
        var emailExists = workers.Any(w =>
            string.Equals(w.Email, email, StringComparison.OrdinalIgnoreCase)
        );

        return emailExists
            ? ValidationResult.Error("[red]Worker with such email already exists![/]")
            : ValidationResult.Success();
    }

    internal ValidationResult ValidateEmail(string emailAddress)
    {
        emailAddress = emailAddress.Trim();
        try
        {
            var addr = new MailAddress(emailAddress);
            if (!addr.Host.Contains('.'))
                return ValidationResult.Error("[red]Email domain must contain a dot.[/]");
            if (!Regex.IsMatch(addr.Host, @"\.[a-zA-Z]{2,}$"))
                return ValidationResult.Error("[red]Email must have a valid domain suffix.[/]");
            return ValidationResult.Success();
        }
        catch
        {
            return ValidationResult.Error("[red]Provided Email is not valid, please try again.[/]");
        }
    }
}
