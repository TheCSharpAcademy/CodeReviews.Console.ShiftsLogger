using System.Net.Http.Json;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;
using Spectre.Console;

namespace ConsoleFrontEnd.Services;

public class WorkerService : IWorkerService
{
    private readonly HttpClient httpClient = new HttpClient()
    {
        BaseAddress = new Uri("https://localhost:7009/"),
    };

    public async Task<ApiResponseDto<List<Workers>>> GetAllWorkers(
        WorkerFilterOptions workerFilterOptions
    )
    {
        HttpResponseMessage response;
        try
        {
            // Debug log for incoming search parameter
            AnsiConsole.MarkupLine(
                $"[yellow]Filter options received:[/]\n\n"
                    + $"[blue]WorkerId:[/] {workerFilterOptions.WorkerId}\t"
                    + $"[blue]Name:[/] {workerFilterOptions.Name}\t"
                    + $"[blue]Phone Number:[/] {workerFilterOptions.PhoneNumber}\t"
                    + $"[blue]Email:[/] {workerFilterOptions.Email}\t"
                    + $"[blue]Search:[/] {workerFilterOptions.Search}\t"
                    + $"[blue]Sort By:[/] {workerFilterOptions.SortBy}\t"
                    + $"[blue]Sort Order:[/] {workerFilterOptions.SortOrder}\t"
            );

            var queryParams = new List<string>();
            if (workerFilterOptions.WorkerId != null && workerFilterOptions.WorkerId != 0)
                queryParams.Add($"workerId={workerFilterOptions.WorkerId}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.Name))
                queryParams.Add($"name={workerFilterOptions.Name}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.PhoneNumber))
                queryParams.Add($"phoneNumber={workerFilterOptions.PhoneNumber}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.Email))
                queryParams.Add($"email={workerFilterOptions.Email}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.Search))
                queryParams.Add($"search={workerFilterOptions.Search}");

            var queryString = "api/workers";
            if (queryParams.Count > 0)
                queryString += "?" + string.Join("&", queryParams);

            AnsiConsole.MarkupLine(
                $"[blue]Final request URL: {httpClient.BaseAddress}{queryString}[/]\n"
            );

            response = await httpClient.GetAsync(queryString);
            if (!response.IsSuccessStatusCode)
            {
                AnsiConsole.Markup("[Red]Workers not retrieved.[/]\n");
                return new ApiResponseDto<List<Workers>>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                AnsiConsole.Markup("[Green]Workers retrieved successfully.[/]\n");
                var workers =
                    await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Workers>>>()
                    ?? new ApiResponseDto<List<Workers>>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "Data obtained",
                        Data = new List<Workers>(),
                    };

                return workers;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetAllWorkers: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<List<Workers?>>> GetWorkerById(int id)
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.GetAsync($"api/workers/{id}");

            if (response.StatusCode is not System.Net.HttpStatusCode.OK)
            {
                AnsiConsole.Markup($"[Red]Error: Worker not found[/]\n");
                return new ApiResponseDto<List<Workers>>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                AnsiConsole.Markup("[Green]Worker retrieved successfully.[/]\n");
                return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Workers>>>()
                    ?? new ApiResponseDto<List<Workers>>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "Worker found",
                        Data = new List<Workers>(),
                        TotalCount = 0,
                    };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetWorkerById: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<Workers>> CreateWorker(Workers createdWorker)
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.PostAsJsonAsync("api/workers", createdWorker);
            if (response.StatusCode is not System.Net.HttpStatusCode.Created)
            {
                Console.WriteLine($"Error: Status Code - {response.StatusCode}");
                return new ApiResponseDto<Workers>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                Console.WriteLine("Worker created successfully.");
                return new ApiResponseDto<Workers>
                {
                    ResponseCode = response.StatusCode,
                    Data = response.Content.ReadFromJsonAsync<Workers>().Result ?? createdWorker,
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for CreateWorker: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<Workers?>> UpdateWorker(int id, Workers updatedWorker)
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.PutAsJsonAsync($"api/workers/{id}", updatedWorker);
            if (response.StatusCode is not System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new ApiResponseDto<Workers>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                AnsiConsole.Markup("[Green]Worker updated successfully.[/]\n");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                return new ApiResponseDto<Workers>
                {
                    ResponseCode = response.StatusCode,
                    Data = response.Content.ReadFromJsonAsync<Workers>().Result ?? updatedWorker,
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for UpdateWorker: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<string?>> DeleteWorker(int id)
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.DeleteAsync($"api/workers/{id}");
            if (response.StatusCode is not System.Net.HttpStatusCode.NoContent)
            {
                AnsiConsole.Markup("[red]Error: Worker not found please try again![/]\n");
                return new ApiResponseDto<string>
                {
                    ResponseCode = response.StatusCode,
                    Message = $"Error: {response.StatusCode}",
                    Data = null,
                };
            }
            else
            {
                AnsiConsole.Markup("[green]Worker deleted successfully![/]");
                return new ApiResponseDto<string>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for DeleteWorker: {ex}");
            throw;
        }
    }
}
