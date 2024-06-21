using Newtonsoft.Json;
using RestSharp;
using Shiftlogger.UI.DTOs;

namespace Shiftlogger.UI.Services;

internal class ShiftService
{
    internal string ApiBaseUrl = "https://localhost:7225/api/";

    internal async Task<List<ShiftReqestDto>?> GetAllShifts()
    {
        if (!await IsApiRunning())
        {
            Console.WriteLine("API is not running.");
            return null;
        }
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("shift");
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rawResponse = response.Content;
                var shifts = JsonConvert.DeserializeObject<List<ShiftReqestDto>>(rawResponse);
                return shifts;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                if (response.Content != null)
                    Console.WriteLine(response.Content);
                return null;
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch shifts.");
            Console.WriteLine(httpEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch shifts.");
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    internal async Task<ShiftReqestDto?> GetShiftById(int id)
    {
        if (!await IsApiRunning())
        {
            Console.WriteLine("API is not running.");
            return null;
        }
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("shift/{id}");
            request.AddUrlSegment("id", id);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rawResponse = response.Content;
                var shift = JsonConvert.DeserializeObject<ShiftReqestDto>(rawResponse);
                return shift;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                if (response.Content != null)
                    Console.WriteLine(response.Content);
                return null;
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch shifts.");
            Console.WriteLine(httpEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch shifts.");
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    internal async Task<ShiftReqestDto?> PostShift(ShiftNewDto shift)
    {
        if (!await IsApiRunning())
        {
            Console.WriteLine("API is not running.");
            return null;
        }
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("shift", Method.Post);
            request.AddJsonBody(shift);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var rawResponse = response.Content;
                var addedShift = JsonConvert.DeserializeObject<ShiftReqestDto>(rawResponse);
                return addedShift;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                if (response.Content != null)
                    Console.WriteLine(response.Content);
                return null;
            }
            return null;
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch shifts.");
            Console.WriteLine(httpEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch shifts.");
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    internal async Task<bool> PutShift(int id, ShiftDto shift)
    {
        if (!await IsApiRunning())
        {
            Console.WriteLine("API is not running.");
            return false;
        }
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("shift/{id}", Method.Put);
            request.AddUrlSegment("id", id);
            request.AddJsonBody(shift);
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                if (response.Content != null)
                    Console.WriteLine(response.Content);
                return false;
            }
            return true;
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch shifts.");
            Console.WriteLine(httpEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch shifts.");
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    internal async Task<bool> DeleteShift(int id)
    {
        if (!await IsApiRunning())
        {
            Console.WriteLine("API is not running.");
            return false;
        }
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("shift/{id}", Method.Delete);
            request.AddUrlSegment("id", id);
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                if (response.Content != null)
                    Console.WriteLine(response.Content);
                return false;
            }
            return true;
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch shifts.");
            Console.WriteLine(httpEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch shifts.");
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> IsApiRunning()
    {
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("health");
            var response = await client.ExecuteAsync(request);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to check API status.");
            Console.WriteLine(httpEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to check API status.");
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}