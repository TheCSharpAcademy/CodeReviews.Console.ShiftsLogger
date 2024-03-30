using Newtonsoft.Json;
using ShiftLoggerClient.Models;
using System.Text;

namespace ShiftLoggerClient.ClientControllers;

internal class EmployeeClientController
{
    internal static EmployeeDTO AddEmployeeDTO(EmployeeDTO newEmployee)
    {

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            string jsonEmployee = JsonConvert.SerializeObject(newEmployee);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/employee", content).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the response content to EmployeeDTO
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            EmployeeDTO addedEmployee = JsonConvert.DeserializeObject<EmployeeDTO>(jsonResponse);

            return addedEmployee;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    internal static EmployeeDTO GetEmployeeDTO(int employeeID)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            HttpResponseMessage response = client.GetAsync($"api/employee/id?id={employeeID}").Result;
            response.EnsureSuccessStatusCode();

            string jsonContent = response.Content.ReadAsStringAsync().Result;
            var employee = JsonConvert.DeserializeObject<EmployeeDTO>(jsonContent);
            return employee;
        }

        catch (HttpRequestException ex)
        {

            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return new EmployeeDTO();
        }

        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
    internal static List<EmployeeDTO> GetEmployeeDTOList()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            HttpResponseMessage response = client.GetAsync("api/employee").Result;
            response.EnsureSuccessStatusCode();

            string jsonContent = response.Content.ReadAsStringAsync().Result;
            var employees = JsonConvert.DeserializeObject<List<EmployeeDTO>>(jsonContent);
            return employees;
        }

        catch (HttpRequestException ex)
        {

            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            Console.ReadKey();
            return new List<EmployeeDTO>();
        }

        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
    internal static EmployeeDTO ModifyEmployeeDTO(EmployeeDTO modifiedEmployee)
    {

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            string jsonEmployee = JsonConvert.SerializeObject(modifiedEmployee);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"api/employee/{modifiedEmployee.Id}", content).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the response content to EmployeeDTO
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            EmployeeDTO modEmployeeReturn = JsonConvert.DeserializeObject<EmployeeDTO>(jsonResponse);

            return modEmployeeReturn;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    internal static bool DeleteEmployeeDTO(EmployeeDTO employeeToDelete)
    {

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            string jsonEmployee = JsonConvert.SerializeObject(employeeToDelete);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync($"api/employee/{employeeToDelete.Id}").Result;
            response.EnsureSuccessStatusCode();

            return true;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

}
