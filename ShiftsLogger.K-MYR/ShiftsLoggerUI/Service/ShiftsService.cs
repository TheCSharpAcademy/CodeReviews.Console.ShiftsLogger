using Newtonsoft.Json;
using ShiftsLoggerUI.Models;
using Spectre.Console;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ShiftsLoggerUI;

internal class ShiftsService
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7223")
    };

    private static TokenResponse? _Token;

    internal static bool AddShift((DateTime, DateTime) value)
    {
        try
        {
            CheckToken();

            var content = new StringContent(JsonConvert.SerializeObject(new { startTime = value.Item1, endTime = value.Item2 }), Encoding.UTF8, "application/json");

            var result = _httpClient.PostAsync("api/Shifts", content).Result;

            if (result.IsSuccessStatusCode)
            {
                Helpers.WriteAndWait("[springgreen2_1]Shift Added Sucessfully! Press Any Key To Continue[/]");
                return true;
            }
            else
            {
                Helpers.WriteAndWait($"[red]Shift Could Not Be Added! Press Any Key To Continue[/]");
                return false;
            }
        }
        catch (Exception)
        {
            Helpers.WriteAndWait("[red]An Error Occured Calling The API! Press Any Key To Continue[/]");
            return false;
        }
    }

    internal static bool UpdateShift(Shift shift, (DateTime, DateTime) value)
    {
        try
        {
            CheckToken();

            var content = new StringContent(JsonConvert.SerializeObject(new { startTime = value.Item1, endTime = value.Item2 }), Encoding.UTF8, "application/json");
            var result = _httpClient.PutAsync($"api/Shifts/{shift.Id}", content).Result;

            if (result.IsSuccessStatusCode)
            {
                Helpers.WriteAndWait("[springgreen2_1]Shift Updated Sucessfully! Press Any Key To Continue[/]");
                return true;
            }
            else
            {
                Helpers.WriteAndWait($"[red]Shift Could Not Be Updated! Press Any Key To Continue[/]");
                return false;
            }
        }
        catch (Exception)
        {
            Helpers.WriteAndWait("[red]An Error Occured Calling The API! Press Any Key To Continue[/]");
            return false;
        }
    }

    internal static bool DeleteById(int id)
    {
        try
        {
            CheckToken();

            var result = _httpClient.DeleteAsync($"api/Shifts/{id}").Result;

            if (result.IsSuccessStatusCode)
            {
                Helpers.WriteAndWait("[springgreen2_1]Shift Deleted Sucessfully! Press Any Key To Continue[/]");
                return true;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Helpers.WriteAndWait($"[red]Shift Could Not Be Found! Press Any Key To Continue[/]");
                return false;
            }
            else
            {
                Helpers.WriteAndWait($"[red]Shift Could Not Be Deleted! Press Any Key To Continue[/]");
                return false;
            }
        }
        catch (Exception)
        {
            Helpers.WriteAndWait("[red]An Error Occured Calling The API! Press Any Key To Continue[/]");
            return false;
        }
    }

    internal static async Task<List<Shift>> GetShifts()
    {
        try
        {
            CheckToken();

            var shifts = await _httpClient.GetFromJsonAsync<List<Shift>>("/api/Shifts/all");

            if (shifts is null)
                return new List<Shift>();

            return shifts;
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                Helpers.WriteAndWait("[red]No Records Were Found[/]");
            else
                Helpers.WriteAndWait("[red]An Error Occured Calling The API! Press Any Key To Continue[/]");

            return new List<Shift>();
        }
        catch (Exception)
        {
            Helpers.WriteAndWait("[red]An Error Occured Calling The API! Press Any Key To Continue[/]");

            return new List<Shift>();
        }
    }

    internal static async Task<bool> Login((string, string) value)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { email = value.Item1, password = value.Item2 }), Encoding.UTF8, "application/json");
            var result = _httpClient.PostAsync("/login", content).Result;

            if (result.IsSuccessStatusCode)
            {
                _Token = JsonConvert.DeserializeObject<TokenResponse>(await result.Content.ReadAsStringAsync());
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_Token!.TokenType, _Token.AccessToken);

                Helpers.WriteAndWait("[springgreen2_1]Login Sucessful! Press Any Key To Continue[/]");
                return true;
            }
            else
            {
                Helpers.WriteAndWait($"[red]Login Unsucessful! Press Any Key To Continue[/]");
                return false;
            }            
        }
        catch (Exception)
        {
            Helpers.WriteAndWait($"[red]An Error Occured Calling The API! Press Any Key To Continue[/]");
            return false;
        }
    }

    internal static async void Register((string, string) value)
    {
        try
        {
            var result = _httpClient.PostAsync("/register", new StringContent(JsonConvert.SerializeObject(new { email = value.Item1, password = value.Item2 }), Encoding.UTF8, "application/json")).Result;

            if (result.IsSuccessStatusCode)
            {
                Helpers.WriteAndWait("[springgreen2_1]Account Sucessfully Created! Press Any Key To Continue[/]");
            }
            else
            {
                var response = JsonConvert.DeserializeObject<RegisterResponse>(await result.Content.ReadAsStringAsync());

                if (response is not null)
                    Helpers.WriteAndWait($"[red]Account Could Not Be Created! {response.Title.TrimEnd('.')}: {response.Errors.ToString().EscapeMarkup()}Press Any Key To Continue[/]");
            }
        }
        catch (Exception)
        {
            Helpers.WriteAndWait($"[red]An Error Occured Calling The API! Press Any Key To Continue[/]");
        }
    }

    private static async void CheckToken()
    {
        try
        {
            if (_Token!.ExpirationDate < DateTime.UtcNow)
            {
                var result = _httpClient.PostAsync("/refresh", new StringContent(JsonConvert.SerializeObject(new { refreshToken = _Token.RefreshToken }))).Result;

                _Token = JsonConvert.DeserializeObject<TokenResponse>(await result.Content.ReadAsStringAsync());
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_Token!.TokenType, _Token.AccessToken);
            }
        }
        catch (Exception)
        {
            Helpers.WriteAndWait("[red]An Error Occured Calling The API! Press Any Key To Continue[/]");
        }
    }
}
