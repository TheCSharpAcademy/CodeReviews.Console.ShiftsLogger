using Newtonsoft.Json;
using RestSharp;
using ShiftLoggerUi.DTOs;

namespace ShiftLoggerUi.Services;

internal class ShiftService
{
    private readonly RestClient _client;

    public ShiftService()
    {
        _client = new RestClient("https://localhost:7225/");
    }

    public List<ShiftDto> GetAllShifts()
    {
        var request = new RestRequest("shifts", Method.Get);
        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<List<ShiftDto>>(response.Content);
        }

        return new List<ShiftDto>();
    }

    public List<ShiftDto> GetShiftsByWorker(int workerId)
    {
        var request = new RestRequest($"shifts/{workerId}", Method.Get);
        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<List<ShiftDto>>(response.Content);
        }

        return new List<ShiftDto>();
    }

    public List<ShiftDto> GetShiftsByDepartment(int departmentId)
    {
        var request = new RestRequest($"shifts/department/{departmentId}", Method.Get);
        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<List<ShiftDto>>(response.Content);
        }

        return new List<ShiftDto>();
    }

    public ShiftDto CreateShift(ShiftDto shift)
    {
        var request = new RestRequest("shifts", Method.Post);
        request.AddJsonBody(shift);

        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            return JsonConvert.DeserializeObject<ShiftDto>(response.Content);
        }

        return null;
    }

    public bool UpdateShift(int shiftId, ShiftDto shift)
    {
        var request = new RestRequest($"shifts/{shiftId}", Method.Put);
        request.AddJsonBody(shift);

        var response = _client.ExecuteAsync(request).Result;
        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }

    public bool DeleteShift(int shiftId)
    {
        var request = new RestRequest($"shifts/{shiftId}", Method.Delete);
        var response = _client.ExecuteAsync(request).Result;

        return response.StatusCode == System.Net.HttpStatusCode.NoContent;
    }
}
