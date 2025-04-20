using System.Text.Json;
using RestSharp;

public class ShiftService : IApiService
{
    internal Worker _worker;
    public ShiftService(Worker worker)
    {
        _worker = worker;
    }

    public async Task<List<Shift>> GetAllShiftsAsync()
    {
        RestResponse? response = await ExecuteRestAsync(
            async () => await _client.GetAsync(
                new RestRequest($"Workers/0/Shifts/all")
            )
        );

        if (response == null)
            return [];
        
        List<Shift>? shifts = JsonSerializer.Deserialize<List<Shift>>(response.Content, _jsonOptions);
        return shifts ?? [];
    }

    public async Task<List<Shift>> GetAllShiftsOfWorkerAsync()
    {
        RestResponse? response = await ExecuteRestAsync(
            async () => await _client.GetAsync(
                new RestRequest($"Workers/{_worker.EmployeeId}/Shifts")
            )
        );

        if (response == null)
            return [];

        List<Shift>? shifts = JsonSerializer.Deserialize<List<Shift>>(response.Content, _jsonOptions);
        return shifts ?? [];
    }
}