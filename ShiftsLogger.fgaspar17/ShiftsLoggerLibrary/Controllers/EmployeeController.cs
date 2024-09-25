using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ShiftsLoggerLibrary;

public static class EmployeeController
{
    private const string Endpoint = ShiftsLoggerApiClient.BaseUrl + "/employees";
    public static async Task<List<Employee>> GetEmployeesAsync()
    {
        try
        {
            using var responseMessage = await ShiftsLoggerApiClient.Instance.GetAsync(Endpoint);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<List<Employee>>() ?? new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return null;
    }

    public static async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        try
        {
            using var responseMessage = await ShiftsLoggerApiClient.Instance.GetAsync(Endpoint + $"/{employeeId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<Employee>() ?? new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return null;
    }

    public static async Task<Employee> InsertEmployeeAsync(Employee employee)
    {
        try
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            using var responseMessage = await ShiftsLoggerApiClient.Instance.PostAsync(Endpoint, stringContent);

            return await responseMessage.Content.ReadFromJsonAsync<Employee>() ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return null;
    }

    public static async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        try
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            using var responseMessage = await ShiftsLoggerApiClient.Instance.PutAsync(Endpoint + $"/{employee.EmployeeId}", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return false;
    }

    public static async Task<bool> DeleteEmployeeByIdAsync(int employeeId)
    {
        try
        {
            using var responseMessage = await ShiftsLoggerApiClient.Instance.DeleteAsync(Endpoint + $"/{employeeId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return false;
    }
}