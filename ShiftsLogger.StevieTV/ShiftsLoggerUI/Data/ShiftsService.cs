using System.Net;
using RestSharp;
using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI.Data;

public class ShiftsService
{
    private const string ApiUrl = "https://localhost:44315/api/";

    public static List<Shift> GetShifts()
    {
        var client = new RestClient(ApiUrl);

        try
        {
            var shifts = client.GetJson<List<Shift>>("shifts");
            if (shifts == null) return new List<Shift>();

            foreach (var shift in shifts)
            {
                shift.Duration = shift.EndTime - shift.StartTime;
            }
        
            return shifts;
        }
        catch (Exception)
        {
          AnsiConsole.WriteLine("Unable to access API");
          return new List<Shift>();
        }
    }

    public static bool AddShift(Shift shift)
    {
        var client = new RestClient(ApiUrl);

        try
        {
            var result = client.PostJson("shifts", shift);
            return result == HttpStatusCode.OK;
        }
        catch (Exception)
        {
            AnsiConsole.WriteLine("Unable to add shift - API inaccessible");
            return false;
        }
    }

    public static bool DeleteShift(Shift shift)
    {
        var client = new RestClient(ApiUrl);
        var request = new RestRequest($"shifts/{shift.ShiftId}", Method.Delete);

        try
        {
            var response = client.ExecuteAsync(request);
            return response.Result.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception)
        {
            AnsiConsole.WriteLine("Unable to Delete shift - API inaccessible");
            return false;
        }
    }

    public static bool UpdateShift(Shift shift)
    {
        var client = new RestClient(ApiUrl);

        try
        {
            var result = client.PutJson($"shifts/{shift.ShiftId}", shift);
            return result == HttpStatusCode.OK;
        }
        catch (Exception)
        {
            AnsiConsole.WriteLine("Unable to update shift - API inaccessible");
            return false;
        }
    }
}