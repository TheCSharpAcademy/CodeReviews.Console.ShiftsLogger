using RestSharp;
using ShiftsLogger.Console.Models;
using Spectre.Console;

namespace ShiftsLogger.Console;

public class ApiClient
{
    private RestClient _client = new RestClient("http://localhost:5000");
    
    public List<Employee>? GetEmployees()
    {
        var request = new RestRequest("/employees", Method.Get);
        
        try
        {
            var response = _client.Execute<List<Employee>>(request);
    
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.StatusCode}: {response.ErrorMessage}[/]");
                return null;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }
    
        return null;
    }

    public Employee? GetEmployeeById(int id)
    {
        var request = new RestRequest($"/employees/{id}", Method.Get);

        try
        {
            var response = _client.Execute<Employee>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }
    
    public Employee? CreateEmployee(Employee employee)
    {
        var request = new RestRequest($"/employees/", Method.Post)
            .AddJsonBody(employee);

        try
        {
            var response = _client.ExecutePost<Employee>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }
    
    public Employee? UpdateEmployee(int id, Employee employee)
    {
        var request = new RestRequest($"/employees/{id}/", Method.Put)
            .AddJsonBody(employee);

        try
        {
            var response = _client.ExecutePut<Employee>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }

    public string? DeleteEmployee(int id)
    {
        var request = new RestRequest($"/employees/{id}", Method.Delete);

        try
        {
            var response = _client.ExecuteDelete(request);

            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }
    
    public List<Shift>? GetShiftsByEmployeeId(int id)
    {
        var request = new RestRequest($"/employees/{id}/shifts", Method.Get);

        try
        {
            var response = _client.Execute<List<Shift>>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }

    public List<Shift>? GetShifts()
    {
        var request = new RestRequest($"/shifts/", Method.Get);

        try
        {
            var response = _client.Execute<List<Shift>>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }
        
        return null;
    }

    public Shift? GetShiftById(int id)
    {
        var request = new RestRequest($"/shifts/{id}", Method.Get);

        try
        {
            var response = _client.Execute<Shift>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }

    public Shift? CreateShift(Shift shift)
    {
        var request = new RestRequest($"/shifts/", Method.Post)
            .AddJsonBody(shift);

        try
        {
            var response = _client.ExecutePost<Shift?>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }
    
    public Shift? UpdateShift(int id, Shift shift)
    {
        var request = new RestRequest($"/shifts/{id}/", Method.Put)
            .AddJsonBody(shift);

        try
        {
            var response = _client.ExecutePut<Shift>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }
    
    public string? DeleteShift(int id)
    {
        var request = new RestRequest($"/shifts/{id}", Method.Delete);

        try
        {
            var response = _client.ExecuteDelete(request);

            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error - {response.ErrorMessage}[/]");
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
        }

        return null;
    }
}