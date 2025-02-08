using Newtonsoft.Json;
using RestSharp;
using ShiftsLoggerWebAPI.DTOs;

namespace ShiftLoggerUI.Services;

public class ShiftsService
{
    RestClient client;
    RestRequest request;

    public ShiftsService()
    {
        client = new RestClient("https://localhost:7178/api/Shift/");
    }

    public ShiftDto GetShift(int id)
    {
        try
        {
            request = new RestRequest($"{id}");
            var response = client.GetAsync(request);
            string responseContent = response.Result.Content;
            var shiftDto = JsonConvert.DeserializeObject<ShiftDto>(responseContent);
            return shiftDto;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string AddShift(ShiftDto shiftDto)
    {
        try
        {
            request = new RestRequest("", Method.Post);
            request.AddJsonBody(shiftDto);
            var response = client.ExecuteAsync(request);
            string rawResponse = response.Result.Content;
            return rawResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<ShiftDto> GetShifts()
    {
        try
        {
            request = new RestRequest("");
            var response = client.GetAsync(request);
            string responseContent = response.Result.Content;
            var shifts = JsonConvert.DeserializeObject<List<ShiftDto>>(responseContent);
            return shifts;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<ShiftDto> GetLast10Shifts(int employeeId)
    {
        try
        {
            request = new RestRequest($"Employee/{employeeId}");
            var response = client.GetAsync(request);
            string responseContent = response.Result.Content;
            var shift = JsonConvert.DeserializeObject<List<ShiftDto>>(responseContent);
            return shift;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string UpdateShift(ShiftDto shiftDto)
    {
        try
        {
            request = new RestRequest("", Method.Put);
            request.AddJsonBody(shiftDto);
            var response = client.ExecuteAsync(request);
            string rawResponse = response.Result.Content;
            return rawResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string RemoveShift(int id)
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