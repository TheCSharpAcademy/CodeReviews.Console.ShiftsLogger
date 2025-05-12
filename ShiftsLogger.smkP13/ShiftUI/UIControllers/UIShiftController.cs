using Newtonsoft.Json;
using RestSharp;
using ShiftUI.UIModels;
using Spectre.Console;

namespace ShiftUI.UIControllers;

class UIShiftController
{

    internal static void CreateNewShift(UIShift shift)
    {
        try
        {
            using RestClient client = new("https://localhost:7029/");
            RestRequest request = new("api/Shift");
            request.AddBody(shift);
            RestResponse response = client.PostAsync(request).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AnsiConsole.WriteLine("Shift successfully added.");
                return;
            }
            AnsiConsole.WriteLine("The shift was not added. Verify shift data format.");
        }
        catch (Exception ex) { Console.Write(ex.Message); }
    }

    internal static void DeleteShift(int id)
    {
        try
        {
            using RestClient client = new("https://localhost:7029/");
            RestRequest request = new($"api/Shift/{id}");
            RestResponse response = client.DeleteAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK) AnsiConsole.WriteLine("Shift successfully deleted.");
            else AnsiConsole.WriteLine("The shift was not deleted. Verify shift data format.");
        }
        catch (Exception ex) { Console.Write(ex.Message); }
    }

    internal static List<UIShift>? GetAllShifts()
    {
        List<UIShift>? shiftsDTO = [];
        try
        {
            using RestClient client = new("https://localhost:7029/");
            RestRequest request = new("api/Shift");
            RestResponse response = client.GetAsync(request).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string? rawResponse = response.Content;
                shiftsDTO = JsonConvert.DeserializeObject<List<UIShift>>(rawResponse);
                return shiftsDTO;
            }
        }
        catch (Exception ex) { Console.Write(ex.Message); }
        return shiftsDTO;
    }

    internal static List<UIShift>? GetShiftsByUserId(int userId)
    {
        List<UIShift>? shifts = [];
        try
        {
            using RestClient client = new("https://localhost:7029/");
            RestRequest request = new($"api/Shift/User/{userId}");
            RestResponse response = client.GetAsync(request).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string? rawResponse = response.Content;
                if(rawResponse != null) shifts = JsonConvert.DeserializeObject<List<UIShift>>(rawResponse);
            }
        }
        catch (Exception ex) { Console.Write(ex.Message); }
        return shifts;
    }

    internal static void UpdateShift(UIShift shift)
    {
        try
        {
            using RestClient client = new("https://localhost:7029/");
            RestRequest request = new($"api/Shift/{shift.id}");
            request.AddBody(shift);
            RestResponse response = client.PutAsync(request).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AnsiConsole.WriteLine("Shift successfully updated.");
                return;
            }
            AnsiConsole.WriteLine("The shift was not updated. Verify shift data format.");
        }
        catch (Exception ex) { Console.Write(ex.Message); }
    }
}
