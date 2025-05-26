using System.Net.Http.Json;
using ConsoleFrontEnd.ApiShiftService;
using ConsoleFrontEnd.Models;

namespace ConsoleFrontEnd.Services;

public class ShiftService : IShiftService
{
    internal static ShiftFilterOptions shiftFilterOptions = new()
    {
        WorkerId = null,
        LocationId = null,
        StartTime = null,
        EndTime = null,
    };
    private static HttpClient httpClient = new HttpClient();

    // Fix: Implement interface signatures exactly as declared in IShiftService

    public async Task<List<Shifts>> GetAllShifts()
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.GetAsync($"api/shifts?");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new List<Shifts>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine("No shifts found.");
                return new List<Shifts>();
            }
            else
            {
                Console.WriteLine("Shifts retrieved successfully.");
                var result = await response.Content.ReadFromJsonAsync<List<Shifts>>();
                return result
                        ?.Select(shift => new Shifts
                        {
                            ShiftId = shift.ShiftId,
                            WorkerId = shift.WorkerId,
                            LocationId = shift.LocationId,
                            StartTime = shift.StartTime,
                            EndTime = shift.EndTime,
                            Location = shift.Location,
                            Worker = shift.Worker,
                        })
                        .ToList() ?? new List<Shifts>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetAllShifts: {ex}");
            throw;
        }
    }

    public async Task<List<Shifts>> GetShiftById(int id)
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.GetAsync($"api/shifts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new List<Shifts>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine("No shifts found.");
                return new List<Shifts>();
            }
            else
            {
                Console.WriteLine("Shift retrieved successfully.");
                var result = await response.Content.ReadFromJsonAsync<Shifts>();
                return result != null ? new List<Shifts> { result } : new List<Shifts>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetShiftById: {ex}");
            throw;
        }
    }

    public async Task<Shifts> CreateShift(Shifts createdShifts)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/shifts", createdShifts);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Shifts>();
                return result
                    ?? throw new Exception("Failed to create createdShifts, no data returned.");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                throw new Exception($"Failed to create createdShifts: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for CreateShift: {ex}");
            throw;
        }
    }

    public async Task<Shifts> UpdateShift(int id, Shifts updatedShift)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"api/shifts/{id}", updatedShift);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Shifts>();
                return result ?? throw new Exception("Failed to update shift, no data returned.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Shift not found.");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for UpdateShift: {ex}");
            throw;
        }
    }

    public async Task<string> DeleteShift(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/shifts/{id}");
            if (response.IsSuccessStatusCode)
            {
                return "Shift deleted successfully.";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return "Shift not found.";
            }
            else
            {
                return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for DeleteShift: {ex}");
            throw;
        }
    }
}
