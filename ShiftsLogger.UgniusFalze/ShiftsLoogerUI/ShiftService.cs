using RestSharp;
using ShiftsLoogerUI.Records;

namespace ShiftsLoogerUI;

public class ShiftService
{
    private string ApiUrl { get; set; }
    public ShiftService(string apiUrl = "https://localhost:7200/api")
    {
        ApiUrl = apiUrl;
    }

    public List<Shift> GetShifts()
    {
        var client = new RestClient(ApiUrl);

        var response = client.GetJson<List<Shift>>("shifts");
        return response ?? new List<Shift>();
    }
    
}