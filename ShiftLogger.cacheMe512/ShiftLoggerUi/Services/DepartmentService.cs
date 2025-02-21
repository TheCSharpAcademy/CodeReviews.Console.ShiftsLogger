using Newtonsoft.Json;
using RestSharp;
using ShiftLoggerUi.DTOs;

namespace ShiftLoggerUi.Services;

internal class DepartmentService
{
    private readonly RestClient _client;

    public DepartmentService()
    {
        _client = new RestClient("https://localhost:7225/");
    }

    public List<DepartmentDto> GetAllDepartments()
    {
        var request = new RestRequest("departments", Method.Get);
        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<List<DepartmentDto>>(response.Content);
        }

        return new List<DepartmentDto>();
    }

    public DepartmentDto GetDepartmentById(int departmentId)
    {
        var request = new RestRequest($"departments/{departmentId}", Method.Get);
        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<DepartmentDto>(response.Content);
        }

        return null;
    }

    public DepartmentDto CreateDepartment(DepartmentDto department)
    {
        var request = new RestRequest("departments", Method.Post);
        request.AddJsonBody(department);

        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            return JsonConvert.DeserializeObject<DepartmentDto>(response.Content);
        }

        return null;
    }

    public bool UpdateDepartment(int departmentId, DepartmentDto department)
    {
        var request = new RestRequest($"departments/{departmentId}", Method.Put);
        request.AddJsonBody(department);

        var response = _client.ExecuteAsync(request).Result;
        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }

    public bool DeleteDepartment(int departmentId)
    {
        var request = new RestRequest($"departments/{departmentId}", Method.Delete);
        var response = _client.ExecuteAsync(request).Result;

        return response.StatusCode == System.Net.HttpStatusCode.NoContent;
    }
}
