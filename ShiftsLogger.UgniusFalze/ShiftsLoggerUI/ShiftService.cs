using Microsoft.Extensions.Configuration;
using RestSharp;
using ShiftsLoggerUI.Records;

namespace ShiftsLoggerUI;

public class ShiftService
{
    private string ApiUrl { get; set; }

    public ShiftService()
    {
        var url = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
            .GetSection("AppSettings")["ApiUrl"];
        if (url == null)
        {
            throw new Exception("Failed to get api url");
        }

        ApiUrl = url;
    }
    
    public List<Shift> GetShifts()
    {
        var client = new RestClient(ApiUrl);
        var response = client.GetJson<List<Shift>>("shifts");
        return response ?? new List<Shift>();
    }

    public bool AddShift(Shift shift)
    {
        var client = new RestClient(ApiUrl);
        var response = client.PostJson("shifts", shift);
        return response == System.Net.HttpStatusCode.Created;
    }

    public bool DeleteShift(Shift shift)
    {
        var client = new RestClient(ApiUrl);
        var request = new RestRequest("shifts/{id}", Method.Delete)
            .AddUrlSegment("id", shift.ShiftId);
        var response = client.Execute<RestResponse>(request);
        return response.IsSuccessful;
    }

    public bool UpdateShift(Shift shift)
    {
        var client = new RestClient(ApiUrl);
        var request = new RestRequest("shifts/{id}", Method.Put)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(shift);
        request.AddUrlSegment("id", shift.ShiftId);
        var response = client.Execute<RestResponse>(request);
        return response.IsSuccessful;
    }
    
}