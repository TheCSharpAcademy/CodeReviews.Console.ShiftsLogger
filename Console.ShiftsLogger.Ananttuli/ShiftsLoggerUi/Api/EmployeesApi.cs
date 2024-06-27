using System.Net.Http.Json;
using System.Text.Json;
using ShiftsLoggerUi.Api.Employees;
using ShiftsLoggerUi.Api.Shifts;

namespace ShiftsLoggerUi.Api;

public class EmployeesApi(Client httpClient)
{
    readonly Client HttpClient = httpClient;

    public async Task<Response<List<EmployeeDto>?>> GetEmployees()
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["EMPLOYEES"]);

            await using Stream stream = await HttpClient.client.GetStreamAsync(endpoint);

            var employees = await JsonSerializer
                .DeserializeAsync<List<EmployeeDto>>(stream);

            return new Response<List<EmployeeDto>?>(employees != null, employees ?? null);
        }
        catch
        {
            return new Response<List<EmployeeDto>?>(false, null, "Could not fetch employees");
        }
    }

    public async Task<Response<EmployeeDto?>> GetEmployee(int id)
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["EMPLOYEES"]);

            await using Stream stream = await HttpClient.client.GetStreamAsync($"{endpoint}/{id}");

            var employee = await JsonSerializer
                .DeserializeAsync<EmployeeDto>(stream);

            return new Response<EmployeeDto?>(employee != null, employee ?? null);
        }
        catch
        {
            return new Response<EmployeeDto?>(false, null, "Could not fetch employee");
        }
    }

    public async Task<Response<EmployeeDto?>> CreateEmployee(EmployeeCreateDto employee)
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["EMPLOYEES"]);

            var response = await HttpClient.client.PostAsJsonAsync(endpoint, employee);

            var apiError = await ApiErrorResponse.ExtractErrorFromResponse(response);

            if (apiError.Data != null)
            {
                return new Response<EmployeeDto?>(false, null, apiError.Data);
            }

            var createdEmployee = await response.Content.ReadFromJsonAsync<EmployeeDto>();

            return new Response<EmployeeDto?>(createdEmployee?.EmployeeId != null, createdEmployee ?? null);
        }
        catch
        {
            return new Response<EmployeeDto?>(false, null, "Unknown error");
        }
    }

    public async Task<Response<EmployeeDto?>> UpdateEmployee(EmployeeUpdateDto employee)
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["EMPLOYEES"]);

            var response = await HttpClient.client.PutAsJsonAsync($"{endpoint}/{employee.EmployeeId}", employee);

            var apiError = await ApiErrorResponse.ExtractErrorFromResponse(response);

            if (apiError.Data != null)
            {
                return new Response<EmployeeDto?>(false, null, apiError.Data);
            }

            var updatedEmployee = await response.Content.ReadFromJsonAsync<EmployeeDto>();

            return new Response<EmployeeDto?>(updatedEmployee?.EmployeeId != null, updatedEmployee ?? null);
        }
        catch
        {
            return new Response<EmployeeDto?>(false, null, "Unknown error");
        }
    }

    public async Task<Response<bool>> DeleteEmployee(int id)
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["EMPLOYEES"]);

            var response = await HttpClient.client.DeleteAsync($"{endpoint}/{id}");

            var apiError = await ApiErrorResponse.ExtractErrorFromResponse(response);

            if (apiError?.Data != null)
            {
                return new Response<bool>(false, false, apiError.Data);
            }

            return new Response<bool>(true, true);
        }
        catch
        {
            return new Response<bool>(false, false, "Unknown error");
        }
    }
}

