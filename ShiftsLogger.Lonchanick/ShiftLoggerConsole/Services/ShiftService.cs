using System.Net.Http.Json;
using Newtonsoft.Json;
using ShiftsLoggerConsole.Models;

namespace ShiftLoggerConsole.Services;

public class ShiftService
{
    public HttpClient client = new();
    public static readonly string getUrl="http://localhost:5180/api/Shift";
    public static readonly string getByIdUrl="http://localhost:5180/api/Worker/";
    public static readonly string updateUrl="http://localhost:5180/api/Worker/";
    public async Task<IEnumerable<Shift>?> GetAsync()
    {
        List<Shift>? shifts=null;

        HttpResponseMessage response = await client.GetAsync(getUrl);

        if(response is not null)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            shifts = JsonConvert.DeserializeObject<List<Shift>>(data);
            return shifts;
        }

        return shifts;
    }

    public Task<Worker?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Add(Shift shift)
    {
        string tempUrl=getUrl;
        var r = await client.PostAsJsonAsync(tempUrl,shift);

        if (r.IsSuccessStatusCode)
            return 1;

        return -1;

    }

}