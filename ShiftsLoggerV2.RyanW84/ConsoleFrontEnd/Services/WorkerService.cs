using System.Net.Http.Json;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;

namespace ConsoleFrontEnd.Services;

public class WorkerService : IWorkerService
{
    private readonly HttpClient _httpClient;

    public WorkerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponseDto<List<Workers>>> GetAllWorkers(
        WorkerFilterOptions workerFilterOptions
    )
    {
        try
        {
            var queryParams = new List<string>();
            if (workerFilterOptions.WorkerId != null)
                queryParams.Add($"workerId={workerFilterOptions.WorkerId}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.Name))
                queryParams.Add($"name={Uri.EscapeDataString(workerFilterOptions.Name)}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.PhoneNumber))
                queryParams.Add(
                    $"phoneNumber={Uri.EscapeDataString(workerFilterOptions.PhoneNumber)}"
                );
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.Email))
                queryParams.Add($"email={Uri.EscapeDataString(workerFilterOptions.Email)}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.Search))
                queryParams.Add($"search={Uri.EscapeDataString(workerFilterOptions.Search)}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.SortBy))
                queryParams.Add($"sortBy={Uri.EscapeDataString(workerFilterOptions.SortBy)}");
            if (!string.IsNullOrWhiteSpace(workerFilterOptions.SortOrder))
                queryParams.Add($"sortOrder={Uri.EscapeDataString(workerFilterOptions.SortOrder)}");

            var queryString = "api/workers";
            if (queryParams.Count > 0)
                queryString += "?" + string.Join("&", queryParams);

            var response = await _httpClient.GetAsync(queryString);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponseDto<List<Workers>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error retrieving workers.",
                    Data = null,
                };
            }

            return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Workers>>>()
                ?? new ApiResponseDto<List<Workers>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = "No data returned.",
                    Data = new List<Workers>(),
                };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<List<Workers>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<List<Workers?>>> GetWorkerById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/workers/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponseDto<List<Workers?>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Worker not found.",
                    Data = null,
                };
            }

            return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Workers?>>>()
                ?? new ApiResponseDto<List<Workers?>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = "No data returned.",
                    Data = new List<Workers?>(),
                };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<List<Workers?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<Workers>> CreateWorker(Workers createdWorker)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/workers", createdWorker);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                return new ApiResponseDto<Workers>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error creating worker.",
                    Data = null,
                };
            }

            var worker = await response.Content.ReadFromJsonAsync<Workers>();
            return new ApiResponseDto<Workers>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Worker created successfully.",
                Data = worker ?? createdWorker,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<Workers>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<Workers?>> UpdateWorker(int id, Workers updatedWorker)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/workers/{id}", updatedWorker);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ApiResponseDto<Workers?>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error updating worker.",
                    Data = null,
                };
            }

            var worker = await response.Content.ReadFromJsonAsync<Workers>();
            return new ApiResponseDto<Workers?>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Worker updated successfully.",
                Data = worker ?? updatedWorker,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<Workers?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<string?>> DeleteWorker(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/workers/{id}");
            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return new ApiResponseDto<string?>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error deleting worker.",
                    Data = null,
                };
            }

            return new ApiResponseDto<string?>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Worker deleted successfully.",
                Data = null,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<string?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }
}
