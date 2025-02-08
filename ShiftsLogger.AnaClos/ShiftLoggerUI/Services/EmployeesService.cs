using Newtonsoft.Json;
using RestSharp;
using ShiftsLoggerWebAPI.DTOs;

namespace ShiftLoggerUI.Services;

public class EmployeesService
{
    RestClient client;
    RestRequest request;

    public EmployeesService()
    {
        client = new RestClient("https://localhost:7178/api/Employee/");
    }

    public EmployeeDto GetEmployee(int id)
    {
        try
        {
            request = new RestRequest($"{id}");
            var response = client.GetAsync(request);
            string responseContent = response.Result.Content;
            var employeeDto = JsonConvert.DeserializeObject<EmployeeDto>(responseContent);
            return employeeDto;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<EmployeeDto>? GetEmployees()
    {
        try
        {
            request = new RestRequest("");
            var response = client.GetAsync(request);
            string responseContent = response.Result.Content;
            var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(responseContent);
            return employees;
        }
        catch (Exception ex)
        {
            return null;
        }

    }
    public string AddEmployee(EmployeeDto employeeDto)
    {

        try
        {
            request = new RestRequest("", Method.Post);
            request.AddJsonBody(employeeDto);
            var response = client.ExecuteAsync(request);
            string rawResponse = response.Result.Content;
            return rawResponse;
        }
        catch (Exception ex)
        {
            return null;
        }

    }
    public string UpdateEmployee(EmployeeDto employeeDto)
    {
        try
        {
            request = new RestRequest("", Method.Put);
            request.AddJsonBody(employeeDto);
            var response = client.ExecuteAsync(request);
            string rawResponse = response.Result.Content;
            return rawResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string RemoveEmployee(int id)
    {
        try
        {
            request = new RestRequest($"{id}", Method.Delete);
            var response = client.ExecuteAsync(request);
            string rawResponse = response.Result.Content;
            return rawResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }    
}

