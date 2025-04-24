using System.Text;
using System.Text.Json;

public class ApiHandler
{
    private const string API_URL = "https://localhost:7225/api/shiftlog";

    public async Task<List<Shift>> GetAllShifts()
    {
        List<Shift>? allShifts = null;
        using (var client = new HttpClient())
        {
            try
            {
                using (var stream = await client.GetStreamAsync(API_URL))
                {
                    allShifts = JsonSerializer.Deserialize<List<Shift>>(stream);
                }

                allShifts?.Sort((s1, s2) => s2.startDateTime.CompareTo(s1.startDateTime));
            }
            catch (HttpRequestException e)
            {
                WriteHttpRequestException(e);
                Console.ReadLine();
            }
        }

        return allShifts ?? new();
    }

    public async Task<Shift?> GetShift(string id)
    {
        Shift? shift = null;

        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        using (var client = new HttpClient())
        {
            string url = $"{API_URL}/{id}";
            try
            {
                using (var stream = await client.GetStreamAsync(url))
                {
                    shift = JsonSerializer.Deserialize<Shift>(stream);
                }
            }
            catch (HttpRequestException e)
            {
                WriteHttpRequestException(e);
                Console.ReadLine();

                return null;
            }
        }

        return shift;
    }

    public async Task<bool> PostShift(ShiftDto shiftDto)
    {
        using (var client = new HttpClient())
        {
            var serializedContent = JsonSerializer.Serialize(shiftDto);
            StringContent content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(API_URL, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                WriteHttpRequestException(e);
                return false;
            }
        }

        return true;
    }

    public async Task<bool> PutShift(Shift updatedRecord)
    {
        using (var client = new HttpClient())
        {
            var serializedContent = JsonSerializer.Serialize(updatedRecord);
            StringContent content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync(API_URL, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                WriteHttpRequestException(e);
                return false;
            }
        }

        return true;
    }

    public async Task<bool> DeleteShift(string idString)
    {
        using (var client = new HttpClient())
        {
            string url = $"{API_URL}/{idString}";
            try
            {
                var response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                WriteHttpRequestException(e);
                return false;
            }
        }

        return true;
    }

    private void WriteHttpRequestException(HttpRequestException e)
    {
        Console.WriteLine();
        Console.WriteLine("HTTP Error: " + e.HttpRequestError);
        Console.WriteLine(e.Message);
    }
}