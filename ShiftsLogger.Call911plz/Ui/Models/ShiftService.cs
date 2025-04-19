

using System.Text.Json;
using RestSharp;

public class ShiftService : IApiService
{
    internal Worker _worker;
    public ShiftService(Worker worker)
    {
        _worker = worker;
    }

    public async Task<List<Shift>> GetAllShiftsByWorkerIdAsync()
    {
        RestResponse? response = await ExecuteRestAsync(
            async () => await _client.GetAsync(
                new RestRequest($"Workers/{_worker.EmployeeId}/Shifts")
            )
        );

        if (response == null)
            return [];

        List<Shift>? workers = JsonSerializer.Deserialize<List<Shift>>(response.Content, _jsonOptions);
        return workers ?? [];
    }
}