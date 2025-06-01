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
            var queryString =
                $"api/workers?workerId={workerFilterOptions.WorkerId}&workerName={workerFilterOptions.Name}&workerPhoneNumber{workerFilterOptions.PhoneNumber}&workerEmail{workerFilterOptions.Email}";

            response = await httpClient.GetAsync(queryString);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new ApiResponseDto<List<Workers>>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else if (response.StatusCode is System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine("No workers found.");
                return new ApiResponseDto<List<Workers>>
                {
                    ResponseCode = response.StatusCode,
                    Message = "No workers found.",
                    Data = new List<Workers>(),
                    TotalCount = 0,
                };
            }
            else
            {
                Console.WriteLine("Workers retrieved successfully.");
                ApiResponseDto<List<Workers>>? createdWorker =
                    await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Workers>>>()
                    ?? new ApiResponseDto<List<Workers>>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "No data returned.",
                        Data = new List<Workers>(),
                        TotalCount = 0,
                    };

                return createdWorker;
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
                Console.WriteLine(
                    $"Error: Status Code:{response.StatusCode} - Reason Phrase:{response.ReasonPhrase}"
                );
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return new ApiResponseDto<List<Workers>>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                Console.WriteLine("Worker retrieved successfully.");
                return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Workers>>>()
                    ?? new ApiResponseDto<List<Workers>>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "No data returned.",
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
            if (!response.IsSuccessStatusCode)
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
                Console.WriteLine("Shift created successfully.");
                return await response.Content.ReadFromJsonAsync<ApiResponseDto<Workers>>()
                    ?? new ApiResponseDto<Workers>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "No data returned.",
                        Data = null,
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
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
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
                Console.WriteLine("Worker updated successfully.");
                return await response.Content.ReadFromJsonAsync<ApiResponseDto<Workers>>()
                    ?? new ApiResponseDto<Workers>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "No data returned.",
                        Data = null,
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
            if (response.StatusCode is System.Net.HttpStatusCode.NotFound)
            {
                AnsiConsole.Markup("[red]Error: Worker not found.[/]");
                return new ApiResponseDto<string>
                {
                    ResponseCode = response.StatusCode,
                    Message = "Worker not found.",
                    Data = null,
                };
            }
            else if (response.StatusCode is System.Net.HttpStatusCode.NoContent)
            {
                AnsiConsole.Markup("[green]Worker deleted successfully![/]");
                return new ApiResponseDto<string>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                AnsiConsole.Markup(
                    $"[red]Error: {response.StatusCode} - {response.ReasonPhrase}[/]"
                );
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
