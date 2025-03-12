using System.Net.Http.Headers;
using System.Net.Http.Json;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.Services;

class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7029/api/")
        };

        _httpClient.DefaultRequestHeaders.Accept.Clear();

        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ShiftsLoggerClient");
    }

    internal async Task<ApiResponse<List<EmployeeDTO>>> GetAllEmployees()
    {
        try
        {
            var requestUri = "employee";

            using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return new ApiResponse<List<EmployeeDTO>>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            var employees = await response.Content.ReadFromJsonAsync<List<EmployeeDTO>>();

            return new ApiResponse<List<EmployeeDTO>>
            {
                Success = true,
                Message = "Success",
                Data = employees
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<EmployeeDTO>>
            {
                Success = false,
                Message = $"Error message: {ex.Message}"
            };
        }
    }

    internal async Task<ApiResponse<EmployeeDTO>> GetEmployeeById(long id)
    {
        try
        {
            var requestUri = $"employee/{id}";

            using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return new ApiResponse<EmployeeDTO>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            var employee = await response.Content.ReadFromJsonAsync<EmployeeDTO>();

            return new ApiResponse<EmployeeDTO>
            {
                Success = true,
                Message = "Success",
                Data = employee
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return new ApiResponse<EmployeeDTO>
            {
                Success = false,
                Message = $"Error message: {ex.Message}"
            };
        }
    }

    internal async Task<ApiResponse<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDTO)
    {
        try
        {
            var requestUri = "employee";

            var postResponse = await _httpClient.PostAsJsonAsync(requestUri, employeeDTO);

            if (!postResponse.IsSuccessStatusCode)
            {
                string errorMessage = await postResponse.Content.ReadAsStringAsync();
                return new ApiResponse<EmployeeDTO>
                {
                    Success = false,
                    Message = errorMessage
                };
            }

            var createdEmployee = await postResponse.Content.ReadFromJsonAsync<EmployeeDTO>();

            return new ApiResponse<EmployeeDTO>
            {
                Success = true,
                Message = "Employee created successfully",
                Data = createdEmployee
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message:{ex.Message}");
            return new ApiResponse<EmployeeDTO>
            {
                Success = false,
                Message = $"Error message:{ex.Message}"
            };
        }
    }

    internal async Task<ApiResponse<EmployeeDTO>> UpdateEmployee(EmployeeDTO employeeDTO, long id)
    {
        try
        {
            var requestUri = $"employee/{id}";

            var postResponse = await _httpClient.PutAsJsonAsync(requestUri, employeeDTO);

            if (!postResponse.IsSuccessStatusCode)
            {
                string errorMessage = await postResponse.Content.ReadAsStringAsync();
                return new ApiResponse<EmployeeDTO>
                {
                    Success = false,
                    Message = errorMessage
                };
            }

            var updatedEmployee = await postResponse.Content.ReadFromJsonAsync<EmployeeDTO>();

            return new ApiResponse<EmployeeDTO>
            {
                Success = true,
                Message = "Employee's name has been successfully updated",
                Data = updatedEmployee
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message:{ex.Message}");
            return new ApiResponse<EmployeeDTO>
            {
                Success = false,
                Message = $"Error message:{ex.Message}"
            };
        }
    }

    internal async Task<ApiResponse<bool>> DeleteEmployee(long id)
    {
        try
        {
            var requestUri = $"employee/{id}";
            using HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Employee has been deleted successfully",
                    StatusCode = response.StatusCode
                };
            }

            return new ApiResponse<bool>
            {
                Success = false,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return new ApiResponse<bool>
            {
                Success = false,
                Message = $"Error message: {ex.Message}"
            };
        }
    }
}