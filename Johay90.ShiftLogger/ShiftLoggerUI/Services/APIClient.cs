using System.Net.Http.Json;
using System.Text.Json;
using SharedLibrary.DTOs;

namespace ShiftLoggerUI.Services
{
    public class APIClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public APIClient(string baseUrl, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl);
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ICollection<EmployeeDto>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("api/Employees");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ICollection<EmployeeDto>>(_jsonOptions);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Employees/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmployeeDto>(_jsonOptions);
        }

        public async Task CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Employees", employeeDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Employees/{id}", employeeDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Employees/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<ICollection<ShiftDto>> GetAllShiftsAsync()
        {
            var response = await _httpClient.GetAsync("api/Shifts");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ICollection<ShiftDto>>(_jsonOptions);
        }

        public async Task<ShiftDto> GetShiftAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Shifts/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ShiftDto>(_jsonOptions);
        }

        public async Task CreateShiftAsync(CreateShiftDto shiftDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Shifts", shiftDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateShiftAsync(int id, UpdateShiftDto shiftDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Shifts/{id}", shiftDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteShiftAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Shifts/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}