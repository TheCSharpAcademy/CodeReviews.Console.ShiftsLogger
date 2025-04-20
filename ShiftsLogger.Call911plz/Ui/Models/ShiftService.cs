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

    public async Task<Shift?> CreateShiftAsync(ShiftDto shift)
    {
        RestResponse? response = await ExecuteRestAsync(
            async () => await _client.PostAsync(
                new RestRequest($"Workers/{_worker.WorkerId}/Shifts")
                    .AddJsonBody(shift)
            )
        );

        if (response == null)  
            return null;

        return JsonSerializer.Deserialize<Shift>(response.Content, _jsonOptions);
    }

    public async Task<Shift?> UpdateShiftAsync(int shiftId, ShiftDto shift)
    {
        RestResponse? response = await ExecuteRestAsync(
            async () => await _client.PutAsync(
                new RestRequest($"Workers/{_worker.WorkerId}/Shifts/{shiftId}")
                    .AddJsonBody(shift)
            )
        );

        if (response == null)
            return null;
        
        return JsonSerializer.Deserialize<Shift>(response.Content, _jsonOptions);
    }

    public async Task<bool> DeleteShiftAsync(int shiftId)
    {
        RestResponse? response = await ExecuteRestAsync(
            async () => await _client.DeleteAsync(
                new RestRequest($"Workers/{_worker.WorkerId}/Shifts/{shiftId}")
            )
        );

        if (response == null)
            return false;
        return true;
    }
}