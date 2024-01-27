using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using ShiftsLoggerClient.Models;
using NewtonJson = Newtonsoft.Json;

namespace ShiftsLoggerClient.Controllers;

public class JsonController
{
    public async static Task<List<T>?> DeserializeResponse<T>(HttpResponseMessage response) where T: class
    {
        return await JsonSerializer.DeserializeAsync<List<T>>(response.Content.ReadAsStream());
    }

    public static string CreateShiftPatch(DateTime shiftEndTime)
    {
        var patchShiftEndTime = new JsonPatchDocument<ShiftJson>();
        patchShiftEndTime.Replace( p => p.ShiftEndTime, shiftEndTime);
        return NewtonJson.JsonConvert.SerializeObject(patchShiftEndTime);
    }

    public static string? AppSettings()
    {       
        try
        {
            IConfiguration jsonConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            return jsonConfig.GetSection("Settings")["ApplicationUrl"];
        }
        catch
        {
            return null;
        }   
    }
}