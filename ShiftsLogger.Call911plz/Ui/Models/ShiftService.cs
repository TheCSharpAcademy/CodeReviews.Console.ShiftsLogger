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
                new RestRequest($"Workers/{_worker.WorkerId}/Shifts")
            )
        );

        if (response == null)
            return [];
            

        List<Shift>? shifts = JsonSerializer.Deserialize<List<Shift>>(response.Content, _jsonOptions);
        return shifts ?? [];
    }

    public async Task<Shift?> GetShiftByShiftIdAsync(int shiftId)
    {
        RestResponse? response = await ExecuteRestAsync(
            async () => await _client.GetAsync(
                new RestRequest($"Workers/{_worker.WorkerId}/Shifts/{shiftId}")
            )
        );

        if (response == null)
            return null;

        Shift? shift = JsonSerializer.Deserialize<Shift>(response.Content, _jsonOptions);
        return shift;
    }
}