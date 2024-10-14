using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ShiftLogger.Console_App.Models;

namespace ShiftLogger.Console_App;

public class ShiftService
{
    private string _url;
    private readonly HttpClient client;
    public ShiftService(string url)
    {
        _url = url;
        client = new HttpClient();
    }
    public async Task<IEnumerable<Shift>> GetAllShiftAsync()
    {
        try
        {
            Uri uri = new Uri(_url);

            using HttpResponseMessage responseMessage = await client.GetAsync(uri);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();

                var allShift = JsonConvert.DeserializeObject<List<Shift>>(response) ?? new List<Shift>();
                return allShift;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nException Caught: ");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }

        return new List<Shift>();
    }

    public async Task<Shift> CreateShiftAsync(Shift shift)
    {
        try
        {
            using StringContent jsonContent = new(JsonConvert.SerializeObject(shift), Encoding.UTF8, "application/json");
            Uri uri = new Uri(_url);

            using HttpResponseMessage responseMessage = await client.PostAsync(uri, jsonContent);
            if (responseMessage.StatusCode == HttpStatusCode.Created)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();
                var createdShift = JsonConvert.DeserializeObject<Shift>(response);

                return createdShift ?? new Shift();
            }
            else if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Bad Request");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nException Caught: ");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }

        return new Shift();
    }

    public async Task<Shift> UpdateShiftAsync(Shift shift)
    {
        try
        {
            Uri uri = new Uri(_url + $"/{shift.Id}");
            using StringContent jsonContent = new(JsonConvert.SerializeObject(shift), Encoding.UTF8, "application/json");
            using HttpResponseMessage responseMessage = await client.PutAsync(uri, jsonContent);

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                return shift;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("\nException Caught: ");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }

        return new Shift();
    }

    public async Task<int> DeleteIdAsync(int id)
    {
        try
        {
            Uri uri = new Uri(_url + $"/{id}");

            using HttpResponseMessage responseMessage = await client.DeleteAsync(uri);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                return id;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nException Caught: ");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }

        return 0;
    }

}
