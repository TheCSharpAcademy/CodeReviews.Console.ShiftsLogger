using RestSharp;
using Newtonsoft.Json;
using Shared.Models;
using ShiftsLoggerUI.Utilities;
using Spectre.Console;

namespace ShiftsLoggerUI.Controllers;

public class ShiftController
{
    private readonly RestClient _restClient = new RestClient("https://localhost:44349/api/Shift");

    internal List<Shift>? GetAllShifts()
    {
        List<Shift>? allShifts = FetchShifts("");
        return allShifts;
    }

    internal void CreateShift(Shift shift)
    {
        ExecuteQuery(Method.Post, shift: shift);
    }

    internal void UpdateShift(Shift shift)
    {
        ExecuteQuery(Method.Put, $"/{shift.Id}", shift);
    }

    internal void DeleteShift(int id)
    {
        ExecuteQuery(Method.Delete, $"/{id}");
    }

    private List<Shift>? FetchShifts(string endPoint)
    {
        try
        {
            var request = new RestRequest(endPoint);
            var response = _restClient.ExecuteAsync(request).Result;
            
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                // Handle specific case of Internal Server Error (500)
                AnsiConsole.MarkupLine("[red]Internal Server Error occurred (500). Please try again later.[/]");
                Util.AskUserToContinue();
                return null;
            }
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AnsiConsole.MarkupLine($"{response.StatusCode}: {response.ErrorMessage} ");
                Util.AskUserToContinue();
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<List<Shift>>(response.Content);
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]Failed to parse the response content: {e.Message}[/]");
                Util.AskUserToContinue();
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"An unexpected error occurred: {ex.Message}");
            Util.AskUserToContinue();
            return null;
        }
    }

    private void ExecuteQuery(Method method, string endpoint = "", Shift shift = null)
    {
        try
        {
            var request = new RestRequest(endpoint, method);
            if (method != Method.Delete) request.AddJsonBody(shift);
            var response = _restClient.ExecuteAsync(request).Result;
            
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                // Handle specific case of Internal Server Error (500)
                AnsiConsole.MarkupLine("[red]Internal Server Error occurred (500). Please try again later.[/]");
                Util.AskUserToContinue();
                return;
            }
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                AnsiConsole.MarkupLine($"Request failed: {response.StatusCode} - {response.ErrorMessage}");
                Util.AskUserToContinue();
                return;
            }

            AnsiConsole.MarkupLine("Query executed successfully.");
            Util.AskUserToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"An unexpected error occurred: {ex.Message}");
            Util.AskUserToContinue();
        }
    }
}